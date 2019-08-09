using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class BinaryTools
	{
		public static Comparison<byte> comp = delegate(byte a, byte b)
		{
			return (int)a - (int)b;
		};
	}
}
