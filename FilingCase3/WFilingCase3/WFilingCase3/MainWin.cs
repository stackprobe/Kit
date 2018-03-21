using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
			this.taskTrayIcon.Visible = true;
			this.mtEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		private void CloseWindow()
		{
			this.mtEnabled = false;
			this.taskTrayIcon.Visible = false;

			// プロセス終了時にすること
			{
				BusyDlg.perform(delegate
				{
					Gnd.i.serverProc.end();
					Gnd.i.serverProc = null;
				});
			}

			this.Close();
		}

		private bool mtEnabled;
		private bool mtBusy;
		private long mtCount;

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			if (this.mtEnabled == false || this.mtBusy)
				return;

			this.mtBusy = true;

			try
			{
				{
					Icon icon;

					if (Gnd.i.serverProc.isRunning())
						icon = Gnd.i.iconServerRunning;
					else
						icon = Gnd.i.iconServerNotRunning;

					if (this.taskTrayIcon.Icon != icon)
					{
						this.taskTrayIcon.Icon = icon;

						{
							string text;

							if (Gnd.i.serverProc.isRunning())
								text = "FilingCase3 ";
							else
								text = "FilingCase3 / サービスは停止しています。";

							this.taskTrayIcon.Text = text;
						}
					}
				}

				if (Gnd.i.evStop.waitForMillis(0))
				{
					this.CloseWindow();
					return;
				}
			}
			finally
			{
				this.mtBusy = false;
				this.mtCount++;
			}
		}

		private void 再起動ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.mtEnabled = false;
			this.taskTrayIcon.Visible = false;

			BusyDlg.perform(delegate
			{
				Gnd.i.serverProc.end();
				Gnd.i.serverProc.start();
			});

			this.taskTrayIcon.Visible = true;
			this.mtEnabled = true;
		}

		private void beforeShowDialog()
		{
			this.mtEnabled = false;
			this.taskTrayIcon.Visible = false;

			BusyDlg.perform(delegate
			{
				Gnd.i.serverProc.end();
			});
		}

		private void afterShowDialog()
		{
			Gnd.i.serverProc.start();
			this.taskTrayIcon.Visible = true;
			this.mtEnabled = true;
		}

		private void ポート番号ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			beforeShowDialog();

			using (PortNoDlg f = new PortNoDlg())
			{
				f.ShowDialog();
			}
			afterShowDialog();
		}

		private void データディレクトリToolStripMenuItem_Click(object sender, EventArgs e)
		{
			beforeShowDialog();

			using (RootDirDlg f = new RootDirDlg())
			{
				f.ShowDialog();
			}
			afterShowDialog();
		}

		private void 詳細設定ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			beforeShowDialog();

			using (SettingDlg f = new SettingDlg())
			{
				f.ShowDialog();
			}
			afterShowDialog();
		}
	}
}
