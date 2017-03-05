namespace Charlotte
{
	partial class MainWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.taskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.ttiMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.ttiMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// taskTrayIcon
			// 
			this.taskTrayIcon.ContextMenuStrip = this.ttiMenu;
			this.taskTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskTrayIcon.Icon")));
			this.taskTrayIcon.Text = "WFilingCase3";
			// 
			// ttiMenu
			// 
			this.ttiMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.ttiMenu.Name = "TTIMenu";
			this.ttiMenu.Size = new System.Drawing.Size(119, 26);
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Interval = 2000;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WFilingCase3_MainWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.ttiMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon taskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip ttiMenu;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.Timer mainTimer;
	}
}

