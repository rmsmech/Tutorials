using Haley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsPageDemo.Model
{
    public class SecretConfig : ChangeNotifier
    {
		private string _password;
		public string Password {
			get { return _password; }
			set { SetProp(ref _password, value); }
		}

		private int _secretNumber;
		public int SecretNumber {
			get { return _secretNumber; }
			set { SetProp(ref _secretNumber, value); }
		}

		private string _userName;
		public string UserName {
			get { return _userName; }
			set { SetProp(ref _userName, value); }
		}

        public string HostName { get; set; }

        public SecretConfig() { }
	}
}
