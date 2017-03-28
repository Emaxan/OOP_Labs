using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace RombusObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 l -0.5,0.7 l 0.5,0.7 l 0.5,-0.7 z";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyRombus();
			obj.SetParams(new MyParams {
											Fields = (int) (
																MyFields.Name
																|MyFields.Thickness
																|MyFields.BgColor
																|MyFields.BorderColor
																|MyFields.GAngle
																|MyFields.Position
																|MyFields.Length1
																|MyFields.Angle
															),
											Position = new Position(0, 0),
											Length1 = 0,
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