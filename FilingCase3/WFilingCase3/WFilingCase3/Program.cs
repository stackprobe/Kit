using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			onBoot();

			Application.ThreadException += new ThreadExceptionEventHandler(applicationThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(currentDomainUnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(sessionEnding);

			{
				ArgsReader args = new ArgsReader();

				for (; ; )
				{
#if false
					if (args.argIs("/dummy"))
					{
						continue;
					}
#endif
					if (args.argIs("/S"))
					{
						using (NamedEventObject ev = new NamedEventObject(Consts.EV_STOP))
						{
							ev.set();
						}
						Environment.Exit(0);
					}
					break;
				}
			}

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0) && GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
			{
				checkSelfDir();
				Directory.SetCurrentDirectory(selfDir);
				checkAloneExe();
				checkLogonUser();

				Gnd.i.loadConf();
				Gnd.i.loadData();

				Gnd.i.evStop = new NamedEventObject(Consts.EV_STOP);

				Gnd.i.iconServerRunning = new Icon(Gnd.i.getIconFile("app_16_11"));
				Gnd.i.iconServerNotRunning = new Icon(Gnd.i.getIconFile("app_16_01"));

				// Kill Zombies
				{
					Gnd.i.serverProc.endKick();
					Thread.Sleep(100);
					Gnd.i.serverProc.endKick();
					Thread.Sleep(100);
					Gnd.i.serverProc.endKick();
					Thread.Sleep(100);
				}

				Gnd.i.serverProc.start();

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				// ここではフォームを開けない -> MainWin.cs CloseWindow()

				Gnd.i.saveData();

				FileTools.clearTMP();

				Gnd.i.evStop.Dispose();
				Gnd.i.evStop = null;

				GlobalProcMtx.Release();
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public const string APP_IDENT = "{1dafa8a1-bfd6-47a7-b00b-5c536522d758}";
		public const string APP_TITLE = "FilingCase3";

		private static void applicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void currentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void sessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		public static string selfFile;
		public static string selfDir;

		public static void onBoot()
		{
			selfFile = Assembly.GetEntryAssembly().Location;
			selfDir = Path.GetDirectoryName(selfFile);
		}

		private static void checkSelfDir()
		{
			string dir = selfDir;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (dir != SJIS.GetString(SJIS.GetBytes(dir)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (dir.Substring(1, 2) != ":\\")
			{
				MessageBox.Show(
					"ネットワークパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void checkAloneExe()
		{
			if (File.Exists("app_16_11.dat")) // リリースに含まれるファイル
				return;

			if (Directory.Exists(@"..\Debug")) // ? devenv
				return;

			MessageBox.Show(
				"WHY AM I ALONE ?",
				"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			Environment.Exit(6);
		}

		private static void checkLogonUser()
		{
			string userName = Environment.GetEnvironmentVariable("UserName");
			Encoding SJIS = Encoding.GetEncoding(932);

			if (
				userName == null ||
				userName == "" ||
				userName != SJIS.GetString(SJIS.GetBytes(userName)) ||
				userName.StartsWith(" ") ||
				userName.EndsWith(" ")
				)
			{
				MessageBox.Show(
					"Windows ログオンユーザー名に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(7);
			}
		}
	}
}
