using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using IEditableInt;
using Newtonsoft.Json;
using Params;

namespace CircleObject {
	[Serializable]
	sealed class MyCircle: MyGraphicalObject, IEditable {
		[JsonConstructor]
		private MyCircle(int x,
						int y,
						int radiusX,
						Color bgColor,
						Color borderColor,
						int borderThickness,
						float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			RadiusX = radiusX;
			CreateObject();
			Hash = GetHashCode();
		}

		protected override string StdName{ get; } = "Circle";

		public int RadiusX{ get; set; }

		public static MyCircle CreateFigure(MyParams param) {
			return new MyCircle(param.Position.X, param.Position.Y, param.RadiusX, param.BgColor, param.BorderColor, param.Thickness, param.GAngle);
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
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									RadiusX = RadiusX
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
			Update();
		}

		public override void CreateObject() {
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
									RenderTransform = tg,
									RenderSize = new Size(1,1)
								};
			Figure.MouseDown += SelectableEvent;
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
			((Ellipse) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
		}
	}
}
