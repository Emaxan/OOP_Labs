using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace LineObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 l 1,1";

		public override MyGraphicalObject CreateShape() {
			return MyLine.CreateFigure(new MyParams {
														Position = new Position(0, 0),
														Length1 = 0,
														BgColor = Colors.Transparent,
														BorderColor = Colors.Black,
														Thickness = 1,
														GAngle = 0
													});
		}
	}
}