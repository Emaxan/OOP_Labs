using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace TriangleObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 h 1 l -0.5,-0.5 z";

		public override MyGraphicalObject CreateShape() {
			return MyTriangle.CreateFigure(new MyParams {
														Position = new Position(0, 0),
														Length1 = 0,
														Length2 = 0,
														Angle = 0,
														BgColor = Colors.Transparent,
														BorderColor = Colors.Black,
														Thickness = 1,
														GAngle = 0
													});
		}
	}
}