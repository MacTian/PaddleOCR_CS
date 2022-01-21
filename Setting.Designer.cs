
namespace CV_app
{
    partial class Setting
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
            this.tbpImageFilter = new System.Windows.Forms.TabPage();
            this.btnPreview = new System.Windows.Forms.Button();
            this.grpImageProcess = new System.Windows.Forms.GroupBox();
            this.txtFilterC = new System.Windows.Forms.TextBox();
            this.txtBlocksize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbtnBinaryInv = new System.Windows.Forms.RadioButton();
            this.rdbtnBinary = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtErodeY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtErodeX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtDilateY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDilateX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkImageProcess = new System.Windows.Forms.CheckBox();
            this.m_cb_TriggerActivation = new System.Windows.Forms.ComboBox();
            this.m_cb_TriggerSource = new System.Windows.Forms.ComboBox();
            this.m_btn_SoftTriggerCommand = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpRecipe = new System.Windows.Forms.TabPage();
            this.tbpCam = new System.Windows.Forms.TabPage();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.chkOutput = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbReverseY = new System.Windows.Forms.ComboBox();
            this.cmbReverseX = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCalib = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_lbl_Gain = new System.Windows.Forms.Label();
            this.m_txt_Gain = new System.Windows.Forms.TextBox();
            this.m_lbl_Shutter = new System.Windows.Forms.Label();
            this.m_txt_Shutter = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtCaptureDelay = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cb_TriggerMode = new System.Windows.Forms.ComboBox();
            this.tbpOCRSetting = new System.Windows.Forms.TabPage();
            this.picTips = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnFinishSet = new System.Windows.Forms.Button();
            this.txtOperateRecords = new System.Windows.Forms.TextBox();
            this.btnOCRRegion = new System.Windows.Forms.Button();
            this.btnSearchRegion = new System.Windows.Forms.Button();
            this.btnCreatePattern = new System.Windows.Forms.Button();
            this.btnSelectModel = new System.Windows.Forms.Button();
            this.tbpAllParams = new System.Windows.Forms.TabPage();
            this.dgvCCDParams = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbpImageFilter.SuspendLayout();
            this.grpImageProcess.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpCam.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tbpOCRSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTips)).BeginInit();
            this.tbpAllParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCCDParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbpImageFilter
            // 
            this.tbpImageFilter.Controls.Add(this.btnPreview);
            this.tbpImageFilter.Controls.Add(this.grpImageProcess);
            this.tbpImageFilter.Controls.Add(this.chkImageProcess);
            this.tbpImageFilter.Location = new System.Drawing.Point(4, 22);
            this.tbpImageFilter.Name = "tbpImageFilter";
            this.tbpImageFilter.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbpImageFilter.Size = new System.Drawing.Size(407, 445);
            this.tbpImageFilter.TabIndex = 1;
            this.tbpImageFilter.Text = "图像预处理";
            this.tbpImageFilter.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(33, 388);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(80, 38);
            this.btnPreview.TabIndex = 38;
            this.btnPreview.Text = "效果预览";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // grpImageProcess
            // 
            this.grpImageProcess.Controls.Add(this.txtFilterC);
            this.grpImageProcess.Controls.Add(this.txtBlocksize);
            this.grpImageProcess.Controls.Add(this.label1);
            this.grpImageProcess.Controls.Add(this.label3);
            this.grpImageProcess.Controls.Add(this.rdbtnBinaryInv);
            this.grpImageProcess.Controls.Add(this.rdbtnBinary);
            this.grpImageProcess.Controls.Add(this.panel5);
            this.grpImageProcess.Controls.Add(this.panel6);
            this.grpImageProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpImageProcess.Location = new System.Drawing.Point(33, 63);
            this.grpImageProcess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpImageProcess.Name = "grpImageProcess";
            this.grpImageProcess.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpImageProcess.Size = new System.Drawing.Size(340, 299);
            this.grpImageProcess.TabIndex = 37;
            this.grpImageProcess.TabStop = false;
            this.grpImageProcess.Text = "图像预处理";
            // 
            // txtFilterC
            // 
            this.txtFilterC.Location = new System.Drawing.Point(265, 114);
            this.txtFilterC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFilterC.Name = "txtFilterC";
            this.txtFilterC.Size = new System.Drawing.Size(50, 26);
            this.txtFilterC.TabIndex = 42;
            // 
            // txtBlocksize
            // 
            this.txtBlocksize.Location = new System.Drawing.Point(101, 114);
            this.txtBlocksize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBlocksize.Name = "txtBlocksize";
            this.txtBlocksize.Size = new System.Drawing.Size(50, 26);
            this.txtBlocksize.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "常数值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "区块值";
            // 
            // rdbtnBinaryInv
            // 
            this.rdbtnBinaryInv.AutoSize = true;
            this.rdbtnBinaryInv.Location = new System.Drawing.Point(169, 45);
            this.rdbtnBinaryInv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbtnBinaryInv.Name = "rdbtnBinaryInv";
            this.rdbtnBinaryInv.Size = new System.Drawing.Size(91, 24);
            this.rdbtnBinaryInv.TabIndex = 38;
            this.rdbtnBinaryInv.TabStop = true;
            this.rdbtnBinaryInv.Text = "反二值化";
            this.rdbtnBinaryInv.UseVisualStyleBackColor = true;
            // 
            // rdbtnBinary
            // 
            this.rdbtnBinary.AutoSize = true;
            this.rdbtnBinary.Location = new System.Drawing.Point(18, 45);
            this.rdbtnBinary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbtnBinary.Name = "rdbtnBinary";
            this.rdbtnBinary.Size = new System.Drawing.Size(75, 24);
            this.rdbtnBinary.TabIndex = 37;
            this.rdbtnBinary.TabStop = true;
            this.rdbtnBinary.Text = "二值化";
            this.rdbtnBinary.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtErodeY);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.txtErodeX);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(9, 162);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(308, 60);
            this.panel5.TabIndex = 34;
            // 
            // txtErodeY
            // 
            this.txtErodeY.Enabled = false;
            this.txtErodeY.Location = new System.Drawing.Point(252, 10);
            this.txtErodeY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtErodeY.Name = "txtErodeY";
            this.txtErodeY.Size = new System.Drawing.Size(36, 26);
            this.txtErodeY.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "X";
            // 
            // txtErodeX
            // 
            this.txtErodeX.Enabled = false;
            this.txtErodeX.Location = new System.Drawing.Point(171, 10);
            this.txtErodeX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtErodeX.Name = "txtErodeX";
            this.txtErodeX.Size = new System.Drawing.Size(36, 26);
            this.txtErodeX.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "腐蚀";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtDilateY);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtDilateX);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Location = new System.Drawing.Point(9, 229);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(308, 60);
            this.panel6.TabIndex = 35;
            // 
            // txtDilateY
            // 
            this.txtDilateY.Enabled = false;
            this.txtDilateY.Location = new System.Drawing.Point(252, 10);
            this.txtDilateY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDilateY.Name = "txtDilateY";
            this.txtDilateY.Size = new System.Drawing.Size(36, 26);
            this.txtDilateY.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "X";
            // 
            // txtDilateX
            // 
            this.txtDilateX.Enabled = false;
            this.txtDilateX.Location = new System.Drawing.Point(171, 10);
            this.txtDilateX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDilateX.Name = "txtDilateX";
            this.txtDilateX.Size = new System.Drawing.Size(36, 26);
            this.txtDilateX.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 9);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 20);
            this.label12.TabIndex = 30;
            this.label12.Text = "膨胀";
            // 
            // chkImageProcess
            // 
            this.chkImageProcess.AutoSize = true;
            this.chkImageProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkImageProcess.Location = new System.Drawing.Point(33, 22);
            this.chkImageProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkImageProcess.Name = "chkImageProcess";
            this.chkImageProcess.Size = new System.Drawing.Size(75, 30);
            this.chkImageProcess.TabIndex = 36;
            this.chkImageProcess.Text = "启用";
            this.chkImageProcess.UseVisualStyleBackColor = true;
            this.chkImageProcess.CheckedChanged += new System.EventHandler(this.chkImageProcess_CheckedChanged);
            // 
            // m_cb_TriggerActivation
            // 
            this.m_cb_TriggerActivation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cb_TriggerActivation.FormattingEnabled = true;
            this.m_cb_TriggerActivation.Location = new System.Drawing.Point(203, 114);
            this.m_cb_TriggerActivation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_cb_TriggerActivation.Name = "m_cb_TriggerActivation";
            this.m_cb_TriggerActivation.Size = new System.Drawing.Size(149, 21);
            this.m_cb_TriggerActivation.TabIndex = 12;
            this.m_cb_TriggerActivation.SelectedIndexChanged += new System.EventHandler(this.m_cb_TriggerActivation_SelectedIndexChanged);
            // 
            // m_cb_TriggerSource
            // 
            this.m_cb_TriggerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cb_TriggerSource.FormattingEnabled = true;
            this.m_cb_TriggerSource.Location = new System.Drawing.Point(203, 49);
            this.m_cb_TriggerSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_cb_TriggerSource.Name = "m_cb_TriggerSource";
            this.m_cb_TriggerSource.Size = new System.Drawing.Size(149, 21);
            this.m_cb_TriggerSource.TabIndex = 8;
            this.m_cb_TriggerSource.SelectedIndexChanged += new System.EventHandler(this.m_cb_TriggerSource_SelectedIndexChanged);
            // 
            // m_btn_SoftTriggerCommand
            // 
            this.m_btn_SoftTriggerCommand.Location = new System.Drawing.Point(203, 81);
            this.m_btn_SoftTriggerCommand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_btn_SoftTriggerCommand.Name = "m_btn_SoftTriggerCommand";
            this.m_btn_SoftTriggerCommand.Size = new System.Drawing.Size(148, 20);
            this.m_btn_SoftTriggerCommand.TabIndex = 10;
            this.m_btn_SoftTriggerCommand.Text = "发送软触发命令";
            this.m_btn_SoftTriggerCommand.UseVisualStyleBackColor = true;
            this.m_btn_SoftTriggerCommand.Click += new System.EventHandler(this.m_btn_SoftTriggerCommand_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "触发极性";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "软触发";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "触发源";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpRecipe);
            this.tabControl1.Controls.Add(this.tbpCam);
            this.tabControl1.Controls.Add(this.tbpImageFilter);
            this.tabControl1.Controls.Add(this.tbpOCRSetting);
            this.tabControl1.Controls.Add(this.tbpAllParams);
            this.tabControl1.Location = new System.Drawing.Point(548, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(415, 471);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tbpRecipe
            // 
            this.tbpRecipe.Location = new System.Drawing.Point(4, 22);
            this.tbpRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpRecipe.Name = "tbpRecipe";
            this.tbpRecipe.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpRecipe.Size = new System.Drawing.Size(407, 445);
            this.tbpRecipe.TabIndex = 4;
            this.tbpRecipe.Text = "配方选择";
            this.tbpRecipe.UseVisualStyleBackColor = true;
            // 
            // tbpCam
            // 
            this.tbpCam.Controls.Add(this.btnSaveModel);
            this.tbpCam.Controls.Add(this.chkOutput);
            this.tbpCam.Controls.Add(this.btnSave);
            this.tbpCam.Controls.Add(this.groupBox6);
            this.tbpCam.Controls.Add(this.btnCalib);
            this.tbpCam.Controls.Add(this.groupBox3);
            this.tbpCam.Controls.Add(this.groupBox5);
            this.tbpCam.Location = new System.Drawing.Point(4, 22);
            this.tbpCam.Name = "tbpCam";
            this.tbpCam.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbpCam.Size = new System.Drawing.Size(407, 445);
            this.tbpCam.TabIndex = 0;
            this.tbpCam.Text = "相机参数";
            this.tbpCam.UseVisualStyleBackColor = true;
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveModel.Location = new System.Drawing.Point(140, 402);
            this.btnSaveModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(116, 32);
            this.btnSaveModel.TabIndex = 34;
            this.btnSaveModel.Text = "保存模板";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Visible = false;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // chkOutput
            // 
            this.chkOutput.AutoSize = true;
            this.chkOutput.Location = new System.Drawing.Point(7, 374);
            this.chkOutput.Name = "chkOutput";
            this.chkOutput.Size = new System.Drawing.Size(74, 17);
            this.chkOutput.TabIndex = 33;
            this.chkOutput.Text = "信号输出";
            this.chkOutput.UseVisualStyleBackColor = true;
            this.chkOutput.Visible = false;
            this.chkOutput.CheckedChanged += new System.EventHandler(this.chkOutput_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(272, 402);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 32);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "参数保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbReverseY);
            this.groupBox6.Controls.Add(this.cmbReverseX);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Location = new System.Drawing.Point(7, 291);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(385, 70);
            this.groupBox6.TabIndex = 29;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "图像格式控制";
            // 
            // cmbReverseY
            // 
            this.cmbReverseY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReverseY.FormattingEnabled = true;
            this.cmbReverseY.Items.AddRange(new object[] {
            "true",
            "false"});
            this.cmbReverseY.Location = new System.Drawing.Point(296, 30);
            this.cmbReverseY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbReverseY.Name = "cmbReverseY";
            this.cmbReverseY.Size = new System.Drawing.Size(77, 21);
            this.cmbReverseY.TabIndex = 3;
            this.cmbReverseY.SelectedIndexChanged += new System.EventHandler(this.cmbReverseY_SelectedIndexChanged);
            // 
            // cmbReverseX
            // 
            this.cmbReverseX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReverseX.FormattingEnabled = true;
            this.cmbReverseX.Items.AddRange(new object[] {
            "true",
            "false"});
            this.cmbReverseX.Location = new System.Drawing.Point(115, 30);
            this.cmbReverseX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbReverseX.Name = "cmbReverseX";
            this.cmbReverseX.Size = new System.Drawing.Size(77, 21);
            this.cmbReverseX.TabIndex = 2;
            this.cmbReverseX.SelectedIndexChanged += new System.EventHandler(this.cmbReversX_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(208, 34);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "垂直翻转";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 34);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "水平翻转";
            // 
            // btnCalib
            // 
            this.btnCalib.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCalib.Location = new System.Drawing.Point(7, 402);
            this.btnCalib.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCalib.Name = "btnCalib";
            this.btnCalib.Size = new System.Drawing.Size(116, 32);
            this.btnCalib.TabIndex = 31;
            this.btnCalib.Text = "一键校准";
            this.btnCalib.UseVisualStyleBackColor = true;
            this.btnCalib.Visible = false;
            this.btnCalib.Click += new System.EventHandler(this.btnCalib_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_lbl_Gain);
            this.groupBox3.Controls.Add(this.m_txt_Gain);
            this.groupBox3.Controls.Add(this.m_lbl_Shutter);
            this.groupBox3.Controls.Add(this.m_txt_Shutter);
            this.groupBox3.Location = new System.Drawing.Point(7, 191);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(385, 92);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "基本参数设置";
            // 
            // m_lbl_Gain
            // 
            this.m_lbl_Gain.AutoSize = true;
            this.m_lbl_Gain.Location = new System.Drawing.Point(27, 57);
            this.m_lbl_Gain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lbl_Gain.Name = "m_lbl_Gain";
            this.m_lbl_Gain.Size = new System.Drawing.Size(31, 13);
            this.m_lbl_Gain.TabIndex = 15;
            this.m_lbl_Gain.Text = "增益";
            // 
            // m_txt_Gain
            // 
            this.m_txt_Gain.Location = new System.Drawing.Point(267, 58);
            this.m_txt_Gain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_txt_Gain.Name = "m_txt_Gain";
            this.m_txt_Gain.Size = new System.Drawing.Size(89, 20);
            this.m_txt_Gain.TabIndex = 16;
            this.m_txt_Gain.Leave += new System.EventHandler(this.m_txt_Gain_Leave);
            // 
            // m_lbl_Shutter
            // 
            this.m_lbl_Shutter.AutoSize = true;
            this.m_lbl_Shutter.Location = new System.Drawing.Point(27, 30);
            this.m_lbl_Shutter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lbl_Shutter.Name = "m_lbl_Shutter";
            this.m_lbl_Shutter.Size = new System.Drawing.Size(55, 13);
            this.m_lbl_Shutter.TabIndex = 13;
            this.m_lbl_Shutter.Text = "曝光时间";
            // 
            // m_txt_Shutter
            // 
            this.m_txt_Shutter.Location = new System.Drawing.Point(267, 25);
            this.m_txt_Shutter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_txt_Shutter.Name = "m_txt_Shutter";
            this.m_txt_Shutter.Size = new System.Drawing.Size(88, 20);
            this.m_txt_Shutter.TabIndex = 14;
            this.m_txt_Shutter.Leave += new System.EventHandler(this.m_txt_Shutter_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtCaptureDelay);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.m_cb_TriggerActivation);
            this.groupBox5.Controls.Add(this.m_cb_TriggerSource);
            this.groupBox5.Controls.Add(this.m_btn_SoftTriggerCommand);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.m_cb_TriggerMode);
            this.groupBox5.Location = new System.Drawing.Point(7, 7);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(385, 176);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "触发控制";
            // 
            // txtCaptureDelay
            // 
            this.txtCaptureDelay.Enabled = false;
            this.txtCaptureDelay.Location = new System.Drawing.Point(203, 146);
            this.txtCaptureDelay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCaptureDelay.Name = "txtCaptureDelay";
            this.txtCaptureDelay.Size = new System.Drawing.Size(149, 20);
            this.txtCaptureDelay.TabIndex = 16;
            this.txtCaptureDelay.Text = "0";
            this.txtCaptureDelay.TextChanged += new System.EventHandler(this.txtCaptureDelay_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "触发延迟(毫秒)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "触发模式";
            // 
            // m_cb_TriggerMode
            // 
            this.m_cb_TriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cb_TriggerMode.FormattingEnabled = true;
            this.m_cb_TriggerMode.Location = new System.Drawing.Point(203, 16);
            this.m_cb_TriggerMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_cb_TriggerMode.Name = "m_cb_TriggerMode";
            this.m_cb_TriggerMode.Size = new System.Drawing.Size(149, 21);
            this.m_cb_TriggerMode.TabIndex = 6;
            this.m_cb_TriggerMode.SelectedIndexChanged += new System.EventHandler(this.m_cb_TriggerMode_SelectedIndexChanged);
            // 
            // tbpOCRSetting
            // 
            this.tbpOCRSetting.Controls.Add(this.picTips);
            this.tbpOCRSetting.Controls.Add(this.checkBox1);
            this.tbpOCRSetting.Controls.Add(this.btnFinishSet);
            this.tbpOCRSetting.Controls.Add(this.txtOperateRecords);
            this.tbpOCRSetting.Controls.Add(this.btnOCRRegion);
            this.tbpOCRSetting.Controls.Add(this.btnSearchRegion);
            this.tbpOCRSetting.Controls.Add(this.btnCreatePattern);
            this.tbpOCRSetting.Controls.Add(this.btnSelectModel);
            this.tbpOCRSetting.Location = new System.Drawing.Point(4, 22);
            this.tbpOCRSetting.Name = "tbpOCRSetting";
            this.tbpOCRSetting.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tbpOCRSetting.Size = new System.Drawing.Size(407, 445);
            this.tbpOCRSetting.TabIndex = 2;
            this.tbpOCRSetting.Text = "读取设置";
            this.tbpOCRSetting.UseVisualStyleBackColor = true;
            // 
            // picTips
            // 
            this.picTips.Location = new System.Drawing.Point(120, 176);
            this.picTips.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picTips.Name = "picTips";
            this.picTips.Size = new System.Drawing.Size(284, 266);
            this.picTips.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTips.TabIndex = 7;
            this.picTips.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 327);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "读取二维码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnFinishSet
            // 
            this.btnFinishSet.Location = new System.Drawing.Point(5, 257);
            this.btnFinishSet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFinishSet.Name = "btnFinishSet";
            this.btnFinishSet.Size = new System.Drawing.Size(110, 43);
            this.btnFinishSet.TabIndex = 5;
            this.btnFinishSet.Text = "结束设置";
            this.btnFinishSet.UseVisualStyleBackColor = true;
            this.btnFinishSet.Visible = false;
            this.btnFinishSet.Click += new System.EventHandler(this.btnFinishSet_Click);
            // 
            // txtOperateRecords
            // 
            this.txtOperateRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOperateRecords.Location = new System.Drawing.Point(120, 6);
            this.txtOperateRecords.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOperateRecords.Multiline = true;
            this.txtOperateRecords.Name = "txtOperateRecords";
            this.txtOperateRecords.ReadOnly = true;
            this.txtOperateRecords.Size = new System.Drawing.Size(285, 167);
            this.txtOperateRecords.TabIndex = 4;
            this.txtOperateRecords.Text = "操作说明\r\n1.点击框选特征按钮，用鼠标框选模板特征(唯一且不变的特征)\r\n2.点击框选范围按钮，用鼠标框选整个打标内容在图像上可能出现的范围\r\n3.点击框选按钮" +
    "，用鼠标框选需要读取的字符内容范围";
            // 
            // btnOCRRegion
            // 
            this.btnOCRRegion.Location = new System.Drawing.Point(5, 191);
            this.btnOCRRegion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOCRRegion.Name = "btnOCRRegion";
            this.btnOCRRegion.Size = new System.Drawing.Size(110, 43);
            this.btnOCRRegion.TabIndex = 3;
            this.btnOCRRegion.Text = "框选内容";
            this.btnOCRRegion.UseVisualStyleBackColor = true;
            this.btnOCRRegion.Click += new System.EventHandler(this.btnOCRRegion_Click);
            this.btnOCRRegion.MouseEnter += new System.EventHandler(this.btnOCRRegion_MouseEnter);
            // 
            // btnSearchRegion
            // 
            this.btnSearchRegion.Location = new System.Drawing.Point(5, 129);
            this.btnSearchRegion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearchRegion.Name = "btnSearchRegion";
            this.btnSearchRegion.Size = new System.Drawing.Size(110, 43);
            this.btnSearchRegion.TabIndex = 2;
            this.btnSearchRegion.Text = "框选范围";
            this.btnSearchRegion.UseVisualStyleBackColor = true;
            this.btnSearchRegion.Click += new System.EventHandler(this.btnSearchRegion_Click);
            this.btnSearchRegion.MouseEnter += new System.EventHandler(this.btnSearchRegion_MouseEnter);
            // 
            // btnCreatePattern
            // 
            this.btnCreatePattern.Location = new System.Drawing.Point(5, 68);
            this.btnCreatePattern.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreatePattern.Name = "btnCreatePattern";
            this.btnCreatePattern.Size = new System.Drawing.Size(110, 43);
            this.btnCreatePattern.TabIndex = 1;
            this.btnCreatePattern.Text = "框选特征";
            this.btnCreatePattern.UseVisualStyleBackColor = true;
            this.btnCreatePattern.Click += new System.EventHandler(this.btnCreatePattern_Click);
            this.btnCreatePattern.MouseEnter += new System.EventHandler(this.btnCreatePattern_MouseEnter);
            // 
            // btnSelectModel
            // 
            this.btnSelectModel.Location = new System.Drawing.Point(5, 6);
            this.btnSelectModel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectModel.Name = "btnSelectModel";
            this.btnSelectModel.Size = new System.Drawing.Size(110, 43);
            this.btnSelectModel.TabIndex = 0;
            this.btnSelectModel.Text = "选择产品";
            this.btnSelectModel.UseVisualStyleBackColor = true;
            this.btnSelectModel.Visible = false;
            this.btnSelectModel.Click += new System.EventHandler(this.btnSelectModel_Click);
            // 
            // tbpAllParams
            // 
            this.tbpAllParams.Controls.Add(this.dgvCCDParams);
            this.tbpAllParams.Location = new System.Drawing.Point(4, 22);
            this.tbpAllParams.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpAllParams.Name = "tbpAllParams";
            this.tbpAllParams.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpAllParams.Size = new System.Drawing.Size(407, 445);
            this.tbpAllParams.TabIndex = 3;
            this.tbpAllParams.Text = "全部参数";
            this.tbpAllParams.UseVisualStyleBackColor = true;
            // 
            // dgvCCDParams
            // 
            this.dgvCCDParams.AllowUserToAddRows = false;
            this.dgvCCDParams.AllowUserToDeleteRows = false;
            this.dgvCCDParams.AllowUserToResizeRows = false;
            this.dgvCCDParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCCDParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvCCDParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCCDParams.Location = new System.Drawing.Point(2, 2);
            this.dgvCCDParams.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvCCDParams.MultiSelect = false;
            this.dgvCCDParams.Name = "dgvCCDParams";
            this.dgvCCDParams.ReadOnly = true;
            this.dgvCCDParams.RowHeadersVisible = false;
            this.dgvCCDParams.RowHeadersWidth = 51;
            this.dgvCCDParams.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCCDParams.RowTemplate.Height = 24;
            this.dgvCCDParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCCDParams.Size = new System.Drawing.Size(403, 441);
            this.dgvCCDParams.TabIndex = 3;
            this.dgvCCDParams.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCCDParams_CellMouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ename";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "值";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "setting";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "备注";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "type";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            this.Column7.Width = 125;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(514, 471);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 491);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Setting";
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.Shown += new System.EventHandler(this.Setting_Shown);
            this.tbpImageFilter.ResumeLayout(false);
            this.tbpImageFilter.PerformLayout();
            this.grpImageProcess.ResumeLayout(false);
            this.grpImageProcess.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbpCam.ResumeLayout(false);
            this.tbpCam.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tbpOCRSetting.ResumeLayout(false);
            this.tbpOCRSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTips)).EndInit();
            this.tbpAllParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCCDParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tbpImageFilter;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.GroupBox grpImageProcess;
        private System.Windows.Forms.TextBox txtFilterC;
        private System.Windows.Forms.TextBox txtBlocksize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbtnBinaryInv;
        private System.Windows.Forms.RadioButton rdbtnBinary;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtErodeY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtErodeX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtDilateY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDilateX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkImageProcess;
        private System.Windows.Forms.ComboBox m_cb_TriggerActivation;
        private System.Windows.Forms.ComboBox m_cb_TriggerSource;
        private System.Windows.Forms.Button m_btn_SoftTriggerCommand;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpCam;
        private System.Windows.Forms.CheckBox chkOutput;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbReverseY;
        private System.Windows.Forms.ComboBox cmbReverseX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCalib;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label m_lbl_Gain;
        private System.Windows.Forms.TextBox m_txt_Gain;
        private System.Windows.Forms.Label m_lbl_Shutter;
        private System.Windows.Forms.TextBox m_txt_Shutter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_cb_TriggerMode;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tbpOCRSetting;
        private System.Windows.Forms.TabPage tbpAllParams;
        private System.Windows.Forms.DataGridView dgvCCDParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtOperateRecords;
        private System.Windows.Forms.Button btnOCRRegion;
        private System.Windows.Forms.Button btnSearchRegion;
        private System.Windows.Forms.Button btnCreatePattern;
        private System.Windows.Forms.Button btnSelectModel;
        private System.Windows.Forms.Button btnFinishSet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tbpRecipe;
        private System.Windows.Forms.TextBox txtCaptureDelay;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.PictureBox picTips;
    }
}