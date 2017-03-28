using System.Windows.Media;
using GeneralObject;
using MyPosition;
using Params;

namespace LineObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 l 1,1";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyLine();
			obj.SetParams(new MyParams {
											Fields = (int) (
																MyFields.Name
																|MyFields.Thickness
																|MyFields.BgColor
																|MyFields.BorderColor
																|MyFields.GAngle
																|MyFields.Position
																|MyFields.Length1
															),
											Position = new Position(0, 0),
											Length1 = 0,
											BgColor = Colors.Transparent,
											BorderColor = Colors.Black,
											Thickness = 1,
											GAngle = 0
										});
			return obj;
		}
	}
}