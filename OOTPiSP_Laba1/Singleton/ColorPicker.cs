using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOTPiSP_Laba1.Singleton {
	public class ColorPicker {
		private static bool _colorPicker = false, _colorPickerControls = false;

		protected ColorPicker() { }

		private static ColorPicker _picker;

		public static ColorPicker GetInstance() => _picker ?? (_picker = new ColorPicker());

		public Color? PickColor(Color initailColor) {
			var s = Assembly.GetExecutingAssembly().Location;
			if (s == null)
				return null;
			s = s.Substring(0, s.LastIndexOf('\\'));
			var f = new FileInfo(s + @"/ext/ColorPickerControls.dll");
			if (File.Exists(s + @"/ext/ColorPicker.dll"))
				_colorPicker = true;
			if (f.Exists)
				_colorPickerControls = true;
			if (_colorPicker && _colorPickerControls) {
				var ass = Assembly.LoadFile(f.FullName);
				var type = ass.GetType("ColorPickerControls.Dialogs.ColorPickerFullWithAlphaDialog", true, true);
				var constructorInfo = type.GetConstructor(new Type[0]);
				dynamic dia = constructorInfo?.Invoke(new object[0]);
				dia.InitialColor = initailColor;
				Color? c = dia.ShowDialog() == true ? dia.SelectedColor : null;
				return c;
			}

			else {
				MessageBox.Show("Can't select color.\n" + (_colorPicker ? "" : "ColorPicker.dll corrupted or not exist.\n") +
								(_colorPickerControls ? "" : "ColorPickerControls.dll corrupted or not exist."));
				return null;
			}
		}
	}
}
