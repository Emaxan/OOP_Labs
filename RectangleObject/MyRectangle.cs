using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using Newtonsoft.Json;
using Params;

namespace RectangleObject {
	[Serializable]
	sealed class MyRectangle: MyGraphicalObject {
		[JsonConstructor]
		private MyRectangle(int x,
							int y,
							int length1,
							int length2,
							Color bgColor,
							Color borderColor,
							int borderThickness,
							float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			Length1 = length1;
			Length2 = length2;
			CreateObject();
			Hash = GetHashCode();
		}

		protected override string StdName{ get; } = "Rectangle";

		public int Length1{ get; set; }

		public int Length2{ get; set; }

		public static MyRectangle CreateFigure(MyParams param) {
			return new MyRectangle(param.Position.X, param.Position.Y, param.Length1, param.Length2, param.BgColor, param.BorderColor, param.Thickness, param.GAngle);
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
														|MyFields.Length2
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									Length1 = Length1,
									Length2 = Length2
								};
		}

		public override void SetParams(MyParams param) {
			if ((param.Fields & (int)MyFields.Name) != 0) Name = param.Name;
			if ((param.Fields & (int)MyFields.Thickness) != 0) BorderThickness = param.Thickness;
			if ((param.Fields & (int)MyFields.BgColor) != 0) BgColor = param.BgColor;
			if ((param.Fields & (int)MyFields.BorderColor) != 0) BorderColor = param.BorderColor;
			if ((param.Fields & (int)MyFields.GAngle) != 0) AngleGlobal = param.GAngle;
			if ((param.Fields & (int)MyFields.Position) != 0) Position = param.Position;
			if ((param.Fields & (int)MyFields.Length1) != 0) Length1 = param.Length1;
			if ((param.Fields & (int)MyFields.Length2) != 0) Length2 = param.Length1;
			Update();
		}

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
			Figure.MouseDown += SelectableEvent;
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
	}
}
