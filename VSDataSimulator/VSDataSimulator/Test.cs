using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSDataSimulator
{
    public partial class TestFrm : Form
    {
        int Read_Data_Status = 1;
        string data = "";
        public TestFrm()
        {
            InitializeComponent();
        }

        private void TestFrm_Load(object sender, EventArgs e)
        {
            //bkWorker2.RunWorkerAsync();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if(!sp.IsOpen)
                sp.Open();
            byte[] cmd = new byte[] { 0xaa, 0x9b, 0xcc, 0x33, 0xc3, 0x3c };
            //byte[] cmd = new byte[] { 0xaa, 0x82, 0x00,0x08,0x00,0x00, 0x39, 0x00, 0x03, 0x00, 0x0B,   0x00, 0x64,    0xcc,0x33,0xc3, 0x3c };
            sp.Write(cmd, 0, cmd.Length);
        }

        private void sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Read_Data_Status = 1;
            int len = 14;
            byte[] ReDatas = new byte[14];
            for (int i = 0; i < 14;)
            {
                if (sp.BytesToRead > 0)
                {
                    sp.Read(ReDatas, i, 1);
                    i++;
                }
            }

            data = "";
            foreach (byte b in ReDatas)
            {
                data = data + "  " + b.ToString();
            }

            //int len = 2;
            //byte[] ReDatas = new byte[len];
            //for (int i = 0; i < 2;)
            //{
            //    if (sp.BytesToRead > 0)
            //    {
            //        sp.Read(ReDatas, i, 1);
            //        i++;
            //    }
            //}

            //data = "";
            //foreach (byte b in ReDatas)
            //{
            //    data = data + "  " + b.ToString();
            //}
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { lbRs.Text = data; }));
            }
            Read_Data_Status = 0;
        }

        private void bkWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                BackgroundWorker bk = (BackgroundWorker)sender;
                if (bk.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                bk.ReportProgress(1, Read_Data_Status);
                Thread.Sleep(10);
            }
        }

        private void bkWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int s = (int)e.UserState;   //获得所报告的GenDataOrSend_Flag变量值
            if (s == 0)   //不处于读数据
            {
                //ShowData(lv);
                lbRs.Text = data;
                Read_Data_Status = 1;
                
            }
        }

        private void btnTestArrayList_Click(object sender, EventArgs e)
        {
            ArrayList aList = new ArrayList();
            byte[] bs = new byte[] { 0xaa, 0x3a, 0x3e, 0x82 };
            foreach(byte b in bs)
            {
                aList.Add(b);
            }
            //aList.Add(170);
            //aList.Add(58);
            //aList.Add(0x3e);
            //aList.Add(0x82);
            int xPos = aList.IndexOf((byte)170);
            int yPos = aList.IndexOf((byte)58);
            int zPos = aList.IndexOf(0xcc);
            MessageBox.Show("0xaa Position: " + xPos + ", 0x82 Position: " + yPos + ", 0xcc Position: " + zPos);

            Queue<byte> qList = new Queue<byte>();
            qList.Enqueue(0xaa);
            qList.Enqueue(58);
            qList.Enqueue(0x3e);
            qList.Enqueue(0x82);

            int i = qList.ToArray().ToList().IndexOf(0x3a);
            for (int k = 0; k < i; k++)
                qList.Dequeue();
            string s = "";
            foreach (byte b in qList)
                s = s + b + ",";
            //MessageBox.Show(s);

        }

        private void btnFloatToByte_Click(object sender, EventArgs e)
        {
            Console.WriteLine("IsLittleEndian: " + BitConverter.IsLittleEndian);
            MessageBox.Show(BitConverter.IsLittleEndian + "");
            //byte[] b1 = BitConverter.GetBytes(19.62f);
            byte[] b2 = BitConverter.GetBytes(19.625f);
        }
    }
}
