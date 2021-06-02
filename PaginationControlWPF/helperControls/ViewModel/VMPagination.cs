using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using helperControls.Models;



namespace helperControls.ViewModel
{
    public class VMPagination : ChangeNotifier
    {
        #region Properties
        private PaginationModel _pagination;
        public PaginationModel pagination
        {
            get { return _pagination; }
            set { _pagination = value; onPropertyChanged(); }
        }

        #endregion

        #region Command Properties
        public ICommand cmd_change_page { get; set; }

        #endregion

        #region CommandMethods
        private void changePage(string obj)
        {
            try
            {
                int parameter = int.Parse(obj);
                int newpage = _pagination.current_page; //Get current page

                switch (parameter)
                {
                    case 0:
                        newpage--;
                        if (newpage < 1) newpage = 1;
                        break;
                    case 1:
                        newpage++;
                        if (newpage > _pagination.total_pages) newpage = _pagination.total_pages;
                        break;
                }
                _pagination.current_page = newpage;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void calculatePagination()
        {
            try
            {
                if (pagination.items_per_page == 0) pagination.items_per_page = 1; //Setting minimum
                if (pagination.items_per_page > pagination.total_items) pagination.items_per_page = pagination.total_items; //Setting maximum
                pagination.total_pages = pagination.total_items / pagination.items_per_page; // Problem to be solved
                pagination.current_page = 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Methods

        private void _initiation()
        {
            cmd_change_page = new DelegateCommand<string>(changePage, null);
        }

        public void seed(PaginationModel pagination)
        {
            _pagination = pagination;
        }

        #endregion

        public VMPagination() 
        { 
            _initiation(); 

        }
    }
}
