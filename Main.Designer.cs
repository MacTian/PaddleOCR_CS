
namespace CV_app
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblResultstr = new System.Windows.Forms.Label();
            this.chkConnectCam = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkOutput = new System.Windows.Forms.CheckBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnImageView = new System.Windows.Forms.Button();
            this.picRegion = new System.Windows.Forms.PictureBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("YouYuan", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(303, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "视觉检测系统";
            // 
            // lblResultstr
            // 
            this.lblResultstr.Font = new System.Drawing.Font("YouYuan", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResultstr.Location = new System.Drawing.Point(419, 626);
            this.lblResultstr.Name = "lblResultstr";
            this.lblResultstr.Size = new System.Drawing.Size(301, 90);
            this.lblResultstr.TabIndex = 83;
            this.lblResultstr.Text = "HelloWorld合格123456";
            // 
            // chkConnectCam
            // 
            this.chkConnectCam.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkConnectCam.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkConnectCam.Image = global::CV_app.Properties.Resources.online;
            this.chkConnectCam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkConnectCam.Location = new System.Drawing.Point(726, 0);
            this.chkConnectCam.Name = "chkConnectCam";
            this.chkConnectCam.Size = new System.Drawing.Size(172, 74);
            this.chkConnectCam.TabIndex = 84;
            this.chkConnectCam.Text = "联机";
            this.chkConnectCam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConnectCam.UseVisualStyleBackColor = true;
            this.chkConnectCam.CheckedChanged += new System.EventHandler(this.chkConnectCam_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Image = global::CV_app.Properties.Resources.clear;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(726, 285);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(298, 107);
            this.btnClear.TabIndex = 82;
            this.btnClear.Text = "清除数据";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkOutput
            // 
            this.chkOutput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkOutput.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOutput.Image = global::CV_app.Properties.Resources.enablealarm;
            this.chkOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkOutput.Location = new System.Drawing.Point(726, 398);
            this.chkOutput.Name = "chkOutput";
            this.chkOutput.Size = new System.Drawing.Size(298, 107);
            this.chkOutput.TabIndex = 81;
            this.chkOutput.Text = "启用输出";
            this.chkOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOutput.UseVisualStyleBackColor = true;
            this.chkOutput.CheckedChanged += new System.EventHandler(this.chkOutput_CheckedChanged);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSetting.AutoSize = true;
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSetting.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Image = global::CV_app.Properties.Resources.setting;
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetting.Location = new System.Drawing.Point(726, 511);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(298, 107);
            this.btnSetting.TabIndex = 78;
            this.btnSetting.Text = "参数设置";
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnImageView
            // 
            this.btnImageView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImageView.AutoSize = true;
            this.btnImageView.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImageView.Image = global::CV_app.Properties.Resources.images;
            this.btnImageView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImageView.Location = new System.Drawing.Point(726, 624);
            this.btnImageView.Name = "btnImageView";
            this.btnImageView.Size = new System.Drawing.Size(298, 107);
            this.btnImageView.TabIndex = 77;
            this.btnImageView.Text = "记录查看";
            this.btnImageView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImageView.UseVisualStyleBackColor = true;
            this.btnImageView.Click += new System.EventHandler(this.btnImageView_Click);
            // 
            // picRegion
            // 
            this.picRegion.Location = new System.Drawing.Point(726, 80);
            this.picRegion.Name = "picRegion";
            this.picRegion.Size = new System.Drawing.Size(298, 199);
            this.picRegion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRegion.TabIndex = 4;
            this.picRegion.TabStop = false;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(0, 80);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(720, 540);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 3;
            this.picImage.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::CV_app.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(945, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 74);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::CV_app.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(300, 74);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("YouYuan", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 626);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 33);
            this.label2.TabIndex = 85;
            this.label2.Text = "总数:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("YouYuan", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(9, 678);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 33);
            this.label3.TabIndex = 86;
            this.label3.Text = "不良:";
            // 
            // lblNG
            // 
            this.lblNG.AutoSize = true;
            this.lblNG.Font = new System.Drawing.Font("YouYuan", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNG.Location = new System.Drawing.Point(93, 678);
            this.lblNG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(32, 33);
            this.lblNG.TabIndex = 88;
            this.lblNG.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("YouYuan", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotal.Location = new System.Drawing.Point(93, 626);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(32, 33);
            this.lblTotal.TabIndex = 87;
            this.lblTotal.Text = "0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1024, 718);
            this.Controls.Add(this.lblNG);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkConnectCam);
            this.Controls.Add(this.lblResultstr);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkOutput);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnImageView);
            this.Controls.Add(this.picRegion);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkOutput;
        private System.Windows.Forms.Button btnImageView;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.CheckBox chkConnectCam;
        public System.Windows.Forms.PictureBox picImage;
        public System.Windows.Forms.PictureBox picRegion;
        public System.Windows.Forms.Label lblResultstr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblNG;
        public System.Windows.Forms.Label lblTotal;
    }
}