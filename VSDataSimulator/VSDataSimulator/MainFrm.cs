using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//1.   https://www.cnblogs.com/Traveller-Lee/p/6940221.html
//2.   http://www.eeworld.com.cn/mcu/article_2017010532971.html
//3.   https://wenku.baidu.com/view/79cf0978f342336c1eb91a37f111f18583d00ca3.html
//   181 2639  9120  王鹏
namespace VSDataSimulator
{
    public partial class MainFrm : Form
    {
        #region 控制变量
        System.Timers.Timer timer1 = null;    //用于定时采集数据
        System.Timers.Timer timer2 = null;    //用于定时发送数据

        int inTimer1 = 0, inTimer2 = 0;
        RunningStatus Running_Flag = RunningStatus.Running;  //界面按钮“开始”和“停止”切换

        private static int NUM_OF_DATAPOINT = 12;
        private double[] Data = new double[NUM_OF_DATAPOINT];

        //与触屏大小有关的参数设置
        static int Length_ScreenSize = 800;
        static int Width_ScreenSize = 480;
        //有关绘图区域的参数设置
        static int Length_Graph_Area = 648;
        static int Width_Graph_Area = Width_ScreenSize;

        //与坐标轴位置有关的参数设置
        static int Offset_Axis_X = Width_Graph_Area / 2;                                    //X轴偏移量（相对于屏幕底部，实际位于屏幕竖直方向的中间位置）
        static int Offset_Axis_Y = 10;                                                      //Y轴偏移量（相对于屏幕左边）
        static int Interval_X_Between = (int)((Length_Graph_Area - Offset_Axis_Y) / 11);    //线段中相邻两点的X坐标间距

        //与触屏按键相关的参数设置
        WAY_OF_DATA_TRANSFER CurrentDataWay = WAY_OF_DATA_TRANSFER.CONTINUOUS;
        double CurrentScalor = 1.0;        //当前放大倍数

        //绘制曲线相关的参数设置
        static int Color_Curve_Line = 0xF800;
        static int Color_Curve_Dot = 0x00FFFF;//Color.BlueViolet;

        ALLOWANCE_ONESTEP OneStepAllowance = ALLOWANCE_ONESTEP.DISABLED;   // ENABLED 表示可以单步采集生成数据， DISABLED：表示单步生成数据停止

        //串口通讯参数
        Boolean Port_Selected_OK = false;   //选择端口完毕
        string PortName = "";
        int BaudRate = 11520;
        int DataBits = 8;
        //decimal StopBits = 1;
        System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.None;
        //string ParityBit = "无";
        System.IO.Ports.Parity ParityBit = System.IO.Ports.Parity.None;

        //采集数据与发送数据标志
        DATA_STATUS GenDataOrSend_Flag = DATA_STATUS.DATA_GENERATING;  //1表示正在采集数据、0表示正在发送数据

        //来自显示屏的信息（可能是点击触屏所产生的9字节命令、也可能是显示屏执行主机发送的命令后反馈代码）
        ArrayList CmdBytesList = new ArrayList();
        #endregion

        public MainFrm()
        {
            InitializeComponent();
            
        }

        //函数GenData_12  --- 用于生成 NUM_OF_DATAPOINT 个数据
        private void GenData_12(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Running_Flag == RunningStatus.Running)
            {
                if (Interlocked.Exchange(ref inTimer1, 1) == 0)  //将1赋予给inTimer1并返回其原始值
                {
                    if (GenDataOrSend_Flag == DATA_STATUS.DATA_GENERATING)    //GenDataOrSend_Flag为1时表示在采集数据
                    {
                        #region 实现单步生成数据
                        if (CurrentDataWay== WAY_OF_DATA_TRANSFER.ONESTEP && OneStepAllowance==ALLOWANCE_ONESTEP.ENABLED)
                        {
                            Random rd = new Random();
                            for (int i = 0; i < NUM_OF_DATAPOINT; i++)
                            {
                                Data[i] = rd.Next(100);
                                //Thread.Sleep(10);
                            }
                            OneStepAllowance = ALLOWANCE_ONESTEP.DISABLED;  
                        }
                        #endregion
                        #region 连续生成数据
                        else if (CurrentDataWay == WAY_OF_DATA_TRANSFER.CONTINUOUS)
                        {
                            Random rd = new Random();
                            for (int i = 0; i < NUM_OF_DATAPOINT; i++)
                            {
                                Data[i] = rd.Next(100);
                                //Thread.Sleep(10);
                            }
                        }
                        #endregion
                    }
                    GenDataOrSend_Flag = DATA_STATUS.DATA_SENDING;
                }//构造12个数据

                Interlocked.Exchange(ref inTimer1, 0);
                //Thread.Sleep(10);
            }
            else
            {
                timer1.Stop();
            }
        }

