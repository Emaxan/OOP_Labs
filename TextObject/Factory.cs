using System.Windows.Media;
using GeneralObject;
using Params;

namespace TextObject {
	public class Factory: GeneralFactory {
		public const string PathData = "M 0,0 l 0.25,-0.5 l 0.15,0.3 l -0.05,-0.10 h -0.19";

		public override MyGraphicalObject CreateShape() {
			var obj = new MyText();
			obj.SetParams(new MyParams {
											Fields = (int) (
																MyFields.Name
																| MyFields.BgColor
																| MyFields.BorderColor
																| MyFields.GAngle
																| MyFields.Position
																| MyFields.Length1
																| MyFields.Length2
																| MyFields.Text
															),
											X = 0,
											Y = 0,
											Length1 = 0,
											Length2 = 0,
											BorderColor = Colors.Black,
											BgColor = Colors.Transparent,
											GAngle = 0,
											Text = ""
										});
			return obj;
		}
	}
}