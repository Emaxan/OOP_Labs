using System.Windows.Media;

namespace OOTPiSP_Laba1.Factory {
	public class RombusFactory: IMainFactory {
		public MyGraphicalObject CreateFigure(params object[] parameters) {
			return new MyRombus((int) parameters[0],
				(int) parameters[1],
				(int) parameters[2],
				(int) parameters[3],
				(Color) parameters[4],
				(Color) parameters[5],
				(int) parameters[6],
				(float) parameters[7]);
		}
	}
}
