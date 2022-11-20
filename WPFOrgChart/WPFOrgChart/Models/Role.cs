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
    public class Role : OrgBaseModel {

        private string _title;
        public string Title {
            get { return _title; }
            set { SetProp(ref _title, value); }
        }

        private string _reportsTo;
        public string ReportsTo {
            get { return _reportsTo; }
            set { SetProp(ref _reportsTo, value); }
        }

        private int _level = 1; //All role starts at level one
        public int Level {
            get { return _level; }
            set { SetProp(ref _level, value); }
        }

        private string _aboveRole;
        public string AboveRole {
            get { return _aboveRole; }
            set { SetProp(ref _aboveRole, value);}
        }

        private string _department;
        public string DepartmentId {
            get { return _department; }
            set { 
                SetProp(ref _department, value); 
            }
        }

        public override string ToString() {
            return Title;
        }

        public override object Clone() {
            //Use Map properties from Haley.Utils.
            Role clone = new Role(); //Create the object and send in as reference
            this.MapProperties<Role, Role>(clone); //Mapping a Role object to another ROle object by the matching property names.
            return clone;
        }

        //void ProcessCurrentLevel() {
        //    //based on the level above which this is present, we decide our level.
        //    //Our level is always at 1.
        //    //get the level of the aboveRole
        //    //Let us keep it class as a DTO class and not include any methods.
        //}

        public Role() {
            //reports to and Department will store only the ID of the specific elements
        }
    }
}
