using System.Windows.Media;
using GeneralObject;
using Params;

namespace RectangleObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 h 1 v 0.5 h -1 z";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyRectangle();
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
															),
											X = 0,
											Y = 0,
											Length1 = 0,
											Length2 = 0,
											BgColor = Colors.Transparent,
											BorderColor = Colors.Black,
											Thickness = 1,
											GAngle = 0
										});
			return obj;
		}
	}
}