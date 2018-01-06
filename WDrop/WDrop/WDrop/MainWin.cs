using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WDrop
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();

			this.MainLabel.Text = Path.GetFileName(Gnd.I.ParentProgFile) + " (" + Gnd.I.ParentProcId + ")\n<D>";
			this.TT.SetToolTip(this.MainLabel, Gnd.I.ParentProgFile + " (" + Gnd.I.ParentProcId + ")");
			this.Message.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			{
				int l = int.MaxValue;
				int t = 0;

				foreach (Screen s in Screen.AllScreens)
				{
					if (s.Bounds.Left < l)
					{
						l = s.Bounds.Left;
						t = s.Bounds.Top;
					}
				}
				this.Left = l;
				this.Top = t;
			}

			this.MainWin_Resize(null, null);
			this.MainTimer.Enabled = true;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private void MainWin_Resize(object sender, EventArgs e)
		{
			this.MainLabel.Left = (this.Width - this.MainLabel.Width) / 2 - 20;
			this.MainLabel.Top = this.Height / 2 - 40;
		}

		private void MainWin_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void MainWin_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				if (paths == null)
					throw null;

				if (paths.Length <= 0)
					throw new Exception("パスがありません。");

				if (2 <= paths.Length)
					throw new Exception("複数のパスが指定されました。");

				string path = paths[0];

				if (
					path == null ||
					path == ""
					)
					throw null;

				if (Directory.Exists(path) == false && File.Exists(path) == false)
					throw new Exception("存在しないパスです。");

				Encoding SJIS = Encoding.GetEncoding(932);

				if (path != SJIS.GetString(SJIS.GetBytes(path)))
					throw new Exception("シフトJISに変換出来ない文字を含んでいます。");

				File.WriteAllText(Gnd.I.OutFile, path, SJIS);
				this.Close();
			}
			catch (Exception ex)
			{
				this.Message.Text = ex.Message;
				this.Message.ForeColor = Color.White;
				this.Message.BackColor = Color.Red;
			}
		}

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (Gnd.I.ParentAliveMutexName == "Test_{0ffd61b7-4555-423b-803f-3aa670666076}")
				return;

			try
			{
				using (Mutex mutex = new Mutex(false, Gnd.I.ParentAliveMutexName))
				{
					if (mutex.WaitOne(0))
					{
						mutex.ReleaseMutex();
						throw null;
					}
				}
			}
			catch
			{
				this.Close();
			}
		}

		private void MainWin_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)0x1b) // escape
			{
				this.Close();
			}
		}

		private void 閉じるXToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
