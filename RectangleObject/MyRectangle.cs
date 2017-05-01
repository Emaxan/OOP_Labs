using System;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using IEditableInt;
using Params;

namespace RectangleObject {
	[Serializable]
	sealed class MyRectangle: MyGraphicalObject, IEditable {
		internal MyRectangle() {
			Figure = new Rectangle();
			Figure.MouseDown += SelectableEvent;
		}

		protected override string StdName{ get; } = "Rectangle";

		public int Length1{ get; set; }

		public int Length2{ get; set; }

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
									X = X,
									Y = Y,
									Length1 = Length1,
									Length2 = Length2
								};
		}

		public override void SetParams(MyParams param) {
			if((param.Fields&(int) MyFields.Name) != 0) Name = param.Name;
			if((param.Fields&(int) MyFields.Thickness) != 0) BorderThickness = param.Thickness;
			if((param.Fields&(int) MyFields.BgColor) != 0) BgColor = param.BgColor;
			if((param.Fields&(int) MyFields.BorderColor) != 0) BorderColor = param.BorderColor;
			if((param.Fields&(int) MyFields.GAngle) != 0) AngleGlobal = param.GAngle;
			if((param.Fields&(int) MyFields.Position) != 0) { X = param.X; Y = param.Y; }
			if ((param.Fields&(int) MyFields.Length1) != 0) Length1 = param.Length1;
			if((param.Fields&(int) MyFields.Length2) != 0) Length2 = param.Length1;
			Update();
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, X, Y);
			var tt = new TranslateTransform(X, Y);
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
			UpdateHash();
		}
	}
}
