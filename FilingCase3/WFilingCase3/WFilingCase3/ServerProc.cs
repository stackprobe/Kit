using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte
{
	public class ServerProc
	{
		private string serverFile
		{
			get
			{
				string file = "Server.exe";

				if (File.Exists(file) == false)
					file = @"C:\Factory\Program\FilingCase3\Server.exe"; // devenv

				file = FileTools.makeFullPath(file);
				return file;
			}
		}

		private Process _proc = null;

		public void start()
		{
			if (_proc != null) // ? 既に実行中
				return;

			string argsFile = Path.Combine(FileTools.getTMP(), "{652bd2b7-3515-44b0-83f3-1110bd2f0d2b}");

			File.WriteAllLines(
				argsFile,
				new string[]
				{
					"/P",
					"" + Gnd.i.portNo,
					"/C",
					"" + Gnd.i.connectMax,
					"/R",
					Gnd.i.rootDir,
					"/D",
					Gnd.i.keepDiskFree_MB + "000000",
					"/E",
					Consts.EV_SERVER_STOP,
					"ANTI-MIS-EXEC",
				},
				StringTools.ENCODING_SJIS
				);

			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = serverFile;
				psi.Arguments = "//R " + argsFile;
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;

				switch (Gnd.i.showConsole)
				{
					case Consts.ShowConsole_e.SHOW_MINIMIZE:
						psi.CreateNoWindow = false;
						psi.UseShellExecute = true;
						psi.WindowStyle = ProcessWindowStyle.Minimized;
						break;

					case Consts.ShowConsole_e.SHOW_NORMAL:
						psi.CreateNoWindow = false;
						break;
				}

				_proc = Process.Start(psi);
			}
		}

		public void end()
		{
			if (_proc == null) // ? 既に停止
				return;

			while (_proc.HasExited == false)
			{
				this.endKick();
				Thread.Sleep(2000);
			}
			_proc = null;
		}

		public void endKick()
		{
			using (NamedEventObject ev = new NamedEventObject(Consts.EV_SERVER_STOP)) // 停止リクエスト
			{
				ev.set();
			}
		}

		public bool isRunning()
		{
			if (_proc != null && _proc.HasExited)
				_proc = null;

			return _proc != null;
		}
	}
}
