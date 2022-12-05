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
using System.Collections; //For adding the generic IEnumberable
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WPFOrgChart.Enums;
using WPFOrgChart.Models;
using System.Windows.Threading;
using Haley.Services;
using winCtrl = System.Windows.Controls;

namespace WPFOrgChart.ViewModels {
    public class MainVM : BaseVM, IConfigHandler {

        #region Attributes
        IDialogService _ds = new DialogService();
        IConfigManager _cfgMgr = new ConfigManagerService() { FileExtension = ".orgC" };
        OrgConfig _configCache = new OrgConfig();
        TargetType CurrentTab = TargetType.Person;
        //Setup a timer to autoclear the textblock message in 2 seconds.
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(2.5) };
         //Since this is a simple application, directly initiating a config manager. For complex/enterprise apps, consider using a dependency injection and reuse config manager.
        #endregion

        #region Properties
        public Guid UniqueId { get; set; }

        #region Current Elements
        //An object for the editable value and an Ienumerable for the results

        private object _current = new Person();
        private IEnumerable _currentValues;

        private CurrentView _displayView = CurrentView.OrgChart;

        private string _message;

        private bool _messageVisible = false;

        public object Current {
            get { return _current; }
            set { SetProp(ref _current, value); }
        }
        public IEnumerable CurrentValues {
            get { return _currentValues; }
            set { _currentValues = value; OnPropertyChanged(); }
        }
        public CurrentView DisplayView {
            get { return _displayView; }
            set { SetProp(ref _displayView, value); }
        }

        public string Message {
            get { return _message; }
            set { SetProp(ref _message, value); }
        }
        public bool MessageVisible {
            get { return _messageVisible; }
            set { SetProp(ref _messageVisible, value); }
        }
        #endregion

        #region Collections
        private ObservableCollection<Department> _departments = new ObservableCollection<Department>();
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();
        private ObservableCollection<Role> _roles = new ObservableCollection<Role>();

        public ObservableCollection<Department> Departments {
            get { return _departments; }
            set { SetProp(ref _departments, value); }
        }

        public ObservableCollection<Person> Persons {
            get { return _persons; }
            set { SetProp(ref _persons, value); }
        }
        public ObservableCollection<Role> Roles {
            get { return _roles; }
            set { SetProp(ref _roles, value); }
        }
        #endregion

        #region Commands
        public ICommand CMDAddUpdate => new DelegateCommand(addUpdate);
        public ICommand CMDChangeView => new DelegateCommand<CurrentView>((p) => { DisplayView = p; });
        public ICommand CMDDeleteSelectedItems => new DelegateCommand<object>(DeleteSelected);
        public ICommand CMDEditCurrent => new DelegateCommand<object>(EditSelected);
        public ICommand CMDKeyDown => new DelegateCommand<object>(HandleKeyDown);
        public ICommand CMDReset => new DelegateCommand(reset);
        public ICommand CMDResetReportings => new DelegateCommand(resetReportings);
        public ICommand CMDTabChanged => new DelegateCommand<object>(tabChanged);
        #endregion

        #endregion

        public MainVM() {
            Initialize();
        }

        #region Config Management
        public Task OnConfigLoaded(IConfig config) {
            //When config is loaded from file
            _configCache = config as OrgConfig; //If we need any further processing we can do here.
            //On first load, we can either merge or overwrite. We go with overwrite (assuming we only load during the startup of the application)
            if (_configCache == null) return null;

            //TO DO LATER:
            //before adding the items, ensure that we add only the values that are reasonable. 
            //If roles contain any removed/deleted department, do not add it. Same applies for persons as well. 
            Departments = new ObservableCollection<Department>(_configCache.Departments?.ToList());
            Roles = new ObservableCollection<Role>(_configCache.Roles?.ToList());
            Persons = new ObservableCollection<Person>(_configCache.Employees?.ToList());

            ProcessRoleLevels();
            ProcessEmployeeManagers();

            return Task.FromResult(true);
        }
        public IConfig OnConfigSaving() {
            //send in a value that wil lbe saved to the file.
            //Fill the config file 
            _configCache.Departments = Departments.ToList();
            _configCache.Roles = Roles.ToList();
            _configCache.Employees = Persons.ToList();

            //Ids are chaning on loading.
            return _configCache;
        }

