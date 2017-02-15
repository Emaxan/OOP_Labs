using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using OOTPiSP_Laba1.Windows;

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

		public int Hash { get; set; }
		public float AngleGlobal { get; set; }
		public Position Position { get; set; }
		public Color BgColor { get; set; }
		public Color BorderColor { get; set; }
		public int BorderThickness { get; set; }

		[XmlIgnore]
		public abstract bool IsSelectedProp { get; set; }

		public abstract void ReadPage();
		public abstract void WritePage();
		public abstract void CreateObject();
		public abstract void Update();
	}

	[Serializable]
	public class Position {
		public Position() {
		}

		public Position(int x, int y) {
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}
}