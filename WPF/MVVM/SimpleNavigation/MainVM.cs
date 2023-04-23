using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SimpleNavigation {
    public class MainVM : ObservableObject {

		private PageId _pageID;
		public PageId PageID {
			get { return _pageID; }
			set { SetProperty(ref _pageID, value); }
		}

		private string _test;
		public string Test {
			get { return _test; }
			set { SetProperty(ref _test, value); }
		}


		public ICommand CMDChangePage => new RelayCommand<PageId>(ChangePage);

        void ChangePage(PageId newPage) {
			PageID = newPage;
		}

		public MainVM() { PageID = PageId.A; Test = "Hello there..."; }
    }
}
