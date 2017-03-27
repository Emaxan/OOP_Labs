using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ISelectableInt;
using MyPosition;
using Newtonsoft.Json;
using Params;

namespace GeneralObject {
	public abstract class GeneralFactory {
		public abstract MyGraphicalObject CreateShape();
	}

	[Serializable]
	public abstract class MyGraphicalObject/*:ISelectable*/ {

		public event EventHandler<MouseButtonEventArgs> ObjectChanged;

		[JsonConstructor]
		protected MyGraphicalObject(int x, int y, Color bgColor, Color borderColor, int borderThickness, float angleGlobal) {
			Position = new Position(x, y);
			BgColor = bgColor;
			BorderColor = borderColor;
			AngleGlobal = angleGlobal % 360;
			BorderThickness = borderThickness;
		}

		[NonSerialized]
		public UIElement Figure;
		
		private bool _isSelected;

		/// <summary>
		/// Hash of the object
		/// </summary>
		public int Hash { get; protected set; }

		/// <summary>
		/// Angle from start position
		/// </summary>
		public float AngleGlobal { get; set; }

		/// <summary>
		/// Position of the object
		/// </summary>
		public Position Position { get; set; }

		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor { get; set; }

		/// <summary>
		/// Color of the object border
		/// </summary>
		public Color BorderColor { get; set; }

		/// <summary>
		/// Thickness of the object border
		/// </summary>
		public int BorderThickness { get; set; }

		/// <summary>
		/// Object name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Object standard name. Used instead of Name if user don't set it.
		/// </summary>
		protected abstract string StdName { get; }

		/// <summary>
		/// True, if object is selected. Otherwise false.
		/// </summary>
		public bool IsSelectedProp {
			get { return _isSelected; }
			protected set {
				_isSelected = value;
				Update();
			}
		}
		
		/// <summary>
		/// Return parameters of the object
		/// </summary>
		/// <returns>Struct MyParam</returns>
		public abstract MyParams GetParams();

		/// <summary>
		/// Set parameters of the object
		/// </summary>
		public abstract void SetParams(MyParams param);

		/// <summary>
		/// Create figure by object data
		/// </summary>
		public abstract void CreateObject();

		/// <summary>
		/// Update graphical object with current object data
		/// </summary>
		public abstract void Update();

		protected virtual void SelectableEvent(object sender, MouseButtonEventArgs args) {
			if ((!(this is ISelectable)))
				return;
			((ISelectable)this).Select();
			Update();
			ObjectChangedFunc(this, args);
		}
		public virtual void Select() { IsSelectedProp = true; }
		public virtual void Unselect() { IsSelectedProp = false; }

		protected void ObjectChangedFunc(object sender, MouseButtonEventArgs args) => ObjectChanged?.Invoke(sender, args);

		public bool IsSelectable => this is ISelectable;

		public override string ToString() => $"{(Name == "" ? StdName : Name)} ({Position})";
	}
}