using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ivi.Visa.Interop;
using System.Threading;
using ModbusDll;
using System.Xml;

namespace AnalogMultimeters
{
    public partial class MainFrom : Form
    {
        private Ivi.Visa.Interop.ResourceManager m_rm;
        private Ivi.Visa.Interop.FormattedIO488 m_ioArbFG;
        private Ivi.Visa.Interop.IMessage m_msg;
        Thread th_MonitorStatue = null;//监听到位信号
        Thread th_RefleshFrom = null;//刷新下位机和万用表的连接
        Mutex ReadMutex = new Mutex();
        Mutex ReflashShow = new Mutex();

        MyFunctions myfunction = new MyFunctions();
        public enum MsgType
        {
            RcvMsg = 1,
            SendMsg = 2,
            ErrorMsg = 3,
            WaitTest = 4
        };
        public enum ResultView
        {
            TestOne = 1,
            TestTwo = 2,
            TestThree = 3,
            TestFour = 4,
            TestResult = 5,
            TestClear = 6
        };
        public MainFrom()
        {
            InitializeComponent();
            try
            {
                m_ioArbFG = new FormattedIO488();
                m_rm = new Ivi.Visa.Interop.ResourceManager();
            }
            catch
            {
                MessageBox.Show("请先安装安捷伦IO驱动", "ERROR");
            }
        }

