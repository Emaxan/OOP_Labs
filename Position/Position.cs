using System;
using Newtonsoft.Json;

namespace MyPosition
{
	[Serializable]
	public class Position {

		[JsonConstructor]
		public Position() {
		}

		public Position(int x, int y) {
			X = x;
			Y = y;
		}
		[JsonProperty]
		public int X { get; set; }
		[JsonProperty]
		public int Y { get; set; }

		public override string ToString() => $"{X}:{Y}";
	}
}
