using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public abstract class MyGraphicalObject {
		[NonSerialized] [XmlIgnore] public UIElement Figure;

		public bool IsSelected;

		[NonSerialized] [XmlIgnore] public Page Page;

		public MyGraphicalObject() {
		}

		public MyGraphicalObject(int x, int y, Color bgColor, Color borderColor, int borderThickness, float angleGlobal) {
			Position = new Position(x, y);
			BgColor = bgColor;
			BorderColor = borderColor;
			AngleGlobal = angleGlobal%360;
			BorderThickness = borderThickness;
			Hash = GetHashCode();
		}
		/// <summary>
		/// Hash of the object
		/// </summary>
		public int Hash { get; set; }
		/// <summary>
		/// Angle from start position
		/// </summary>
		public float AngleGlobal { get; set; }
		/// <summary>
		/// Position of the object
		/// </summary>
		public Position Position { get; set; }
		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor { get; set; }
		/// <summary>
		/// Color of the object border
		/// </summary>
		public Color BorderColor { get; set; }
		/// <summary>
		/// Thickness of the object border
		/// </summary>
		public int BorderThickness { get; set; }
		/// <summary>
		/// Object name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Object standart name. Used instead oа Name if user dont set it.
		/// </summary>
		public abstract string StdName { get; }

		[XmlIgnore]
		public abstract bool IsSelectedProp { get; set; }
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

		public override string ToString() {
			return $"{(Name == "" ? StdName : Name)} ({Position.X}:{Position.Y})";
		}
	}
}