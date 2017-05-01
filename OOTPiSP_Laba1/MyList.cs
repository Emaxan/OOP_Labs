using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Params;

namespace OOTPiSP_Laba1 {
	[Serializable]
	[JsonArray]
	public class MyList<T>: IList {
		private int _myCount;

		public T[] Objects = new T[0];

		public T this[int index] {
			get { return Objects[index]; }
			set { Objects[index] = value; }
		}

		public IEnumerable<T> ToList => Objects.ToList();

		public int Count {
			get { return Objects.Length; }
		}

		public object SyncRoot {
			get { return Objects.SyncRoot; }
		}

		public bool IsSynchronized {
			get { return Objects.IsSynchronized; }
		}

		object IList.this[int index] {
			get { return Objects[index]; }
			set { Objects[index] = (T) value; }
		}

		public void CopyTo(Array array, int arrayIndex) { Objects.CopyTo(array, arrayIndex); }
		IEnumerator IEnumerable.GetEnumerator() { return Objects.GetEnumerator(); }

		void IList.RemoveAt(int index) { Remove(index); }

		bool IList.IsReadOnly {
			get { return false; }
		}

		bool IList.IsFixedSize {
			get { return false; }
		}

		int IList.Add(object value) { return Add((T) value); }

		bool IList.Contains(object value) {
			for(var i = 0; i < _myCount; i++) {
				if(Objects[i].Equals((T) value)) return true;
			}
			return false;
		}

		public int Add(T obj) {
			Array.Resize(ref Objects, _myCount + 1);
			Objects[_myCount++] = obj;
			return _myCount - 1;
		}

		public void Clear() { for(var i = 0; i < Objects.Length; i++) Remove(i); }

		int IList.IndexOf(object value) {
			for(var i = 0; i < _myCount; i++) {
				if(Objects[i].Equals((T) value)) return i;
			}
			return -1;
		}

		void IList.Insert(int index, object value) {
			Array.Resize(ref Objects, _myCount + 1);
			for(var i = _myCount; i >= index; i--) {
				Objects[i + 1] = Objects[i];
			}
			Objects[index] = (T) value;
		}

		void IList.Remove(object value) { Remove((T) value); }

		public void Remove(T item) { RemoveHash(item.GetHashCode()); }

		public void RemoveHash(int hash) {
			for(var i = 0; i < _myCount; i++)
				if(Objects[i].GetHashCode() == hash)
					Remove(i);
		}

		public void Remove(int index) {
			for(var i = index; i < _myCount - 1; i++) Objects[i] = Objects[i + 1];
			Array.Resize(ref Objects, --_myCount);
		}

		public T GetHash(int hash) { return Objects.Where(_object => _object.GetHashCode() == hash).First(); }
	}
}
