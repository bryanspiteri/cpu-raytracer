
namespace RaytracerWindow
{
	partial class MainWindow
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.renderBtn = new System.Windows.Forms.Button();
			this.renderProgress = new System.Windows.Forms.ProgressBar();
			this.progressLabel = new System.Windows.Forms.Label();
			this.widthBox = new System.Windows.Forms.TextBox();
			this.heightBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.msaaCount = new System.Windows.Forms.TextBox();
			this.bouncesTxt = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.threadCount = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// renderBtn
			// 
			this.renderBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.renderBtn.Location = new System.Drawing.Point(673, 397);
			this.renderBtn.Name = "renderBtn";
			this.renderBtn.Size = new System.Drawing.Size(115, 41);
			this.renderBtn.TabIndex = 0;
			this.renderBtn.Text = "Render";
			this.renderBtn.UseVisualStyleBackColor = true;
			this.renderBtn.Click += new System.EventHandler(this.renderBtn_Click);
			// 
			// renderProgress
			// 
			this.renderProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.renderProgress.Location = new System.Drawing.Point(13, 397);
			this.renderProgress.Maximum = 100000;
			this.renderProgress.Name = "renderProgress";
			this.renderProgress.Size = new System.Drawing.Size(654, 41);
			this.renderProgress.TabIndex = 1;
			// 
			// progressLabel
			// 
			this.progressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(13, 376);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(23, 15);
			this.progressLabel.TabIndex = 2;
			this.progressLabel.Text = "0%";
			// 
			// widthBox
			// 
			this.widthBox.Location = new System.Drawing.Point(12, 62);
			this.widthBox.MaxLength = 4;
			this.widthBox.Name = "widthBox";
			this.widthBox.Size = new System.Drawing.Size(122, 23);
			this.widthBox.TabIndex = 3;
			this.widthBox.Text = "1280";
			// 
			// heightBox
			// 
			this.heightBox.Location = new System.Drawing.Point(140, 62);
			this.heightBox.MaxLength = 4;
			this.heightBox.Name = "heightBox";
			this.heightBox.Size = new System.Drawing.Size(122, 23);
			this.heightBox.TabIndex = 4;
			this.heightBox.Text = "720";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "Width:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(140, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "Height:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "MSAA Samples:";
			// 
			// msaaCount
			// 
			this.msaaCount.Location = new System.Drawing.Point(12, 106);
			this.msaaCount.MaxLength = 4;
			this.msaaCount.Name = "msaaCount";
			this.msaaCount.Size = new System.Drawing.Size(122, 23);
			this.msaaCount.TabIndex = 7;
			this.msaaCount.Text = "100";
			// 
			// bouncesTxt
			// 
			this.bouncesTxt.Location = new System.Drawing.Point(140, 106);
			this.bouncesTxt.MaxLength = 4;
			this.bouncesTxt.Name = "bouncesTxt";
			this.bouncesTxt.Size = new System.Drawing.Size(122, 23);
			this.bouncesTxt.TabIndex = 9;
			this.bouncesTxt.Text = "50";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(140, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Light Bounces:";
			// 
			// threadCount
			// 
			this.threadCount.Location = new System.Drawing.Point(268, 62);
			this.threadCount.MaxLength = 4;
			this.threadCount.Name = "threadCount";
			this.threadCount.Size = new System.Drawing.Size(122, 23);
			this.threadCount.TabIndex = 11;
			this.threadCount.Text = "8";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(268, 44);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 15);
			this.label5.TabIndex = 12;
			this.label5.Text = "Threads:";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.threadCount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.bouncesTxt);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.msaaCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.heightBox);
			this.Controls.Add(this.widthBox);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.renderProgress);
			this.Controls.Add(this.renderBtn);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CPU Raytracer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button renderBtn;
		private System.Windows.Forms.ProgressBar renderProgress;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.TextBox widthBox;
		private System.Windows.Forms.TextBox heightBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox msaaCount;
		private System.Windows.Forms.TextBox bouncesTxt;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox threadCount;
		private System.Windows.Forms.Label label5;
	}
}

