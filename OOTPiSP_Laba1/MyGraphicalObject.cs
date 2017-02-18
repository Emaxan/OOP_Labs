using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public abstract class MyGraphicalObject {
		protected MyGraphicalObject() { }

		protected MyGraphicalObject(int x, int y, Color bgColor, Color borderColor, int borderThickness, float angleGlobal) {
			Position = new Position(x, y);
			BgColor = bgColor;
			BorderColor = borderColor;
			AngleGlobal = angleGlobal%360;
			BorderThickness = borderThickness;
			Hash = GetHashCode();
		}

		[NonSerialized]
		[XmlIgnore]
		public UIElement Figure;

		[NonSerialized]
		[XmlIgnore]
		public Page Page;

		private bool _isSelected;

		/// <summary>
		/// Hash of the object
		/// </summary>
		public int Hash{ get; set; }

		/// <summary>
		/// Angle from start position
		/// </summary>
		public float AngleGlobal{ get; set; }

		/// <summary>
		/// Position of the object
		/// </summary>
		public Position Position{ get; set; }

		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor{ get; set; }

		/// <summary>
		/// Color of the object border
		/// </summary>
		public Color BorderColor{ get; set; }

		/// <summary>
		/// Thickness of the object border
		/// </summary>
		public int BorderThickness{ get; set; }

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
		[XmlIgnore]
		public bool IsSelectedProp {
			get { return _isSelected; }
			set {
				_isSelected = value;
				Update();
			}
		}

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

		public override string ToString() => $"{(Name == ""? StdName : Name)} ({Position})";
	}
}
