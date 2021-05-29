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

namespace NotificationDemo
{
    public class DialogVM : ChangeNotifier, IHaleyVM
    {
        public event EventHandler<FrameClosingEventArgs> OnWindowsClosed;

        public ICommand closeCommand => new DelegateCommand(_close);

        private void _close()
        {
            //

            OnWindowsClosed.Invoke(this, null);
        }

        private bool isManager;
        public bool IsManager
        {
            get { return isManager; }
            set { SetProp(ref isManager, value); }
        }

        private bool _isEmployee;
        public bool IsEmployee
        {
            get { return _isEmployee; }
            set { SetProp(ref _isEmployee, value); }
        }

        private bool _isPresident;
        public bool IsPresident
        {
            get { return _isPresident; }
            set { SetProp(ref _isPresident, value); }
        }

        public DialogVM() { }

    }
}
