﻿using System;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OOTPiSP_Laba1.Windows.Pages;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyEllipse: MyCircle {

		[JsonConstructor]
		protected MyEllipse(int x,
							int y,
							int radiusX,
							int radiusY,
							Color bgColor,
							Color borderColor,
							int borderThickness,
							float angleGlobal)
			: base(x, y, radiusX, bgColor, borderColor, borderThickness, angleGlobal) {
			RadiusY = radiusY;
			CreateObject();
		}

		public static MyEllipse CreateFigure(int x,
											int y,
											int radiusX,
											int radiusY,
											Color bgColor,
											Color borderColor,
											int borderThickness,
											float angleGlobal) {
			return new MyEllipse(x, y, radiusX, radiusY, bgColor, borderColor, borderThickness, angleGlobal);
		}

		protected override string StdName{ get; } = "Ellipse";

		[JsonProperty]
		public int RadiusY{ get; set; }

		public override void CreateObject() {
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
			Page = new MyEllipsePage();
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
			((Ellipse) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
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

		public override void WritePage() => ((MyEllipsePage) Page).Figure = this;
	}
}
