using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using Newtonsoft.Json;
using Params;
using static System.Math;

namespace RombusObject {
	[Serializable]
	sealed class MyRombus: MyGraphicalObject {
		[JsonConstructor]
		private MyRombus(int x,
						int y,
						int length1,
						float angle,
						Color bgColor,
						Color borderColor,
						int borderThickness,
						float angleGlobal)
			: base(x, y, bgColor, borderColor, borderThickness, angleGlobal) {
			Length1 = length1;
			Angle = angle;
			CreateObject();
			Hash = GetHashCode();
		}

		protected override string StdName{ get; } = "Rombus";

		public int Length1{ get; set; }

		public float Angle {
			get { return (float) (RadAngle1*180/PI); }
			set { RadAngle1 = (float) (PI*(value > 90? 180 - value : value)/180); }
		}

		internal float RadAngle1{ get; private set; }

		public static MyRombus CreateFigure(MyParams param) {
			return new MyRombus(param.Position.X, param.Position.Y, param.Length1, param.Angle, param.BgColor, param.BorderColor, param.Thickness, param.GAngle);
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
														|MyFields.Angle
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									Length1 = Length1,
									Angle = Angle
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
			if ((param.Fields & (int)MyFields.Angle) != 0) Angle = param.Angle;
			Update();
		}

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
			Figure.MouseDown += SelectableEvent;
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
	}
}
