using Haley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;

namespace SettingsPageDemo.Model
{
	public interface IShape {
		void CalculateArea();
		double Value { get; set; }
	}

    public abstract class ShapeBase : ChangeNotifier, IShape {

		protected string BaseMessage  = @"The Area for {0} is {1}"; //the dollar sign will make the 0 and 1 as actual values. So, donot use here.
		private double _value;
		public double Value {
			get { return _value; }
			set { SetProp(ref _value, value); }
		}

		private string _result;
		public string Result {
			get { return _result; }
			set { SetProp(ref _result, value); }
		}

		public abstract void CalculateArea();
    }

    public class CircleShape : ShapeBase  {
		private double _radius;
		public double Radius {
			get { return _radius; }
			set { SetProp(ref _radius, value); }
		}

        public override string ToString() {
			return string.Format(BaseMessage, nameof(CircleShape), Value.ToString("0.###"));
        }

        public CircleShape() { }
        public override void CalculateArea() {
			Value =  (Radius * Radius) * Math.PI;
			Result = this.ToString();
        }
    }

    public class SquareShape : ShapeBase {
		private double _length;
		public double Length {
			get { return _length; }
			set { SetProp(ref _length, value); }
		}

		public SquareShape() { }
        public override void CalculateArea() {
			Value = Length * Length;
            Result = this.ToString();
        }

        public override string ToString() {
            return string.Format(BaseMessage, nameof(SquareShape), Value.ToString("0.###"));
        }
    }

	public class RectangleShape : SquareShape {
		private double _width;
		public double Width {
			get { return _width; }
			set { SetProp(ref _width, value); }
		}

		public RectangleShape() { }
		public override void CalculateArea() {
			Value = Width * Length;
            Result = this.ToString();
        }

        public override string ToString() {
            return string.Format(BaseMessage, nameof(RectangleShape), Value.ToString("0.###"));
        }
    }

    public class TriangleShape : ShapeBase {

		//You can go ahead with differnt formulas and values based on your need.

		private double _height;
		public double Height {
			get { return _height; }
			set { SetProp(ref _height, value); }
		}

		private double _length;
		public double Length {
			get { return _length; }
			set { SetProp(ref _length, value); }
		}

		public TriangleShape() { }

		public override void CalculateArea() {
			Value = (Height * Length) / 2;
            Result = this.ToString();
        }

        public override string ToString() {
            return string.Format(BaseMessage, nameof(TriangleShape), Value.ToString("0.###"));
        }
    }
}
