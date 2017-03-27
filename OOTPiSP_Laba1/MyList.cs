using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OOTPiSP_Laba1 {
	[Serializable]
	[JsonArray]
	public class MyList<T>: ICollection<T> {
		public int MyCount;
		
		public T[] Objects = new T[0];

		public T this[int index] {
			get { return Objects[index]; }
			set { Objects[index] = value; }
		}

		public int Count {
			get { return Objects.Length; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public IEnumerable<T> ToList => Objects.ToList(); 

		public void Add(T obj) {
			Array.Resize(ref Objects, MyCount + 1);
			Objects[MyCount++] = obj;
		}

		public void Clear() { for(var i = 0; i < Objects.Length; i++) Remove(i); }
		public bool Contains(T item) { return Objects.Contains(item); }
		public void CopyTo(T[] array, int arrayIndex) { Objects.CopyTo(array, arrayIndex); }

		public bool Remove(T item) {
			RemoveHash(item.GetHashCode());
			return true;
		}

		public void RemoveHash(int hash) { for(var i = 0; i < MyCount; i++) if(Objects[i].GetHashCode() == hash) Remove(i); }

		public void Remove(int index) {
			for(var i = index; i < MyCount - 1; i++) Objects[i] = Objects[i + 1];
			Array.Resize(ref Objects, --MyCount);
		}

		public T GetHash(int hash) {
			return Objects.Where(_object => _object.GetHashCode() == hash).First();
		}

		public IEnumerator<T> GetEnumerator() { return (IEnumerator<T>) Objects.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return Objects.GetEnumerator(); }
	}
}