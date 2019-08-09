using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImgTools
{
	public static class FileTools
	{
		public static int GetExtPos(string path)
		{
			path = path.Replace('/', '\\'); // 2bs

			int extPos = path.LastIndexOf('.');

			if (extPos < path.LastIndexOf('\\'))
				extPos = -1;

			return extPos;
		}
	
		public static string UpdateSuffix(string path, string ext)
		{
			int extPos = GetExtPos(path);

			if (extPos != -1)
				path = path.Substring(0, extPos);

			if (StringTools.ToFormat(path).EndsWith("~9999"))
			{
				int count = int.Parse(path.Substring(path.Length - 4));
				count++;
				path = path.Substring(0, path.Length - 4) + count;
			}
			else
				path = path + "~1000";

			return path + ext;
		}

		public static string UpdateSuffixLoop(string path, string ext)
		{
			do
			{
				path = UpdateSuffix(path, ext);
			}
			while (File.Exists(path));

			return path;
		}

		public static void WriteFileTest(string file)
		{
			File.WriteAllText(file, "テスト", Encoding.UTF8);
		}

		public static void DeleteFile(string file)
		{
			File.Delete(file);
		}
	}
}