        //串口数据发送
        private void SerialPortDataSend(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Running_Flag == RunningStatus.Running)
            {
                if (Interlocked.Exchange(ref inTimer2, 1) == 0 )  //将1赋予给inTimer1并返回其原始值
                {
                    #region 定时读串口
                    ////开辟接受缓冲区
                    ////byte[] ReDatas = new byte[ComDevice.BytesToRead];
                    //int len = 9;
                    //byte[] ReDatas = new byte[len];
                    ////从串口读取数据
                    //for (int i = 0; i < len;)
                    //{
                    //    if (ComDevice.BytesToRead > 0)
                    //    {
                    //        ComDevice.Read(ReDatas, i, 1);
                    //        i++;
                    //    }

                    //}

                    ///*
                    // * 1. 连续： 170  121  0  0  0  204  51  195  60
                    // * 2. 单点： 170  121  0  0  1  204  51  195  60
                    // * 3. 开始： 170  121  0  0  2  204  51  195  60
                    //*/
                    //if (ReDatas.Length == 9)  //一个来自触屏的完整的按键命令应该有9个字节
                    //{
                    //    AddCommand(ReDatas);
                    //    if (ReDatas[4] == 0)
                    //    {
                    //        CurrentDataWay = WAY_OF_DATA_TRANSFER.CONTINUOUS;
                    //    }
                    //    else if (ReDatas[4] == 1)
                    //    {
                    //        CurrentDataWay = WAY_OF_DATA_TRANSFER.ONESTEP;
                    //        OneStepAllowance = ALLOWANCE_ONESTEP.ENABLED;  //控制单步生成数据，避免数据缓冲区被连续生成的数据所覆盖
                    //    }
                    //}
                    #endregion
                    //1. 数据发送操作
                    byte[] curveData = CurveDataToBeSend();
                    //byte[] dotData = DotDataToBeSend();
                    byte[] maxValueData = MaxMinScalorValueToBeSend(0);
                    byte[] minValueData = MaxMinScalorValueToBeSend(1);
                    byte[] scalorValueData = MaxMinScalorValueToBeSend(2);

                    byte[] testMaxValueData = TestDigitSendAndShow();
                    if (ComDevice.IsOpen)
                    {
                        try
                        {
                            ComDevice.Write(curveData, 0, curveData.Length);
                            //ComDevice.Write(dotData, 0, dotData.Length);
                            Thread.Sleep(10);
                            //写最大值
                            ComDevice.Write(maxValueData, 0, maxValueData.Length);
                            Thread.Sleep(10);
                            //写最小值
                            ComDevice.Write(minValueData, 0, minValueData.Length);
                            Thread.Sleep(10);
                            //写当前的缩放因子CurrentScalor
                            ComDevice.Write(scalorValueData, 0, scalorValueData.Length);
                            Thread.Sleep(10);
                            //写小数的MaxValue
                            ComDevice.Write(testMaxValueData, 0, testMaxValueData.Length);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("串口处于关闭状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                Interlocked.Exchange(ref inTimer2, 0);
                //Thread.Sleep(10); 
            }
            else
            {
                timer2.Stop();
            }
        }
        //将缓冲区中的数据构建要发送的命令字节
        private byte[] CurveDataToBeSend()
        {
            //为了避免数据过大，超出屏幕的显示范围，应根据当前数据的最大值，调整CurrentScalor大小
            double maxValue = Data.Max();
            double minValue = Data.Min();
            if(maxValue>=0 && maxValue >= (Width_ScreenSize - Offset_Axis_X))
            {
                CurrentScalor = (Width_ScreenSize - Offset_Axis_X) / maxValue;
            }
            if(minValue<0 && Math.Abs(minValue) >= Offset_Axis_X)
            {
                CurrentScalor = Offset_Axis_X / Math.Abs(minValue);
            }
            int x = Offset_Axis_Y;  //Y轴偏移屏幕坐标的距离
            byte[] curveData = new byte[125];
            curveData[0] = 0xAA;
            curveData[1] = 0x82;
            curveData[2] = 0x00;
            curveData[3] = 0x08;
            curveData[4] = 0x00;
            curveData[5] = 0x00;
            curveData[6] = 0x39;
            curveData[7] = 0x00;
            curveData[8] = 0x03;
            curveData[9] = 0x00;
            curveData[10] = 0x0B;
            for(int i=0; i<11; i++)  //11条线段
            {
                curveData[10 + 10 * i + 1] = Util.GetHighByte(x);   //线段起始端点横坐标高字节
                curveData[10 + 10 * i + 2] = Util.GetLowByte(x);    //线段起始端点横坐标低字节
                curveData[10 + 10 * i + 3] = Util.GetHighByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i] * CurrentScalor));//线段起始端点纵坐标高字节
                curveData[10 + 10 * i + 4] = Util.GetLowByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i] * CurrentScalor));//线段起始端点纵坐标低字节
                x = x + Interval_X_Between;
                curveData[10 + 10 * i + 5] = Util.GetHighByte(x);  //线段结束端点横坐标高字节
                curveData[10 + 10 * i + 6] = Util.GetLowByte(x);   //线段结束端点横坐标低字节
                curveData[10 + 10 * i + 7] = Util.GetHighByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i + 1] * CurrentScalor));//线段结束端点纵坐标高字节
                curveData[10 + 10 * i + 8] = Util.GetLowByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i + 1] * CurrentScalor)); //线段结束端点纵坐标低字节

                curveData[10 + 10 * i + 9] = Util.GetHighByte(Color_Curve_Line); //线段颜色高字节
                curveData[10 + 10 * i + 10] = Util.GetLowByte(Color_Curve_Line); //线段颜色低字节
            }
            curveData[121] = Util.GetHighByte(0xCC33);
            curveData[122] = Util.GetLowByte(0xCC33);
            curveData[123] = Util.GetHighByte(0xC33C);
            curveData[124] = Util.GetLowByte(0xC33C);

            return curveData;
        }

        private byte[] DotDataToBeSend()
        {
            double maxValue = Data.Max();
            double minValue = Data.Min();
            if (maxValue >= 0 && maxValue >= (Width_ScreenSize - Offset_Axis_X))
            {
                CurrentScalor = (Width_ScreenSize - Offset_Axis_X) / maxValue;
            }
            if (minValue < 0 && Math.Abs(minValue) >= Offset_Axis_X)
            {
                CurrentScalor = Offset_Axis_X / Math.Abs(minValue);
            }
            int x = Offset_Axis_Y;  //Y轴偏移屏幕坐标的距离
            byte[] dotData = new byte[87];
            dotData[0] = 0xAA;
            dotData[1] = 0x82;
            dotData[2] = 0x00;
            dotData[3] = 0x08;
            dotData[4] = 0x01;
            dotData[5] = 0x10;
            dotData[6] = 0x26;
            dotData[7] = 0x00;
            dotData[8] = 0x01;
            dotData[9] = 0x00;
            dotData[10] = 0x0C;
            for (int i = 0; i < 12; i++)  //12个点
            {
                dotData[10 + 6 * i + 1] = Util.GetHighByte(x);
                dotData[10 + 6 * i + 2] = Util.GetLowByte(x);
                dotData[10 + 6 * i + 3] = Util.GetHighByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i] * CurrentScalor));
                dotData[10 + 6 * i + 4] = Util.GetLowByte(Width_ScreenSize - Offset_Axis_X - (Int16)(Data[i] * CurrentScalor));
                dotData[10 + 6 * i + 5] = Util.GetHighByte(Color_Curve_Dot); //点颜色高字节
                dotData[10 + 6 * i + 6] = Util.GetLowByte(Color_Curve_Dot); //点颜色低字节
                x = x + Interval_X_Between;
            }
            dotData[83] = Util.GetHighByte(0xCC33);
            dotData[84] = Util.GetLowByte(0xCC33);
            dotData[85] = Util.GetHighByte(0xC33C);
            dotData[86] = Util.GetLowByte(0xC33C);
            return dotData;
        }

        private byte[] TestDigitSendAndShow()
        {
            float maxValue = (float)Data.Max();
            byte[] CmdData = new byte[14];
            CmdData[0] = 0xAA;
            CmdData[1] = 0x44;
            CmdData[2] = 0x00;
            CmdData[3] = 0x02;
            CmdData[4] = 0x00;
            CmdData[5] = 0x00;

            byte[] dataBytes = Util.FloatToByte((float)maxValue);
            CmdData[6] = dataBytes[3];
            CmdData[7] = dataBytes[2];
            CmdData[8] = dataBytes[1];
            CmdData[9] = dataBytes[0];

            //CmdData[6] = 0x40;//4020C49C
            //CmdData[7] = 0x20;
            //CmdData[8] = 0xC4;
            //CmdData[9] = 0x9C;

            CmdData[10] = Util.GetHighByte(0xCC33);
            CmdData[11] = Util.GetLowByte(0xCC33);
            CmdData[12] = Util.GetHighByte(0xC33C);
            CmdData[13] = Util.GetLowByte(0xC33C);
            return CmdData;
        }
        private byte[] MaxMinScalorValueToBeSend(int maxOrMinOrScalor)  //0: Max   1: Min   2: CurrentScalor
        {
            float maxValue = (float)Data.Max();
            float minValue = (float)Data.Min();
            float scalorValue = (float)CurrentScalor;
            //byte[] CmdData = new byte[12];
            byte[] CmdData = new byte[14];
            if (maxOrMinOrScalor == 0)
            {
                CmdData[0] = 0xAA;
                CmdData[1] = 0x44;
                CmdData[2] = 0x00;
                CmdData[3] = 0x02;
                CmdData[4] = 0x00;
                CmdData[5] = 0x00;

                //CmdData[6] = Util.GetHighByte((Int16)maxValue);
                //CmdData[7] = Util.GetLowByte((Int16)maxValue);


                byte[] dataBytes = Util.FloatToByte(maxValue);
                CmdData[6] = dataBytes[3];
                CmdData[7] = dataBytes[2];
                CmdData[8] = dataBytes[1];
                CmdData[9] = dataBytes[0];

            }
            else if(maxOrMinOrScalor == 1)
            {
                CmdData[0] = 0xAA;
                CmdData[1] = 0x44;
                CmdData[2] = 0x00;
                CmdData[3] = 0x02;
                CmdData[4] = 0x00;
                CmdData[5] = 0x04;
                //CmdData[6] = Util.GetHighByte((Int16)minValue);
                //CmdData[7] = Util.GetLowByte((Int16)minValue);

                byte[] dataBytes = Util.FloatToByte(minValue);
                CmdData[6] = dataBytes[3];
                CmdData[7] = dataBytes[2];
                CmdData[8] = dataBytes[1];
                CmdData[9] = dataBytes[0];
            }
            else
            {
                CmdData[0] = 0xAA;
                CmdData[1] = 0x44;
                CmdData[2] = 0x00;
                CmdData[3] = 0x02;
                CmdData[4] = 0x00;
                CmdData[5] = 0x08;
                //CmdData[6] = Util.GetHighByte((Int16)CurrentScalor);
                //CmdData[7] = Util.GetLowByte((Int16)CurrentScalor);

                byte[] dataBytes = Util.FloatToByte(scalorValue);
                CmdData[6] = dataBytes[0];
                CmdData[7] = dataBytes[1];
                CmdData[8] = dataBytes[2];
                CmdData[9] = dataBytes[3];
            }
            CmdData[10] = Util.GetHighByte(0xCC33);
            CmdData[11] = Util.GetLowByte(0xCC33);
            CmdData[12] = Util.GetHighByte(0xC33C);
            CmdData[13] = Util.GetLowByte(0xC33C);
            return CmdData;
        }
        //
        private void ShowCmdData(ListView lv, byte[] cmdData)
        {
            if (GenDataOrSend_Flag == DATA_STATUS.DATA_SENDING)
            {

            }
        }
        private void ShowData(ListView lv)
        {
            if (GenDataOrSend_Flag == DATA_STATUS.DATA_SENDING)
            {
                lv.Items.Clear();
                lv.Columns.Clear();
                ColumnHeader ch0 = new ColumnHeader();
                ch0.Text = "位置";
                ch0.Width = 40;
                ch0.TextAlign = HorizontalAlignment.Left;
                lv.Columns.Add(ch0);

                ColumnHeader ch1 = new ColumnHeader();
                ch1.Text = "数值";
                ch1.Width = 120;
                ch1.TextAlign = HorizontalAlignment.Left;
                lv.Columns.Add(ch1);

                lv.BeginUpdate();
                for (int i = 0; i < Data.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(Data[i].ToString());
                    lv.Items.Add(lvi);
                }

                lv.EndUpdate();
            }
        }

        
        private void FillPortOption()
        {
            cbBaudRate.Items.Add("1200");
            cbBaudRate.Items.Add("2400");
            cbBaudRate.Items.Add("4800");
            cbBaudRate.Items.Add("9600");
            cbBaudRate.Items.Add("19200");
            cbBaudRate.Items.Add("38400");
            cbBaudRate.Items.Add("43000");
            cbBaudRate.Items.Add("56000");
            cbBaudRate.Items.Add("57600");
            cbBaudRate.Items.Add("115200");
            cbBaudRate.SelectedIndex = 0;

            cbDataBits.Items.Add("8");
            cbDataBits.Items.Add("7");
            cbDataBits.Items.Add("6");
            cbDataBits.Items.Add("5");
            cbDataBits.SelectedIndex = 0;

            List<ComboData> stopList = new List<ComboData>();
            stopList.Add(new ComboData("0", System.IO.Ports.StopBits.None));
            stopList.Add(new ComboData("1", System.IO.Ports.StopBits.One));
            stopList.Add(new ComboData("1.5", System.IO.Ports.StopBits.OnePointFive));
            stopList.Add(new ComboData("2", System.IO.Ports.StopBits.Two));
            cbStopBits.DataSource = stopList;
            cbStopBits.DisplayMember = "Key";
            cbStopBits.ValueMember = "Value";
            cbStopBits.SelectedIndex = 0;

            List<ComboData> pList = new List<ComboData>();
            pList.Add(new ComboData("无", Parity.None));
            pList.Add(new ComboData("奇校验", Parity.Odd));
            pList.Add(new ComboData("偶校验", Parity.Even));
            cbParity.DataSource = pList;
            cbParity.DisplayMember = "Key";
            cbParity.ValueMember = "Value";

            cbParity.SelectedIndex = 0;

        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            List<string> comList = GetComlist(false); //首先获取本机关联的串行端口列表   
            if (comList.Count == 0)
            {
                MessageBox.Show("当前设备不存在串行端口！", "提示信息");
                System.Environment.Exit(0); //彻底退出应用程序   
            }
            else
            {
                //string targetCOMPort = ConfigurationManager.AppSettings["COMPort"].ToString();
                foreach (string comName in comList)
                {
                    cbSerialPorts.Items.Add(comName);
                }
                cbSerialPorts.SelectedIndex = 0;
            }

            FillPortOption();

            Profile.LoadProfile();
            PortName = Profile.G_COMPORT;
            BaudRate = int.Parse(Profile.G_BAUDRATE);
            DataBits = Convert.ToInt32(Profile.G_DATABITS);
            
            StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), Profile.G_STOP, false);
            
            ParityBit = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), Profile.G_PARITY, false);
            
            for (int i=0; i< cbSerialPorts.Items.Count; i++)
            {
                if (cbSerialPorts.Items[i].ToString() == PortName)
                {
                    cbSerialPorts.SelectedIndex = i;
                    break;
                }
            }

            for(int i=0; i< cbBaudRate.Items.Count; i++)
            {
                if (cbBaudRate.Items[i].ToString() == BaudRate.ToString())
                {
                    cbBaudRate.SelectedIndex = i;
                    break;
                }                    
            }
            for (int i = 0; i < cbDataBits.Items.Count; i++)
            {
                if (cbDataBits.Items[i].ToString() == DataBits.ToString())
                {
                    cbDataBits.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cbStopBits.Items.Count; i++)
            {
                String s = ((ComboData)cbStopBits.Items[i]).Value.ToString();
                if (s == StopBits.ToString())
                {
                    cbStopBits.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cbParity.Items.Count; i++)
            {
                String s = ((ComboData)cbParity.Items[i]).Value.ToString();
                if (s == ParityBit.ToString())
                {
                    cbParity.SelectedIndex = i;
                    break;
                }
            }
            
            //
            timer1 = new System.Timers.Timer();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(GenData_12);
            timer1.Interval = 1000;
            //timer1.Enabled = true;
                      
            timer2 = new System.Timers.Timer();
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(SerialPortDataSend);
            timer2.Interval = 1000;
            //timer2.Enabled = true;

            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);

        }

        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            #region 注释
            ////开辟接受缓冲区
            ////byte[] ReDatas = new byte[ComDevice.BytesToRead];
            //int len =ComDevice.BytesToRead;
            //byte[] ReDatas = new byte[len];
            ////从串口读取数据
            //if (Running_Flag == RunningStatus.Running && len == 9)
            ////if (Running_Flag == RunningStatus.Running)
            //{
            //    for (int i = 0; i < len;)
            //    {
            //        if (ComDevice.BytesToRead > 0)
            //        {
            //            ComDevice.Read(ReDatas, i, 1);
            //            i++;
            //        }

            //    }


            //    /*
            //     * 1. 连续： 170  121  0  0  0  204  51  195  60
            //     * 2. 单点： 170  121  0  0  1  204  51  195  60
            //     * 3. 开始： 170  121  0  0  2  204  51  195  60
            //    */
            //    if (ReDatas.Length == 9)  //一个来自触屏的完整的按键命令应该有9个字节
            //    {
            //        AddCommand(ReDatas);
            //        if (ReDatas[4] == 0)
            //        {
            //            CurrentDataWay = WAY_OF_DATA_TRANSFER.CONTINUOUS;
            //        }
            //        else if (ReDatas[4] == 1)
            //        {
            //            CurrentDataWay = WAY_OF_DATA_TRANSFER.ONESTEP;
            //            OneStepAllowance = ALLOWANCE_ONESTEP.ENABLED;  //控制单步生成数据，避免数据缓冲区被连续生成的数据所覆盖
            //        }
            //    }
            //}
            #endregion
            int len = ComDevice.BytesToRead;
            byte[] ReDatas = new byte[len];
            if (Running_Flag == RunningStatus.Running)
            {
                ComDevice.Read(ReDatas, 0, len);
                foreach(byte b in ReDatas)
                    CmdBytesList.Add(b);

                //命令组合
                /*   0x3A 0x3E ===> 接收到正确格式的指令帧并已执行指令,等待下一条指令 
                 *   0x21 0x3E ===> 接收到正确格式的指令帧但指令错误，等待下一条指令 
                 *   0xAA 0x82 ===> 
                 *   0xAA 0X79 ===> 接收到触屏，后续将是7个字节的数据，共9字节的命令
                 *   0xAA 0X78 ===> 接收到触屏，后续将是7个字节的数据，共9字节的命令
                 */
                int xAA_Pos = CmdBytesList.IndexOf((byte)170);   //170 == 0xaa
                int x79_Pos = CmdBytesList.IndexOf((byte)121);   //121 == 0x79
                int x78_Pos = CmdBytesList.IndexOf((byte)120);   //120 == 0x78

                int x3A_Pos = CmdBytesList.IndexOf((byte)58);    //58  == 0x3A
                int x3E_Pos = CmdBytesList.IndexOf((byte)62);    //62  == 0x3E
                int x21_Pos = CmdBytesList.IndexOf((byte)33);    //33  == 0x21

                int xCC_Pos = CmdBytesList.IndexOf((byte)204);   //204 == 0xCC
                int x33_Pos = CmdBytesList.IndexOf((byte)51);    //51  == 0x33
                int xC3_Pos = CmdBytesList.IndexOf((byte)195);   //195 == 0xC3
                int x3C_Pos = CmdBytesList.IndexOf((byte)60);    //60  == 0x3C
                #region 判断触屏键
                if ((xAA_Pos>=0 && x78_Pos >= 0) || (xAA_Pos >= 0 && x79_Pos >= 0))
                {
                    if (CmdBytesList.Count >= 9)
                    {
                        if(xCC_Pos == xAA_Pos + 5 && x33_Pos == xCC_Pos +1 && xC3_Pos ==x33_Pos+1 && x3C_Pos == xC3_Pos + 1)
                        {
                            //合法的触屏键被触发
                            byte[] touchKeyData = new byte[9];
                            for (int i = 0; i < 9; i++)
                            {
                                touchKeyData[i] = (byte)CmdBytesList[xAA_Pos + i];
                            }
                            AddCommand(touchKeyData);
                            //触屏键号
                            byte keyID = (byte)touchKeyData[4];
                            if(keyID==0)
                                CurrentDataWay = WAY_OF_DATA_TRANSFER.CONTINUOUS;
                            else if (keyID == 1)
                            {
                                CurrentDataWay = WAY_OF_DATA_TRANSFER.ONESTEP;
                                OneStepAllowance = ALLOWANCE_ONESTEP.ENABLED;  //控制单步生成数据，避免数据缓冲区被连续生成的数据所覆盖
                            }
                            else if (keyID == 2)
                            {
                                CurrentScalor = CurrentScalor + 0.2;
                            }
                            else if(keyID == 3)
                            {
                                CurrentScalor = CurrentScalor - 0.2;
                            }
                            else if (keyID == 4)
                            {
                                CurrentScalor = CurrentScalor * 2;
                            }
                            else if (keyID == 5)
                            {
                                CurrentScalor = CurrentScalor / 2;
                            }
                            //清除CmdBytesList中x3C_Pos位置及之前的所有元素
                            CmdBytesList.RemoveRange(0, x3C_Pos + 1);                            
                        }
                    }
                }
                #endregion

            }
            //else if (Running_Flag == RunningStatus.Stopped)
            //{
            //    bkWorker1.CancelAsync();
            //}
        }
        private void AddCommand(byte[] data)
        {
            if (rd_Hex.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for(int i=0; i<data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                ShowCommand(sb.ToString().ToUpper());
            }
            else if (rd_ASCII.Checked)
            {
                ShowCommand(new ASCIIEncoding().GetString(data));
            }
            else if (rd_UTF8.Checked)
            {
                ShowCommand(new UTF8Encoding().GetString(data));
            }
            else if (rd_Unicode.Checked)
            {
                ShowCommand(new UnicodeEncoding().GetString(data));
            }
        }
        //接收端的ListView显示接收到的命令代码
        private void ShowCommand(string content)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                //往命令ListView控件中添加数据
                DateTime now = DateTime.Now;
                
                lvCmd.BeginUpdate();
               
                ListViewItem lvi = new ListViewItem();
                lvi.Text = now.ToLongTimeString();
                lvi.SubItems.Add(content);
                lvCmd.Items.Insert(0, lvi);

                lvCmd.EndUpdate();
            }));
        }

        private List<string> GetComlist(bool isUseReg)
        {
            List<string> list = new List<string>();
            try
            {
                if (isUseReg)
                {
                    RegistryKey RootKey = Registry.LocalMachine;
                    RegistryKey Comkey = RootKey.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");

                    String[] ComNames = Comkey.GetValueNames();

                    foreach (String ComNamekey in ComNames)
                    {
                        string TemS = Comkey.GetValue(ComNamekey).ToString();
                        list.Add(TemS);
                    }
                }
                else
                {
                    foreach (string com in SerialPort.GetPortNames())  //自动获取串行口名称  
                        list.Add(com);
                }
            }
            catch
            {
                MessageBox.Show( "串行端口检查异常！", "提示信息");
                System.Environment.Exit(0); //彻底退出应用程序   
            }
            return list;
        }

        //定时刷新界面，以显示采集到的最新完整数据
        private void bkWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true )
            {

                    BackgroundWorker bk = (BackgroundWorker)sender;
                    if (bk.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    bk.ReportProgress(1, GenDataOrSend_Flag);

                    Thread.Sleep(10);
                
            }
        }

        private void bkWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int s = (int)e.UserState;   //获得所报告的GenDataOrSend_Flag变量值
            if (s == 0)   //不处于采集数据状态，可以显示所采集到的数据
            {
                ShowData(lv);
                GenDataOrSend_Flag = DATA_STATUS.DATA_GENERATING;
            }
        }

        private bool CheckPortSetting()
        {
            if (cbSerialPorts.Text.Trim() == "")
                return false;
            if (cbBaudRate.SelectedIndex == -1)
                return false;
            if (cbDataBits.SelectedIndex == -1)
                return false;
            if (cbParity.SelectedIndex == -1)
                return false;

            return true;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(ComDevice.IsOpen == false)
            {
                GenDataOrSend_Flag = DATA_STATUS.DATA_GENERATING;
                inTimer1 = 0;
                inTimer2 = 0;

                BaudRate = Convert.ToInt32(cbBaudRate.Text);
                DataBits = Convert.ToInt32(cbDataBits.Text);
                
                StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), cbStopBits.SelectedValue.ToString(), false);
                
                ParityBit = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), cbParity.SelectedValue.ToString(), false);
                PortName = cbSerialPorts.Text;

                ComDevice.PortName = PortName;
                ComDevice.BaudRate = BaudRate;
                ComDevice.DataBits = DataBits;
                ComDevice.StopBits = StopBits;
                ComDevice.Parity = ParityBit;
                try
                {
                    ComDevice.Open();
                    Running_Flag = RunningStatus.Running;
                    timer1.Start();
                    
                    bkWorker1.RunWorkerAsync();

                    timer2.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "未能成功开启串口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnStart.Text = "停止";
            }
            else
            {
                try
                {
                    ComDevice.Close();
                    Running_Flag = RunningStatus.Stopped;
                    bkWorker1.CancelAsync();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "串口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnStart.Text = "开始";
            }
            cbSerialPorts.Enabled = !ComDevice.IsOpen;
            cbBaudRate.Enabled = !ComDevice.IsOpen;
            cbDataBits.Enabled = !ComDevice.IsOpen;
            cbStopBits.Enabled = !ComDevice.IsOpen;
            cbParity.Enabled = !ComDevice.IsOpen;
            
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bkWorker1.CancelAsync();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckPortSetting())
            {
                Profile.G_BAUDRATE = cbBaudRate.Text;
                Profile.G_COMPORT = cbSerialPorts.Text;
                Profile.G_DATABITS = cbDataBits.Text;
                Profile.G_STOP = cbStopBits.SelectedValue.ToString();
                Profile.G_PARITY = cbParity.SelectedValue.ToString();
                
                Profile.SaveProfile();

                BaudRate = Convert.ToInt32(cbBaudRate.Text);
                DataBits = Convert.ToInt32(cbDataBits.Text);
                //StopBits = Convert.ToDecimal(cbStopBits.Text);
                StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits),cbStopBits.SelectedValue.ToString(), false);
                //ParityBit = cbParity.SelectedValue.ToString();
                ParityBit = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), cbParity.SelectedValue.ToString(), false);
                PortName = cbSerialPorts.Text;

                ComDevice.PortName = PortName;
                ComDevice.BaudRate = BaudRate;
                ComDevice.DataBits = DataBits;
                ComDevice.StopBits = System.IO.Ports.StopBits.One;
                ComDevice.Parity = ParityBit;

                Port_Selected_OK = true;
            }
        }
    }

    //运行状态
    public enum RunningStatus
    {
        Running=1,
        Stopped=0
    }
    //采集数据抑或发送数据
    public enum DATA_STATUS
    {
        DATA_GENERATING = 1,
        DATA_SENDING = 0
    }
    //命令类型
    public enum WAY_OF_DATA_TRANSFER
    {
        CONTINUOUS=1,
        ONESTEP=2
    }
    //
    public enum ALLOWANCE_ONESTEP
    {
        ENABLED = 1,
        DISABLED = 0
    }
    //
    public class ComboData
    {
        String key;
        Object value;
        public ComboData(String m_key, object m_value)
        {
            this.key = m_key;
            this.value = m_value;
        }
        public String Key
        {
            get { return key; }
            set { key = value; }
        }
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
