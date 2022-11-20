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


namespace WPFOrgChart.Models {
    public class Person : OrgBaseModel {
        private string _firstName;
        public string FirstName {
            get { return _firstName; }
            set { SetProp(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName {
            get { return _lastName; }
            set { SetProp(ref _lastName, value); }
        }

        private string _emailID;
        public string EmailId {
            get { return _emailID; }
            set { SetProp(ref _emailID, value); }
        }

        private string _roleId;
        public string RoleId {
            get { return _roleId; }
            set { SetProp(ref _roleId, value); }
        }

        private string _deptId;
        public string DepartmentId {
            get { return _deptId; }
            set { SetProp(ref _deptId, value); }
        }

        private string _possibleManager;
        public string PossibleManager {
            get { return _possibleManager; }
            set { SetProp(ref _possibleManager, value); }
        }

        public void SetName(string firstname, string lastname) {
            FirstName = firstname;
            LastName = lastname;
        }

        public override string ToString() {
            return $@"{FirstName} {LastName}";
        }

        public override object Clone() {
            Person clone = new Person();
            this.MapProperties<Person, Person>(clone); //Mapping a Role object to another ROle object by the matching property names.
            return clone;
        }

        public Person() {
           //A person will have a role and he wil lbelong to some department.
        }
    }
}
