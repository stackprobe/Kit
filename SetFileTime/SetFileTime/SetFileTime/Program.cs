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
				string path = null;
				string cTime = null;
				string wTime = null;
				string aTime = null;
				int argi = 0;

				for (; ; )
				{
					if (args[argi] == "/-")
						break;

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
					if (cTime != null)
						Directory.SetCreationTime(path, GetDateTime(cTime));

					if (wTime != null)
						Directory.SetLastWriteTime(path, GetDateTime(wTime));

					if (aTime != null)
						Directory.SetLastAccessTime(path, GetDateTime(aTime));
				}
				else if (File.Exists(path))
				{
					if (cTime != null)
						File.SetCreationTime(path, GetDateTime(cTime));

					if (wTime != null)
						File.SetLastWriteTime(path, GetDateTime(wTime));

					if (aTime != null)
						File.SetLastAccessTime(path, GetDateTime(aTime));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static DateTime GetDateTime(string sTime)
		{
			long time = long.Parse(sTime);

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
				y < 1970 || 3000 < y ||
				m < 1 || 12 < m ||
				d < 1 || 31 < d ||
				h < 0 || 23 < h ||
				i < 0 || 59 < i ||
				s < 0 || 59 < s
				)
				throw new Exception(
					"Unknown date-time format: " +
					y + ", " +
					m + ", " +
					d + ", " +
					h + ", " +
					i + ", " +
					s + ", " +
					sTime
					);

			return new DateTime(y, m, d, h, i, s);
		}
	}
}
