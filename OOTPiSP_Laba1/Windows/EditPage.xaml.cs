using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyPosition;
using Params;

namespace OOTPiSP_Laba1.Windows {
	/// <summary>
	/// Interaction logic for EditPage.xaml
	/// </summary>
	public partial class EditPage {

		private int _fields;

		public EditPage() { InitializeComponent(); }

		private int Fields {
			set {
				if ((value & (int)MyFields.Name) == 0) {
					Rd0.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.Thickness) == 0) {
					Rd1.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.BgColor) == 0) {
					Rd2.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.BorderColor) == 0) {
					Rd3.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.GAngle) == 0) {
					Rd4.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.Position) == 0) {
					Rd5.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.RadiusX) == 0) {
					Rd6.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.RadiusY) == 0) {
					Rd7.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.Length1) == 0) {
					Rd8.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.Length2) == 0) {
					Rd9.Height = new GridLength(0);
				}
				if ((value & (int)MyFields.Angle) == 0) {
					Rd10.Height = new GridLength(0);
				}
				_fields = value;
			}	
		}

		/// <summary>
		/// Read data from page
		/// </summary>
		public void ReadData(out MyParams myParams) {
			myParams = new MyParams {Fields = _fields};
			if ((_fields & (int)MyFields.Name) != 0) {
			myParams.Name = TbName.Text;
			}
			if ((_fields & (int)MyFields.Thickness) != 0) {
			myParams.Thickness = int.Parse(TbThickness.Text);
			}
			if ((_fields & (int)MyFields.BgColor) != 0) {
			myParams.BgColor = ((SolidColorBrush)BgColor.Background).Color;
			}
			if ((_fields & (int)MyFields.BorderColor) != 0) {
			myParams.BorderColor = ((SolidColorBrush)BColor.Background).Color;
			}
			if ((_fields & (int)MyFields.GAngle) != 0) {
			myParams.GAngle = float.Parse(TbGAngle.Text);
			}
			if ((_fields & (int)MyFields.Position) != 0) {
			myParams.Position = new Position(int.Parse(TbPosX.Text), int.Parse(TbPosY.Text));
			}
			if ((_fields & (int)MyFields.RadiusX) != 0) {
			myParams.RadiusX = int.Parse(TbRadiusX.Text);
			}
			if ((_fields & (int)MyFields.RadiusY) != 0) {
			myParams.RadiusY = int.Parse(TbRadiusY.Text);
			}
			if ((_fields & (int)MyFields.Length1) != 0) {
			myParams.Length1 = int.Parse(TbLength1.Text);
			}
			if ((_fields & (int)MyFields.Length2) != 0) {
			myParams.Length2 = int.Parse(TbLength2.Text);
			}
			if ((_fields & (int)MyFields.Angle) != 0) {
			myParams.Angle = float.Parse(TbAngle.Text);
			}
		}

		/// <summary>
		/// Write data to page
		/// </summary>
		public void WriteData(MyParams myParams) {
			Fields = myParams.Fields;
			if ((_fields & (int)MyFields.Name) != 0) {
			TbName.Text = myParams.Name;
			}
			if ((_fields & (int)MyFields.Thickness) != 0) {
			TbThickness.Text = myParams.Thickness.ToString();
			}
			if ((_fields & (int)MyFields.BgColor) != 0) {
			BColor.Background = new SolidColorBrush(myParams.BorderColor);
			}
			if ((_fields & (int)MyFields.BorderColor) != 0) {
			BgColor.Background = new SolidColorBrush(myParams.BgColor);
			}
			if ((_fields & (int)MyFields.GAngle) != 0) {
			TbGAngle.Text = myParams.GAngle.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.Position) != 0) {
			TbPosX.Text = myParams.Position.X.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.Position) != 0) {
			TbPosY.Text = myParams.Position.Y.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.RadiusX) != 0) {
			TbRadiusX.Text = myParams.RadiusX.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.RadiusY) != 0) {
			TbRadiusY.Text = myParams.RadiusY.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.Length1) != 0) {
			TbLength1.Text = myParams.Length1.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.Length2) != 0) {
				TbLength2.Text = myParams.Length2.ToString(CultureInfo.InvariantCulture);
			}
			if ((_fields & (int)MyFields.Angle) != 0) {
				TbAngle.Text = myParams.Angle.ToString(CultureInfo.InvariantCulture);
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
			if (((TextBox)sender).Text == "")
				((TextBox)sender).Text = "0";
			if (!int.TryParse(((TextBox)sender).Text, out value))
				((TextBox)sender).Text = "0";
		}

		private void Numbers_PreviewKeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Space)
				e.Handled = true;
		}
	}
}
