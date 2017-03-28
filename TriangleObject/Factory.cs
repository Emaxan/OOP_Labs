using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace TriangleObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 h 1 l -0.5,-0.5 z";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyTriangle();
			obj.SetParams(new MyParams {
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
											Position = new Position(0, 0),
											Length1 = 0,
											Length2 = 0,
											Angle = 0,
											BgColor = Colors.Transparent,
											BorderColor = Colors.Black,
											Thickness = 1,
											GAngle = 0
										});
			return obj;
		}
	}
}