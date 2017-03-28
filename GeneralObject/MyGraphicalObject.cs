using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ISelectableInt;
using MyPosition;
using Params;

namespace GeneralObject {
	public abstract class GeneralFactory {
		public abstract MyGraphicalObject CreateShape();
	}

	[Serializable]
	public abstract class MyGraphicalObject {
		private float _angleGlobal;
		private bool _isSelected;

		[NonSerialized]
		public UIElement Figure;

		/// <summary>
		///     Hash of the object
		/// </summary>
		public int Hash{ get; protected set; }

		/// <summary>
		///     Angle from start position
		/// </summary>
		public float AngleGlobal {
			get { return _angleGlobal; }
			set { _angleGlobal = value%360; }
		}

		/// <summary>
		///     Position of the object
		/// </summary>
		public Position Position{ get; set; }

		/// <summary>
		///     Background color
		/// </summary>
		public Color BgColor{ get; set; }

		/// <summary>
		///     Color of the object border
		/// </summary>
		public Color BorderColor{ get; set; }

		/// <summary>
		///     Thickness of the object border
		/// </summary>
		public int BorderThickness{ get; set; }

		/// <summary>
		///     Object name
		/// </summary>
		public string Name{ get; set; }

		/// <summary>
		///     Object standard name. Used instead of Name if user don't set it.
		/// </summary>
		protected abstract string StdName{ get; }

		/// <summary>
		///     True, if object is selected. Otherwise false.
		/// </summary>
		public bool IsSelectedProp {
			get { return _isSelected; }
			protected set {
				_isSelected = value;
				Update();
			}
		}

		public bool IsSelectable => this is ISelectable;

		public event EventHandler<MouseButtonEventArgs> ObjectChanged;

		/// <summary>
		///     Return parameters of the object
		/// </summary>
		/// <returns>Struct MyParam</returns>
		public abstract MyParams GetParams();

		/// <summary>
		///     Set parameters of the object
		/// </summary>
		public abstract void SetParams(MyParams param);

		/// <summary>
		///     Update graphical object with current object data
		/// </summary>
		public abstract void Update();

		public void UpdateHash() { Hash = GetHashCode(); }

		protected virtual void SelectableEvent(object sender, MouseButtonEventArgs args) {
			if((!IsSelectable))
				return;
			((ISelectable) this).Select();
			Update();
			ObjectChangedFunc(this, args);
		}

		public virtual void Select() { IsSelectedProp = true; }
		public virtual void Unselect() { IsSelectedProp = false; }

		protected void ObjectChangedFunc(object sender, MouseButtonEventArgs args) => ObjectChanged?.Invoke(sender, args);

		public override string ToString() => $"{(Name == ""? StdName : Name)} ({Position})";
	}
}
