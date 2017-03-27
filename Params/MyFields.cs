using System;

namespace Params {
	[Flags]
	public enum MyFields {
		Name = 1,           //0x1
		Thickness = 2,      //0x2
		BorderColor = 4,    //0x4
		BgColor = 8,        //0x8
		GAngle = 16,        //0x10
		Position = 32,      //0x20
		RadiusX = 64,       //0x40
		RadiusY = 128,      //0x80
		Length1 = 256,      //0x100
		Length2 = 512,      //0x200
		Angle = 1024        //0x400
	}
}