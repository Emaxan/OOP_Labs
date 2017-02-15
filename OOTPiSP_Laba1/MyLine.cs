using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using OOTPiSP_Laba1.Windows.Pages;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyLine : MyGraphicalObject {
		public MyLine() {
		}

		public MyLine(int x, int y, int length1, Color bgColor, Color borderColor, int borderThickness,
			float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			Length1 = length1;
			CreateObject();
		}

		public virtual string Name { get; set; } = string.Empty;
		public virtual string StdName { get; set; } = "Line";

		public int Length1 { get; set; }

		[XmlIgnore]
		public override bool IsSelectedProp {
			get { return IsSelected; }
			set {
				IsSelected = value;
				((Line) Figure).StrokeDashArray = value ? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			}
		}

		public override void ReadPage() {
			var fig = ((MyLinePage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			BorderThickness = fig.BorderThickness;
			Length1 = fig.Length1;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() {
			((MyLinePage) Page).Figure = this;
		}

		public override void CreateObject() {
			Page = new MyLinePage();
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Line {
				Stroke = new SolidColorBrush(BorderColor),
				StrokeThickness = BorderThickness,
				Fill = new SolidColorBrush(BgColor),
				X1 = 0,
				Y1 = 0,
				X2 = Length1,
				Y2 = 0,
				RenderTransform = tg
			};
			((MyLinePage) Page).Figure = this;
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Line) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Line) Figure).StrokeThickness = BorderThickness;
			((Line) Figure).Fill = new SolidColorBrush(BgColor);
			((Line) Figure).X2 = Length1;
			((Line) Figure).RenderTransform = tg;
		}

		public override string ToString() {
			return $"{(Name == "" ? StdName : Name)} ({Position.X}:{Position.Y})";
		}
	}
}