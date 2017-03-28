using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using IEditableInt;
using Params;

namespace CircleObject {
	[Serializable]
	sealed class MyCircle: MyGraphicalObject, IEditable {
		internal MyCircle() {
			Figure = new Ellipse();
			Figure.MouseDown += SelectableEvent;
		}

		protected override string StdName{ get; } = "Circle";

		public int RadiusX{ get; set; }

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
			if((param.Fields&(int) MyFields.Name) != 0) Name = param.Name;
			if((param.Fields&(int) MyFields.Thickness) != 0) BorderThickness = param.Thickness;
			if((param.Fields&(int) MyFields.BgColor) != 0) BgColor = param.BgColor;
			if((param.Fields&(int) MyFields.BorderColor) != 0) BorderColor = param.BorderColor;
			if((param.Fields&(int) MyFields.GAngle) != 0) AngleGlobal = param.GAngle;
			if((param.Fields&(int) MyFields.Position) != 0) Position = param.Position;
			if((param.Fields&(int) MyFields.RadiusX) != 0) RadiusX = param.RadiusX;
			Update();
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
			UpdateHash();
		}
	}
}
