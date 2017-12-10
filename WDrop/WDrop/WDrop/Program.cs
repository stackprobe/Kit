using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WDrop
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Main2();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private static void Main2()
		{
			{
				string[] args = Environment.GetCommandLineArgs();
				int c = 1;

				Gnd.I = new Gnd();
				Gnd.I.ParentProgFile = args[c++];
				Gnd.I.ParentProcId = int.Parse(args[c++]);
				Gnd.I.ParentAliveMutexName = args[c++];
				Gnd.I.OutFile = args[c++];
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());
		}
	}
}
