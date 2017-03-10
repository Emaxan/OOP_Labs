using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using OOTPiSP_Laba1.Interfaces;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using SelectionMode = System.Windows.Controls.SelectionMode;

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
				if(_start != 1) return;
				switch(SelMode) {
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
			foreach(MyGraphicalObject o in e.AddedItems) {
				if(o is ISelectable) ((ISelectable) o).Select();
				else {
					MessageBox.Show("You can't select this object.");
				}
				o.Update();
			}
			foreach(MyGraphicalObject o in e.RemovedItems) {
				if(o is ISelectable) ((ISelectable)o).Unselect();
				else MessageBox.Show("You can't unselect this object.");
				o.Update();
			}
			LbObjects.ItemsSource = _list.ToList;
		}

		private void CMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			_xStart = (int) e.MouseDevice.GetPosition(CMain).X;
			_yStart = (int) e.MouseDevice.GetPosition(CMain).Y;
		}

		private void MenSingleselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Single;

		private void MenMultiselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Multiple;

		private void MenExtendedselect_Checked(object sender, RoutedEventArgs e) => SelMode = SelectionMode.Extended;

		private void MenSingleselect_OnClick(object sender, RoutedEventArgs e) {
			if(SelMode != SelectionMode.Single) SelMode = SelectionMode.Single;
		}

		private void MenMultiselect_OnClick(object sender, RoutedEventArgs e) {
			if(SelMode != SelectionMode.Multiple) SelMode = SelectionMode.Multiple;
		}

		private void MenExtendedselect_OnClick(object sender, RoutedEventArgs e) {
			if(SelMode != SelectionMode.Extended) SelMode = SelectionMode.Extended;
		}

		private void CMain_MouseMove(object sender, MouseEventArgs e) {
			var xEnd = (int) e.MouseDevice.GetPosition(CMain).X;
			var yEnd = (int) e.MouseDevice.GetPosition(CMain).Y;
			LMouseCoords.Content = (xEnd >= 0) && (yEnd >= 0) && (xEnd <= CMain.ActualWidth) && (yEnd <= CMain.ActualHeight)
										? $"Mouse coordinates: ({e.MouseDevice.GetPosition(CMain).X};{e.MouseDevice.GetPosition(CMain).Y})"
										: string.Empty;
			if(((_xStart < 0) || (_yStart < 0) || (_xStart > CMain.ActualWidth) || (_yStart > CMain.ActualHeight)) &&
				(xEnd >= 0) &&
				(yEnd >= 0) && (xEnd <= CMain.ActualWidth) && (yEnd <= CMain.ActualHeight)) {
				_xStart = xEnd;
				_yStart = yEnd;
			}

			if((e.LeftButton != MouseButtonState.Pressed) || (_xStart < 0) || (_yStart < 0) || (_xStart > CMain.ActualWidth) ||
				(_yStart > CMain.ActualHeight) || (xEnd < 0) || (yEnd < 0) || (xEnd > CMain.ActualWidth) ||
				(yEnd > CMain.ActualHeight)) return;
			foreach(var graphicalObject in from MyGraphicalObject go in LbObjects.Items where go.IsSelectedProp select go) {
				graphicalObject.Position.X += xEnd - _xStart;
				graphicalObject.Position.Y += yEnd - _yStart;
				graphicalObject.Update();
			}
			LbObjects.ItemsSource = _list.ToList();
			_xStart = xEnd;
			_yStart = yEnd;
		}

		private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
			switch(
				MessageBox.Show("Save changes?",
					"Warning",
					MessageBoxButton.YesNoCancel,
					MessageBoxImage.Warning,
					MessageBoxResult.Cancel,
					MessageBoxOptions.None)) {
						case MessageBoxResult.Cancel:
							e.Cancel = true;
							break;
						case MessageBoxResult.No:
							break;
						case MessageBoxResult.Yes:
							MenSave.Command.Execute(MenSave.CommandParameter);
							break;
						default:
							throw new ArgumentOutOfRangeException();
			}
		}

		private void Create(MyGraphicalObject obj) {
			var edt = new EditWindow {
				Figure = new[] {obj},
				Owner = this
			};
			edt.ShowDialog();
			LbObjects.ItemsSource = _list.ToList();
			_result = edt.DialogResult == true;
		}

		private void ObjectChanged(object sender, MouseButtonEventArgs e) {
			if(!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.LeftCtrl)) LbObjects.UnselectAll();
			LbObjects.ItemsSource = _list.ToList;
		}

		#region RoutedEvents

		private void SetHotKeys() {
			#region L3

			var open = new KeyGesture(Key.O, ModifierKeys.Control);
			MenOpen.InputGestureText = "Ctrl + O";
			MenOpen.Command = Open;
			Open.InputGestures.Add(open);
			var cmdOpen = new CommandBinding(Open, OpenFile);
			CommandBindings.Add(cmdOpen);

			var save = new KeyGesture(Key.S, ModifierKeys.Control);
			MenSave.InputGestureText = "Ctrl + S";
			MenSave.Command = Save;
			MenSave.CommandParameter = _list.ToList();
			Save.InputGestures.Add(save);
			var cmdSave = new CommandBinding(Save, SaveFile);
			CommandBindings.Add(cmdSave);

			#endregion

			#region General

			var exit = new KeyGesture(Key.X, ModifierKeys.Control);
			MenExit.InputGestureText = "Ctrl + X";
			MenExit.Command = Exit;
			Exit.InputGestures.Add(exit);
			var cmdExit = new CommandBinding(Exit, ExitApp);
			CommandBindings.Add(cmdExit);

			var select = new KeyGesture(Key.A, ModifierKeys.Control);
			MenSelect.InputGestureText = "Ctrl + A";
			MenSelect.Command = Select;
			Select.InputGestures.Add(select);
			var cmdSelect = new CommandBinding(Select, SelectAll);
			CommandBindings.Add(cmdSelect);

			var unselect = new KeyGesture(Key.U, ModifierKeys.Control);
			MenUnselect.InputGestureText = "Ctrl + U";
			MenUnselect.Command = Unselect;
			Unselect.InputGestures.Add(unselect);
			var cmdUnselect = new CommandBinding(Unselect, UnselectAll);
			CommandBindings.Add(cmdUnselect);

			var deleteSel = new KeyGesture(Key.Delete);
			MenDeleteSel.InputGestureText = "Delete";
			MenDeleteSel.Command = Delete;
			Delete.InputGestures.Add(deleteSel);
			var cmdDelete = new CommandBinding(Delete, DeleteSelected);
			CommandBindings.Add(cmdDelete);

			var edit = new KeyGesture(Key.I, ModifierKeys.Control);
			MenEdit.InputGestureText = "Ctrl + I";
			MenEdit.Command = EditElem;
			EditElem.InputGestures.Add(edit);
			var cmdEdit = new CommandBinding(EditElem, Edit);
			CommandBindings.Add(cmdEdit);

			#endregion

			#region L2

			var circle = new KeyGesture(Key.C, ModifierKeys.Control);
			MenCircle.InputGestureText = "Ctrl + C";
			MenCircle.Command = Circle;
			Circle.InputGestures.Add(circle);
			var cmdCirc = new CommandBinding(Circle, DrawCirc);
			CommandBindings.Add(cmdCirc);

			var ellipse = new KeyGesture(Key.E, ModifierKeys.Control);
			MenEllipse.InputGestureText = "Ctrl + E";
			MenEllipse.Command = Ellipse;
			Ellipse.InputGestures.Add(ellipse);
			var cmdElip = new CommandBinding(Ellipse, DrawElip);
			CommandBindings.Add(cmdElip);

			var rectangle = new KeyGesture(Key.R, ModifierKeys.Control);
			MenRectangle.InputGestureText = "Ctrl + R";
			MenRectangle.Command = Rectangle;
			Rectangle.InputGestures.Add(rectangle);
			var cmdRect = new CommandBinding(Rectangle, DrawRect);
			CommandBindings.Add(cmdRect);

			var square = new KeyGesture(Key.Q, ModifierKeys.Control);
			MenSquare.InputGestureText = "Ctrl + Q";
			MenSquare.Command = Square;
			Square.InputGestures.Add(square);
			var cmdSqua = new CommandBinding(Square, DrawSqua);
			CommandBindings.Add(cmdSqua);

			var rombus = new KeyGesture(Key.M, ModifierKeys.Control);
			MenRombus.InputGestureText = "Ctrl + M";
			MenRombus.Command = Rombus;
			Rombus.InputGestures.Add(rombus);
			var cmdRomb = new CommandBinding(Rombus, DrawRomb);
			CommandBindings.Add(cmdRomb);

			var triangle = new KeyGesture(Key.T, ModifierKeys.Control);
			MenTriangle.InputGestureText = "Ctrl + T";
			MenTriangle.Command = Triangle;
			Triangle.InputGestures.Add(triangle);
			var cmdTria = new CommandBinding(Triangle, DrawTria);
			CommandBindings.Add(cmdTria);

			var parallelogram = new KeyGesture(Key.P, ModifierKeys.Control);
			MenParallelogram.InputGestureText = "Ctrl + P";
			MenParallelogram.Command = Parallelogram;
			Parallelogram.InputGestures.Add(parallelogram);
			var cmdPara = new CommandBinding(Parallelogram, DrawPara);
			CommandBindings.Add(cmdPara);

			var line = new KeyGesture(Key.L, ModifierKeys.Control);
			MenLine.InputGestureText = "Ctrl + L";
			MenLine.Command = Line;
			Line.InputGestures.Add(line);
			var cmdLine = new CommandBinding(Line, DrawLine);
			CommandBindings.Add(cmdLine);

			#endregion
		}

		#region L3

		private void OpenFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var ofd = new OpenFileDialog {
											Title = "Open from file...",
											InitialDirectory = @"C:\",
											Filter = "Bson files(*.bson)|*.bson|All files(*.*)|*.*",
											FilterIndex = 1
										};
			if(ofd.ShowDialog() != true) return;
			MyGraphicalObjectList objectList;
			try {
				objectList = MySerializer.Deserialize<MyGraphicalObjectList>(ofd.FileNames[0]);
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
			for(var i = 0; i < objectList.MyCount; i++) {
				objectList[i].CreateObject();
				CMain.Children.Add(objectList[i].Figure);
			}
			_list = objectList;
			LbObjects.ItemsSource = _list.ToList();
			MessageBox.Show("Success!", "Success!");
		}

		private void SaveFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var sfd = new SaveFileDialog {
											Title = "Save to file...",
											InitialDirectory = @"C:\",
											Filter = "Bson files(*.bson)|*.bson|All files(*.*)|*.*",
											FilterIndex = 1
										};

			if(sfd.ShowDialog() != true) return;
			try {
				MySerializer.Serialize(sfd.FileNames[0], _list);
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
			MessageBox.Show("Success!", "Success!");
		}

		#endregion

		#region General

		private void SelectAll(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			if(SelMode == SelectionMode.Single) {
				MessageBox.Show("Can't complete \"Select all\" with single-select mode.", "Error");
				return;
			}
			LbObjects.SelectAll();
		}

		private void UnselectAll(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) => LbObjects.UnselectAll();

		private void DeleteSelected(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			foreach(MyGraphicalObject graphicalObject in LbObjects.SelectedItems) {
				CMain.Children.Remove(_list.GetHash(graphicalObject.Hash).Figure);
				_list.RemoveHash(graphicalObject.Hash);
			}
			LbObjects.ItemsSource = _list.ToList();
		}

		private void Edit(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			if(LbObjects.SelectedItems.Count == 0) return;
			var edt = new EditWindow {
										Figure = LbObjects.SelectedItems.Cast<MyGraphicalObject>().ToArray(),
										Owner = this
									};
			edt.ShowDialog();
			LbObjects.ItemsSource = _list.ToList();
			_result = edt.DialogResult == true;
		}

		private static void ExitApp(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
			=> Application.Current.MainWindow.Close();

		#endregion

		#region L2

		private void DrawCirc(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) { 
		var circle = MyCircle.CreateFigure(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			circle.ObjectChanged += ObjectChanged;
			_list.Add(circle);
			CMain.Children.Add(circle.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if(!_result) _list.RemoveHash(circle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawElip(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var ellipse = MyEllipse.CreateFigure(0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			ellipse.ObjectChanged += ObjectChanged;
			_list.Add(ellipse);
			CMain.Children.Add(ellipse.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(ellipse.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawLine(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var line = MyLine.CreateFigure(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			line.ObjectChanged += ObjectChanged;
			_list.Add(line);
			CMain.Children.Add(line.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(line.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}
		
		private void DrawRect(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var rectangle = MyRectangle.CreateFigure(0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			rectangle.ObjectChanged += ObjectChanged;
			_list.Add(rectangle);
			CMain.Children.Add(rectangle.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(rectangle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawSqua(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var square = MySquare.CreateFigure(0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			square.ObjectChanged += ObjectChanged;
			_list.Add(square);
			CMain.Children.Add(square.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(square.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawRomb(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var rombus = MyRombus.CreateFigure(0, 0, 0, 0f, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			rombus.ObjectChanged += ObjectChanged;
			_list.Add(rombus);
			CMain.Children.Add(rombus.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(rombus.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawTria(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var triangle = MyTriangle.CreateFigure(0, 0, 0, 0, 0f, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0f);
			triangle.ObjectChanged += ObjectChanged;
			_list.Add(triangle);
			CMain.Children.Add(triangle.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(triangle.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		private void DrawPara(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var parallelogram = MyParallelogram.CreateFigure(0, 0, 0, 0, 0, Color.FromArgb(0, 0, 0, 0), Colors.Black, 1, 0);
			parallelogram.ObjectChanged += ObjectChanged;
			_list.Add(parallelogram);
			CMain.Children.Add(parallelogram.Figure);
			LbObjects.ItemsSource = _list.ToList();
			//LbObjects.SelectedIndex = LbObjects.Items.Count - 1;
			//Edit(sender, executedRoutedEventArgs);
			Create(_list.Objects.Last());
			if (!_result) _list.RemoveHash(parallelogram.Hash);
			UnselectAll(sender, executedRoutedEventArgs);
			LbObjects.ItemsSource = _list.ToList();
		}

		#endregion

		#endregion

		#region RoutedCommands

		#region L3

		public static RoutedCommand Open{ get; } = new RoutedCommand("Open", typeof(MainWindow));

		public static RoutedCommand Save{ get; } = new RoutedCommand("Save", typeof(MainWindow));

		#endregion

		#region General

		public static RoutedCommand Exit{ get; } = new RoutedCommand("Exit", typeof(MainWindow));

		public static RoutedCommand Select{ get; } = new RoutedCommand("Select all", typeof(MainWindow));

		public static RoutedCommand Unselect{ get; } = new RoutedCommand("Unselect all", typeof(MainWindow));

		public static RoutedCommand Delete{ get; } = new RoutedCommand("Delete selected", typeof(MainWindow));

		public static RoutedCommand EditElem{ get; } = new RoutedCommand("Edit selected", typeof(MainWindow));

		#endregion

		#region L2

		public static RoutedCommand Circle{ get; } = new RoutedCommand("Draw Circle", typeof(MainWindow));

		public static RoutedCommand Ellipse{ get; } = new RoutedCommand("Draw Ellipse", typeof(MainWindow));

		public static RoutedCommand Line{ get; } = new RoutedCommand("Draw Line", typeof(MainWindow));

		public static RoutedCommand Rectangle{ get; } = new RoutedCommand("Draw Rectangle", typeof(MainWindow));

		public static RoutedCommand Square{ get; } = new RoutedCommand("Draw Square", typeof(MainWindow));

		public static RoutedCommand Rombus{ get; } = new RoutedCommand("Draw Rombus", typeof(MainWindow));

		public static RoutedCommand Triangle{ get; } = new RoutedCommand("Draw Triangle", typeof(MainWindow));

		public static RoutedCommand Parallelogram{ get; } = new RoutedCommand("Draw Parallelogram", typeof(MainWindow));

		#endregion

		#endregion
	}
}