        //初始化并连接万用表设备
        private bool ConnectDevice()
        {
            try
            {
                string conn_str = "TCPIP0::" + GlobalVar.gl_MultimetersIP + "::inst0::INSTR";
                m_msg = m_rm.Open(conn_str, Ivi.Visa.Interop.AccessMode.NO_LOCK, 1000, "")
                     as Ivi.Visa.Interop.IMessage;
                m_ioArbFG.IO = m_msg;
                GlobalVar.gl_UpperMonitor = true;
                return true;
            }
            catch
            {
                GlobalVar.gl_UpperMonitor = false;
                MessageBox.Show("仪器连接时发生错误", "ERROR");
                return false;
            }

        }
        //关闭万用表连接
        public void disposeNetWorkConnect()
        {
            if (m_msg != null || m_ioArbFG.IO != null)
            {
                m_msg = null;
                m_ioArbFG.IO = null;
            }
            Thread.Sleep(50);
        }
        /// <summary>
        /// 万用表写测量指令
        /// </summary>
        /// <param name="Instructions"></param>
        public void MultimetersSend(string Instructions)
        {
            //m_ioArbFG.WriteString(Instructions + ";*WAI", true);
            m_ioArbFG.WriteString(Instructions, true);

        }
        /// <summary>
        /// 万用表读测量值
        /// </summary>
        /// <returns></returns>
        public string MultimetersRead()
        {
            string GetValue = "";
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(5);
                    GetValue = m_ioArbFG.ReadString();
                    if (GetValue != null && GetValue != "")
                        break;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return GetValue;
        }
        /// <summary>
        /// 写入线圈
        /// </summary>
        /// <param name="StartAdress">modbus地址</param>
        public void ModBusSend(string Function, int StartAdress)
        {
            try
            {

                if (GlobalVar.gl_Portcon != null)
                {
                    byte writevalue = new byte();
                    writevalue = (byte)Convert.ToInt16(Function.Substring(1, Function.Length - 1), 2);
                    InputModule input = new InputModule();
                    input.bySlaveID = 1;
                    input.byFunction = GlobalVar.g_WriteMultiCoils;
                    switch (Function.Substring(0, 1))
                    {

                        case "S":
                            input.nStartAddr = StartAdress;
                            input.nDataLength = GlobalVar.gl_WordLength;
                            input.byWriteData = new byte[] { writevalue };
                            GlobalVar.gl_Portcon.SendMessage_Sync(input);
                            break;
                        case "E":
                            input.nStartAddr = StartAdress;
                            input.nDataLength = GlobalVar.gl_WordLongLength;
                            input.byWriteData = new byte[] { writevalue };
                            GlobalVar.gl_Portcon.SendMessage_Sync(input);
                            break;
                        default:
                            myfunction.writelog("传入指令有误" + Function.ToString());
                            break;

                    }
                }
                else
                {
                    myfunction.writelog("写入异常，Modbus为空，未打开");

                }
            }
            catch (Exception ex)
            {
                myfunction.writelog("写入地址" + StartAdress.ToString() + "异常," + ex.ToString());
            }

        }
        /// <summary>
        /// 读取Modbus地址信息
        /// </summary>
        /// <param name="StartAdress">地址</param>
        /// <returns></returns>
        public string ModbusRead(int StartAdress)
        {
            ReadMutex.WaitOne();
            try
            {
                string str = string.Empty;
                InputModule input = new InputModule();
                if (GlobalVar.gl_Portcon == null) { ReadMutex.ReleaseMutex(); myfunction.writelog("读取异常，Modbus为空，未打开"); return ""; }
                input.bySlaveID = 1;
                input.byFunction = GlobalVar.g_ReadCoil;
                input.nStartAddr = StartAdress;
                input.nDataLength = GlobalVar.gl_WordLength;
                OutputModule rev = GlobalVar.gl_Portcon.SendMessage_Sync(input);
                Thread.Sleep(100);
                if ((rev == null) || (rev.byFunction != input.byFunction))
                {
                    return "";
                }
                else
                {
                    if (rev.byFunction == 1)
                    {
                        str = rev.byRecvData[9].ToString();
                    }
                }
                ReadMutex.ReleaseMutex();
                return str;
            }
            catch (Exception ex)
            {
                myfunction.writelog("读取Modbus异常,Modbus地址:" + StartAdress.ToString() + "异常原因" + ex.ToString());
                ReadMutex.ReleaseMutex();
                return "Error";
            }
        }
        /// <summary>
        /// 监听下位机状态
        /// </summary>
        public void ListenSatue()
        {
            while (true)
            {

                if (ModbusRead(GlobalVar.gl_SolenoidvalveSignal) == "1")//判断气缸到位，1表示到位
                {
                    if (GlobalVar.gl_TestComplete)
                    {
                        GlobalVar.gl_TestPcsCount = (int.Parse(GlobalVar.gl_TestPcsCount) + 1).ToString();
                        GlobalVar.gl_TestComplete = false;
                        CommLogDisplay("气缸到位", MsgType.RcvMsg);
                        string[] arrylist = new string[3];
                        arrylist[0] = "0";
                        arrylist[1] = "0";
                        arrylist[2] = "6";
                        RefleshResultView(arrylist);//清除显示
                        Thread.Sleep(100);
                        MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_StartTest, (IntPtr)0, (IntPtr)0);
                        Thread.Sleep(2000);
                        GlobalVar.gl_TestComplete = true;
                        //CommLogDisplay("标志位复位", MsgType.RcvMsg);
                    }
                    else
                        Thread.Sleep(2000);
                }

                Thread.Sleep(100);

            }
        }
        public bool SaveCutMessageToXML()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(dec);
                XmlElement config = xmlDoc.CreateElement("Config");
                xmlDoc.AppendChild(config);
                string nTmp = "";
                int[] list = { };
                for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
                {
                    XmlElement Product = xmlDoc.CreateElement("ProductMessage" + (i + 1));
                    XmlElement Barcode = xmlDoc.CreateElement("TestProjectMessage");    //测试项个数
                    XmlElement Sequence = xmlDoc.CreateElement("Sequence"); //测试项序号
                    XmlElement Testvalue = xmlDoc.CreateElement("Testvalue"); //测试参数
                    XmlElement Modeltest = xmlDoc.CreateElement("Modeltest");//测试模式
                    XmlElement Testrang = xmlDoc.CreateElement("Testrang");//测试量程

                    XmlElement Adressdwn = xmlDoc.CreateElement("Adressdwn");//下表笔地址
                    XmlElement Delaytime = xmlDoc.CreateElement("Delaytime");//延时时间
                    XmlElement Uppervalue = xmlDoc.CreateElement("Uppervalue");//测量上限
                    XmlElement Downvalue = xmlDoc.CreateElement("Downvalue");//测量下限
                    foreach (SetParamer num in flowLayoutPanel1.Controls.OfType<SetParamer>())
                    {

                        config.AppendChild(Product);

                        if ("Number" + i == num.Name)        //判断是否已经存在界面中
                        {


                            foreach (TextBox txt in num.Controls.OfType<TextBox>())
                            {
                                if (txt.Name == "textBox1")
                                {
                                    XmlElement Adressupp = xmlDoc.CreateElement("Adressupp");//上表笔地址
                                    nTmp = txt.Text;
                                    Adressupp.InnerText = nTmp;
                                    Testvalue.AppendChild(Adressupp); //添加上表笔地址

                                }
                                if (txt.Name == "textBox2")
                                {
                                    nTmp = txt.Text;
                                    Adressdwn.InnerText = nTmp;
                                    Testvalue.AppendChild(Adressdwn); //添加下表笔地址

                                }
                                if (txt.Name == "textBox3")
                                {
                                    nTmp = txt.Text;
                                    Delaytime.InnerText = nTmp;
                                    Testvalue.AppendChild(Delaytime); //添加延时时间

                                }
                                if (txt.Name == "textBox4")
                                {

                                    Uppervalue.InnerText = txt.Text;
                                    Testvalue.AppendChild(Uppervalue); //测量上限
                                }
                                if (txt.Name == "textBox5")
                                {

                                    Downvalue.InnerText = txt.Text;
                                    Testvalue.AppendChild(Downvalue); //测量下限

                                }
                                if (txt.Name == "textBox6")
                                {
                                    nTmp = txt.Text;
                                    Sequence.InnerText = nTmp.ToString();
                                    Product.AppendChild(Sequence); //添加序号
                                    Product.AppendChild(Testvalue); //添加序号
                                }

                            }
                            foreach (ComboBox com in num.Controls.OfType<ComboBox>())
                            {
                                if (com.Name == "comboBox1")
                                {

                                    Modeltest.InnerText = com.SelectedItem.ToString();
                                    Testvalue.AppendChild(Modeltest); //添加测试模式
                                }
                                if (com.Name == "comboBox2")
                                {
                                    Product.AppendChild(Barcode);
                                    Testrang.InnerText = com.SelectedItem.ToString();
                                    Testvalue.AppendChild(Testrang); //添加测试量程
                                }
                            }

                        }
                    }
                }
                xmlDoc.Save(GlobalVar.gl_ProjectName);
            }
            catch (Exception em)
            {
                MessageBox.Show("存储XML文件出错：" + em.Message);
                return false;
            }
            return true;
        }
        public bool readCutMessageFromXML()
        {

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(GlobalVar.gl_ProjectName);
                XmlNode root = xmlDoc.SelectSingleNode("Config");
                XmlNodeList list = root.ChildNodes;
                for (int i = 0; i < list.Count; i++)
                {
                    string model = "";
                    string rang = "";
                    string uppadress = "";
                    string dwnadress = "";
                    string uppvalue = "";
                    string dwnvalue = "";
                    string delaytime = "";
                    string number = "";
                    XmlNode PPCNode = list[i];
                    //先获取序号
                    XmlNode Sequence = list[i].SelectSingleNode("Sequence");
                    number = Sequence.InnerText;
                    foreach (XmlNode subnode in PPCNode.ChildNodes)
                    {
                        if (subnode.Name == "Testvalue")
                        {

                            XmlNode Barmodel = subnode.SelectSingleNode("Modeltest");
                            XmlNode Barrang = subnode.SelectSingleNode("Testrang");
                            XmlNode Baruppad = subnode.SelectSingleNode("Adressupp");
                            XmlNode Bardwnad = subnode.SelectSingleNode("Adressdwn");
                            XmlNode BarDelay = subnode.SelectSingleNode("Delaytime");
                            XmlNode Baruppva = subnode.SelectSingleNode("Uppervalue");
                            XmlNode Bardwnva = subnode.SelectSingleNode("Downvalue");
                            model = Barmodel.InnerText;
                            rang = Barrang.InnerText;
                            uppadress = Baruppad.InnerText;
                            dwnadress = Bardwnad.InnerText;
                            delaytime = BarDelay.InnerText;
                            uppvalue = Baruppva.InnerText;
                            dwnvalue = Bardwnva.InnerText;
                            SetParamer setpara = new SetParamer();
                            setpara.Name = "Number" + i;
                            setpara.Width = 543;
                            this.flowLayoutPanel1.Controls.Add(setpara);
                            setpara.LoadContrlovalue(model, rang, uppadress, dwnadress, delaytime, uppvalue, dwnvalue, number);
                            Thread.Sleep(10);
                        }
                    }

                }

            }
            catch (Exception em)
            {
                MessageBox.Show("读取XML异常:" + em.Message);
                myfunction.writelog("读取XML异常");
                return false;
            }
            return true;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case GlobalVar.WM_StartTest://开始测试
                    ClearFlag();
                    flowLayoutPanel1.VerticalScroll.Value = 0;
                    Thread.Sleep(50);
                    Thread th_Starttest = new Thread(StartTest);
                    th_Starttest.IsBackground = true;
                    th_Starttest.Start();
                    break;
                case GlobalVar.WM_ResultPass://接收到测试结果OK
                    ModBusSend("E10", GlobalVar.gl_CompleteTest);   //发送OK结果
                    GlobalVar.gl_TestPcsPassCount = (int.Parse(GlobalVar.gl_TestPcsPassCount) + 1).ToString();
                    myfunction.writelog("发送OK结果");
                    CommLogDisplay("发送OK结果", MsgType.SendMsg);
                    break;
                case GlobalVar.WM_ResultFail: //接收到测试结果NG  
                    ModBusSend("E11", GlobalVar.gl_CompleteTest);   //发送NG结果
                    GlobalVar.gl_TestPcsFailCount = (int.Parse(GlobalVar.gl_TestPcsFailCount) + 1).ToString();
                    myfunction.writelog("发送NG结果");
                    CommLogDisplay("发送NG结果", MsgType.SendMsg);
                    break;
                case GlobalVar.WM_RemoveControl:
                    flowLayoutPanel1.Controls.RemoveByKey(GlobalVar.gl_ControlName);
                    MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RenamControl, (IntPtr)0, (IntPtr)0);
                    break;
                case GlobalVar.WM_RenamControl:
                    int i = 0;
                    foreach (SetParamer num in flowLayoutPanel1.Controls.OfType<SetParamer>())
                    {
                        num.Name = "Number" + i;
                        i++;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
        public void ClearFlag()
        {

            GlobalVar.gl_TestOneComplete = GlobalVar.gl_TestTwoComplete =
                GlobalVar.gl_TestThreeComplete = GlobalVar.gl_TestFourComplete = false;

        }
        public void StartTest()
        {
            int i = 0;

            foreach (SetParamer num in flowLayoutPanel1.Controls.OfType<SetParamer>())
            {
                string model = "";
                string type = "";
                string uppadress = "";
                string dwnadress = "";
                string uppvalue = "";
                string dwnvalue = "";
                string delaytime = "";
                string result = "";
                if ("Number" + i == num.Name)        //判断是否已经存在界面中
                {
                    if (i > 0)
                        this.Invoke(new Action(() =>
                        {
                            if (flowLayoutPanel1.VerticalScroll.Maximum > i * 150)
                                flowLayoutPanel1.VerticalScroll.Value += 150;

                        }));
                    else
                        this.Invoke(new Action(() =>
                        {
                            flowLayoutPanel1.VerticalScroll.Value = 0;
                        }));

                    foreach (TextBox txt in num.Controls.OfType<TextBox>())
                    {
                        if (txt.Name == "textBox1")//上表笔地址
                        {
                            uppadress = txt.Text;

                        }
                        if (txt.Name == "textBox2")//下表笔地址
                        {
                            dwnadress = txt.Text;

                        }
                        if (txt.Name == "textBox3")//延时
                        {
                            delaytime = txt.Text;

                        }
                        if (txt.Name == "textBox4")//上限
                        {
                            uppvalue = txt.Text;
                        }
                        if (txt.Name == "textBox5")//下限
                        {
                            dwnvalue = txt.Text;

                        }
                        if (txt.Name == "textBox6")//序号
                            this.Invoke(new Action(() =>
                            {
                                txt.BackColor = Color.Lime;
                            }));
                        if (txt.Name == "textBox7")//序号
                            this.Invoke(new Action(() =>
                            {
                                txt.Text = "wait";
                                txt.BackColor = Color.Gray;
                            }));
                    }
                    foreach (ComboBox com in num.Controls.OfType<ComboBox>())
                    {
                        if (com.Name == "comboBox1")//模式
                        {
                            this.Invoke(new Action(() =>
                            {
                                model = com.SelectedItem.ToString();
                            }));

                            switch (model)
                            {
                                case "4W电阻":
                                    type = "Configure:Fresistance";
                                    model = "Measure:Fresistance?";
                                    break;
                                case "电容":
                                    type = "Configure:Capacitance";
                                    model = "Measure:Capacitance?";
                                    break;
                                case "直流电压":
                                    type = "CONFigure[:VOLTage][:DC]";
                                    model = "Measure[:VOLTage][:DC]?";
                                    break;
                                case "直流电流":
                                    type = "CONFigure:CURRent[:DC]";
                                    model = "Measure:CURRent[:DC]?";
                                    break;
                                case "交流电流":
                                    type = "CONFigure:CURRent:AC";
                                    model = "Measure:Current:AC?";
                                    break;
                                case "二极管":
                                    type = "CONFigure:DIODe";
                                    model = "Measure:DIODe?";
                                    break;
                                case "交流电压":
                                    type = "CONFigure[:VOLTage]:AC";
                                    model = "Measure:VOLTage:AC?;*WAI";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    try
                    {
                        //MultimetersSend(type);
                        //MultimetersSend(model);
                        //ModBusSend("S1", int.Parse(uppadress));//上表笔地址
                        //ModBusSend("S1", int.Parse(dwnadress));//下表笔地址
                        Thread.Sleep(200);
                        //string Value = MultimetersRead();
                        //if (Value != "")
                        //{
                        //    Decimal value = ChangeDataToD(Value);
                        //    if ((value > Decimal.Parse(dwnvalue)) &&
                        //        (Decimal.Parse(uppvalue) > value)
                        //        )
                        //        result = "Pass";
                        //    else
                        //        result = "Fail";
                        //    foreach (TextBox txt in num.Controls.OfType<TextBox>())
                        //    {
                        //        if (txt.Name == "textBox7")//结果
                        //            this.Invoke(new Action(() =>
                        //            {
                        //                txt.Text = value.ToString();
                        //                txt.BackColor = (result == "PASS") ? Color.PaleGreen : Color.Red;
                        //            }));
                        //        if (txt.Name == "textBox6")//序号
                        //            this.Invoke(new Action(() =>
                        //            {
                        //                txt.BackColor = Color.Turquoise;
                        //            }));
                        //    }
                        Thread.Sleep(int.Parse(delaytime));
                        //}
                        //else
                        //{

                        //    foreach (TextBox txt in num.Controls.OfType<TextBox>())
                        //    {
                        //        if (txt.Name == "textBox7")//结果
                        //            this.Invoke(new Action(() =>
                        //            {
                        //                txt.Text = "PASS";
                        //                txt.BackColor = (result == "PASS") ? Color.Green : Color.Red;
                        //            }));
                        //        if (txt.Name == "textBox6")//序号
                        //            this.Invoke(new Action(() =>
                        //            {
                        //                txt.BackColor = Color.Turquoise;
                        //            }));
                        //    }

                        //}
                        foreach (TextBox txt in num.Controls.OfType<TextBox>())
                        {
                            if (txt.Name == "textBox7")//结果
                                this.Invoke(new Action(() =>
                                {
                                    txt.Text = "Fail";

                                    txt.BackColor = (txt.Text == "PASS") ? Color.LightGreen : Color.Red;
                                }));
                            if (txt.Name == "textBox6")//序号
                                this.Invoke(new Action(() =>
                                {
                                    txt.BackColor = Color.Turquoise;
                                }));
                        }
                    }
                    catch (Exception ex)
                    {
                        myfunction.writelog("错误" + ex.Message);
                    }
                }
                i++;
            }

        }
        /// <summary>
        /// 通讯信息
        /// </summary>
        /// <param name="str">显示内容</param>
        /// <param name="nMsgType"></param>
        void CommLogDisplay(string str, MsgType nMsgType)
        {
            if (rtboxCMLog.InvokeRequired)
            {
                rtboxCMLog.BeginInvoke(new Action(() =>
                {
                    CommLogDisplay(str, nMsgType);
                }
                 ));
                return;
            }
            try
            {
                if (rtboxCMLog.Lines.Length > 200)
                {
                    rtboxCMLog.Text = "";
                }
                int nSelectStart = rtboxCMLog.TextLength;
                Color clr = Color.Lime;
                switch (nMsgType)
                {
                    case MsgType.SendMsg:
                        clr = Color.Lime;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "发送:" + str + "\n");
                        break;
                    case MsgType.RcvMsg://接收的消息
                        clr = Color.Cyan;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "接收:" + str + "\n");
                        break;
                    case MsgType.ErrorMsg://运行异常
                        clr = Color.Red;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "运行异常:" + str + "\n");
                        break;
                    case MsgType.WaitTest://等待测试
                        clr = Color.LightGreen;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "异常解除:" + str + "\n");
                        break;
                    default:
                        clr = Color.Snow;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                                str + "\n");
                        break;
                }
                int nLength = rtboxCMLog.TextLength - 1;
                rtboxCMLog.Select(nSelectStart, nLength);
                rtboxCMLog.SelectionColor = clr;
                rtboxCMLog.AppendText("\n");
                rtboxCMLog.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                myfunction.writelog("写入通讯LOG异常" + ex.ToString());
                return;
            }
        }
        /// <summary>
        /// 显示测试结果
        /// </summary>
        /// <param name="value">测量值</param>
        /// <param name="result">测量结果</param>        
        public void RefleshResultView(object Arrylist)
        {
            ReflashShow.WaitOne();
            string[] arrylist = (string[])Arrylist;
            string value = arrylist[0];
            string result = arrylist[1];
            string resultview = arrylist[2];

            ReflashShow.ReleaseMutex();

        }
        /// <summary>
        /// 科学计数法转换
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
            {
                dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            }
            return dData;
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (btn_Connect.Text == "连接")
            {
                if (ConnectDevice())
                {
                    btn_Connect.Text = "断开";
                    btn_Connect.BackColor = Color.Tomato;

                }
            }
            else
            {
                disposeNetWorkConnect();
                btn_Connect.Text = "连接";
                btn_Connect.BackColor = Color.LimeGreen;
                GlobalVar.gl_UpperMonitor = false;
            }
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            myfunction.writelog("软件开启");
            //检查更新
            UpdateClass update = new UpdateClass();
            update.GetVersion();
            GlobalVar.gl_IntPtr_MainWindow = this.Handle;
            myfunction.ReadGlobalInfoFromTBS();
            string[] arrylist = new string[3];
            arrylist[0] = "0";
            arrylist[1] = "0";
            arrylist[2] = "6";
            RefleshResultView(arrylist);//清除显示
            txt_IPadress.Text = GlobalVar.gl_MultimetersIP;
            txt_mobbusIP.Text = GlobalVar.gl_ModBusIP;
            txt_modbusPort.Text = GlobalVar.gl_ModBusPort;
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            myfunction.writelog("软件关闭");
            myfunction.WriteGlobalInfoToTBS();
            disposeNetWorkConnect();

        }

        private void txt_IPadress_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_MultimetersIP = txt_IPadress.Text.Trim();
        }

        private void combox_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GlobalVar.gl_ChooseFunction = combox_mode.SelectedItem.ToString();
            //combox_range.Items.Clear();
            //switch (GlobalVar.gl_ChooseFunction)
            //{
            //    case "4W电阻":
            //        combox_range.Items.Add("1G");
            //        combox_range.Items.Add("100M");
            //        combox_range.Items.Add("10M");
            //        combox_range.Items.Add("1M");
            //        combox_range.Items.Add("100K");
            //        combox_range.Items.Add("10K");
            //        combox_range.Items.Add("1K");
            //        combox_range.Items.Add("100");
            //        break;
            //    case "电容":
            //        combox_range.Items.Add("10uF");
            //        combox_range.Items.Add("1uF");
            //        combox_range.Items.Add("100nF");
            //        combox_range.Items.Add("10nF");
            //        combox_range.Items.Add("1nF");
            //        break;
            //    case "直流电压":
            //        combox_range.Items.Add("1000V");
            //        combox_range.Items.Add("100V");
            //        combox_range.Items.Add("10V");
            //        combox_range.Items.Add("1V");
            //        combox_range.Items.Add("100mV");
            //        break;
            //    case "直流电流":
            //    case "交流电流":
            //        combox_range.Items.Add("3A");
            //        combox_range.Items.Add("1A");
            //        combox_range.Items.Add("100mA");
            //        combox_range.Items.Add("10mA");
            //        combox_range.Items.Add("1mA");
            //        combox_range.Items.Add("100uA");
            //        break;
            //    case "二极管":
            //        combox_range.Items.Add("AUTO");
            //        break;
            //    case "交流电压":
            //        combox_range.Items.Add("750V");
            //        combox_range.Items.Add("100V");
            //        combox_range.Items.Add("10V");
            //        combox_range.Items.Add("1V");
            //        combox_range.Items.Add("100mV");
            //        break;

            //    default:
            //        break;
            //}
        }

        private void combox_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GlobalVar.gl_ChooseRange = combox_range.SelectedItem.ToString();
        }

        private void btn_modbusConnect_Click(object sender, EventArgs e)
        {

            if (btn_modbusConnect.Text.Trim() == "连接")
            {
                try
                {
                    GlobalVar.gl_ModBusIP = txt_mobbusIP.Text;
                    System.Net.IPAddress ip = System.Net.IPAddress.Parse(GlobalVar.gl_ModBusIP);
                    GlobalVar.gl_ModBusPort = txt_modbusPort.Text;
                    GlobalVar.gl_Portcon = new Modbus(ip, int.Parse(GlobalVar.gl_ModBusPort));
                    btn_modbusConnect.BackColor = Color.Tomato;
                    btn_modbusConnect.Text = "断开";
                    GlobalVar.gl_LowerMachine = true;
                }
                catch (Exception ex)
                {
                    GlobalVar.gl_LowerMachine = false;
                    myfunction.writelog("Modbus开启失败,异常:" + ex.ToString());
                }
            }
            else
            {
                if (GlobalVar.gl_Portcon != null)
                {
                    GlobalVar.gl_Portcon.Dispose();
                    GlobalVar.gl_Portcon = null;
                    GlobalVar.gl_LowerMachine = false;
                }
                btn_modbusConnect.Text = "连接";
                btn_modbusConnect.BackColor = Color.LimeGreen;
            }
        }

        private void MainFrom_SizeChanged(object sender, EventArgs e)
        {
            //Font font = new Font("宋体", tableLayoutPanel5.Height - 6);
            //lbl_IPadress.Font = new Font("宋体", tableLayoutPanel5.Height - 480); 
        }

        private void tsmi_StartJobs_Click(object sender, EventArgs e)
        {
            if (GlobalVar.gl_LowerMachine)
                ModBusSend("S1", GlobalVar.gl_PermitWork);//允许作业
            else
            {
                MessageBox.Show("下位机未连接");
                return;
            }
            CommLogDisplay("允许作业", MsgType.SendMsg);
        }

        private void txt_mobbusIP_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_ModBusIP = txt_mobbusIP.Text;
        }

        private void txt_modbusPort_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_ModBusPort = txt_modbusPort.Text;
        }

        private void tsmi_ClearAlarm_Click(object sender, EventArgs e)
        {
            if (GlobalVar.gl_UpperMonitor)
                MultimetersSend("*CLS");
            else
                MessageBox.Show("万用表未连接");
        }

        private void tsmi_SetParameter_Click(object sender, EventArgs e)
        {
            Parameter para = new Parameter();
            if (para.ShowDialog() == DialogResult.OK)
            {
                myfunction.WriteGlobalInfoToTBS();
            }

        }

        private void btn_AddTestProject_Click(object sender, EventArgs e)
        {
            int ControlCount = this.flowLayoutPanel1.Controls.Count;
            string ControlName = "Number" + ControlCount.ToString();
            SetParamer setpara = new SetParamer();
            setpara.Name = ControlName;
            setpara.Width = 543;
            this.flowLayoutPanel1.Controls.Add(setpara);
            setpara.modifybtnText("Test" + (ControlCount + 1));
        }

        private void btn_SaveData_Click(object sender, EventArgs e)
        {
            SaveCutMessageToXML();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_ProjectName = textBox1.Text;
            GlobalVar.gl_ProjectName = Application.StartupPath + "\\" + GlobalVar.gl_ProjectName + ".xml";
        }

        private void btn_comfirm_Click(object sender, EventArgs e)
        {
            readCutMessageFromXML();
        }

        private void btn_StarTest_Click(object sender, EventArgs e)
        {
            MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_StartTest, (IntPtr)0, (IntPtr)0);
        }




    }
}
