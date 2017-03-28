using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using IEditableInt;
using Params;

namespace SquareObject {
	[Serializable]
	sealed class MySquare: MyGraphicalObject, IEditable {
		internal MySquare() {
			Figure = new Rectangle();
			Figure.MouseDown += SelectableEvent;
		}

		protected override string StdName{ get; } = "Square";

		public int Length1{ get; set; }

		public override MyParams GetParams() {
			return new MyParams {
									Fields = (int) (
														MyFields.Name
														|MyFields.Thickness
														|MyFields.BgColor
														|MyFields.BorderColor
														|MyFields.GAngle
														|MyFields.Position
														|MyFields.Length1
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									Length1 = Length1
								};
		}

		public override void SetParams(MyParams param) {
			if((param.Fields&(int) MyFields.Name) != 0) Name = param.Name;
			if((param.Fields&(int) MyFields.Thickness) != 0) BorderThickness = param.Thickness;
			if((param.Fields&(int) MyFields.BgColor) != 0) BgColor = param.BgColor;
			if((param.Fields&(int) MyFields.BorderColor) != 0) BorderColor = param.BorderColor;
			if((param.Fields&(int) MyFields.GAngle) != 0) AngleGlobal = param.GAngle;
			if((param.Fields&(int) MyFields.Position) != 0) Position = param.Position;
			if((param.Fields&(int) MyFields.Length1) != 0) Length1 = param.Length1;
			Update();
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
			((Rectangle) Figure).Height = Length1;
			((Rectangle) Figure).RenderTransform = tg;
			((Rectangle) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			UpdateHash();
		}
	}
}
