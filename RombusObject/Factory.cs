using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace RombusObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 l -0.5,0.7 l 0.5,0.7 l 0.5,-0.7 z";

		public override MyGraphicalObject CreateShape() {
			return MyRombus.CreateFigure(new MyParams {
														Position = new Position(0, 0),
														Length1 = 0,
														Angle = 0,
														BgColor = Colors.Transparent,
														BorderColor = Colors.Black,
														Thickness = 1,
														GAngle = 0
													});
		}
	}
}