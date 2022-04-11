using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Haley.Models;
using Haley.Abstractions;
using Haley.MVVM;

namespace QuickMVVMSetup
{
    public class Person : ChangeNotifier
    {
        private string _FName;
        public string FName
        {
            get { return _FName; }
            set { _FName = value; OnPropertyChanged(nameof(FName)); }
        }

        private string _LName;
        public string LName
        {
            get { return _LName; }
            set { _LName = value; OnPropertyChanged(nameof(LName)); }
        }

        private string _Age;
        public string Age
        {
            get { return _Age; }
            set { _Age = value; OnPropertyChanged(nameof(Age)); }
        }

        public Person() { }
    }
}
