namespace Charlotte
{
	partial class SettingDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDlg));
			this.btnDefault = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtKeepDiskFree = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtConnectMax = new System.Windows.Forms.TextBox();
			this.errorProv = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProv)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDefault
			// 
			this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDefault.Location = new System.Drawing.Point(11, 139);
			this.btnDefault.Margin = new System.Windows.Forms.Padding(2);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(97, 35);
			this.btnDefault.TabIndex = 6;
			this.btnDefault.Text = "デフォルト";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(354, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "データディレクトリのドライブに確保する空き領域 [MB] :";
			// 
			// txtKeepDiskFree
			// 
			this.txtKeepDiskFree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeepDiskFree.Location = new System.Drawing.Point(378, 80);
			this.txtKeepDiskFree.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtKeepDiskFree.MaxLength = 10;
			this.txtKeepDiskFree.Name = "txtKeepDiskFree";
			this.txtKeepDiskFree.Size = new System.Drawing.Size(190, 27);
			this.txtKeepDiskFree.TabIndex = 3;
			this.txtKeepDiskFree.Text = "1234567890";
			this.txtKeepDiskFree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "最大同時接続数 :";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(493, 139);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(97, 35);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(392, 139);
			this.btnOk.Margin = new System.Windows.Forms.Padding(2);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(97, 35);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtConnectMax
			// 
			this.txtConnectMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectMax.Location = new System.Drawing.Point(378, 29);
			this.txtConnectMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtConnectMax.MaxLength = 10;
			this.txtConnectMax.Name = "txtConnectMax";
			this.txtConnectMax.Size = new System.Drawing.Size(190, 27);
			this.txtConnectMax.TabIndex = 1;
			this.txtConnectMax.Text = "1234567890";
			this.txtConnectMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// errorProv
			// 
			this.errorProv.ContainerControl = this;
			// 
			// SettingDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(601, 185);
			this.Controls.Add(this.btnDefault);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtKeepDiskFree);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.txtConnectMax);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FilingCase3";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingDlg_FormClosed);
			this.Load += new System.EventHandler(this.SettingDlg_Load);
			this.Shown += new System.EventHandler(this.SettingDlg_Shown);
			((System.ComponentModel.ISupportInitialize)(this.errorProv)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtKeepDiskFree;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtConnectMax;
		private System.Windows.Forms.ErrorProvider errorProv;
	}
}