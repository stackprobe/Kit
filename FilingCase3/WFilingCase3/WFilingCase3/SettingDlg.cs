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
	public partial class SettingDlg : Form
	{
		public SettingDlg()
		{
			InitializeComponent();

			// load
			{
				txtConnectMax.Text = "" + Gnd.i.connectMax;
				txtKeepDiskFree.Text = "" + Gnd.i.keepDiskFree_MB;
			}
		}

		private void SettingDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void SettingDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void SettingDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void SettingDlg_FormClosed(object sender, FormClosedEventArgs e)
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
				errorProv.Clear();

				Control eControl = null;

				try
				{
					eControl = txtConnectMax;
					int connectMax = int.Parse(txtConnectMax.Text);

					if (IntTools.isRange(connectMax, 1, IntTools.IMAX) == false)
						throw new Exception("1 ～ 1000000000 の値を入力して下さい。");

					eControl = txtKeepDiskFree;
					int keepDiskFree = int.Parse(txtKeepDiskFree.Text);

					if (IntTools.isRange(keepDiskFree, 1, IntTools.IMAX) == false)
						throw new Exception("1 ～ 1000000000 の値を入力して下さい。");

					Gnd.i.connectMax = connectMax;
					Gnd.i.keepDiskFree_MB = keepDiskFree;
				}
				catch (Exception ex)
				{
					errorProv.SetError(eControl, ex.Message);
					return;
				}
			}
			this.Close();
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			this.txtConnectMax.Text = "" + 100;
			this.txtKeepDiskFree.Text = "" + 500;
		}
	}
}
