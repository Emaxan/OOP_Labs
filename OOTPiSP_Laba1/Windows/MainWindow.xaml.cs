using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using GeneralObject;
using IEditableInt;
using ISelectableInt;
using Params;

namespace OOTPiSP_Laba1.Windows {
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		private readonly int _start;
		private MyList<MyGraphicalObject> _list = new MyList<MyGraphicalObject>();
		private bool _result;
		private List<GeneralFactory> Factories { get; set; }

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

		private void IncludeLibrary(string spath) {
			try {
				Thread.Sleep(50);
				Type type = Assembly.LoadFile(spath).GetExportedTypes()[0];
				Factories.Add(Activator.CreateInstance(type) as GeneralFactory);
				Dispatcher.BeginInvoke(new Action(() => {
					var path = new System.Windows.Shapes.Path {
						Stretch = Stretch.Uniform,
						Stroke = Brushes.Black,
						StrokeThickness = 2,
						Data = Geometry.Parse(type.GetField("PathData").GetValue(null) as string)
					};
					var button = new Button {
						Content = path,
						Style = (Style)BSelectAll.FindResource("ButtonStyle1")
					};
					button.Click += ((o, eventArgs) => {
						var b = o as Button;
						int id = DpTop.Children.IndexOf(b);
						var obj = Factories[id].CreateShape();
						obj.ObjectChanged += ObjectChanged;
						Create(ref obj);
						_list.Add(obj);
						CMain.Children.Add(obj.Figure);
						if (!_result)
							_list.RemoveHash(obj.Hash);
						LbObjects.ItemsSource = _list.ToList();
					});
					DpTop.Children.Add(button);
				}));
			}
			catch (Exception e) {
				MessageBox.Show(e.Message + '\n' + e.InnerException, "Error!");
			}
		}

		private void Window_loaded(object sender, RoutedEventArgs args) {
			Factories = new List<GeneralFactory>();
			var fileSystemWatcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory + @"ext\Objects\", "*.dll");
			fileSystemWatcher.Created += (x, y) => 
			{
				IncludeLibrary(y.FullPath);
			};
			fileSystemWatcher.EnableRaisingEvents = true;
			foreach(FileInfo file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"ext\Objects\").GetFiles("*.dll")) {
				IncludeLibrary(file.FullName);
			}
		}

		private void ObjectsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			foreach(MyGraphicalObject o in e.AddedItems) {
				var selectable = o as ISelectable;
				if(selectable != null) selectable.Select();
				else {
					MessageBox.Show("You can't select this object.");
				}
				o.Update();
			}
			foreach(MyGraphicalObject o in e.RemovedItems) {
				var selectable = o as ISelectable;
				if(selectable != null) selectable.Unselect();
				else MessageBox.Show("You can't unselect this object.");
				o.Update();
			}
			LbObjects.ItemsSource = _list.ToList;
		}

		private void CMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			_xStart = (int) e.MouseDevice.GetPosition(CMain).X;
			_yStart = (int) e.MouseDevice.GetPosition(CMain).Y;
		}

		private void MenSingleselect_Checked(object sender, RoutedEventArgs e) { SelMode = SelectionMode.Single; }

		private void MenMultiselect_Checked(object sender, RoutedEventArgs e) { SelMode = SelectionMode.Multiple; }

		private void MenExtendedselect_Checked(object sender, RoutedEventArgs e) { SelMode = SelectionMode.Extended; }

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

		private void Create(ref MyGraphicalObject obj) {
			var param = obj.GetParams();
			var page = new EditPage();
			page.WriteData(param);
			var edt = new EditWindow {
										Pages = new[] {page},
										Owner = this
									};
			edt.ShowDialog();
			LbObjects.ItemsSource = _list.ToList();
			_result = edt.DialogResult == true;
			if(!_result) return;
			page.ReadData(out param);
			obj.SetParams(param);
		}

		private void ObjectChanged(object sender, EventArgs e) {
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
		}

		#region L3

		private void OpenFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var s = AppDomain.CurrentDomain.BaseDirectory;
			s = s.Substring(0, s.LastIndexOf('\\')) + @"/ext/Mods/Serializer.dll";
			var f = new FileInfo(s);
			if(f.Exists) {
				Type type = Assembly.LoadFile(s).GetExportedTypes()[0];
				var ofd = new OpenFileDialog {
												Title = "Open from file...",
												InitialDirectory = @"C:\",
												Filter = "Bson files(*.bson)|*.bson|All files(*.*)|*.*",
												FilterIndex = 1
											};
				if(ofd.ShowDialog() != true) return;
				MyList<MyGraphicalObject> objectList;
				try {
					objectList = (MyList<MyGraphicalObject>)type.GetMethod("Deserialise")
						.MakeGenericMethod(typeof(MyList<MyGraphicalObject>))
						.Invoke(null, new object[] {ofd.FileNames[0]});
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
					objectList[i].Update();
					CMain.Children.Add(objectList[i].Figure);
				}
				_list = objectList;
				LbObjects.ItemsSource = _list.ToList();
				MessageBox.Show("Success!", "Success!");
				return;
			}
			MessageBox.Show("Serialiser.dll corrupted or not exist!", "Error!");
		}

		private void SaveFile(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			var s = AppDomain.CurrentDomain.BaseDirectory;
			s = s.Substring(0, s.LastIndexOf('\\')) + @"/ext/Mods/Serializer.dll";
			var f = new FileInfo(s);
			if(f.Exists) {
				Type type = Assembly.LoadFile(s).GetExportedTypes()[0];
				var sfd = new SaveFileDialog {
												Title = "Save to file...",
												InitialDirectory = @"C:\",
												Filter = "Bson files(*.bson)|*.bson|All files(*.*)|*.*",
												FilterIndex = 1
											};

				if(sfd.ShowDialog() != true) return;
				try {
					type.GetMethod("Serialise")
						.MakeGenericMethod(typeof(MyList<MyGraphicalObject>))
						.Invoke(null, new object[] { sfd.FileNames[0], _list });
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
				return;
			}
			MessageBox.Show("Serialiser.dll corrupted or not exist!", "Error!");
		}

		#endregion

		#region General

		private void SelectAll(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs) {
			if(SelMode == SelectionMode.Single) {
				MessageBox.Show("Can't complete \"Select all\" with single-select mode.", "Error");
				return;
			}
			var list = LbObjects.Items.Cast<MyGraphicalObject>().Where(go => go is ISelectable);
			foreach(var graphicalObject in list.Cast<ISelectable>()) {
				graphicalObject.Select();
			}
			ObjectChanged(sender, EventArgs.Empty);
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
			// ReSharper disable once SuspiciousTypeConversion.Global
			var figure = LbObjects.SelectedItems.Cast<MyGraphicalObject>().Where(go => go is IEditable).ToArray();
			if (figure.Length == 0) {
				MessageBox.Show("No elements to edit or they don't implement IEditable.");
				return;
			}
			var pages = new EditPage[figure.Length];
			for(var i = 0; i < figure.Length; i++) {
				MyParams param = figure[i].GetParams();
				pages[i] = new EditPage();
				pages[i].WriteData(param);
			}
			var edt = new EditWindow {
										Pages = pages,
										Owner = this
									};
			if(edt.ShowDialog() != true) return;
			for (var i = 0; i < figure.Length; i++) {
				MyParams param;
				pages[i].ReadData(out param);
				figure[i].SetParams(param);
			}
			LbObjects.ItemsSource = _list.ToList();
		}

		private static void ExitApp(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
			=> Application.Current.MainWindow.Close();

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
		
		#endregion
	}
}
