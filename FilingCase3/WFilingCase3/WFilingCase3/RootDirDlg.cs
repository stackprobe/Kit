using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class RootDirDlg : Form
	{
		public RootDirDlg()
		{
			InitializeComponent();

			txtRootDir.ForeColor = new TextBox().ForeColor;
			txtRootDir.BackColor = new TextBox().BackColor;

			// load
			{
				txtRootDir.Text = Gnd.i.rootDir;
			}
		}

		private void RootDirDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void RootDirDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void RootDirDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void RootDirDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			// save
			{
				Gnd.i.rootDir = txtRootDir.Text;
			}
			this.Close();
		}

		private void btnRootDir_Click(object sender, EventArgs e)
		{
			this.Visible = false;

			RelayDlg.perform(delegate
			{
				//FolderBrowserDialogクラスのインスタンスを作成
				using (FolderBrowserDialog fbd = new FolderBrowserDialog())
				{
					//上部に表示する説明テキストを指定する
					fbd.Description = "「データ ディレクトリ」を指定してください。";
					//ルートフォルダを指定する
					//デフォルトでDesktop
					fbd.RootFolder = Environment.SpecialFolder.MyComputer;
					//最初に選択するフォルダを指定する
					//RootFolder以下にあるフォルダである必要がある
					fbd.SelectedPath = txtRootDir.Text;
					//ユーザーが新しいフォルダを作成できるようにする
					//デフォルトでTrue
					fbd.ShowNewFolderButton = true;

					//ダイアログを表示する
					if (fbd.ShowDialog(this) == DialogResult.OK)
					{
						//選択されたフォルダを表示する
						//Console.WriteLine(fbd.SelectedPath);

						try
						{
							string dir = fbd.SelectedPath;

							try
							{
								dir = Gnd.i.cTools.toFairFullPath(dir);
							}
							catch (Exception ex)
							{
								throw new FaultOperation("旧 Windows 系ファイルシステムで問題を起こす可能性のあるパスは使用出来ません。", ex);
							}

							if (100 < StringTools.ENCODING_SJIS.GetBytes(dir).Length)
							{
								throw new FaultOperation("パスが長すぎます。\nShift_JIS で 100 バイト以下になるようにして下さい。");
							}
							txtRootDir.Text = dir;
						}
						catch (Exception ex)
						{
							FaultOperation.caught(ex);
						}
					}
				}
			});

			this.Visible = true;
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			txtRootDir.Text = Gnd.defRootDir;
		}

		private void txtRootDir_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				txtRootDir.SelectAll();
				e.Handled = true;
				return;
			}
			if (e.KeyChar == (char)32 || e.KeyChar == (char)13) // space || enter
			{
				btnRootDir_Click(null, null);
				return;
			}
		}
	}
}
