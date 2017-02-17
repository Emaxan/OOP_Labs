using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OOTPiSP_Laba1.Windows {
	/// <summary>
	///     Логика взаимодействия для EditWindow.xaml
	/// </summary>
	public partial class EditWindow {
		private readonly SolidColorBrush _activeBg = new SolidColorBrush(Color.FromArgb(200, 150, 150, 150));
		private Border _active;
		private double _xSt, _ySt;
		public MyGraphicalObject[] Figure;

		public EditWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			for (var i = 0; i < Figure.Length; i++) {
				Figure[i].WritePage();
				GLinks.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
				if (Figure.Length == 1)
					continue;
				var border = new Border {
					BorderThickness = new Thickness(1),
					BorderBrush = Brushes.Black,
					CornerRadius = new CornerRadius(15),
					Height = 30,
					Width = 30,
					Cursor = Cursors.Hand
				};
				if (i == 0) {
					_active = border;
					border.Background = _activeBg;
				}

				border.MouseLeftButtonUp += NavigationButtonClick;

				border.MouseLeave += (mySender, myE) => {
					((Border) mySender).BorderBrush = new SolidColorBrush(Colors.Black);
					((Border) mySender).Background = Equals((Border) mySender, _active)
						? _activeBg
						: Brushes.Transparent;
				};

				border.MouseEnter += NavigationButton_OnMouseEnter;
				GLinks.Children.Add(border);
				Grid.SetColumn(border, i);
				var label = new Label {
					Content = i + 1,
					HorizontalContentAlignment = HorizontalAlignment.Center,
					VerticalContentAlignment = VerticalAlignment.Center,
					HorizontalAlignment = HorizontalAlignment.Stretch,
					VerticalAlignment = VerticalAlignment.Stretch
				};
				border.Child = label;
			}
			FMain.NavigationService.Navigate(Figure[0].Page);
		}

		private void NavigationButtonClick(object sender, MouseButtonEventArgs e) {
			FMain.NavigationService.Navigate(Figure[Grid.GetColumn((Border) sender)].Page);
			_active.Background = Brushes.Transparent;
			_active = (Border) sender;
			_active.Background = _activeBg;
		}

		private void TopBorder_OnMouseMove(object sender, MouseEventArgs e) {
			if (e.LeftButton != MouseButtonState.Pressed) return;
			var x = PointToScreen(e.GetPosition(TopText)).X;
			var y = PointToScreen(e.GetPosition(TopText)).Y;
			Top += y - _ySt;
			Left += x - _xSt;
			_xSt = x;
			_ySt = y;
		}

		private void TopBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			_xSt = PointToScreen(e.MouseDevice.GetPosition(TopText)).X;
			_ySt = PointToScreen(e.MouseDevice.GetPosition(TopText)).Y;
		}

		private void NavigationButton_OnMouseEnter(object sender, MouseEventArgs e) {
			((Border) sender).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
			((Border) sender).Background = new SolidColorBrush(Color.FromArgb(150, 50, 50, 50));
		}

		private void NavigationButton_OnMouseLeave(object sender, MouseEventArgs e) {
			((Border) sender).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
			((Border) sender).Background = new SolidColorBrush(Color.FromArgb(0, 50, 50, 50));
		}

		private void ButtonOk_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if (!BClose.Focus()) MessageBox.Show("FocusError");
			foreach (var graphicalObject in Figure) graphicalObject.ReadPage();
			DialogResult = true;
			Close();
		}

		private void ButtonCancel_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			DialogResult = false;
			Close();
		}

		private void EditWindow_OnKeyUp(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.Enter:
					ButtonOk_OnMouseLeftButtonUp(sender, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
					e.Handled = true;
					break;
				case Key.Escape:
					ButtonCancel_OnMouseLeftButtonUp(sender, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
					e.Handled = true;
					break;
				case Key.Tab:
					if (Figure.Length == 1) break;
					if (Keyboard.Modifiers == ModifierKeys.Control) {
						FMain.NavigationService.Navigate(
							Figure[(Grid.GetColumn(_active) + 1)%Figure.Length].Page);
						_active.Background = Brushes.Transparent;
						_active = (Border) GLinks.Children[(Grid.GetColumn(_active) + 1)%GLinks.Children.Count];
						_active.Background = _activeBg;
					}
					if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift)) {
						FMain.NavigationService.Navigate(
							Figure[(Grid.GetColumn(_active) - 1 + Figure.Length)%Figure.Length].Page);
						_active.Background = Brushes.Transparent;
						_active = (Border) GLinks.Children[(Grid.GetColumn(_active) - 1 + GLinks.Children.Count)%GLinks.Children.Count];
						_active.Background = _activeBg;
					}
					e.Handled = true;
					break;
			}
		}
	}
}