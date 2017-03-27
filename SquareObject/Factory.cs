using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace SquareObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 h 1 v 1 h -1 z";

		public override MyGraphicalObject CreateShape() {
			return MySquare.CreateFigure(new MyParams {
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