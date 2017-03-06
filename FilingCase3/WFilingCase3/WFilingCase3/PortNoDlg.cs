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
	public partial class PortNoDlg : Form
	{
		public PortNoDlg()
		{
			InitializeComponent();

			// load
			{
				txtPortNo.Text = "" + Gnd.i.portNo;
			}
		}

		private void PortNoDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void PortNoDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void PortNoDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void PortNoDlg_FormClosed(object sender, FormClosedEventArgs e)
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

				try
				{
					int portNo = int.Parse(txtPortNo.Text);

					if (IntTools.isRange(portNo, 1, 65535) == false)
						throw new Exception("1 ～ 65535 の値を入力して下さい。");

					Gnd.i.portNo = portNo;
				}
				catch (Exception ex)
				{
					errorProv.SetError(this.txtPortNo, ex.Message);
					return;
				}
			}
			this.Close();
		}
	}
}
