using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haley.Abstractions;
using Haley.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoogleAuth.Services {
    internal static class Globals {
        internal static bool Authorised;
        internal static IClient GClient = new FluentClient();

        static Dictionary<string, Action<string>> _callBacks = new Dictionary<string, Action<string>>();

        internal static void Clear() {
            Authorised = false;
            _callBacks = new Dictionary<string, Action<string>>();
            GClient = new FluentClient();
        }

        static IWebHost _host;
        public static void RegisterCallBack(string key, Action<string> callback) {
            if (!_callBacks.ContainsKey(key)) {
                _callBacks.Add(key, null);
            }

            _callBacks[key] = callback; //add or replace;
        }

        public static void MakeCall(string key, string value) {
            if (_callBacks.ContainsKey(key)) {
                try {
                    _callBacks[key].Invoke(value); //a thread exception could happen.
                } catch (Exception ex) {
                    throw;
                }
            }
        }


        public static void InitiateListener() {
            _host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<StartupArgs>()
                .UseUrls("http://localhost:8500")
                .Build();

            Task.Run(() => { _host.Run(); }); //Because the active thread will be blocked by the host.
        }
    }

    internal class StartupArgs {

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app) {
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        public StartupArgs() { }
    }
}
