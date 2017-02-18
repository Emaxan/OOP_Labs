using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OOTPiSP_Laba1 {
	public class MySerializer {
		public static T Deserialize<T>(string path) {
			using(Stream reader = new FileStream(path, FileMode.Open))
				return (T) new XmlSerializer(typeof(T)).Deserialize(new XmlTextReader(reader));
		}

		public static void Serialize<T>(string path, T list) {
			using(TextWriter writer = new StreamWriter(path))
				new XmlSerializer(typeof(T)).Serialize(new XmlTextWriter(writer), list);
		}
	}
}