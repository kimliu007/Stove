namespace VSDataSimulator
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lv = new System.Windows.Forms.ListView();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSerialPorts = new System.Windows.Forms.ComboBox();
            this.ComDevice = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bkWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rd_Unicode = new System.Windows.Forms.RadioButton();
            this.rd_UTF8 = new System.Windows.Forms.RadioButton();
            this.rd_ASCII = new System.Windows.Forms.RadioButton();
            this.rd_Hex = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvCmd = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv.Location = new System.Drawing.Point(9, 19);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(188, 244);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(484, 48);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "串口：";
            // 
            // cbSerialPorts
            // 
            this.cbSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerialPorts.FormattingEnabled = true;
            this.cbSerialPorts.Location = new System.Drawing.Point(53, 20);
            this.cbSerialPorts.Name = "cbSerialPorts";
            this.cbSerialPorts.Size = new System.Drawing.Size(121, 20);
            this.cbSerialPorts.TabIndex = 4;
            // 
            // ComDevice
            // 
            this.ComDevice.BaudRate = 115200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSerialPorts);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 82);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前串口设置";
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(248, 51);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(121, 20);
            this.cbParity.TabIndex = 15;
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(53, 51);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(121, 20);
            this.cbStopBits.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "校验方式：";
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(438, 20);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(121, 20);
            this.cbDataBits.TabIndex = 12;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(248, 20);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(121, 20);
            this.cbBaudRate.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(385, 48);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(37, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "停止位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "数据位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "波特率：";
            // 
            // bkWorker1
            // 
            this.bkWorker1.WorkerReportsProgress = true;
            this.bkWorker1.WorkerSupportsCancellation = true;
            this.bkWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkWorker1_DoWork);
            this.bkWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkWorker1_ProgressChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rd_Unicode);
            this.groupBox2.Controls.Add(this.rd_UTF8);
            this.groupBox2.Controls.Add(this.rd_ASCII);
            this.groupBox2.Controls.Add(this.rd_Hex);
            this.groupBox2.Location = new System.Drawing.Point(13, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 44);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编码方式";
            // 
            // rd_Unicode
            // 
            this.rd_Unicode.AutoSize = true;
            this.rd_Unicode.Location = new System.Drawing.Point(442, 23);
            this.rd_Unicode.Name = "rd_Unicode";
            this.rd_Unicode.Size = new System.Drawing.Size(65, 16);
            this.rd_Unicode.TabIndex = 10;
            this.rd_Unicode.Text = "Unicode";
            this.rd_Unicode.UseVisualStyleBackColor = true;
            this.rd_Unicode.Visible = false;
            // 
            // rd_UTF8
            // 
            this.rd_UTF8.AutoSize = true;
            this.rd_UTF8.Location = new System.Drawing.Point(315, 23);
            this.rd_UTF8.Name = "rd_UTF8";
            this.rd_UTF8.Size = new System.Drawing.Size(53, 16);
            this.rd_UTF8.TabIndex = 9;
            this.rd_UTF8.Text = "UTF-8";
            this.rd_UTF8.UseVisualStyleBackColor = true;
            this.rd_UTF8.Visible = false;
            // 
            // rd_ASCII
            // 
            this.rd_ASCII.AutoSize = true;
            this.rd_ASCII.Location = new System.Drawing.Point(188, 23);
            this.rd_ASCII.Name = "rd_ASCII";
            this.rd_ASCII.Size = new System.Drawing.Size(53, 16);
            this.rd_ASCII.TabIndex = 8;
            this.rd_ASCII.Text = "ASCII";
            this.rd_ASCII.UseVisualStyleBackColor = true;
            this.rd_ASCII.Visible = false;
            // 
            // rd_Hex
            // 
            this.rd_Hex.AutoSize = true;
            this.rd_Hex.Checked = true;
            this.rd_Hex.Location = new System.Drawing.Point(55, 23);
            this.rd_Hex.Name = "rd_Hex";
            this.rd_Hex.Size = new System.Drawing.Size(59, 16);
            this.rd_Hex.TabIndex = 0;
            this.rd_Hex.TabStop = true;
            this.rd_Hex.Text = "16进制";
            this.rd_Hex.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvCmd);
            this.groupBox3.Location = new System.Drawing.Point(13, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 271);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "执行命令";
            // 
            // lvCmd
            // 
            this.lvCmd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvCmd.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCmd.Location = new System.Drawing.Point(7, 20);
            this.lvCmd.Name = "lvCmd";
            this.lvCmd.Size = new System.Drawing.Size(336, 245);
            this.lvCmd.TabIndex = 1;
            this.lvCmd.UseCompatibleStateImageBehavior = false;
            this.lvCmd.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "时间";
            this.columnHeader2.Width = 97;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "当前触屏命令";
            this.columnHeader1.Width = 181;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lv);
            this.groupBox4.Location = new System.Drawing.Point(378, 136);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 269);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "当前数据";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 417);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrm";
            this.Text = "主窗体";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSerialPorts;
        private System.IO.Ports.SerialPort ComDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.ComponentModel.BackgroundWorker bkWorker1;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rd_Unicode;
        private System.Windows.Forms.RadioButton rd_UTF8;
        private System.Windows.Forms.RadioButton rd_ASCII;
        private System.Windows.Forms.RadioButton rd_Hex;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvCmd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

