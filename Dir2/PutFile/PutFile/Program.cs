using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PutFile
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			PutFile(args[0], args[1], GetDateTime(args[3]));
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

		private static void PutFile(string rFile, string wFile, DateTime wTime)
		{
			File.Delete(wFile);
			File.Move(rFile, wFile);
			File.SetLastWriteTime(wFile, wTime);
		}
	}
}
