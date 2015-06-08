using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgToCsv
{
	public class StringTools
	{
		private static readonly string _hex = "0123456789abcdef";

		public static void AddHex(StringBuilder buff, byte chr)
		{
			buff.Append(_hex[chr / 16]);
			buff.Append(_hex[chr % 16]);
		}

		public static int ReadHex(ArrayTools.Reader<char> buff)
		{
			int h = _hex.IndexOf(char.ToLower(buff.Next()));
			int l = _hex.IndexOf(char.ToLower(buff.Next()));

			return h * 16 + l;
		}

		public static string GetUUID()
		{
			return Guid.NewGuid().ToString("B");
		}
	}
}
