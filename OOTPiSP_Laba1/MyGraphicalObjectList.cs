using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OOTPiSP_Laba1 {
	[Serializable]
	[JsonArray]
	public class MyGraphicalObjectList: ICollection<MyGraphicalObject> {
		public int MyCount;
		
		public MyGraphicalObject[] Objects = new MyGraphicalObject[0];

		public MyGraphicalObject this[int index] {
			get { return Objects[index]; }
			set { Objects[index] = value; }
		}

		public IEnumerable<MyGraphicalObject> ToList => Objects.ToList(); 

		public void Add(MyGraphicalObject obj) {
			Array.Resize(ref Objects, MyCount + 1);
			Objects[MyCount++] = obj;
		}

		public void Clear() { for(var i = 0; i < Objects.Length; i++) Remove(i); }
		public bool Contains(MyGraphicalObject item) => Objects.Contains(item);
		public void CopyTo(MyGraphicalObject[] array, int arrayIndex) => Objects.CopyTo(array, arrayIndex);

		public bool Remove(MyGraphicalObject item) {
			RemoveHash(item.Hash);
			return true;
		}

		public int Count => Objects.Length;

		public bool IsReadOnly => false;

		public void Remove(int index) {
			for(var i = index; i < MyCount - 1; i++) Objects[i] = Objects[i + 1];
			Array.Resize(ref Objects, --MyCount);
		}

		public void RemoveHash(int hash) { for(var i = 0; i < MyCount; i++) if(Objects[i].Hash == hash) Remove(i); }

		public MyGraphicalObject GetHash(int hash) {
			for(var i = 0; i < MyCount; i++) if(Objects[i].Hash == hash) return Objects[i];
			return null;
		}

		public IEnumerator<MyGraphicalObject> GetEnumerator() { return (IEnumerator<MyGraphicalObject>) Objects.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return Objects.GetEnumerator(); }
	}
}