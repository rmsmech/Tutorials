using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.Models;
using Haley.MVVM;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Haley.FlexiMenuTest.ViewModels
{
    //Basevm is coming from haley
    public class MainViewModel : BaseVM
    {
        private IDialogService _ds;
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProp(ref _message, value); }
        }

        private double _profit;
        public double Profit
        {
            get { return _profit; }
            set { SetProp(ref _profit, value); }
        }

        private int _employeeCount;
        public int EmployeeCount
        {
            get { return _employeeCount; }
            set { SetProp(ref _employeeCount, value); }
        }

        private double _sales;
        public double Sales
        {
            get { return _sales; }
            set { SetProp(ref _sales, value); }
        }

        void _showMessage()
        {
            //Can be used to show a simple message or another window for input.
            _ds.Info("Information", Message);
        }
        public ICommand CmdShowNotification => new DelegateCommand(_showMessage);

        public MainViewModel() 
        { 
            Message = "Message raised from command.";
            //initial values
            Profit = 4500.0;
            Sales = 6500.0;
            EmployeeCount = 432;
            _ds = ContainerStore.Singleton.DI.Resolve<IDialogService>(); 
        }
    }
}
