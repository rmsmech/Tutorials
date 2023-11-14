using Haley.Models;
using SettingsPageDemo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsPageDemo.Model
{
    public class GeneralConfig  : ChangeNotifier
    {
		private ThemeEnum _theme;
		public ThemeEnum Theme {
			get { return _theme; }
			set { SetProp(ref _theme, value); }
		}

		private LanguageEnum _lang;
		public LanguageEnum Lang {
			get { return _lang; }
			set { SetProp(ref _lang, value); }
		}

		private string _storageDirectory;
		public string StorageDirectory {
			get { return _storageDirectory; }
			set { SetProp(ref _storageDirectory, value); }
		}

        public string UserMessage { get; set; }

		private TargetEnum _defaultTarget = TargetEnum.Rectangle;
		public TargetEnum DefaultTarget {
			get { return _defaultTarget ; }
			set { SetProp(ref _defaultTarget , value); }
		}

		private UnitEnum _units = UnitEnum.centimeter;
		public UnitEnum Units {
			get { return _units; }
			set { SetProp(ref _units, value); }
		}


		public GeneralConfig() { }

    }
}
