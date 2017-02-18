using System;
using System.Windows.Media;
using System.Windows.Shapes;
using OOTPiSP_Laba1.Windows.Pages;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyRectangle: MySquare {
		internal MyRectangle() { }

		internal MyRectangle(int x,
		                   int y,
		                   int length1,
		                   int length2,
		                   Color bgColor,
		                   Color borderColor,
		                   int borderThickness,
		                   float angleGlobal)
			: base(x, y, length1, bgColor, borderColor, borderThickness, angleGlobal) {
			Length2 = length2;
			CreateObject();
		}

		protected override string StdName{ get; } = "Rectangle";

		public int Length2{ get; set; }

		public override void CreateObject() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Rectangle {
				                       Stroke = new SolidColorBrush(BorderColor),
				                       StrokeThickness = BorderThickness,
				                       Fill = new SolidColorBrush(BgColor),
				                       Width = Length1,
				                       Height = Length2,
				                       RenderTransform = tg
			                       };
			Page = new MyRectanglePage();
			((MyRectanglePage) Page).Figure = this;
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Rectangle) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Rectangle) Figure).StrokeThickness = BorderThickness;
			((Rectangle) Figure).Fill = new SolidColorBrush(BgColor);
			((Rectangle) Figure).Width = Length1;
			((Rectangle) Figure).Height = Length2;
			((Rectangle) Figure).RenderTransform = tg;
			((Rectangle) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
		}

		public override void ReadPage() {
			var fig = ((MyRectanglePage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			BorderThickness = fig.BorderThickness;
			Length1 = fig.Length1;
			Length2 = fig.Length2;
			BgColor = fig.BgColor;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() => ((MyRectanglePage) Page).Figure = this;
	}
}
