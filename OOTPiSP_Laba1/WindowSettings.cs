using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class WindowSettings {
		private int _width;
		private int _height;
		private int _top;
		private int _left;
		private Color _bgDarkColor;
		private Color _bgMidColor;
		private Color _bgLightColor;
		private Color _bgButtonsColor;

		public int Width {
			get {
				return _width;
			}
			set {
				_width = value;
				Application.Current.Dispatcher.Invoke(() => {
														Application.Current.MainWindow.Width = value;
													});
			}
		}

		public int Height {
			get { return _height; }
			set {
				_height = value;
				Application.Current.Dispatcher.Invoke(() => {
														Application.Current.MainWindow.Height = value;
													});
			}
		}

		public int Top {
			get { return _top; }
			set {
				_top = value;
				Application.Current.Dispatcher.Invoke(() => {
														Application.Current.MainWindow.Top = value;
													});
			}
		}

		public int Left {
			get { return _left; }
			set {
				_left = value;
				Application.Current.Dispatcher.Invoke(() => {
														Application.Current.MainWindow.Left = value;
													});
			}
		}

		public Color BgDarkColor {
			get { return _bgDarkColor; }
			set {
				_bgDarkColor = value;
				Properties.Settings.Default["BgDarkColor"] = new SolidColorBrush(value);
				Properties.Settings.Default.Save();
			}
		}

		public Color BgMidColor {
			get {
				return _bgMidColor;
			}
			set {
				_bgMidColor = value;
				Properties.Settings.Default["BgMidColor"] = new SolidColorBrush(value);
				Properties.Settings.Default.Save();
			}
		}

		public Color BgLightColor {
			get {
				return _bgLightColor;
			}
			set {
				_bgLightColor = value;
				Properties.Settings.Default["BgLightColor"] = new SolidColorBrush(value);
				Properties.Settings.Default.Save();
			}
		}

		public Color BgButtonsColor {
			get {
				return _bgButtonsColor;
			}
			set {
				_bgButtonsColor = value;
				Properties.Settings.Default["BgButtonsColor"] = new SolidColorBrush(value);
				Properties.Settings.Default.Save();
			}
		}

		public void Save() {
			var ser = new XmlSerializer(typeof(WindowSettings));
			using(var stream = new FileStream("conf.ini", FileMode.Create)) {
				ser.Serialize(stream, this);
			}
		}

		public void Load() {
			var ser = new XmlSerializer(typeof(WindowSettings));
			var f = new FileInfo("conf.ini");
			if(!f.Exists) {
				Save();
			}
			using (var stream = new FileStream("conf.ini", FileMode.Open)) {
				WindowSettings t = null;
				try {
					t = (WindowSettings)ser.Deserialize(stream);
				}
				catch (Exception) {}
				if (t == null)
					return;
				Properties.Settings.Default.BgButtonsColor = new SolidColorBrush(t.BgButtonsColor);
				Properties.Settings.Default.BgDarkColor = new SolidColorBrush(t.BgDarkColor);
				Properties.Settings.Default.BgLightColor = new SolidColorBrush(t.BgLightColor);
				Properties.Settings.Default.BgMidColor = new SolidColorBrush(t.BgMidColor);
				Properties.Settings.Default.Save();
				BgButtonsColor = t.BgButtonsColor;
				BgDarkColor = t.BgDarkColor;
				BgMidColor = t.BgMidColor;
				BgLightColor = t.BgLightColor;
				Application.Current.Dispatcher.Invoke(() => {
					Height = (int)(Application.Current.MainWindow.Height = t.Height);
					Left = (int)(Application.Current.MainWindow.Left = t.Left);
					Top = (int)(Application.Current.MainWindow.Top = t.Top);
					Width = (int)(Application.Current.MainWindow.Width = t.Width);
				});
			}
		}
	}
}