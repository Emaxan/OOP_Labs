﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP_Laba1.Singleton;

namespace OOTPiSP_Laba1.Windows.Pages {
	/// <summary>
	///     Логика взаимодействия для MyParallelogramPage.xaml
	/// </summary>
	[Serializable]
	public partial class MyParallelogramPage {
		private MyParallelogram _figure;

		public MyParallelogramPage() {
			InitializeComponent();
		}

		public MyParallelogram Figure {
			get {
				_figure.BgColor = ((SolidColorBrush) BgColor.Background).Color;
				_figure.BorderColor = ((SolidColorBrush) BColor.Background).Color;
				_figure.Name = TbName.Text;
				_figure.AngleGlobal = float.Parse(TbGAngle.Text);
				_figure.Angle = float.Parse(TbAngle.Text);
				_figure.Length1 = int.Parse(TbLength1.Text);
				_figure.Length2 = int.Parse(TbLength2.Text);
				_figure.Position.X = int.Parse(TbPosX.Text);
				_figure.Position.Y = int.Parse(TbPosY.Text);
				_figure.BorderThickness = int.Parse(TbThickness.Text);
				return _figure;
			}
			set {
				_figure = value;
				BColor.Background = new SolidColorBrush(value.BorderColor);
				BgColor.Background = new SolidColorBrush(value.BgColor);
				TbName.Text = value.Name;
				TbGAngle.Text = value.AngleGlobal.ToString(CultureInfo.InvariantCulture);
				TbAngle.Text = value.Angle.ToString(CultureInfo.InvariantCulture);
				TbLength1.Text = value.Length1.ToString(CultureInfo.InvariantCulture);
				TbLength2.Text = value.Length2.ToString(CultureInfo.InvariantCulture);
				TbPosX.Text = value.Position.X.ToString(CultureInfo.InvariantCulture);
				TbPosY.Text = value.Position.Y.ToString(CultureInfo.InvariantCulture);
				TbThickness.Text = value.BorderThickness.ToString();
			}
		}

		private void Numbers_KeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.D0:
				case Key.D1:
				case Key.D2:
				case Key.D3:
				case Key.D4:
				case Key.D5:
				case Key.D6:
				case Key.D7:
				case Key.D8:
				case Key.D9:
				case Key.NumPad0:
				case Key.NumPad1:
				case Key.NumPad2:
				case Key.NumPad3:
				case Key.NumPad4:
				case Key.NumPad5:
				case Key.NumPad6:
				case Key.NumPad7:
				case Key.NumPad8:
				case Key.NumPad9:
				case Key.Tab:
					break;
				default:
					e.Handled = true;
					break;
			}
		}

		private void Color_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			var picker = ColorPicker.GetInstance();
			Color? c = picker.PickColor(((SolidColorBrush)((Border)sender).Background).Color);
			if (c != null)
				((Border)sender).Background = new SolidColorBrush((Color)c);
		}

		private void Numbers_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
			int value;
			if (((TextBox) sender).Text == "") ((TextBox) sender).Text = "0";
			if (!int.TryParse(((TextBox) sender).Text, out value)) ((TextBox) sender).Text = "0";
		}

		private void Numbers_PreviewKeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Space) e.Handled = true;
		}
	}
}