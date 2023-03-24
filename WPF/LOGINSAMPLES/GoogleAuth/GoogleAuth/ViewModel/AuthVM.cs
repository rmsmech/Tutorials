using Haley.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AuthService;
using GoogleAuth.Services;
using Haley.Abstractions;
using Haley.Utils;

namespace GoogleAuth.ViewModel
{
    public class AuthVM : BaseVM
    {
        public ICommand CMDGoogleLogin => new DelegateCommand(GoogleLogin);

        private void GoogleLogin() {
            //Authenticate using google.
            var requrl = TokenService.GetRequestURL();
            if (string.IsNullOrWhiteSpace(requrl)) return;

            //Open the process.
            ProcessStartInfo pinfo = new ProcessStartInfo(requrl) {
                    UseShellExecute = true };
            Process.Start(pinfo);
        }

        private async void HandleGoogleAuth(string code) {
            //New code is provided (the grant)
            var gatoken = await TokenService.GetAccessToken(code);
            //Check the user name / if verified/genuine, move to next page.
            Globals.GClient.SetAuthenticator(new TokenAuthProvider().SetToken(gatoken)); //Set authenticator.
            Globals.Authorised = true;
            InvokeVMClosed(this, true); //This will then close the window as well.
        }

        public AuthVM() {
            Globals.RegisterCallBack("google", this.HandleGoogleAuth);
        }
    }
}
