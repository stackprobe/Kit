namespace WDrop
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
			this.MainLabel = new System.Windows.Forms.Label();
			this.Message = new System.Windows.Forms.Label();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TT = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// MainLabel
			// 
			this.MainLabel.AutoSize = true;
			this.MainLabel.ForeColor = System.Drawing.Color.Gray;
			this.MainLabel.Location = new System.Drawing.Point(12, 9);
			this.MainLabel.Name = "MainLabel";
			this.MainLabel.Size = new System.Drawing.Size(147, 40);
			this.MainLabel.TabIndex = 0;
			this.MainLabel.Text = "Program.exe (12345)\r\n<D>";
			this.TT.SetToolTip(this.MainLabel, "D:\\Program.exe (12345)");
			// 
			// Message
			// 
			this.Message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Message.AutoSize = true;
			this.Message.Location = new System.Drawing.Point(12, 232);
			this.Message.Name = "Message";
			this.Message.Size = new System.Drawing.Size(64, 20);
			this.Message.TabIndex = 1;
			this.Message.Text = "Message";
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainWin
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.Message);
			this.Controls.Add(this.MainLabel);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "<D>";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWin_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWin_DragEnter);
			this.Resize += new System.EventHandler(this.MainWin_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MainLabel;
		private System.Windows.Forms.Label Message;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ToolTip TT;
    }
}

