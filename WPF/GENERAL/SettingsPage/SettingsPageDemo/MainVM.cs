using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Haley.Abstractions;
using Haley.Models;
using Haley.MVVM;
using SettingsPageDemo.Enums;
using SettingsPageDemo.Model;

namespace SettingsPageDemo
{
    public class MainVM : BaseVM
    {
		private ViewEnum _currentView = ViewEnum.HomeView;
		public ViewEnum CurrentView {
			get { return _currentView; }
			set { SetProp(ref _currentView, value); }
		}

		private ShapeBase _currentShape;
		public ShapeBase CurrentShape {
			get { return _currentShape; }
			set { SetProp(ref _currentShape, value); }
		}

		private TargetEnum _selectedTarget;
		public TargetEnum SelectedTarget {
			get { return _selectedTarget; }
			set { 
				SetProp(ref _selectedTarget, value);
				SetCurrentShape();
			}
		}
        private void SetCurrentShape() {
			switch (SelectedTarget) {
				case TargetEnum.Circle:
					CurrentShape = new CircleShape();
					break;
				case TargetEnum.Rectangle:
					CurrentShape = new RectangleShape();
					break;
				case TargetEnum.Square:
					CurrentShape = new SquareShape();	
					break;
				case TargetEnum.Triangle:
					CurrentShape = new TriangleShape();
					break;
			}
		}

		public ICommand CMDCalculate => new DelegateCommand(CalculateArea);

        private void CalculateArea() {
			if (CurrentShape == null) return;
			CurrentShape?.CalculateArea();
        }

        public override void OnViewLoaded(object sender) {
            base.OnViewLoaded(sender);
            SetCurrentShape();
        }

        public ICommand CMDChangeCurrentView { get; set; }

		public MainVM() {
			CMDChangeCurrentView = new DelegateCommand<ViewEnum>(p => CurrentView = p);
		}
    }
}
