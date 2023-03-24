using GoogleAuth.Services;
using Haley.Models;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoogleAuth.ViewModel
{
    public class HomeVM :BaseVM
    {
		private string _userName;
		public string UserName {
			get { return _userName; }
			set { SetProp(ref _userName, value); }
		}

		private string _email;
		public string Email {
			get { return _email; }
			set { SetProp(ref _email, value); }
		}

		public ICommand CMDSignOut => new DelegateCommand(SignOut);

        private void SignOut() {
			InvokeVMClosed(this, true);
        }

        async void Initialize() {
			//Get the global gclient and fetch the user name and display
			var userInfoRaw = await Globals.GClient
				.WithEndPoint(@"https://www.googleapis.com/oauth2/v2/userinfo")
				//.InheritAuthentication()
				.GetAsync();
			var userInfo = await userInfoRaw.AsStringResponseAsync();
			if (!userInfo.IsSuccessStatusCode) return;
			var jobj = JsonObject.Parse(userInfo.Content);
			if (jobj == null) return;
			Email = jobj["email"]?.ToString();
			UserName = jobj["name"]?.ToString();
		}

        public HomeVM() {
			Initialize();
		} 
    }
}
