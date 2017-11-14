using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Toolkit
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string[] args)
		{
			Queue<string> argq = new Queue<string>(args);

			while (1 <= argq.Count)
			{
				if (EqualsIgnoreCase(argq.Peek(), "/MASK-RESOURCE-IMAGE"))
				{
					argq.Dequeue();
					string rFile = argq.Dequeue();
					string wFile = argq.Dequeue();
					string hashRFile = 1 <= argq.Count ? argq.Dequeue() : rFile;
					byte[] hash;

					using (SHA512 sha512 = SHA512.Create())
					using (FileStream fs = new FileStream(hashRFile, FileMode.Open, FileAccess.Read))
					{
						hash = sha512.ComputeHash(fs);
					}
					using (Image img = Image.FromFile(rFile))
					using (Bitmap bmp = new Bitmap(img))
					using (Graphics g = Graphics.FromImage(bmp))
					{
						g.FillRectangle(
							new LinearGradientBrush(
								new Point(0, 0),
								new Point(bmp.Width, bmp.Height),
								Color.FromArgb(255, 64, 32, 0),
								Color.FromArgb(255, 128, 64, 0)
								),
							0,
							0,
							bmp.Width,
							bmp.Height
							);
						//g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height); // 単色

						DrawCode(g, bmp.Width, bmp.Height, hash);
						bmp.Save(wFile);
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/IMG-TO-IMG"))
				{
					argq.Dequeue();
					string rFile = argq.Dequeue();
					string wFile = argq.Dequeue();
					int q = -1;

					if (1 <= argq.Count)
						q = int.Parse(argq.Dequeue()); // 0 ～ 100

					new ImgToImg().Perform(rFile, wFile, q);

					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/EVENT-LOG"))
				{
					argq.Dequeue();
					long dtMin = long.Parse(argq.Dequeue());
					long dtMax = long.Parse(argq.Dequeue());
					bool messageOn = int.Parse(argq.Dequeue()) != 0;
					string wFile = argq.Dequeue();

					using (CsvFileWriter writer = new CsvFileWriter(wFile))
					{
						foreach (var el in EventLog.GetEventLogs())
						{
							foreach (EventLogEntry ele in el.Entries)
							{
								long dt = ToLong(ele.TimeGenerated);

								if (dtMin <= dt && dt <= dtMax)
								{
									List<string> row = new List<string>();

									row.Add("" + dt); // 日付と時刻
									row.Add("" + GetLevel(ele.EntryType)); // レベル
									row.Add("" + ele.Source); // ソース

									if (messageOn)
										row.Add("" + ele.Message);

									writer.writeRow(row.ToArray());
								}
							}
						}
					}
					continue;
				}
				// ここへ追加..
				throw new Exception("不明なオプション：" + argq.Peek());
			}
		}

		private void DrawCode(Graphics g, int gw, int gh, byte[] hash)
		{
			int dh = Math.Min(10, gh);

			for (int i = 0; i / 8 < hash.Length && i < gw; i++)
			{
				bool bit = (hash[i / 8] & (1 << (i % 8))) != 0;

				if (bit)
					g.DrawLine(new Pen(Color.FromArgb(255, 255, 128, 0)), new Point(i, 0), new Point(i, dh));
			}
		}

		private bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		private long ToLong(DateTime dt)
		{
			int y = dt.Year;
			int m = dt.Month;
			int d = dt.Day;
			int h = dt.Hour;
			int i = dt.Minute;
			int s = dt.Second;

			return
				y * 10000000000L +
				m * 100000000L +
				d * 1000000L +
				h * 10000L +
				i * 100L +
				s;
		}

		private string GetLevel(EventLogEntryType type)
		{
			switch (type)
			{
				case EventLogEntryType.Error: return "エラー";
				case EventLogEntryType.FailureAudit: return "失敗の監査";
				case EventLogEntryType.Information: return "情報";
				case EventLogEntryType.SuccessAudit: return "成功の監査";
				case EventLogEntryType.Warning: return "警告";

				default:
					break;
			}
			return "" + (int)type;
		}
	}
}
