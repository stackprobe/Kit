using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.ServiceProcess;
using System.Management;

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
				if (EqualsIgnoreCase(argq.Peek(), "/MASK-RESOURCE-IMAGE-NBC"))
				{
					argq.Dequeue();
					string rFile = argq.Dequeue();
					string wFile = argq.Dequeue();

					using (Image img = Image.FromFile(rFile))
					using (Bitmap bmp = new Bitmap(img))
					using (Graphics g = Graphics.FromImage(bmp))
					{
						g.FillRectangle(Brushes.DarkRed, 0, 0, bmp.Width, bmp.Height); // 単色

						bmp.Save(wFile);
					}
					continue;
				}
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

									writer.WriteRow(row.ToArray());
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
				if (EqualsIgnoreCase(argq.Peek(), "/MD5"))
				{
					using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
					{
						OutputHashes(argq, (FileStream reader) => md5.ComputeHash(reader));
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/SHA-512"))
				{
					using (SHA512 sha512 = SHA512.Create())
					{
						OutputHashes(argq, (FileStream reader) => sha512.ComputeHash(reader));
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/SHA-512-128"))
				{
					using (SHA512 sha512 = SHA512.Create())
					{
						OutputHashes(argq, (FileStream reader) =>
						{
							byte[] hash = sha512.ComputeHash(reader);
							byte[] hash16 = new byte[16];

							Array.Copy(hash, hash16, 16);

							return hash16;
						});
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/SERVICES"))
				{
					argq.Dequeue();
					string wFile = argq.Dequeue();

					ServiceName_StartMode snsm = new ServiceName_StartMode();

					using (CsvFileWriter writer = new CsvFileWriter(wFile))
					{
						foreach (ServiceController sc in ServiceController.GetServices())
						{
							writer.WriteCell("" + sc.ServiceName);
							writer.WriteCell("" + sc.DisplayName);
							writer.WriteCell(ToString(sc.ServiceType));
							writer.WriteCell(ToString(sc.Status));
							writer.WriteCell("" + (sc.CanPauseAndContinue ? 1 : 0));
							writer.WriteCell("" + (sc.CanShutdown ? 1 : 0));
							writer.WriteCell("" + (sc.CanStop ? 1 : 0));
							writer.WriteCell(snsm.GetStartMode(sc.ServiceName));
							writer.EndRow();
						}
					}
					continue;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/SERVICE-COMMAND"))
				{
					argq.Dequeue();
					string command = argq.Dequeue();
					string targetServiceName = argq.Dequeue();

					ServiceController[] scs = ServiceController.GetServices().Where(value => value.ServiceName == targetServiceName).ToArray();

					if (scs.Length != 1)
						throw new Exception("サービスを１つに絞れません。" + scs.Length);

					ServiceController sc = scs[0];
					TimeSpan timeout = TimeSpan.FromMinutes(3.0);

					if (EqualsIgnoreCase(command, "START"))
					{
						sc.Start();
						sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
					}
					else if (EqualsIgnoreCase(command, "STOP"))
					{
						sc.Stop();
						sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					}
					else if (EqualsIgnoreCase(command, "PAUSE"))
					{
						sc.Pause();
						sc.WaitForStatus(ServiceControllerStatus.Paused, timeout);
					}
					else if (EqualsIgnoreCase(command, "CONTINUE"))
					{
						sc.Continue();
						sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
					}
					else if (EqualsIgnoreCase(command, "AUTO-DELAY"))
					{
						//ServiceName_StartMode.SetStartMode(targetServiceName, "Auto Start");
					}
					else if (EqualsIgnoreCase(command, "AUTO"))
					{
						//ServiceName_StartMode.SetStartMode(targetServiceName, "Auto Start");
					}
					else if (EqualsIgnoreCase(command, "MANUAL"))
					{
						//ServiceName_StartMode.SetStartMode(targetServiceName, "Demand Start");
					}
					else if (EqualsIgnoreCase(command, "DISABLED"))
					{
						//ServiceName_StartMode.SetStartMode(targetServiceName, "Disabled");
					}
					else
					{
						throw new Exception("不明なコマンド：" + command);
					}

					continue;
				}
				// ここへ追加..
				throw new Exception("不明なオプション：" + argq.Peek());
			}
		}

		private string ToString(ServiceControllerStatus value)
		{
			switch (value)
			{
				case ServiceControllerStatus.Stopped: return "Stopped";
				case ServiceControllerStatus.StartPending: return "StartPending";
				case ServiceControllerStatus.StopPending: return "StopPending";
				case ServiceControllerStatus.Running: return "Running";
				case ServiceControllerStatus.ContinuePending: return "ContinuePending";
				case ServiceControllerStatus.PausePending: return "PausePending";
				case ServiceControllerStatus.Paused: return "Paused";
			}
			return "" + (int)value;
		}

		private string ToString(ServiceType value)
		{
			List<string> dest = new List<string>();

			if (((int)value & (int)ServiceType.KernelDriver) != 0) dest.Add("KernelDriver");
			if (((int)value & (int)ServiceType.FileSystemDriver) != 0) dest.Add("FileSystemDriver");
			if (((int)value & (int)ServiceType.Adapter) != 0) dest.Add("Adapter");
			if (((int)value & (int)ServiceType.RecognizerDriver) != 0) dest.Add("RecognizerDriver");
			if (((int)value & (int)ServiceType.Win32OwnProcess) != 0) dest.Add("Win32OwnProcess");
			if (((int)value & (int)ServiceType.Win32ShareProcess) != 0) dest.Add("Win32ShareProcess");
			if (((int)value & (int)ServiceType.InteractiveProcess) != 0) dest.Add("InteractiveProcess");

			return string.Join(" ", dest);
		}

		private class ServiceName_StartMode
		{
			string[] Pairs;

			public ServiceName_StartMode()
			{
				List<string> dest = new List<string>();

				using (ManagementObjectSearcher svc = new ManagementObjectSearcher("SELECT Name, StartMode, DelayedAutoStart FROM Win32_Service"))
				{
					foreach (ManagementObject mo in svc.Get())
					{
						string serviceName = "" + mo["Name"];
						string startMode = mo["StartMode"] + "_" + mo["DelayedAutoStart"];

						dest.Add(serviceName);
						dest.Add(startMode);
					}
				}
				Pairs = dest.ToArray();
			}

			public string GetStartMode(string serviceName)
			{
				for (int index = 0; index < Pairs.Length; index += 2)
					if (Pairs[index] == serviceName)
						return Pairs[index + 1];

				return null;
			}

#if false // not works...
			public static void SetStartMode(string serviceName, string startMode)
			{
				using (ManagementObjectSearcher svc = new ManagementObjectSearcher("SELECT * FROM Win32_Service WHERE Name = '" + serviceName + "'"))
				{
					foreach (ManagementObject mo in svc.Get())
					{
						ManagementBaseObject prms = mo.GetMethodParameters("ChangeStartMode");

						prms["StartMode"] = startMode;

						mo.InvokeMethod("ChangeStartMode", prms, null);
					}
				}
			}
#endif
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

		private delegate byte[] ComputeHash(FileStream reader);

		private void OutputHashes(Queue<string> argq, ComputeHash computeHash)
		{
			argq.Dequeue();
			string path = argq.Dequeue();
			string wFile = argq.Dequeue();

			using (FileStream writer = new FileStream(wFile, FileMode.Create, FileAccess.Write)) // 書き込みテスト
			{
				writer.WriteByte(0x00);
				writer.WriteByte(0x00);
				writer.WriteByte(0x00);
			}
			File.Delete(wFile); // path がディレクトリで wFile が path の配下であった場合 files に含まれないように削除する。

			string[] files;

			if (Directory.Exists(path))
			{
				files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
				Array.Sort<string>(files);
			}
			else if (File.Exists(path))
			{
				files = new string[] { path };
			}
			else
			{
				throw new FileNotFoundException(path);
			}

			using (StreamWriter writer = new StreamWriter(wFile, false, ENCODING_SJIS))
			{
				foreach (string file in files)
				{
					using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
					{
						string line = BitConverter.ToString(computeHash(reader)).Replace("-", "").ToLower() + " " + EraseRoot(file, path);
						Console.WriteLine(line);
						writer.WriteLine(line);
					}
				}
			}
		}

		private string EraseRoot(string path, string root)
		{
			if (root.EndsWith("\\") == false)
				root += "\\";

			if (StartsWithIgnoreCase(path, root))
				path = path.Substring(root.Length);

			return path;
		}

		private bool StartsWithIgnoreCase(string a, string b)
		{
			return a.ToLower().StartsWith(b.ToLower());
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
