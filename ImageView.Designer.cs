
namespace CV_app
{
    partial class ImageView
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
            this.btnListView = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.picBoxView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnListView
            // 
            this.btnListView.Location = new System.Drawing.Point(738, 12);
            this.btnListView.Name = "btnListView";
            this.btnListView.Size = new System.Drawing.Size(54, 260);
            this.btnListView.TabIndex = 5;
            this.btnListView.Text = "打开目录";
            this.btnListView.UseVisualStyleBackColor = true;
            this.btnListView.Click += new System.EventHandler(this.btnListView_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(738, 443);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(54, 109);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一张";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(738, 313);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(54, 109);
            this.btnPre.TabIndex = 6;
            this.btnPre.Text = "上一张";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(98, 69);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(327, 289);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 200;
            // 
            // picBoxView
            // 
            this.picBoxView.Location = new System.Drawing.Point(12, 12);
            this.picBoxView.Name = "picBoxView";
            this.picBoxView.Size = new System.Drawing.Size(720, 540);
            this.picBoxView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxView.TabIndex = 8;
            this.picBoxView.TabStop = false;
            // 
            // ImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnListView);
            this.Controls.Add(this.picBoxView);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageView";
            this.Text = "ImageView";
            this.Load += new System.EventHandler(this.ImageView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox picBoxView;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}