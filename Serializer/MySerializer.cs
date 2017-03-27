using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Serializer {
	public static class MySerializer {
		public static T Deserialize<T>(string path) {
			using (Stream fileReader = new FileStream(path, FileMode.Open)) {
				using (var reader = new BsonReader(fileReader)) {

					reader.ReadRootValueAsArray = true;
					var serializer = JsonSerializer.Create(new JsonSerializerSettings {
						TypeNameHandling = TypeNameHandling.Objects,
						ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
					});
					var list = serializer.Deserialize<T>(reader);
					return list;
				}
			}
		}


		public static void Serialize<T>(string path, T list) {
			using (Stream fileWriter = new FileStream(path, FileMode.Create))
			using (var writer = new BsonWriter(fileWriter)) {
				var serializer = JsonSerializer.Create(new JsonSerializerSettings {
					TypeNameHandling = TypeNameHandling.Objects,
					ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
				});
				serializer.Serialize(writer, list);
			}
		}
	}
}