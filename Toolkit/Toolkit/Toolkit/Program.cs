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
		private static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		static void Main(string[] args)
		{
			try
			{
				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					new Program().Main2(File.ReadAllLines(args[1], ENCODING_SJIS));
				}
				else
				{
					new Program().Main2(args);
				}
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
						// グラデーション
						/*
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
						*/
						//g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height); // 単色
						g.FillRectangle(Brushes.DarkRed, 0, 0, bmp.Width, bmp.Height); // 単色

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
				if (EqualsIgnoreCase(argq.Peek(), "/MULTI-RUN"))
				{
					argq.Dequeue();
					int mode = int.Parse(argq.Dequeue()); // (0, 1, 2) == (Hide, Min, Normal)
					List<string> progFiles = new List<string>();

					while (1 <= argq.Count)
						progFiles.Add(argq.Dequeue());

					List<Process> procs = new List<Process>();

					foreach (string progFile in progFiles)
					{
						ProcessStartInfo psi = new ProcessStartInfo();

						psi.FileName = progFile;
						psi.Arguments = "";

						switch (mode)
						{
							case 0: // Hide
								psi.CreateNoWindow = true;
								psi.UseShellExecute = false;
								break;

							case 1: // Min
								psi.CreateNoWindow = false;
								psi.UseShellExecute = true;
								psi.WindowStyle = ProcessWindowStyle.Minimized;
								break;

							case 2: // Normal
								break;

							default:
								throw null;
						}
						Console.WriteLine("1.1 " + progFile);
						procs.Add(Process.Start(psi));
						Console.WriteLine("1.2 " + procs[procs.Count - 1].Id);
					}
					foreach (Process proc in procs)
					{
						Console.WriteLine("2.1 " + proc.Id);
						proc.WaitForExit();
						Console.WriteLine("2.2");
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/SHA-512"))
				{
					argq.Dequeue();
					string path = argq.Dequeue();
					string wFile = argq.Dequeue();

					if (Directory.Exists(path))
					{
						string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
						Array.Sort<string>(files);
						OutputSHA512(files, wFile);
					}
					else if (File.Exists(path))
					{
						OutputSHA512(new string[] { path }, wFile);
					}
					else
					{
						throw new FileNotFoundException(path);
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

		private void OutputSHA512(string[] files, string wFile)
		{
			using (StreamWriter writer = new StreamWriter(wFile, false, ENCODING_SJIS))
			using (SHA512 sha512 = SHA512.Create())
			{
				foreach (string file in files)
				{
					using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
					{
						writer.WriteLine(BitConverter.ToString(sha512.ComputeHash(reader)).Replace("-", "").ToLower() + " " + file);
					}
				}
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
