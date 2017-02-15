using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace OOTPiSP_Laba1 {
	[Serializable]
	public class MyGraphicalObjectList {
		public int Count;

		[XmlArray] [XmlArrayItem(Type = typeof(MyLine)),
		            XmlArrayItem(Type = typeof(MyCircle)),
		            XmlArrayItem(Type = typeof(MyEllipse)),
		            XmlArrayItem(Type = typeof(MyRombus)),
		            XmlArrayItem(Type = typeof(MyRectangle)),
		            XmlArrayItem(Type = typeof(MyTriangle)),
		            XmlArrayItem(Type = typeof(MyParallelogram)),
		            XmlArrayItem(Type = typeof(MySquare))] public MyGraphicalObject[] Objects = new MyGraphicalObject[0];

		public MyGraphicalObject this[int index] {
			get { return Objects[index]; }
			set { Objects[index] = value; }
		}

		public IEnumerable<MyGraphicalObject> ToList => Objects.ToList();

		public void Add(MyGraphicalObject obj) {
			Array.Resize(ref Objects, Count + 1);
			Objects[Count++] = obj;
		}

		public void Remove(int index) {
			for (var i = index; i < Count - 1; i++) Objects[i] = Objects[i + 1];
			Array.Resize(ref Objects, --Count);
		}

		public void RemoveHash(int hash) {
			for (var i = 0; i < Count; i++) if (Objects[i].Hash == hash) Remove(i);
		}

		public MyGraphicalObject GetHash(int hash) {
			for (var i = 0; i < Count; i++) if (Objects[i].Hash == hash) return Objects[i];
			return null;
		}
	}
}