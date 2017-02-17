using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace OOTPiSP_Laba1.Windows {
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		private readonly int _start;
		private MyGraphicalObjectList _list = new MyGraphicalObjectList();
		private bool _result;

		private SelectionMode _selMode;

		private int _xStart, _yStart;

		public MainWindow() {
			_start = 1;
			InitializeComponent();
			WMain.Height = SystemParameters.PrimaryScreenHeight;
			WMain.Width = SystemParameters.PrimaryScreenWidth;
			SetHotKeys();
			SelMode = LbObjects.SelectionMode;
			_start = 0;
		}

		private SelectionMode SelMode {
			get { return _selMode; }
			set {
				_selMode = value;
				LbObjects.SelectionMode = value;
				if (_start != 1) return;
				switch (SelMode) {
					case SelectionMode.Extended:
						MenExtendedselect.IsChecked = true;
						break;
					case SelectionMode.Single:
						MenSingleselect.IsChecked = true;
						break;
					case SelectionMode.Multiple:
						MenMultiselect.IsChecked = true;
						break;
				}
			}
		}

		private void ObjectsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			foreach (MyGraphicalObject o in e.AddedItems) {
				o.IsSelectedProp = true;
				o.Update();
			}
			foreach (MyGraphicalObject o in e.RemovedItems) {
				o.IsSelectedProp = false;
				o.Update();
			}
		}

		private void CMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			_xStart = (int) e.MouseDevice.GetPosition(CMain).X;
			_yStart = (int) e.MouseDevice.GetPosition(CMain).Y;
		}

		private void MenSingleselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Single;

		private void MenMultiselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Multiple;

		private void MenExtendedselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Extended;

		private void MenSingleselect_OnClick(object sender, RoutedEventArgs e) {
			if (SelMode != SelectionMode.Single) SelMode = SelectionMode.Single;
		}

		private void MenMultiselect_OnClick(object sender, RoutedEventArgs e) {
			if (SelMode != SelectionMode.Multiple) SelMode = SelectionMode.Multiple;
		}

		private void MenExtendedselect_OnClick(object sender, RoutedEventArgs e) {
			if (SelMode != SelectionMode.Extended) SelMode = SelectionMode.Extended;
		}

		private void CMain_MouseMove(object sender, MouseEventArgs e) {
			var xEnd = (int) e.MouseDevice.GetPosition(CMain).X;
			var yEnd = (int) e.MouseDevice.GetPosition(CMain).Y;
			LMouseCoords.Content = (xEnd >= 0) && (yEnd >= 0) && (xEnd <= CMain.ActualWidth) && (yEnd <= CMain.ActualHeight)
				? $"Mouse сoordinates: ({e.MouseDevice.GetPosition(CMain).X};{e.MouseDevice.GetPosition(CMain).Y})"
				: string.Empty;
			if (((_xStart < 0) || (_yStart < 0) || (_xStart > CMain.ActualWidth) || (_yStart > CMain.ActualHeight)) &&
			    (xEnd >= 0) &&
			    (yEnd >= 0) && (xEnd <= CMain.ActualWidth) && (yEnd <= CMain.ActualHeight)) {
				_xStart = xEnd;
				_yStart = yEnd;
			}

			if ((e.LeftButton != MouseButtonState.Pressed) || (_xStart < 0) || (_yStart < 0) || (_xStart > CMain.ActualWidth) ||
			    (_yStart > CMain.ActualHeight) || (xEnd < 0) || (yEnd < 0) || (xEnd > CMain.ActualWidth) ||
			    (yEnd > CMain.ActualHeight)) return;
			foreach (var graphicalObject in from MyGraphicalObject go in LbObjects.Items where go.IsSelectedProp select go) {
				graphicalObject.Position.X += xEnd - _xStart;
				graphicalObject.Position.Y += yEnd - _yStart;
				graphicalObject.Update();
			}
			LbObjects.ItemsSource = _list.ToList;
			_xStart = xEnd;
			_yStart = yEnd;
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
			switch (
				MessageBox.Show("Are you wont exit without saving?", "Warning", MessageBoxButton.YesNoCancel,
					MessageBoxImage.Warning,
					MessageBoxResult.Cancel, MessageBoxOptions.None)) {
						case MessageBoxResult.Cancel:
							e.Cancel = true;
							break;
						case MessageBoxResult.Yes:
							break;
						case MessageBoxResult.No:
							MenSave.Command.Execute(MenSave.CommandParameter);
							e.Cancel = true;
							break;
						default:
							throw new ArgumentOutOfRangeException();
			}
		}

		#region RoutedEvents

		private void SetHotKeys() {
			#region L3

			var open = new KeyGesture(Key.O, ModifierKeys.Control);
			var save = new KeyGesture(Key.S, ModifierKeys.Control);
			MenOpen.InputGestureText = "Ctrl + O";
			MenSave.InputGestureText = "Ctrl + S";
			MenOpen.Command = Open;
			MenSave.Command = Save;
			MenSave.CommandParameter = _list;
			Open.InputGestures.Add(open);
			Save.InputGestures.Add(save);
			var cmdOpen = new CommandBinding(Open, OpenFile);
			var cmdSave = new CommandBinding(Save, SaveFile);
			CommandBindings.Add(cmdOpen);
			CommandBindings.Add(cmdSave);

			#endregion

			#region General

			var exit = new KeyGesture(Key.X, ModifierKeys.Control);
			var select = new KeyGesture(Key.A, ModifierKeys.Control);
			var unselect = new KeyGesture(Key.U, ModifierKeys.Control);
			var deleteSel = new KeyGesture(Key.Delete);
			var edit = new KeyGesture(Key.I, ModifierKeys.Control);
			MenExit.InputGestureText = "Ctrl + X";
			MenSelect.InputGestureText = "Ctrl + A";
			MenUnselect.InputGestureText = "Ctrl + U";
			MenDeleteSel.InputGestureText = "Delete";
			MenEdit.InputGestureText = "Ctrl + I";
			MenExit.Command = Exit;
			MenSelect.Command = Select;
			MenUnselect.Command = Unselect;
			MenDeleteSel.Command = Delete;
			MenEdit.Command = EditElem;
			Exit.InputGestures.Add(exit);
			Select.InputGestures.Add(select);
			Unselect.InputGestures.Add(unselect);
			Delete.InputGestures.Add(deleteSel);
			EditElem.InputGestures.Add(edit);
			var cmdExit = new CommandBinding(Exit, ExitApp);
			var cmdSelect = new CommandBinding(Select, SelectAll);
			var cmdUnselect = new CommandBinding(Unselect, UnselectAll);
			var cmdDelete = new CommandBinding(Delete, DeleteSelected);
			var cmdEdit = new CommandBinding(EditElem, Edit);
			CommandBindings.Add(cmdExit);
			CommandBindings.Add(cmdUnselect);
			CommandBindings.Add(cmdSelect);
			CommandBindings.Add(cmdDelete);
			CommandBindings.Add(cmdEdit);

			#endregion

			#region L2

			var circle = new KeyGesture(Key.C, ModifierKeys.Control);
			var ellipse = new KeyGesture(Key.E, ModifierKeys.Control);
			var rectangle = new KeyGesture(Key.R, ModifierKeys.Control);
			var square = new KeyGesture(Key.Q, ModifierKeys.Control);
			var rombus = new KeyGesture(Key.M, ModifierKeys.Control);
			var triangle = new KeyGesture(Key.T, ModifierKeys.Control);
			var parallelogram = new KeyGesture(Key.P, ModifierKeys.Control);
			var line = new KeyGesture(Key.L, ModifierKeys.Control);
			MenCircle.InputGestureText = "Ctrl + C";
			MenEllipse.InputGestureText = "Ctrl + E";
			MenRectangle.InputGestureText = "Ctrl + R";
			MenSquare.InputGestureText = "Ctrl + Q";
			MenRombus.InputGestureText = "Ctrl + M";
			MenTriangle.InputGestureText = "Ctrl + T";
			MenParallelogram.InputGestureText = "Ctrl + P";
			MenLine.InputGestureText = "Ctrl + L";
			MenCircle.Command = Circle;
			MenEllipse.Command = Ellipse;
			MenLine.Command = Line;
			MenRectangle.Command = Rectangle;
			MenSquare.Command = Square;
			MenRombus.Command = Rombus;
			MenTriangle.Command = Triangle;
			MenParallelogram.Command = Parallelogram;
			Circle.InputGestures.Add(circle);
			Ellipse.InputGestures.Add(ellipse);
			Line.InputGestures.Add(line);
			Rectangle.InputGestures.Add(rectangle);
			Square.InputGestures.Add(square);
			Rombus.InputGestures.Add(rombus);
			Triangle.InputGestures.Add(triangle);
			Parallelogram.InputGestures.Add(parallelogram);
			var cmdCirc = new CommandBinding(Circle, DrawCirc);
			var cmdElip = new CommandBinding(Ellipse, DrawElip);
			var cmdLine = new CommandBinding(Line, DrawLine);
			var cmdRect = new CommandBinding(Rectangle, DrawRect);
			var cmdSqua = new CommandBinding(Square, DrawSqua);
			var cmdRomb = new CommandBinding(Rombus, DrawRomb);
			var cmdTria = new CommandBinding(Triangle, DrawTria);
			var cmdPara = new CommandBinding(Parallelogram, DrawPara);
			CommandBindings.Add(cmdCirc);
			CommandBindings.Add(cmdElip);
			CommandBindings.Add(cmdLine);
			CommandBindings.Add(cmdRect);
			CommandBindings.Add(cmdSqua);
			CommandBindings.Add(cmdRomb);
			CommandBindings.Add(cmdTria);
			CommandBindings.Add(cmdPara);

			#endregion
		}

		#region L3

		private void OpenFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var ofd = new OpenFileDialog {
				Title = "Open from file...",
				InitialDirectory = @"C:\",
				Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*",
				FilterIndex = 1
			};
			if (ofd.ShowDialog() != true) return;
			MyGraphicalObjectList objectList;
			try {
				objectList = Deserialize(ofd.OpenFile());
			}
			catch(IOException e) {
				MessageBox.Show($"Ошибка открытия файла: \n{e}", "Error");
				return;
			}
			catch(InvalidOperationException e) {
				MessageBox.Show($"Ошибка в файле: \n{e}", "Error");
				return;
			}
			catch(Exception e) {
				MessageBox.Show($"Неизвестная ошибка: \n{e}", "Error");
				return;
			}
			CMain.Children.Clear();
			for (var i = 0; i < objectList.Count; i++) {
				objectList[i].CreateObject();
				CMain.Children.Add(objectList[i].Figure);
			}
			_list = objectList;
			LbObjects.ItemsSource = _list.ToList;
			MessageBox.Show("Seccess!", "Seccess!");
		}

		public static MyGraphicalObjectList Deserialize(Stream stream) {
			var s = new XmlSerializer(typeof(MyGraphicalObjectList));
			MyGraphicalObjectList objectList;
			using(Stream reader = stream) {
				objectList = (MyGraphicalObjectList) s.Deserialize(new XmlTextReader(reader));
			}
			return objectList;
		}

		private void SaveFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var sfd = new SaveFileDialog {
				Title = "Save to file...",
				InitialDirectory = @"C:\",
				Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*",
				FilterIndex = 1
			};

			if (sfd.ShowDialog() != true) return;
			new XmlSerializer(typeof(MyGraphicalObjectList))
				.Serialize(new XmlTextWriter(sfd.FileNames[0], Encoding.UTF8), _list);
			MessageBox.Show("Seccess!", "Seccess!");
		}

		#endregion

		#region General

		private void SelectAll(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			if (SelMode == SelectionMode.Single) {
				MessageBox.Show("Can't complete \"Select all\" with singleselect mode.", "Error");
				return;
			}
			LbObjects.SelectAll();
		}

		private void UnselectAll(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			LbObjects.UnselectAll();
		}

		private void DeleteSelected(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			foreach (MyGraphicalObject graphicalObject in LbObjects.SelectedItems) {
				CMain.Children.Remove(_list.GetHash(graphicalObject.Hash).Figure);
				_list.RemoveHash(graphicalObject.Hash);
			}
			LbObjects.ItemsSource = _list.ToList;
		}

		private void Edit(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			if (LbObjects.SelectedItems.Count == 0) return;
			var edt = new EditWindow {
				Figure = LbObjects.SelectedItems.Cast<MyGraphicalObject>().ToArray(),
				Owner = this
			};
			edt.ShowDialog();
			LbObjects.ItemsSource = _list.ToList;
			_result = edt.DialogResult == true;
		}

		private static void ExitApp(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			Application.Current.MainWindow.Close();
		}

		#endregion

		#region L2

		private void DrawCirc(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var circle = new MyCircle(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(circle);
			CMain.Children.Add(circle.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(circle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawElip(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var ellipse = new MyEllipse(0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(ellipse);
			CMain.Children.Add(ellipse.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(ellipse.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawLine(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var line = new MyLine(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(line);
			CMain.Children.Add(line.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(line.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawRect(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var rectangle = new MyRectangle(0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(rectangle);
			CMain.Children.Add(rectangle.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(rectangle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawSqua(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var square = new MySquare(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(square);
			CMain.Children.Add(square.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(square.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawRomb(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var rombus = new MyRombus(0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(rombus);
			CMain.Children.Add(rombus.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(rombus.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawTria(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var triangle = new MyTriangle(0, 0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(triangle);
			CMain.Children.Add(triangle.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(triangle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		private void DrawPara(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var parallelogram = new MyParallelogram(0, 0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			_list.Add(parallelogram);
			CMain.Children.Add(parallelogram.Figure);
			LbObjects.ItemsSource = _list.ToList;
			LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			Edit(sender, executedRoutedEventArgs);
			if (!_result) _list.RemoveHash(parallelogram.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList;
		}

		#endregion

		#endregion

		#region RoutedCommands

		// ReSharper disable MemberCanBePrivate.Global

		#region L3

		public static RoutedCommand Open { get; } = new RoutedCommand("Open",
			typeof(MainWindow));

		public static RoutedCommand Save { get; } = new RoutedCommand("Save",
			typeof(MainWindow));

		#endregion

		#region General

		public static RoutedCommand Exit { get; } = new RoutedCommand("Exit",
			typeof(MainWindow));

		public static RoutedCommand Select { get; } = new RoutedCommand("Select all",
			typeof(MainWindow));

		public static RoutedCommand Unselect { get; } = new RoutedCommand("Unselect all",
			typeof(MainWindow));

		public static RoutedCommand Delete { get; } = new RoutedCommand("Delete selected",
			typeof(MainWindow));

		public static RoutedCommand EditElem { get; } = new RoutedCommand("Edit selected",
			typeof(MainWindow));

		#endregion

		#region L2

		public static RoutedCommand Circle { get; } = new RoutedCommand("Draw Circle",
			typeof(MainWindow));

		public static RoutedCommand Ellipse { get; } = new RoutedCommand("Draw Ellipse",
			typeof(MainWindow));

		public static RoutedCommand Line { get; } = new RoutedCommand("Draw Line",
			typeof(MainWindow));

		public static RoutedCommand Rectangle { get; } = new RoutedCommand("Draw Rectangle",
			typeof(MainWindow));

		public static RoutedCommand Square { get; } = new RoutedCommand("Draw Square",
			typeof(MainWindow));

		public static RoutedCommand Rombus { get; } = new RoutedCommand("Draw Rombus",
			typeof(MainWindow));

		public static RoutedCommand Triangle { get; } = new RoutedCommand("Draw Ttiangle",
			typeof(MainWindow));

		public static RoutedCommand Parallelogram { get; } = new RoutedCommand("Draw Parallelogram",
			typeof(MainWindow));

		#endregion

		// ReSharper restore MemberCanBePrivate.Global

		#endregion
	}
}