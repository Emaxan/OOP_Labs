using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Serializer {
	public static class MySerializer {
		public static T Deserialize<T>(string path) where T: IList {
			using(Stream fileReader = new FileStream(path, FileMode.Open)) {
				using(var reader = new BsonReader(fileReader)) {
					reader.ReadRootValueAsArray = true;
					var serializer = JsonSerializer.Create(new JsonSerializerSettings {
						TypeNameHandling = TypeNameHandling.Objects,
						ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
					});
					//var list = serializer.Deserialize<T>(reader);
					//return list;
					var deleteTempString = 0;
					var mainString = "";
					var tempString = "";
					while(reader.Read()) {
						//s += "(" + reader.TokenType + ") " + reader.Value + " ";
						switch (reader.TokenType) {
							case JsonToken.None:
							case JsonToken.EndConstructor:
							case JsonToken.Date:
							case JsonToken.StartConstructor:
							case JsonToken.Comment:
							case JsonToken.Raw:
							case JsonToken.Null:
							case JsonToken.Undefined:
							case JsonToken.Bytes:
							case JsonToken.Boolean:
								//ignore
								break;
							case JsonToken.StartArray:
								mainString += "[";
								break;
							case JsonToken.StartObject:
								tempString += "{";
								break;
							case JsonToken.PropertyName:
								tempString += $"\"{reader.Value}\":";
								if ((string) reader.Value == "$type") {
									reader.Read();
									tempString += $"\"{reader.Value}\",";
									if (Type.GetType(reader.Value.ToString(),
										(aName) => new FileInfo($".\\ext\\Objects\\{aName.Name}.dll").Exists
														? Assembly.LoadFrom($".\\ext\\Objects\\{aName.Name}.dll")
														: null,
										(assem, name, ignore) => assem == null
																	? Type.GetType(name, false, ignore)
																	: assem.GetType(name, false, ignore)
											) == null) {
										deleteTempString = 1;
									}
								}
								break;
							case JsonToken.Integer:
								tempString += $"{reader.Value},";
								break;
							case JsonToken.Float:
								tempString += $"{reader.Value},";
								break;
							case JsonToken.String:
								tempString += $"\"{reader.Value}\",";
								break;
							case JsonToken.EndObject:
								if(deleteTempString == 0) {
									tempString = tempString.Substring(0, tempString.Length - 1) + "},";
									mainString += tempString;
								}
								deleteTempString = 0;
								tempString = "";
								break;
							case JsonToken.EndArray:
								mainString = mainString.Substring(0, mainString.Length - 1) + "]";
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
					var obj = serializer.Deserialize<T>(new JsonTextReader(new StringReader(mainString)));//JsonConvert.DeserializeObject<T>(mainString);
					return obj;
				}
			}
		}

		public static void Serialize<T>(string path, T list) {
			using(Stream fileWriter = new FileStream(path, FileMode.Create))
				using(var writer = new BsonWriter(fileWriter)) {
					var serializer = JsonSerializer.Create(new JsonSerializerSettings {
																						TypeNameHandling = TypeNameHandling.Objects,
																						ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
																					});
					serializer.Serialize(writer, list);
				}
		}
	}
}