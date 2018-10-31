using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SpicaToolkit
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
				if (EqualsIgnoreCase(argq.Peek(), "/MUTEX"))
				{
					string mtxName = argq.Dequeue();
					int millis = int.Parse(argq.Dequeue());
					string enterEvName = argq.Dequeue();
					string leaveEvName = argq.Dequeue();
					string timeoutEvName = argq.Dequeue();
					int ownerProcId = int.Parse(argq.Dequeue());

					OwnerProc = Process.GetProcessById(ownerProcId);

					using (Mutex m = CreateMutex(mtxName))
					using (EventWaitHandle enterEv = CreateNamedEvent(enterEvName))
					using (EventWaitHandle leaveEv = CreateNamedEvent(leaveEvName))
					using (EventWaitHandle timeoutEv = CreateNamedEvent(leaveEvName))
					{
						if (WaitForMillis(m, millis))
						{
							enterEv.Set();
							m.ReleaseMutex();
							leaveEv.WaitOne();
						}
						else
						{
							timeoutEv.Set();
						}
					}
					return;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/NAMED-EVENT"))
				{
					string evName = argq.Dequeue();
					string timeoutEvName = argq.Dequeue();
					int ownerProcId = int.Parse(argq.Dequeue());

					OwnerProc = Process.GetProcessById(ownerProcId);

					using (EventWaitHandle ev = CreateNamedEvent(evName))
					using (EventWaitHandle timeoutEv = CreateNamedEvent(timeoutEvName))
					{
						WaitForMillis(timeoutEv, -1);
					}
					return;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/NAMED-EVENT-WAIT"))
				{
					string evName = argq.Dequeue();
					int millis = int.Parse(argq.Dequeue());
					int ownerProcId = int.Parse(argq.Dequeue());

					OwnerProc = Process.GetProcessById(ownerProcId);

					using (EventWaitHandle ev = CreateNamedEvent(evName))
					{
						WaitForMillis(ev, millis);
					}
					return;
				}
				if (EqualsIgnoreCase(argq.Peek(), "/NAMED-EVENT-SET"))
				{
					string evName = argq.Dequeue();

					using (EventWaitHandle ev = CreateNamedEvent(evName))
					{
						ev.Set();
					}
					return;
				}
				// ここへ追加..
				throw new Exception("不明なオプション：" + argq.Peek());
			}
		}

		private Process OwnerProc = null;

		private bool WaitForMillis(WaitHandle hdl, int millis)
		{
			const int WAIT_MILLIS = 2000;

			do
			{
				if (OwnerProc.HasExited)
					throw new Exception("親プロセスは停止しました。");

				int waitMillis = millis == -1 ? WAIT_MILLIS : Math.Min(WAIT_MILLIS, millis);

				if (hdl.WaitOne(waitMillis))
					return true;

				if (millis != -1)
					millis -= waitMillis;
			}
			while (0 < millis);

			return false;
		}

		private Mutex CreateMutex(string mtxName)
		{
			MutexSecurity security = new MutexSecurity();

			security.AddAccessRule(
				new MutexAccessRule(
					new SecurityIdentifier(
						WellKnownSidType.WorldSid,
						null
						),
					MutexRights.FullControl,
					AccessControlType.Allow
					)
				);

			bool createdNew;
			return new Mutex(false, mtxName, out createdNew, security);
		}

		private EventWaitHandle CreateNamedEvent(string evName)
		{
			EventWaitHandleSecurity security = new EventWaitHandleSecurity();

			security.AddAccessRule(
				new EventWaitHandleAccessRule(
					new SecurityIdentifier(
						WellKnownSidType.WorldSid,
						null
						),
					EventWaitHandleRights.FullControl,
					AccessControlType.Allow
					)
				);

			bool createdNew;
			return new EventWaitHandle(false, EventResetMode.AutoReset, evName, out createdNew, security);
		}

		private bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}
	}
}
