﻿namespace Charlotte
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
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ポート番号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.データディレクトリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.詳細設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.再起動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.ttiMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// taskTrayIcon
			// 
			this.taskTrayIcon.ContextMenuStrip = this.ttiMenu;
			this.taskTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskTrayIcon.Icon")));
			this.taskTrayIcon.Text = "FilingCase3";
			// 
			// ttiMenu
			// 
			this.ttiMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.再起動ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了ToolStripMenuItem});
			this.ttiMenu.Name = "TTIMenu";
			this.ttiMenu.Size = new System.Drawing.Size(111, 76);
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ポート番号ToolStripMenuItem,
            this.データディレクトリToolStripMenuItem,
            this.toolStripMenuItem2,
            this.詳細設定ToolStripMenuItem});
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.設定ToolStripMenuItem.Text = "設定";
			// 
			// ポート番号ToolStripMenuItem
			// 
			this.ポート番号ToolStripMenuItem.Name = "ポート番号ToolStripMenuItem";
			this.ポート番号ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.ポート番号ToolStripMenuItem.Text = "ポート番号";
			this.ポート番号ToolStripMenuItem.Click += new System.EventHandler(this.ポート番号ToolStripMenuItem_Click);
			// 
			// データディレクトリToolStripMenuItem
			// 
			this.データディレクトリToolStripMenuItem.Name = "データディレクトリToolStripMenuItem";
			this.データディレクトリToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.データディレクトリToolStripMenuItem.Text = "データ ディレクトリ";
			this.データディレクトリToolStripMenuItem.Click += new System.EventHandler(this.データディレクトリToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 6);
			// 
			// 詳細設定ToolStripMenuItem
			// 
			this.詳細設定ToolStripMenuItem.Name = "詳細設定ToolStripMenuItem";
			this.詳細設定ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.詳細設定ToolStripMenuItem.Text = "詳細設定";
			this.詳細設定ToolStripMenuItem.Click += new System.EventHandler(this.詳細設定ToolStripMenuItem_Click);
			// 
			// 再起動ToolStripMenuItem
			// 
			this.再起動ToolStripMenuItem.Name = "再起動ToolStripMenuItem";
			this.再起動ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.再起動ToolStripMenuItem.Text = "再起動";
			this.再起動ToolStripMenuItem.Click += new System.EventHandler(this.再起動ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Interval = 2000;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.Timer mainTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ポート番号ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 再起動ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 詳細設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem データディレクトリToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
	}
}

