﻿using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using Newtonsoft.Json;
using Params;

namespace EllipseObject {
	[Serializable]
	sealed class MyEllipse: MyGraphicalObject {
		[JsonConstructor]
		private MyEllipse(int x,
						int y,
						int radiusX,
						int radiusY,
						Color bgColor,
						Color borderColor,
						int borderThickness,
						float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			RadiusY = radiusY;
			RadiusX = radiusX;
			CreateObject();
			Hash = GetHashCode();
		}

		protected override string StdName{ get; } = "Ellipse";

		public int RadiusY{ get; set; }

		public int RadiusX{ get; set; }

		public static MyEllipse CreateFigure(MyParams param) {
			return new MyEllipse(param.Position.X, param.Position.Y, param.RadiusX, param.RadiusY, param.BgColor, param.BorderColor, param.Thickness, param.GAngle);
		}

		public override MyParams GetParams() {
			return new MyParams {
									Fields = (int) (
														MyFields.Name
														|MyFields.Thickness
														|MyFields.BgColor
														|MyFields.BorderColor
														|MyFields.GAngle
														|MyFields.Position
														|MyFields.RadiusX
														|MyFields.RadiusY
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									RadiusX = RadiusX,
									RadiusY = RadiusY
								};
		}

		public override void SetParams(MyParams param) {
			if ((param.Fields & (int)MyFields.Name) != 0) Name = param.Name;
			if ((param.Fields & (int)MyFields.Thickness) != 0) BorderThickness = param.Thickness;
			if ((param.Fields & (int)MyFields.BgColor) != 0) BgColor = param.BgColor;
			if ((param.Fields & (int)MyFields.BorderColor) != 0) BorderColor = param.BorderColor;
			if ((param.Fields & (int)MyFields.GAngle) != 0) AngleGlobal = param.GAngle;
			if ((param.Fields & (int)MyFields.Position) != 0) Position = param.Position;
			if ((param.Fields & (int)MyFields.RadiusX) != 0) RadiusX = param.RadiusX;
			if ((param.Fields & (int)MyFields.RadiusY) != 0) RadiusY = param.RadiusY;
			Update();
		}

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

			Figure.MouseDown += SelectableEvent;
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
	}
}