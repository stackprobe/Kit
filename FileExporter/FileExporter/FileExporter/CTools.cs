using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileExporter
{
	public class CTools
	{
		public static string ZPad(string str, int minlen)
		{
			while (str.Length < minlen)
			{
				str = "0" + str;
			}
			return str;
		}

		public static bool Exists(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}

		public static void CreateFile(string file)
		{
			using (new FileStream(file, FileMode.Create, FileAccess.Write))
			{ }
		}

		public static string ChangeSuffix(string str, string oldSuffix, string suffixNew)
		{
			if (str.Length < oldSuffix.Length)
				throw null;

			return str.Substring(0, str.Length - oldSuffix.Length) + suffixNew;
		}
	}
}
