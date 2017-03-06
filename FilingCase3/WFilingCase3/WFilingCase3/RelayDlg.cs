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
	public partial class RelayDlg : Form
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

		private BusyDlg.operation_d _operation;

		public RelayDlg(BusyDlg.operation_d operation)
		{
			_operation = operation;

			InitializeComponent();
		}

		private void RelayDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void RelayDlg_Shown(object sender, EventArgs e)
		{
			this.mtEnabled = true;
		}

		private void RelayDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void RelayDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.mtEnabled = false;
		}

		private bool mtEnabled;

		private void mainTimer_Tick(object sender, EventArgs e)
		{
			if (this.mtEnabled == false)
				return;

			this.mtEnabled = false;

			try
			{
				_operation();
			}
			catch
			{ }

			this.Close();
		}

		public static void perform(BusyDlg.operation_d operation)
		{
			using (RelayDlg f = new RelayDlg(operation))
			{
				f.ShowDialog();
			}
		}
	}
}
