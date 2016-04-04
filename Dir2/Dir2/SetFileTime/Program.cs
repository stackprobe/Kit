using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SetFileTime
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			Main3(args[0], GetDateTime(args[1]), args[2]);
		}

		private static void Main3(string wFile, DateTime wTime, string successfulFile)
		{
			SetLastWriteTime(wFile, wTime);
			CreateFile(successfulFile);
		}

		private static DateTime GetDateTime(string sTime)
		{
			long time = long.Parse(sTime);

			int l = (int)(time % 1000);
			time /= 1000;
			int s = (int)(time % 100);
			time /= 100;
			int i = (int)(time % 100);
			time /= 100;
			int h = (int)(time % 100);
			time /= 100;
			int d = (int)(time % 100);
			time /= 100;
			int m = (int)(time % 100);
			time /= 100;
			int y = (int)time;

			if (
				y < 1 || 9999 < y ||
				m < 1 || 12 < m ||
				d < 1 || 31 < d ||
				h < 0 || 23 < h ||
				i < 0 || 59 < i ||
				s < 0 || 59 < s ||
				l < 0 || 999 < l
				)
				throw new Exception(
					"不正な日時フォーマット: " +
					y + ", " +
					m + ", " +
					d + ", " +
					h + ", " +
					i + ", " +
					s + ", " +
					l + ", " +
					sTime
					);

			return new DateTime(y, m, d, h, i, s, l);
		}

		private static void SetLastWriteTime(string wFile, DateTime wTime)
		{
			new FileInfo(wFile).LastWriteTime = wTime;
		}

		private static void CreateFile(string wFile)
		{
			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{ }
		}
	}
}
