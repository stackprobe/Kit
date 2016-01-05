using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using System.IO;

namespace WCluster
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
			this.MinimumSize = this.Size;

			this.BackColor = Color.Black;
			this.MainPanel.BackColor = Color.Black;

			this.ProgressImg.Width = this.ProgressImg.Image.Width;
			this.ProgressImg.Height = this.ProgressImg.Image.Height;

			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (this.MT_Count == 0)
				{
					this.MainProcTh = new Thread(MainProc);
					this.MainProcTh.Start();
				}
				if (5 < this.MT_Count)
				{
					if (this.MainProcTh.IsAlive == false)
					{
						this.MT_Enabled = false;
						this.Close();
						return;
					}
				}

				{
					int l = (this.MainPanel.Width - this.ProgressImg.Width) / 2;
					int t = (this.MainPanel.Height - this.ProgressImg.Height) / 2;

					if (this.ProgressImg.Left != l)
						this.ProgressImg.Left = l;

					if (this.ProgressImg.Top != t)
						this.ProgressImg.Top = t;
				}

				Image img = this.ProgressImg.Image;
				img.RotateFlip(RotateFlipType.Rotate90FlipNone);
				this.ProgressImg.Image = img;
			}
			catch (Exception ex)
			{
				this.MT_Enabled = false;
				throw ex;
			}
			finally
			{
				this.MT_Count++;
				this.MT_Busy = false;
			}
		}

		private Thread MainProcTh;
		public static Exception MainProcEx;

		private void MainProc()
		{
			try
			{
				string[] args = Environment.GetCommandLineArgs();

				if (args.Length == 2)
				{
					new Clusterizer().Perform(Environment.GetCommandLineArgs()[1]);
					return;
				}
				if (args.Length == 3)
				{
					string rPath = args[1];
					string wPath = args[2];

					if (File.Exists(rPath))
					{
						if (Directory.Exists(wPath))
							wPath = Path.Combine(wPath, Path.GetFileNameWithoutExtension(rPath));

						new Clusterizer().FileToDirectory(rPath, wPath);
					}
					else if (Directory.Exists(rPath))
					{
						if (Directory.Exists(wPath))
							wPath = Path.Combine(wPath, Path.GetFileName(rPath) + ".wclu");

						new Clusterizer().DirectoryToFile(rPath, wPath);
					}
					return;
				}
				throw new Exception("コマンド引数に問題があります。");
			}
			catch (Exception e)
			{
				MainProcEx = e;
			}
		}
	}
}
