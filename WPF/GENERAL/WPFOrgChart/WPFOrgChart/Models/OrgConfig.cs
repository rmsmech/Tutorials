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
    public class OrgConfig : IConfig {
        public string Id { get; }
        public List<Role> Roles { get; set; }
        public List<Department> Departments { get; set; }
        public List<Person> Employees { get; set; }
        public OrgConfig() {
            Id = Guid.NewGuid().ToString();
            Roles = new List<Role>();
            Departments = new List<Department>();
            Employees = new List<Person>(); 
        }
    }
}
