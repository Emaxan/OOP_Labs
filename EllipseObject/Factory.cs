using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace EllipseObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 A 10,5 360 1 0 1,1 z";

		public override MyGraphicalObject CreateShape() {
			return MyEllipse.CreateFigure(new MyParams {
														Position = new Position(0, 0),
														RadiusX = 0,
														RadiusY = 0,
														BgColor = Colors.Transparent,
														BorderColor = Colors.Black,
														Thickness = 1,
														GAngle = 0
													});
		}
	}
}