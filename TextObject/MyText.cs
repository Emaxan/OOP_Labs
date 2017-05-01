using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GeneralObject;
using Params;

namespace TextObject {
	[Serializable]
	public class MyText: MyGraphicalObject {
		internal MyText() {
			Figure = new Label();
			Figure.MouseDown += SelectableEvent;
		}

		protected override string StdName{ get; } = "Text";

		public int Length1{ get; set; }

		public int Length2{ get; set; }

		public string Text{ get; set; }

		public override MyParams GetParams() {
			return new MyParams {
									Fields = (int) (
														MyFields.Name
														| MyFields.Thickness
														| MyFields.BgColor
														| MyFields.BorderColor
														| MyFields.GAngle
														| MyFields.Position
														| MyFields.Length1
														| MyFields.Length2
														| MyFields.Text
													),
									Name = Name,
									Thickness = BorderThickness,
									BgColor = BgColor,
									BorderColor = BorderColor,
									GAngle = AngleGlobal,
									X = X,
									Y = Y,
									Length1 = Length1,
									Length2 = Length2,
									Text = Text
								};
		}

		public override void SetParams(MyParams param) {
			if ((param.Fields & (int)MyFields.Name) != 0)
				Name = param.Name;
			if ((param.Fields & (int)MyFields.Thickness) != 0)
				BorderThickness = param.Thickness;
			if ((param.Fields & (int)MyFields.BgColor) != 0)
				BgColor = param.BgColor;
			if ((param.Fields & (int)MyFields.BorderColor) != 0)
				BorderColor = param.BorderColor;
			if ((param.Fields&(int) MyFields.GAngle) != 0)
				AngleGlobal = param.GAngle;
			if((param.Fields&(int) MyFields.Position) != 0) {
				X = param.X;
				Y = param.Y;
			}
			if((param.Fields&(int) MyFields.Length1) != 0)
				Length1 = param.Length1;
			if((param.Fields&(int) MyFields.Length2) != 0)
				Length2 = param.Length1;
			if((param.Fields&(int) MyFields.Text) != 0)
				Text = param.Text;
			Update();
		}

		public override void Update() {
			var rt = new RotateTransform(AngleGlobal, X, Y);
			var tt = new TranslateTransform(X, Y);
			var tg = new TransformGroup();
			tg.Children.Add(tt);
			tg.Children.Add(rt);
			((Label) Figure).BorderThickness = new Thickness(BorderThickness);
			((Label) Figure).BorderBrush = new SolidColorBrush(BorderColor);
			((Label) Figure).Background = new SolidColorBrush(BgColor);
			((Label) Figure).Width = Length1;
			((Label) Figure).Height = Length2;
			((Label) Figure).RenderTransform = tg;
			((Label) Figure).Content = Text;
			UpdateHash();
		}
	}
}
