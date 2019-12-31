using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haley.Flipper.MVVM.Models;
using helperControls.Models;
using helperControls.ViewModel;

namespace PaginationWPF.Viewmodels
{
    public class VMMain :ChangeNotifierModel
    {

		private VMPagination _topPagination;
		public VMPagination topPagination
		{
			get { return _topPagination; }
			set { _topPagination = value; onPropertyChanged(); }
		}

		private VMPagination _bottomPagination;
		public VMPagination bottomPagination
		{
			get { return _bottomPagination; }
			set { _bottomPagination = value; onPropertyChanged(); }
		}

		private void _initiation()
		{
			topPagination = new VMPagination();
			bottomPagination = new VMPagination();
			PaginationModel toppmodel = new PaginationModel(1000,25);
			PaginationModel bottompmodel = new PaginationModel(2500,30);
			topPagination.seed(toppmodel);
		}

		public VMMain() 
		{ 
			
		}

    }
}
