using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using OOTPiSP_Laba1.Interfaces;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public abstract class MyGraphicalObject/*:ISelectable*/ {

		public event EventHandler<MouseButtonEventArgs> ObjectChanged;

		[JsonConstructor]
		protected MyGraphicalObject(int x, int y, Color bgColor, Color borderColor, int borderThickness, float angleGlobal) {
			Position = new Position(x, y);
			BgColor = bgColor;
			BorderColor = borderColor;
			AngleGlobal = angleGlobal%360;
			BorderThickness = borderThickness;
			Hash = GetHashCode();
		}

		[NonSerialized]
		public UIElement Figure;

		[NonSerialized]
		public Page Page;

		private bool _isSelected;

		[JsonProperty]
		/// <summary>
		/// Hash of the object
		/// </summary>
		public int Hash{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Angle from start position
		/// </summary>
		public float AngleGlobal{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Position of the object
		/// </summary>
		public Position Position{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Color of the object border
		/// </summary>
		public Color BorderColor{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Thickness of the object border
		/// </summary>
		public int BorderThickness{ get; set; }

		[JsonProperty]
		/// <summary>
		/// Object name
		/// </summary>
		public string Name{ get; set; } = null;

		/// <summary>
		/// Object standard name. Used instead of Name if user don't set it.
		/// </summary>
		protected abstract string StdName{ get; }

		/// <summary>
		/// True, if object is selected. Otherwise false.
		/// </summary>
		public bool IsSelectedProp {
			get { return _isSelected; }
			set {
				_isSelected = value;
				Update();
			}
		}

		public bool IsSelectable => this is ISelectable;

		/// <summary>
		/// Read data from page to object data
		/// </summary>
		public abstract void ReadPage();

		/// <summary>
		/// Update page with current object data
		/// </summary>
		public abstract void WritePage();

		/// <summary>
		/// Create figure by object data
		/// </summary>
		public abstract void CreateObject();

		/// <summary>
		/// Update graphical object with current object data
		/// </summary>
		public abstract void Update();

		public void ObjectChangedFunc(object sender, MouseButtonEventArgs args) => ObjectChanged?.Invoke(sender, args);

		public override string ToString() => $"{(Name == ""? StdName : Name)} ({Position})";

		//public void Select() { IsSelectedProp = true; }
		//public void Unselect() { IsSelectedProp = false; }
	}
}
