using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using IEditableInt;
using Params;
using static System.Math;

namespace TriangleObject {
	[Serializable]
	sealed class MyTriangle: MyGraphicalObject, IEditable {
		internal MyTriangle() {
			Figure = new Polygon();
			Figure.MouseDown += SelectableEvent;
		}

		protected override string StdName{ get; } = "Triangle";

		public int Length1{ get; set; }

		public int Length2{ get; set; }

		public float Angle {
			get { return (float) (RadAngle1*180/PI); }
			set { RadAngle1 = (float) (PI*(value > 90? 180 - value : value)/180); }
		}

		internal float RadAngle1{ get; private set; }

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
														|MyFields.Angle
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									Position = Position,
									Length1 = Length1,
									Length2 = Length2,
									Angle = Angle
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
			if((param.Fields&(int) MyFields.Length2) != 0) Length2 = param.Length1;
			if((param.Fields&(int) MyFields.Angle) != 0) Angle = param.Angle;
			Update();
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
																new Point(Length1 - Length2*Cos(RadAngle1), Length2*Sin(RadAngle1))
															};
			((Polygon) Figure).StrokeThickness = BorderThickness;
			((Polygon) Figure).Stroke = new SolidColorBrush(BorderColor);
			((Polygon) Figure).Fill = new SolidColorBrush(BgColor);
			((Polygon) Figure).RenderTransform = tg;
			((Polygon) Figure).StrokeDashArray = IsSelectedProp? DashStyles.Dash.Dashes : DashStyles.Solid.Dashes;
			UpdateHash();
		}
	}
}
