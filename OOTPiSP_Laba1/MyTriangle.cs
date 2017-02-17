using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using OOTPiSP_Laba1.Windows.Pages;
using static System.Math;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyTriangle : MyParallelogram {
		public MyTriangle() {
		}

		public MyTriangle(int x, int y, int length1, int length2, float angle, Color bgColor, Color borderColor,
			int borderThickness, float angleGlobal)
			: base(x, y, length1, length2, angle, bgColor, borderColor, borderThickness, angleGlobal) {
			CreateObject();
		}

		public override string StdName { get; } = "Triangle";

		[XmlIgnore]
		public override bool IsSelectedProp {
			get { return IsSelected; }
			set {
				IsSelected = value;
				((Polygon) Figure).StrokeDashArray = value ? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			}
		}

		public override void CreateObject() {
			Page = new MyParallelogramPage();
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Polygon {
				Points = new PointCollection {
					new Point(0, 0),
					new Point(Length1, 0),
					new Point(Length1 - Length2*Cos(RadAngle1), Length2*Sin(RadAngle1))
				},
				Stroke = new SolidColorBrush(BorderColor),
				StrokeThickness = BorderThickness,
				Fill = new SolidColorBrush(BgColor),
				RenderTransform = tg
			};
			((MyParallelogramPage) Page).Figure = this;
		}

		public override void ReadPage() {
			var fig = ((MyParallelogramPage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			Angle = fig.Angle;
			Length1 = fig.Length1;
			Length2 = fig.Length2;
			BorderThickness = fig.BorderThickness;
			BgColor = fig.BgColor;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() {
			((MyParallelogramPage) Page).Figure = this;
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Polygon) Figure).Points = new PointCollection {
				new Point(0, 0),
				new Point(Length1, 0),
				new Point(Length1 - Length2*Cos(RadAngle1), Length2*Sin(RadAngle1))
			};
			((Polygon) Figure).StrokeThickness = BorderThickness;
			((Polygon) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Polygon) Figure).Fill = new SolidColorBrush(BgColor);
			((Polygon) Figure).RenderTransform = tg;
		}
	}
}