        public IConfig PrepareDefaultConfig() {
            //On Failure, when creating new config.
            return new OrgConfig(); //No other values needed.
        }
        #endregion

        #region Core Processing
        void addUpdate() {
            //keep it simple and validate within each block.
            //Based on the current tab type, fetch the current object and add to the collection.
            //TO DO: VALIDATE AND CHECK IF WE ARE ADDING OR UPDATING EXISTING VALUES.
            //ADDING TO COLLECTION DIRECTLY AND SINCE CURRENT VALUES ARE REFERENCING THIS, ASSUMIGN THAT IT WILL AUTOMATICALLY UPDATE VALUES IN UI (CHECK AND FIX LATER)
            switch (CurrentTab) {
                case TargetType.Person:
                    if (!ProcessPerson()) return;
                    break;
                case TargetType.Role:
                    if (!ProcessRole()) return;
                    break;
                case TargetType.Department:
                    if (!ProcessDepartment()) return;
                    break;
            }
            //After adding new value, also reset
            reset();
        }
        bool ProcessDepartment() {
            var target = Current as Department;

            //Validate
            if (target == null || string.IsNullOrWhiteSpace(target.Title)) {
                SetMessage();
                return false;
            }
            var existingObj = Departments.FirstOrDefault(p => p.Id == target.Id);
            //Add or update
            if (existingObj != null) {
                //Exists already, now get the element and replace it.
                var index = Departments.IndexOf(existingObj);
                Departments[index] = target; //replaced.
            } else {
                Departments.Add(target); //added
            }
            return true;
        }

        bool ProcessPerson() {
            var target = Current as Person;

            //Validate
            if (target == null || string.IsNullOrWhiteSpace(target.FirstName) || string.IsNullOrWhiteSpace(target.LastName)) {
                SetMessage();
                return false;
            }
            var existingObj = Persons.FirstOrDefault(p => p.Id == target.Id);
            //Add or update
            if (existingObj != null) {
                //Exists already, now get the element and replace it.
                var index = Persons.IndexOf(existingObj);
                Persons[index] = target; //replaced.
            } else {
                Persons.Add(target); //added
            }
            ProcessEmployeeManagers();
            return true;
        }
        bool ProcessRole() {
            var target = Current as Role;

            //Validate
            if (target == null || string.IsNullOrWhiteSpace(target.Title) || string.IsNullOrWhiteSpace(target.DepartmentId)) {
                SetMessage();
                return false;
            }

            //Whenever we try to process a role, validate if the levels are correct. Else set a message and return. (this mostly applies when we are editing existing items.

            var existingObj = Roles.FirstOrDefault(p => p.Id == target.Id);
            //Add or update
            if (existingObj != null) {
                //We need to find if the new item is reporting to. (not the exsiting item)
                if (!string.IsNullOrWhiteSpace(target.ReportsTo)) {
                    // we are planning to report to another role.
                    //Is this another role, above our role ?
                    var possible_supervisor = Roles.FirstOrDefault(p => p.Id == target.ReportsTo);
                    if (possible_supervisor != null && possible_supervisor.Level < target.Level) {
                        var  confirmation = _ds.ShowDialog("Conflict", $@"You are trying to report to a Role which is at a lower level than the existing one. {Environment.NewLine}This might result in breaking the reporting structure of the target role.  {Environment.NewLine} Do you wish to proceed?", NotificationIcon.Warning, DialogMode.Confirmation, blurOtherWindows: true);
                        if (!confirmation.DialogResult.HasValue || !confirmation.DialogResult.Value) return false; //break away

                        //Since user has decided to set it up, remove the supervisor's reporting structure  (or should it be enhanced??)
                        possible_supervisor.ReportsTo = string.Empty; //Now we remove the reporting head.
                    }
                }

                //Exists already, now get the element and replace it.
                var index = Roles.IndexOf(existingObj);
                Roles[index] = target; //replaced.   
            } else {
                    Roles.Add(target); //added
                }
            //Whenever we process a role (add or edit), we need to process their levels as well
            ProcessRoleLevels();
            ProcessEmployeeManagers();
            return true;
        }

