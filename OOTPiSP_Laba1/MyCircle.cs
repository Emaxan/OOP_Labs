using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using OOTPiSP_Laba1.Windows.Pages;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyCircle : MyGraphicalObject {
		public MyCircle() {
		}

		public MyCircle(int x, int y, int radiusX, Color bgColor, Color borderColor, int borderThickness, float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			RadiusX = radiusX;
			CreateObject();
		}

		public override string StdName { get; } = "Circle";

		public int RadiusX { get; set; }

		[XmlIgnore]
		public override bool IsSelectedProp {
			get { return IsSelected; }
			set {
				IsSelected = value;
				((Ellipse) Figure).StrokeDashArray = value ? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			}
		}

		public override void ReadPage() {
			var fig = ((MyCirclePage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			RadiusX = fig.RadiusX;
			BorderThickness = fig.BorderThickness;
			BgColor = fig.BgColor;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() {
			((MyCirclePage) Page).Figure = this;
		}

		public override void CreateObject() {
			Page = new MyCirclePage();
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X - RadiusX, Position.Y - RadiusX);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Ellipse {
				Stroke = new SolidColorBrush(BorderColor),
				StrokeThickness = BorderThickness,
				Fill = new SolidColorBrush(BgColor),
				Width = RadiusX*2,
				Height = RadiusX*2,
				RenderTransform = tg
			};
			((MyCirclePage) Page).Figure = this;
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X - RadiusX, Position.Y - RadiusX);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Ellipse) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Ellipse) Figure).StrokeThickness = BorderThickness;
			((Ellipse) Figure).Fill = new SolidColorBrush(BgColor);
			((Ellipse) Figure).Width = RadiusX*2;
			((Ellipse) Figure).Height = RadiusX*2;
			((Ellipse) Figure).RenderTransform = tg;
		}
	}
}