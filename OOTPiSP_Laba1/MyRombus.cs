using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using OOTPiSP_Laba1.Windows.Pages;
using static System.Math;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyRombus: MyLine {
		internal MyRombus() { }

		internal MyRombus(int x,
		                int y,
		                int length1,
		                float angle,
		                Color bgColor,
		                Color borderColor,
		                int borderThickness,
		                float angleGlobal)
			: base(x, y, length1, bgColor, borderColor, borderThickness, angleGlobal) {
			Angle = angle;
			CreateObject();
		}

		protected override string StdName{ get; } = "Rombus";

		public float Angle {
			get { return (float) (RadAngle1*180/PI); }
			set { RadAngle1 = (float) (PI*(value > 90? 180 - value : value)/180); }
		}

		protected float RadAngle1{ get; private set; }

		public override void CreateObject() {
			var rt = new RotateTransform(AngleGlobal, Position.X, Position.Y);
			var tt = new TranslateTransform(Position.X, Position.Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			Figure = new Polygon {
				                     Points = new PointCollection {
					                                                  new Point(0, 0),
					                                                  new Point(Length1, 0),
					                                                  new Point(Length1*(Cos(RadAngle1) + 1), Length1*Sin(RadAngle1)),
					                                                  new Point(Length1*Cos(RadAngle1), Length1*Sin(RadAngle1))
				                                                  },
				                     Stroke = new SolidColorBrush(BorderColor),
				                     StrokeThickness = BorderThickness,
				                     Fill = new SolidColorBrush(BgColor),
				                     RenderTransform = tg
			                     };
			Page = new MyRombusPage();
			((MyRombusPage) Page).Figure = this;
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
				                                                new Point(Length1*(Cos(RadAngle1) + 1), Length1*Sin(RadAngle1)),
				                                                new Point(Length1*Cos(RadAngle1), Length1*Sin(RadAngle1))
			                                                };
			((Polygon) Figure).StrokeThickness = BorderThickness;
			((Polygon) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Polygon) Figure).Fill = new SolidColorBrush(BgColor);
			((Polygon) Figure).RenderTransform = tg;
			((Polygon) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
		}

		public override void ReadPage() {
			var fig = ((MyRombusPage) Page).Figure;
			Position.X = fig.Position.X;
			Position.Y = fig.Position.Y;
			AngleGlobal = fig.AngleGlobal;
			Angle = fig.Angle;
			Length1 = fig.Length1;
			BorderThickness = fig.BorderThickness;
			BgColor = fig.BgColor;
			BorderColor = fig.BorderColor;
			Name = fig.Name;
			Update();
		}

		public override void WritePage() => ((MyRombusPage) Page).Figure = this;
	}
}
