using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WCluster
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// orig >

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());

			// < orig

			if (MainWin.MainProcEx != null)
			{
				MessageBox.Show(
					"" + MainWin.MainProcEx,
					"WCluster / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
		}
	}
}
