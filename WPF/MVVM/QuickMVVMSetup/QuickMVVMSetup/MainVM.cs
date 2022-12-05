using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Haley.Models;
using Haley.Abstractions;
using Haley.MVVM;

namespace QuickMVVMSetup
{
    public class MainVM : ChangeNotifier
    {
        private Person person;
        public Person TargetPerson
        {
            get { return person; }
            set { person = value; OnPropertyChanged(nameof(TargetPerson)); }
        }

        private ObservableCollection<Person> _persons; //ONLY WHEN ADDED/OR REMOVED (NOT FOR INTERNAL PROPERTY CHANGES)
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }

        public void AddPerson()
        {
            Persons.Add(TargetPerson); //Add it to thecollection
            TargetPerson = new Person(); //resetting it.
        }

        public ICommand CMDAdd => new DelegateCommand(AddPerson);
        public ICommand CMDDelete => new DelegateCommand<Person>(DeletePerson);

        private void DeletePerson(Person obj)
        {
            if (obj == null) return;
            if (!Persons.Contains(obj)) return;

            Persons.Remove(obj);
        }

        public MainVM()
        {
            Persons = new ObservableCollection<Person>();
            TargetPerson = new Person();
        }
    }
}
