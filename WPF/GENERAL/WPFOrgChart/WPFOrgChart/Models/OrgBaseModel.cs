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
    public abstract class OrgBaseModel : ChangeNotifier, ICloneable  {

        //Since this ID is a readonly property, Json Deseriazlier wasn't able to set this value.
        //If we make this public set, we will retrieve the saved value
        public string Id { get; set; }

        public OrgBaseModel() {
            Id = Guid.NewGuid().ToString(); 
        }
        public abstract object Clone();
    }
}
