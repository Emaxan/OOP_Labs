﻿using System.Windows.Media;

namespace OOTPiSP_Laba1.Factory {
	public class SquareFactory: IMainFactory {
		public MyGraphicalObject CreateFigure(params object[] parameters) {
			return new MySquare((int) parameters[0],
				(int) parameters[1],
				(int) parameters[2],
				(Color) parameters[3],
				(Color) parameters[4],
				(int) parameters[5],
				(float) parameters[6]);
		}
	}
}