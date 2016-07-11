using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BmpToCsv
{
	public class FileTools
	{
		public static string GetTmp()
		{
			string dir = Environment.GetEnvironmentVariable("TMP");

			if (string.IsNullOrWhiteSpace(dir))
			{
				dir = Environment.GetEnvironmentVariable("TEMP");

				if (string.IsNullOrWhiteSpace(dir))
					throw new Exception("環境変数 TMP, TEMP が定義されていません。");
			}
			return dir;
		}

		public static string GetTempPath()
		{
			return Path.Combine(GetTmp(), StringTools.GetUUID());
		}

		public static void DeleteFile(string file)
		{
			try
			{
				File.Delete(file);
			}
			catch
			{ }
		}

		public static void DeleteDir(string dir)
		{
			try
			{
				Directory.Delete(dir);
			}
			catch
			{ }
		}

		public static void Delete(string path)
		{
			DeleteFile(path);
			DeleteDir(path);
		}
	}
}
