using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTools
{
	public class StringTools
	{
		public const string DIGIT = "0123456789";
		public const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string alpha = "abcdefghijklmnopqrstuvwxyz";

		public static string ToFormat(string str)
		{
			str = Replace(str, DIGIT, '9');
			str = Replace(str, ALPHA, 'A');
			str = Replace(str, alpha, 'a');

			return str;
		}

		public static string Replace(string str, string fromChrs, char toChr)
		{
			foreach (char fromChr in fromChrs)
			{
				str = str.Replace(fromChr, toChr);
			}
			return str;
		}
	}
}
