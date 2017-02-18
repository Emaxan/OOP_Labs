using System.Windows.Media;

namespace OOTPiSP_Laba1.Factory {
	public class ParallelogramFactory: IMainFactory {
		public MyGraphicalObject CreateFigure(params object[] parameters) {
			return new MyParallelogram((int) parameters[0],
				(int) parameters[1],
				(int) parameters[2],
				(int) parameters[3],
				(float) parameters[4],
				(Color) parameters[5],
				(Color) parameters[6],
				(int) parameters[7],
				(float) parameters[8]);
		}
	}
}