        private void ProcessEmployeeManagers() {
            //Whenever role changes or person changes, do this.
            foreach (var person in Persons) {
                //If he has a role id, get the role id and 
                //Role is mandatory, so every person will have one role (Ensure later on config loding)
                var thisRole = Roles.FirstOrDefault(p => p.Id == person.RoleId); //This role will be based on a department as well.
                if (string.IsNullOrWhiteSpace(thisRole?.ReportsTo)) continue; //This role doesn't report to anyone, yet.

                //If any person has this role, then he/she becomes the manager.
                //TO DO: (May be more than one person could hold same role. We need to find which would be the manager)
                var possible_mgr = Persons.FirstOrDefault(q => q.RoleId == thisRole?.ReportsTo);
                if (possible_mgr != null) {
                    person.PossibleManager = $@"{possible_mgr.FirstName} {possible_mgr.LastName}";
                }
            }
        }

        void IncrementRoleLevel(Role target) {
            //If we increase the level of a target, we need to icnrease its parent too.

            target.Level++; //This level.
            if (!string.IsNullOrWhiteSpace(target.ReportsTo)) {
                var parent = Roles.FirstOrDefault(p => p.Id == target.ReportsTo);
                if (parent != null) IncrementRoleLevel(parent);
            }
        }

        void ProcessRoleLevels() {
            //We can reset all levels (just in case some reporting strucutre got changed)
            foreach (var role in Roles) {
                role.Level = 1;
            }

            //Whenever an object is added, we need to process all levels.
            //For each role, we define their level based on above and report roles.

            //Handle Direct reportings (in separate loop)
            foreach (var role in Roles) {

                if (!string.IsNullOrWhiteSpace(role.ReportsTo)) {
                    var supervising_role = Roles.FirstOrDefault(p => p.Id == role.ReportsTo);
                    if (supervising_role == null) {
                        role.ReportsTo = string.Empty; //supervising/reportsto role is empty
                        continue;
                    }

                    //Now based on our level, we are going to increase the reporting level.
                    if (supervising_role.Level <= role.Level) {
                        //Only if the supervising level is also equal to this level or less than this level, increase it.
                        supervising_role.Level = role.Level; //increase the supervising level.
                        IncrementRoleLevel(supervising_role);
                    }
                }
            }

            //Handle Above Levels (in separate loop)
            foreach (var role in Roles) { 
                if (!string.IsNullOrWhiteSpace(role.AboveRole)) {
                    //this role is above some other role.
                    //get the aboveRole
                    var above_role = Roles.FirstOrDefault(p => p.Id == role.AboveRole);
                    if (above_role == null) {
                        //May be this role is removed.
                        role.AboveRole = string.Empty;
                        continue;
                    }

                    //If above role also exits, get it's level and increment this level
                    //if we are not already above the specified level, then we increase
                    if (role.Level <= above_role.Level) {
                        //only when this role is less or equal to the above role
                        //First bring the role to the level of the above role.
                        role.Level = above_role.Level;
                        IncrementRoleLevel(role);//increment by one. //We need to recursively do this.
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        void clear() {
            Current = null;
        }

        private void DeleteSelected(object obj) {
            //Based on the selected tab, the incoming object could be anything (person,role,department).. but, we know one thinig for sure, whatever it is, it will contain Id (since it is all based on OrgBaseModel)
            //As per MSDN, SelectedItemCollection is implementing ObservableCollection<Object>
            if (obj == null || !(obj is ObservableCollection<object> col) || col.Count == 0) return;

            //Now, get the first object of the col and try to cast it based on the current tab.
            switch (CurrentTab) {
                case TargetType.Person:
                    if (!(col.First() is Person)) return;
                    break;
                case TargetType.Role:
                    if (!(col.First() is Role)) return;
                    var roles_todel = col.Cast<Role>().ToList();
                    //Do some logic validation and delete it. 
                    //Currently, keeping it simple and directly deleting it.
                    //Later, delete an object and also remove any associated references (sometime the Id of one object could be referenced elsewhere).

                    foreach (var item in roles_todel) {
                        Roles.Remove(item);
                    }
                    break;
                case TargetType.Department:
                    if (!(col.First() is Department)) return;
                    var depts_todel = col.Cast<Department>().ToList();
                    //Do some logic validation and delete it. 
                    //Currently, keeping it simple and directly deleting it.
                    //Later, delete an object and also remove any associated references (sometime the Id of one object could be referenced elsewhere).

                    foreach (var item in depts_todel) {
                        Departments.Remove(item);
                    }
                    break;
                default:
                    break;
            }
            reset(); //so that the new collection is also updated in the UI.
        }

        private void EditSelected(object obj) {
            //Incoming obj should be one of the currentValues.
            if (obj == null || obj.GetType() != GetCurrentType() || !(obj is OrgBaseModel obj_base)) return;
            //Dont try to set the object directly, this affects the values and we lose the reset option.
            //The moment we change values in UI, the actual value is affected as it is by reference
            //so, clone it.
            Current = obj_base.Clone();
        }

        Type GetCurrentType() {
            switch (CurrentTab) {
                case TargetType.Person:
                    return typeof(Person);
                case TargetType.Role:
                    return typeof(Role);
                case TargetType.Department:
                    return typeof(Department);
            }
            return null;
        }

        private void HandleKeyDown(object obj) {
            //This is monitoring all key downs. 
            if (!(obj is HotKeyArgs hkargs)) return;

            if (hkargs.PressedKeys.First() == Key.Escape) {
                //reset everything if we press escape
                reset();
            } else if (hkargs.PressedKeys.First() == Key.Enter) {
                //Add / update value
                addUpdate();
            } else if (hkargs.PressedKeys.Count() == 2) {
                if (hkargs.PressedKeys.Contains(Key.LeftCtrl) && hkargs.PressedKeys.Contains(Key.S)) {
                    //We need to save the file.
                    _cfgMgr.SaveAll();
                }
            }

            //To do : Handle other key down events.
        }

        private void HandlerTimer(object? sender, EventArgs e) {
            timer.Stop();
            SetMessage(null); //this will remove and hide.
        }

        private void Initialize() {
            timer.Tick += HandlerTimer;
            //Also setup a way to store the current values to files.
            //Use any suitable way to store the values (Like DB, Text file or server based files etc).
            //here: Using config manager from haley to store files in local(parent) directory.
            UniqueId = Guid.NewGuid();
            //if (_cfgMgr.TryRegister(nameof(MainVM), typeof(OrgConfig), _configCache, this, out _)) {
            //    _cfgMgr.LoadConfig(nameof(MainVM));
            //};
            _cfgMgr.TryRegister(nameof(MainVM), typeof(OrgConfig), _configCache, this, out _);
        }

        private void reset() {
            //TO Implement
            switch (CurrentTab) {
                case TargetType.Person:
                    Current = new Person();
                    CurrentValues = Persons;
                    break;
                case TargetType.Role:
                    Current = new Role();
                    //If we directly set the value, then same observable collection gets shared by two places and thus, "NewPlaceHolderItem" shows up (in the combo box). to avoid that, we initiate a new list.
                    CurrentValues = new ObservableCollection<Role>(Roles.ToList());
                    break;
                case TargetType.Department:
                    Current = new Department();
                    CurrentValues = new ObservableCollection<Department>(Departments.ToList());
                    break;
                default:
                    break;
            }
        }

        private void resetReportings() {
            //For the current object.
            if (Current is Role role) {
                role.AboveRole = String.Empty;
                role.ReportsTo = String.Empty;
            }
        }
        void SetMessage(string msg = "Please enter all mandatory values") {
            Message = msg;
            if (!string.IsNullOrEmpty(msg)) {
                //if message is not null, initiate the timer.
                timer.Stop(); //incase we are trying to set multiple messages in succession.
                timer.Start();
                MessageVisible = true;
            } else {
                MessageVisible = false;
            }
        }

        //Even when tab is not changed, this is getting called.
        private void tabChanged(object obj) {
            //Even when we move focus to another control, it is getting triggered
            if (!(obj is TargetType targetEnum)) return;

            //Now it is persistent
            if (CurrentTab == targetEnum) return; //We didn't change the current tab.

            //Whenever tab changed, let us reset the values irrespective ofwhat has been changed.
            clear(); //Still need to identify the first tab on initialization. 
            //receive the tag of the selected item.
            //Associate an enum with the tag, so that it will be easy for us to identify.
            //Whenever the tabselection changed, we initialize or change or reset the current object and also the current enumerable values.
            CurrentTab = targetEnum;
            reset();
        }
        #endregion
        
    }
}
