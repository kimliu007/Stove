namespace VSDataSimulator
{
    partial class TestFrm
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
            this.components = new System.ComponentModel.Container();
            this.sp = new System.IO.Ports.SerialPort(this.components);
            this.btnTest = new System.Windows.Forms.Button();
            this.lbRs = new System.Windows.Forms.Label();
            this.bkWorker2 = new System.ComponentModel.BackgroundWorker();
            this.btnTestArrayList = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFloatToByte = new System.Windows.Forms.Button();
            this.txtRS = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sp
            // 
            this.sp.BaudRate = 115200;
            this.sp.PortName = "COM6";
            this.sp.ReadBufferSize = 12;
            this.sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_DataReceived);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(45, 81);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(155, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "测试获取显示屏日期设置";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbRs
            // 
            this.lbRs.AutoSize = true;
            this.lbRs.Location = new System.Drawing.Point(43, 57);
            this.lbRs.Name = "lbRs";
            this.lbRs.Size = new System.Drawing.Size(41, 12);
            this.lbRs.TabIndex = 1;
            this.lbRs.Text = "label1";
            // 
            // bkWorker2
            // 
            this.bkWorker2.WorkerReportsProgress = true;
            this.bkWorker2.WorkerSupportsCancellation = true;
            this.bkWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkWorker2_DoWork);
            this.bkWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkWorker2_ProgressChanged);
            // 
            // btnTestArrayList
            // 
            this.btnTestArrayList.Location = new System.Drawing.Point(45, 112);
            this.btnTestArrayList.Name = "btnTestArrayList";
            this.btnTestArrayList.Size = new System.Drawing.Size(155, 23);
            this.btnTestArrayList.TabIndex = 2;
            this.btnTestArrayList.Text = "测试ArrayList";
            this.btnTestArrayList.UseVisualStyleBackColor = true;
            this.btnTestArrayList.Click += new System.EventHandler(this.btnTestArrayList_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(263, 264);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 33);
            this.button7.TabIndex = 9;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.DeepPink;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Image = global::VSDataSimulator.Properties.Resources.add_16x16;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(263, 102);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 33);
            this.button6.TabIndex = 8;
            this.button6.Text = "  增加0.2倍";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DeepPink;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Image = global::VSDataSimulator.Properties.Resources.zoom_in_16x16;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(263, 225);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 33);
            this.button5.TabIndex = 7;
            this.button5.Text = "放大2倍";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DeepPink;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Image = global::VSDataSimulator.Properties.Resources.zoom_out_16x16;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(263, 186);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 33);
            this.button4.TabIndex = 6;
            this.button4.Text = "缩小1/2";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DeepPink;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Image = global::VSDataSimulator.Properties.Resources.zoom_in_16x16;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(263, 141);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 33);
            this.button3.TabIndex = 5;
            this.button3.Text = "  减少0.2倍";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DeepPink;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Image = global::VSDataSimulator.Properties.Resources.cut_16x16;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(263, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 33);
            this.button2.TabIndex = 4;
            this.button2.Text = "单次采集";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepPink;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Image = global::VSDataSimulator.Properties.Resources.right_16x16;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(263, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "连续采集";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnFloatToByte
            // 
            this.btnFloatToByte.Location = new System.Drawing.Point(45, 147);
            this.btnFloatToByte.Name = "btnFloatToByte";
            this.btnFloatToByte.Size = new System.Drawing.Size(155, 23);
            this.btnFloatToByte.TabIndex = 10;
            this.btnFloatToByte.Text = "测试1.25高低字节";
            this.btnFloatToByte.UseVisualStyleBackColor = true;
            this.btnFloatToByte.Click += new System.EventHandler(this.btnFloatToByte_Click);
            // 
            // txtRS
            // 
            this.txtRS.Location = new System.Drawing.Point(45, 186);
            this.txtRS.Name = "txtRS";
            this.txtRS.Size = new System.Drawing.Size(155, 21);
            this.txtRS.TabIndex = 11;
            // 
            // TestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 304);
            this.Controls.Add(this.txtRS);
            this.Controls.Add(this.btnFloatToByte);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTestArrayList);
            this.Controls.Add(this.lbRs);
            this.Controls.Add(this.btnTest);
            this.Name = "TestFrm";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.TestFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort sp;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lbRs;
        private System.ComponentModel.BackgroundWorker bkWorker2;
        private System.Windows.Forms.Button btnTestArrayList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnFloatToByte;
        private System.Windows.Forms.TextBox txtRS;
    }
}