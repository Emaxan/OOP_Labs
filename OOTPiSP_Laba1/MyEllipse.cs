using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using OOTPiSP_Laba1.Windows.Pages;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyEllipse : MyCircle {
		public MyEllipse() {
		}

		public MyEllipse(int x, int y, int radiusX, int radiusY, Color bgColor, Color borderColor, int borderThickness,
			float angleGlobal)
			: base(x, y, radiusX, bgColor, borderColor, borderThickness, angleGlobal) {
			RadiusY = radiusY;
			CreateObject();
		}

		public override string Name { get; set; } = string.Empty;
		public override string StdName { get; set; } = "Ellipse";

		public int RadiusY { get; set; }

		[XmlIgnore]
		public override bool IsSelectedProp {
			get { return IsSelected; }
			set {
				IsSelected = value;
				((Ellipse) Figure).StrokeDashArray = value ? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			}
		}

		public override void ReadPage() {
			var fig = ((MyEllipsePage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			RadiusX = fig.RadiusX;
			RadiusY = fig.RadiusY;
			BorderThickness = fig.BorderThickness;
			BgColor = fig.BgColor;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() {
			((MyEllipsePage) Page).Figure = this;
		}

		public override void CreateObject() {
			Page = new MyEllipsePage();
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X - RadiusX, Position.Y - RadiusY);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Ellipse {
				Stroke = new SolidColorBrush(BorderColor),
				StrokeThickness = BorderThickness,
				Fill = new SolidColorBrush(BgColor),
				Width = RadiusX*2,
				Height = RadiusY*2,
				RenderTransform = tg
			};
			((MyEllipsePage) Page).Figure = this;
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X - RadiusX, Position.Y - RadiusY);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Ellipse) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Ellipse) Figure).StrokeThickness = BorderThickness;
			((Ellipse) Figure).Fill = new SolidColorBrush(BgColor);
			((Ellipse) Figure).Width = RadiusX*2;
			((Ellipse) Figure).Height = RadiusY*2;
			((Ellipse) Figure).RenderTransform = tg;
		}

		public override string ToString() {
			return $"{(Name == "" ? StdName : Name)} ({Position.X}:{Position.Y})";
		}
	}
}