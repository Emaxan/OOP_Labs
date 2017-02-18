using System.Windows.Media;

namespace OOTPiSP_Laba1.Factory {
	public class CircleFactory: IMainFactory {
		public MyGraphicalObject CreateFigure(params object[] parameters) {
			var x = (int)parameters[0];
		    var y = (int)parameters[1];
			var radiusX = (int)parameters[2];
			var bgColor = (Color)parameters[3];
			var borderColor = (Color)parameters[4];
			var borderThickness = (int)parameters[5];
			var angleGlobal = (float)parameters[6];

			return new MyCircle(x, y, radiusX, bgColor, borderColor, borderThickness, angleGlobal);
		}
	}
}
