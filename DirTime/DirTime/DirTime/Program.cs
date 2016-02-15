using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DirTime
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string path = null;
				string cTime = null;
				string wTime = null;
				string aTime = null;
				int argi = 0;

				for (; ; )
				{
					if (args[argi] == "/-")
					{
						argi++;
						break;
					}
					if (args[argi].ToUpper() == "/C")
					{
						argi++;
						cTime = args[argi++];
						continue;
					}
					if (args[argi].ToUpper() == "/W")
					{
						argi++;
						wTime = args[argi++];
						continue;
					}
					if (args[argi].ToUpper() == "/A")
					{
						argi++;
						aTime = args[argi++];
						continue;
					}
					break;
				}
				path = args[argi++];

				if (argi < args.Length)
					cTime = args[argi++];

				if (argi < args.Length)
					wTime = args[argi++];

				if (argi < args.Length)
					aTime = args[argi++];

				if (Directory.Exists(path))
				{
					if (cTime == null && wTime == null && aTime == null)
					{
						PrintDateTime(
							Directory.GetCreationTime(path),
							Directory.GetLastWriteTime(path),
							Directory.GetLastAccessTime(path)
							);
					}
					else
					{
						if (cTime != null)
							Directory.SetCreationTime(path, GetDateTime(cTime));

						if (wTime != null)
							Directory.SetLastWriteTime(path, GetDateTime(wTime));

						if (aTime != null)
							Directory.SetLastAccessTime(path, GetDateTime(aTime));
					}
				}
				else if (File.Exists(path))
				{
					if (cTime == null && wTime == null && aTime == null)
					{
						PrintDateTime(
							File.GetCreationTime(path),
							File.GetLastWriteTime(path),
							File.GetLastAccessTime(path)
							);
					}
					else
					{
						if (cTime != null)
							File.SetCreationTime(path, GetDateTime(cTime));

						if (wTime != null)
							File.SetLastWriteTime(path, GetDateTime(wTime));

						if (aTime != null)
							File.SetLastAccessTime(path, GetDateTime(aTime));
					}
				}
				else
				{
					throw new Exception("存在しないパス: " + path);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void PrintDateTime(DateTime cTime, DateTime wTime, DateTime aTime)
		{
			Console.WriteLine(ToString(cTime));
			Console.WriteLine(ToString(wTime));
			Console.WriteLine(ToString(aTime));
		}

		private static string ToString(DateTime dt)
		{
			return
				ZPad(dt.Year, 4) +
				ZPad(dt.Month, 2) +
				ZPad(dt.Day, 2) +
				ZPad(dt.Hour, 2) +
				ZPad(dt.Minute, 2) +
				ZPad(dt.Second, 2) +
				ZPad(dt.Millisecond, 3);
		}

		private static string ZPad(int value, int minlen)
		{
			string ret = "" + value;

			while (ret.Length < minlen)
				ret = "0" + ret;

			return ret;
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
	}
}
