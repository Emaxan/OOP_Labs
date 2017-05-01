using System.Windows.Media;
using GeneralObject;
using Params;

namespace CircleObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 A 10,10 360 1 0 1,1 z";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyCircle();
			obj.SetParams(new MyParams {
											Fields = (int) (
																MyFields.Name
																|MyFields.Thickness
																|MyFields.BgColor
																|MyFields.BorderColor
																|MyFields.GAngle
																|MyFields.Position
																|MyFields.RadiusX
															),
											X = 0,
											Y = 0,
											RadiusX = 0,
											BgColor = Colors.Transparent,
											BorderColor = Colors.Black,
											Thickness = 1,
											GAngle = 0
										});
			return obj;
		}
	}
}
