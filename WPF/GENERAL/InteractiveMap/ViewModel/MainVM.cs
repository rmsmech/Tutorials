using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shps = System.Windows.Shapes;
using Haley.Models;
using Haley.MVVM;
using Haley.Abstractions;
using Haley.Rest;
using System.Drawing;
using System.Windows.Media.Animation;
using Haley.Utils;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace InteractiveMap.ViewModel {
    public class MainVM : BaseVM {

        IClient _bankClient;

        private string _selectedPath;
        public string SelectedPath {
            get { return _selectedPath; }
            set { SetProp(ref _selectedPath, value); }
        }

        private string _countryData;
        public string CountryData {
            get { return _countryData; }
            set { SetProp(ref _countryData, value); }
        }

        public ICommand CMDMapHover => new DelegateCommand<object>(MapHover);

        private async void MapHover(object obj) {
            //Map hover has happened.
            if (!(obj is MouseEventArgs margs)) return;
            if (!(margs.OriginalSource is Shps.Path spth)) return;
            //Now we are at a point where the click has happened from a Path.
            //Get the Path name.
            SelectedPath =  spth.Name;
            CountryData = await BuildCountryInfo(spth.Name);
        }

        async Task<string> BuildCountryInfo(string country_code) {

            var country_json = await GetCountryData(country_code);
            var data = GetInfo(country_json);
            try {
                StringBuilder sbuilder = new StringBuilder();
                sbuilder.AppendLine("Code".PadRight(20) + " : " + data["id"]);
                sbuilder.AppendLine("ISO Code".PadRight(20) + " : " + data["iso2Code"]);
                sbuilder.AppendLine("Name".PadRight(20) + " : " + data["name"]);
                sbuilder.AppendLine("Capital".PadRight(20) + " : " + data["capitalCity"]);
                sbuilder.AppendLine("Income Level".PadRight(20) + " : " + data["incomeLevel"]?["value"]);
                sbuilder.AppendLine("Region".PadRight(20) + " : " + data["adminregion"]?["value"]);
                return sbuilder.ToString();
            } catch (Exception) {
                //In case of exception, only send raw data.
                return country_json;
            }
        }

        JsonNode GetInfo(string json_input) {
            var data = JsonObject.Parse(json_input); //Since this will be dynamic and we don't have any class /interface for the incoming string. We use json object (which is nothing but a dictionary of key,value
            //We know that the obejct at index=1 is an array.
            if (data[1] is JsonArray jarr) {
                return jarr.First() as JsonNode;
            }
            return data;
        }
        async Task<string> GetCountryData(string code) {
            //Haley client is made of fluent pattern.
            //Method chaining is possible
            var result = await _bankClient
                .WithEndPoint($@"country/{code}")
                .WithParameter(new QueryParam("format","json"))
                .GetAsync();
            if (result.IsSuccessStatusCode) {
                var result_str = await result.AsStringResponseAsync();
                return result_str?.Content;
            }
            return "nothing";
        }

        public MainVM() {

            _bankClient = ClientStore.GetClient("worldbank"); //In some situations,it would be more logical to add this in a dependency service provider.
        }
    }
}
