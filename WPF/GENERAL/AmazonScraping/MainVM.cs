using Haley.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AmazonScraping {

    public class MainVM : BaseVM {
        private WebView2 browser;
        private ScrapWindow _scrapper;
        private int currentPage;
        private Dictionary<int, string> _pageResults = new Dictionary<int, string>();
        private string lastRunResult;
        private string storageDirectory;

        #region HTML Selectors

        private const string SEARCH_BAR = @"input.nav-input#twotabsearchtextbox";
        private const string SEARCH_BUTTON = @"input.nav-input#nav-search-submit-button";
        private const string NEXT_BUTTON = @"span.s-pagination-strip a.s-pagination-next";
        private const string RESULTS_ARRAY = @"span[data-component-type='s-search-results'] div[cel_widget_id^='MAIN-SEARCH_RESULTS']";
        private const string RESULT_IMAGE = @"div.s-product-image-container img.s-image";
        private const string RESULT_TITLE = @"div.s-card-container div.s-title-instructions-style h2.s-line-clamp-4 span.a-text-normal";
        private const string RESULT_PRICE = @"div.s-card-container div.s-price-instructions-style span.a-price span.a-offscreen";

        #endregion HTML Selectors

        private ScrapState _state = ScrapState.Search;
        private string _keyword;

        public string Keyword {
            get { return _keyword; }
            set { SetProp(ref _keyword, value); }
        }

        private string _results;

        public string Results {
            get { return _results; }
            set { SetProp(ref _results, value); }
        }

        public ICommand CMDExecuteScrap => new DelegateCommand(ExecuteScrap, () => {
            return (!string.IsNullOrWhiteSpace(Keyword) && Keyword.Length > 2);
        });


        public ICommand CMDSave => new DelegateCommand(SaveAsCSV);

        private void SaveAsCSV() {
            //save the last run result
            if (string.IsNullOrWhiteSpace(lastRunResult)) return;
            //get the directory.
            if (string.IsNullOrWhiteSpace(storageDirectory)) {
                var exeName = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
                storageDirectory = Path.Combine(Path.GetDirectoryName(exeName), "Results");
                if (!Directory.Exists(storageDirectory)) {
                    Directory.CreateDirectory(storageDirectory);
                }
            }
            
            var fileName = Path.Combine(storageDirectory, $@"{Keyword}.csv");
            File.WriteAllText(fileName, lastRunResult);
            Results += Environment.NewLine + $@"Saved CSV to {fileName}"; 
        }

        private void Cleanup() {
            _state = ScrapState.Search;
            Results = string.Empty;
            currentPage = 1;
            lastRunResult = string.Empty;
            _pageResults = new Dictionary<int, string>();
        }

        private void ExecuteScrap() {
            Cleanup();
            //implement
            _scrapper = new ScrapWindow(new Uri("https://www.amazon.ae"));
            browser = _scrapper.browser;
            //subscriber to the nav events
            browser.NavigationCompleted += NavCompleted;
            browser.WebMessageReceived += MessageReceived;
            _scrapper.ShowDialog(); //
        }

        private bool TryParseResults(out string result) {
            result = string.Empty;
            try {
                List<string> csvResult = new List<string>();
                foreach (var page in _pageResults.Values) {
                    JsonArray pageResult = JsonArray.Parse(page) as JsonArray;
                    if (pageResult == null) continue;
                    foreach (var product in pageResult) {
                        JsonObject productObj = product.AsObject();
                        if (productObj == null) continue;
                        string csvRow = string.Empty;
                        foreach (var productProp in productObj) {
                            csvRow += productProp.Value?.ToString() + ";;;";
                        }
                        csvResult.Add(csvRow);
                    }
                }

                result = string.Join(Environment.NewLine, csvResult);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        private void MessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e) {
            if (bool.TryParse(e.WebMessageAsJson, out var _res) && !_res) {
                //We reached the end.
                browser.ExecuteScriptAsync("window.close()");
                _scrapper.DialogResult = true;
                if (!TryParseResults(out lastRunResult)) {
                    Results = "Failed";
                    return;
                }
                Results = "Success" + Environment.NewLine + lastRunResult.Substring(0, 100) + "......";
                return;
            }

            var jobj = JsonObject.Parse(e.WebMessageAsJson);
            if (!(jobj is JsonArray jarr) || jarr.Count < 1) {
                //Show the message in results.
                return;
            }
            _pageResults.Add(currentPage, e.WebMessageAsJson);
            currentPage++;
            NavigateNextPage();
        }

        private async Task NavigateNextPage() {
            var query = $@"
            let newlocation = document.querySelector(""{NEXT_BUTTON}"")?.href;
            if (newlocation != null){{
                    window.location = newlocation;
                }} else {{
                window.chrome.webview.postMessage(false);
                }}
            ";
            await browser.ExecuteScriptAsync(query.Trim());
        }

        private void NavCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
            switch (_state) {
                case ScrapState.Search:
                    SearchProducts();
                    break;

                case ScrapState.Fetch:
                    FetchProducts();
                    break;
            }
        }

        private async Task<bool> SearchProducts() {
            _state = ScrapState.Fetch;
            //After we display the website for first time, we search for the products with keywords.
            var query = $@"
            document.querySelector(""{SEARCH_BAR}"").value = ""{Keyword}"";
            document.querySelector(""{SEARCH_BUTTON}"").click();
            ";
            await browser.ExecuteScriptAsync(query.Trim());

            return true;
        }

        private async Task FetchProducts() {
            var query = $@"
        function FetchInternal(){{
        let arrayquery = document.querySelectorAll(""{RESULTS_ARRAY}"");
            let productArray= [];
            for (let index = 0; index < arrayquery.length; index++) {{
                let target = arrayquery[index];
                let image = target.querySelector(""{RESULT_IMAGE}"");
                let title = target.querySelector(""{RESULT_TITLE}"");
                let price = target.querySelector(""{RESULT_PRICE}"");
                    productArray.push({{image: image?.src ?? 'NA',
              title: title?.innerText ?? 'NA',
              price: price?.innerText ?? 'NA',
                    }});
            }};
            //window.chrome.webview.postMessage(JSON.stringify(productArray));
            window.chrome.webview.postMessage(productArray);
            }}
            FetchInternal();
            ";

            await browser.ExecuteScriptAsync(query.Trim());
        }

        public MainVM() {
        }
    }

    public enum ScrapState {
        Search,
        Fetch,
    }
}