using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using ISelectableInt;
using Newtonsoft.Json;
using Params;

namespace LineObject {
	[Serializable]
	sealed class MyLine: MyGraphicalObject, ISelectable {
		[JsonConstructor]
		private MyLine(int x,
						int y,
						int length1,
						Color bgColor,
						Color borderColor,
						int borderThickness,
						float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			Length1 = length1;
			CreateObject();
			Hash = GetHashCode();
		}

		protected override string StdName{ get; } = "Line";

		public int Length1{ get; set; }

		public static MyLine CreateFigure(MyParams param) {
			return new MyLine(param.Position.X, param.Position.Y, param.Length1, param.BgColor, param.BorderColor, param.Thickness, param.GAngle);
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

		public override void CreateObject() {
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
			Figure.MouseDown += SelectableEvent;
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
			((Line) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
		}
	}
}
