﻿namespace Charlotte
{
	partial class RelayDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelayDlg));
			this.mainTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// mainTimer
			// 
			this.mainTimer.Enabled = true;
			this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
			// 
			// RelayDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "RelayDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FilingCase3";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RelayDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RelayDlg_FormClosed);
			this.Load += new System.EventHandler(this.RelayDlg_Load);
			this.Shown += new System.EventHandler(this.RelayDlg_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer mainTimer;
	}
}
