using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;
using System.IO;

namespace AnalogMultimeters
{
    /// <summary>
    /// 自定义串口控件
    /// </summary>
    public partial class AxSerialPort : UserControl
    {
        //定义输出的文本类型
        public enum emMsgType : int
        {
            COMMON_TEXT = 0,
            SEND_TEXT_1 = 1,
            SEND_TEXT_2 = 2,
            RECV_TEXT_1 = 3,
            RECV_TEXT_2 = 4,
            ERROR_TEXT = 5,
            TIPS_TEXT = 6,
            EXCEPTION_TEXT = 7
        }
        #region 常量定义
        //串口配置
        const string _NODE_SERIAL_PORT = "SerialPort";
        const string _NODE_BOUDRATE = "Boudrate";
        const string _NODE_STOPBIT = "Stopbit";
        const string _NODE_PARITY = "Parity";
        const string _NODE_DATABIT = "Databit";
        #endregion
        #region 事件定义

        /// <summary>
        /// 串口引脚状态改变事件委托
        /// </summary>
        /// <param name="sender">事件的源，即 System.IO.Ports.SerialPort 对象</param>
        /// <param name="e">包含事件数据的 System.IO.Ports.SerialPinChangedEventArgs 对象</param>
        public delegate void delegate_PinChanged(object sender, SerialPinChangedEventArgs e);
        /// <summary>
        /// 串口pin脚状态改变事件
        /// </summary>
        public event delegate_PinChanged PinChanged;

        /// <summary>
        /// 串口打开事件委托
        /// </summary>
        /// <param name="bFlag">串口的开闭状态，true为打开，false为关闭</param>
        public delegate void delegate_SerialPortOpen(bool bFlag);

        /// <summary>
        /// 串口接收数据事件委托
        /// </summary>
        /// <param name="strRecv">接收消息内容</param>
        public delegate void delegate_DataRecevice(string strRecv);
        /// <summary>
        /// 串口消息接收事件
        /// </summary>
        public event delegate_DataRecevice DataReceived;
        #endregion
        #region 成员变量
        bool m_bAdmin = false;
        string[] m_strListSerialPorts;
        /// <summary>
        /// 串口名称
        /// </summary>
        public string m_strPortName;
        /// <summary>
        /// 波特率
        /// </summary>
        public int m_nBaudRate;
        /// <summary>
        /// 数据位
        /// </summary>
        public int m_nDataBits;
        /// <summary>
        /// 校验位
        /// </summary>
        public Parity m_Parity;
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits m_StopBits;
        OpXML m_xml = null;
        #endregion
        #region 属性
        /// <summary>
        /// 设置串口数据终端准备(DTR)引脚输出状态
        /// </summary>
        public bool DtrEnable
        {
            set
            {
                if (!serialPort1.IsOpen)
                {
                    throw new Exception("检测到串口未打开！");
                }
                //serialPort1.DtrEnable = value;
                //ShowPinState();
                this.Invoke(new Action(() =>
                {
                    chkboxDtr.Checked = value;
                }));

            }
        }
        /// <summary>
        /// 设置串口请求发送(RTS)引脚输出状态
        /// </summary>
        public bool RtsEnable
        {
            set
            {
                if (!serialPort1.IsOpen)
                {
                    throw new Exception("检测到串口未打开！");
                }
                //serialPort1.RtsEnable = value;
                //ShowPinState();
                this.Invoke(new Action(() =>
                {
                    chkboxRts.Checked = value;
                }));
            }
        }
        /// <summary>
        /// 获取或设置用于解释 System.IO.Ports.SerialPort.ReadLine() 和 System.IO.Ports.SerialPort.WriteLine(System.String)
        ///   方法调用结束的值。
        /// </summary>
        public string NewLine
        {
            set
            {
                serialPort1.NewLine = value;
            }
            get
            {
                return serialPort1.NewLine;
            }

        }
        /// <summary>
        /// 串口是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return serialPort1.IsOpen;
            }
        }
        /// <summary>
        /// 管理员模式，串口参数是否可以设置
        /// </summary>
        public bool AdminMode
        {
            set
            {
                m_bAdmin =
                    chkboxRts.Visible =
                    chkboxDtr.Visible = value;
                if (!serialPort1.IsOpen)
                {
                    combox_boudrate.Enabled =
                    combox_databit.Enabled =
                    combox_parity.Enabled =
                    combox_stopbit.Enabled = value;
                }
            }
        }
        /// <summary>
        /// 设置串口读取超时
        /// </summary>
        public int ReadTimeOut
        {
            set
            {
                serialPort1.ReadTimeout = value;
            }
            get
            {
                return serialPort1.ReadTimeout;
            }
        }
        /// <summary>
        /// 设置串口写入超时
        /// </summary>
        public int WriteTimeOut
        {
            set
            {
                serialPort1.WriteTimeout = value;
            }
            get
            {
                return serialPort1.WriteTimeout;
            }
        }
        #endregion
        #region 外部接口
        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            if ((combox_serialPorts.SelectedIndex >= 0)
                && (combox_serialPorts.Items[combox_serialPorts.SelectedIndex].ToString().Trim() != ""))
            {
                btn_OpenSp.PerformClick();
            }
        }
        /// <summary>
        /// 串口写操作
        /// </summary>
        /// <param name="bytes">串口写入的内容</param>
        /// <param name="strExp">若执行发生异常，则内容</param>
        /// <returns></returns>
        public bool Write(byte[] bytes)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.DiscardInBuffer();
                    serialPort1.DiscardOutBuffer();
                    serialPort1.Write(bytes, 0, bytes.Length);
                    CommLogDisplay(Convert.ToString(bytes), emMsgType.SEND_TEXT_1);
                }
                else
                {
                    CommLogDisplay("串口未打开", emMsgType.EXCEPTION_TEXT);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
                return false;
            }
        }
        /// <summary>
        /// 串口写入行操作
        /// </summary>
        /// <param name="strMsg">写入错误内容</param>
        /// <param name="strExp">若程序执行有异常，则会输出异常内容，无异常输出为空</param>
        public void WriteErrorLine(string strMsg)
        {
            try
            {
                CommLogDisplay(strMsg, emMsgType.ERROR_TEXT);
                return;
            }
            catch (Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
                return;
            }

        }
        /// <summary>
        /// 串口写入行操作
        /// </summary>
        /// <param name="strMsg">写入内容</param>
        /// <param name="strExp">若程序执行有异常，则会输出异常内容，无异常输出为空</param>
        public void WriteLine(string strMsg, bool RevStatue)
        {
            try
            {
                if (GlobalVar.modbusStatue)
                {
                    if (RevStatue)
                        CommLogDisplay(strMsg, emMsgType.RECV_TEXT_1);
                    else
                        CommLogDisplay(strMsg, emMsgType.SEND_TEXT_1);
                }
                else
                {
                    CommLogDisplay(strMsg, emMsgType.RECV_TEXT_1);
                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
                return;
            }

        }
        /// <summary>
        /// 串口接收消息
        /// </summary>
        /// <param name="strStopChar">消息结束标志</param>
        /// <param name="strExp">执行发生异常内容</param>
        /// <returns>读取的消息内容</returns>
        public string ReadTo(string strStopChar)
        {
            string strRecv = "";
            try
            {
                if (serialPort1.IsOpen)
                {
                    if (strStopChar == "" || strStopChar == null)
                    {
                        strRecv = serialPort1.ReadTo(strStopChar);
                    }
                    else
                    {
                        strRecv = serialPort1.ReadExisting();
                    }
                    CommLogDisplay(strRecv, emMsgType.RECV_TEXT_1);
                    return strRecv;
                }
                else
                {
                    CommLogDisplay("串口未打开", emMsgType.ERROR_TEXT);
                    return "";
                }
            }
            catch (Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
                return "";
            }
        }
        /// <summary>
        /// 控制条码枪扫条码
        /// </summary>
        /// <param name="strExp">执行发生异常提示</param>
        /// <returns>返回扫描内容</returns>
        public string ScanBarcode(out string strExp)
        {
            strExp = "";
            //打开条码枪命令
            byte[] byOpen = new byte[3];
            byOpen[0] = 0x16;
            byOpen[1] = 0x54;
            byOpen[2] = 0x0D;
            //关闭条码枪命令
            byte[] byClz = new byte[3];
            byClz[0] = 0x16;
            byClz[1] = 0x55;
            byClz[2] = 0x0D;
            if (!Write(byOpen))
            {
                return "";
            }
            string strBar = "";
            int nCount = 10;
            while (nCount-- > 0)
            {
                strBar = ReadTo("\r\n").Trim();
                if (strBar == "")
                {
                    Thread.Sleep(200);
                }
                else
                {
                    break;
                }
            }
            if (strBar == "" || strBar == null)
            {
                Write(byClz);
                return "";
            }
            else
            {
                return strBar;
            }
        }
        #endregion
        #region Construction
        /// <summary>
        /// 构造函数
        /// </summary>
        public AxSerialPort()
        {
            InitializeComponent();
            m_xml = new OpXML(OpXML._XML_PATH);
            try
            {
                if (!m_xml.NodeIsExisted(OpXML._XML_PATH, this.Name))
                {
                    m_xml.AddNode(OpXML._XML_PATH, OpXML._XML_NODE_ROOT, this.Name);
                }
            }
            catch
            {
                File.Delete(OpXML._XML_PATH);
                m_xml = new OpXML(OpXML._XML_PATH);
            }

        }
        #endregion
        void SerialPortPara_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.None;
            initSerialPortsConfig();
            chkboxDtr.Visible = chkboxRts.Visible =
            lab_CD.Visible = lab_CtsState.Visible = lab_DsrState.Visible = lab_RtsState.Visible =
              combox_boudrate.Visible = combox_databit.Visible = combox_parity.Visible = combox_stopbit.Visible =
              label_baudrate.Visible = label_databit.Visible = label_priority.Visible = label_stopBit.Visible = false;
        }
        void btn_Refresh_Click(object sender, EventArgs e)
        {
            initSerialPortsConfig();
        }
        private void comboBox_serialPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_xml.CustomizeSetAttribute(OpXML._XML_PATH, this.Name, _NODE_SERIAL_PORT, "Value", combox_serialPorts.Text.Trim());
        }
        private void comboBox_boudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_xml.CustomizeSetAttribute(OpXML._XML_PATH, this.Name, _NODE_BOUDRATE, "Value", combox_boudrate.Text.Trim());
        }
        private void comboBox_parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_xml.CustomizeSetAttribute(OpXML._XML_PATH, this.Name, _NODE_PARITY, "Value", combox_parity.Text.Trim());
        }
        private void comboBox_stopbit_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_xml.CustomizeSetAttribute(OpXML._XML_PATH, this.Name, _NODE_STOPBIT, "Value", combox_stopbit.Text.Trim());
        }
        private void comboBox_databit_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_xml.CustomizeSetAttribute(OpXML._XML_PATH, this.Name, _NODE_DATABIT, "Value", combox_databit.Text.Trim());
        }

        private void btn_OpenSp_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_OpenSp.Text.Trim() == "打开串口")
                {
                    GlobalVar.g_Portnumber = combox_serialPorts.SelectedItem.ToString();
                    SetCtrlEnable(false);
                    btn_OpenSp.Text = "关闭串口";
                    btn_OpenSp.BackColor = Color.Tomato;
                    tabControl1.SelectedIndex = 1;
                    //GlobalVar.gl_LowerMachine = true;
                }
                else
                {
                    GlobalVar.g_Portnumber = "";
                    btn_OpenSp.Text = "打开串口";
                    btn_OpenSp.BackColor = Color.LimeGreen;
                    GlobalVar.gl_LowerMachine = false;
                    SetCtrlEnable(true);
                }
            }
            catch (System.Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
                serialPort1.Close();
                btn_OpenSp.Text = "打开串口";
                GlobalVar.gl_LowerMachine = false;
                btn_OpenSp.BackColor = Color.LimeGreen;
            }

        }
        //pin脚状态监控事件
        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if (PinChanged != null)
            {
                PinChanged(sender, e);
            }
            ShowPinState();
        }
        //当pin脚状态发生改变时刷新界面上的各pin脚状态显示
        private void ShowPinState()
        {
            if (!serialPort1.IsOpen)
            {
                return;
            }
            this.Invoke((EventHandler)delegate
            {
                lab_DsrState.Text = serialPort1.DsrHolding ? "DSR: 1" : "DSR: 0";

                lab_CtsState.Text = serialPort1.CtsHolding ? "CTS: 1" : "CTS: 0";

                lab_RtsState.Text = serialPort1.RtsEnable ? "RTS: 1" : "RTS: 0";

                lab_CD.Text = serialPort1.CDHolding ? "CD：1" : "CD：0";


                chkboxDtr.Checked = serialPort1.DtrEnable;
                chkboxRts.Checked = serialPort1.RtsEnable;
            });

        }
        //设置界面上控件的Enable状态
        private void SetCtrlEnable(bool bEnable)
        {
            btn_Refresh.Enabled =
                combox_serialPorts.Enabled = bEnable;

            if (m_bAdmin)
            {
                combox_boudrate.Enabled =
                combox_databit.Enabled =
                combox_parity.Enabled =
                combox_stopbit.Enabled = bEnable;
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = "";
            try
            {
                str = serialPort1.ReadTo("\n");
                CommLogDisplay(str, emMsgType.RECV_TEXT_1);
                if (DataReceived != null)
                {
                    DataReceived(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误提醒" + ex.ToString());
                
            }

        }
        private void chkboxDtr_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DtrEnable = chkboxDtr.Checked;
                ShowPinState();
            }

        }
        private void chkboxRts_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.RtsEnable = chkboxRts.Checked;
                ShowPinState();
            }

        }

        public void CommLogDisplay(string str, emMsgType nMsgType)
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
                    case emMsgType.SEND_TEXT_1:
                        clr = Color.Lime;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "发送:" + str + "\n");
                        break;
                    case emMsgType.SEND_TEXT_2:
                        clr = Color.GreenYellow;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "发送:" + str + "\n");
                        break;
                    case emMsgType.RECV_TEXT_1://接收的消息
                        clr = Color.Cyan;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "接收:" + str + "\n");
                        break;
                    case emMsgType.RECV_TEXT_2://接收的消息
                        clr = Color.Aqua;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "接收:" + str + "\n");
                        break;
                    case emMsgType.ERROR_TEXT://通信报错
                        clr = Color.Tomato;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "通信出错:" + str + "\n");
                        break;
                    case emMsgType.EXCEPTION_TEXT://运行异常
                        clr = Color.OliveDrab;
                        rtboxCMLog.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff\n") +
                            "运行异常:" + str + "\n");
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
                //设置滚动条位置
                //rtboxCMLog.Select(nLength, 0);
                rtboxCMLog.ScrollToCaret();
            }
            catch (System.Exception ex)
            {
                AxPopMsg.Exception(ex.ToString());
                return;
            }
        }
        void GetSerialPortConfig()
        {
            if (combox_serialPorts.Text.Trim() == "")
            {
                throw new Exception("串口不能为空！");
            }
            m_strPortName = combox_serialPorts.Text;
            serialPort1.PortName = m_strPortName;
            m_nBaudRate = Convert.ToInt32(combox_boudrate.Text);
            serialPort1.BaudRate = m_nBaudRate;
            m_nDataBits = Convert.ToInt32(combox_databit.Text);
            serialPort1.DataBits = m_nDataBits;
            switch (combox_stopbit.SelectedIndex)
            {
                case 0:
                    m_StopBits = StopBits.One;
                    break;
                case 1:
                    m_StopBits = StopBits.OnePointFive;
                    break;
                case 2:
                    m_StopBits = StopBits.Two;
                    break;
                default:
                    m_StopBits = StopBits.One;
                    break;
            }
            serialPort1.StopBits = m_StopBits;
            switch (combox_parity.SelectedIndex)
            {
                case 0:
                    m_Parity = Parity.None;
                    break;
                case 1:
                    m_Parity = Parity.Odd;
                    break;
                case 2:
                    m_Parity = Parity.Even;
                    break;
                case 3:
                    m_Parity = Parity.Mark;
                    break;
                case 4:
                    m_Parity = Parity.Space;
                    break;
            }
            serialPort1.Parity = m_Parity;
            return;
        }
        //从XML配置文档中读取串口配置
        void initSerialPortsConfig()
        {
            try
            {
                if (!m_xml.NodeIsExisted(OpXML._XML_PATH, this.Name))
                {
                    m_xml.AddNode(OpXML._XML_PATH, OpXML._XML_NODE_ROOT, this.Name);
                }
                m_strListSerialPorts = System.IO.Ports.SerialPort.GetPortNames();
                combox_serialPorts.Items.Clear();
                for (int i = 0; i < m_strListSerialPorts.Length; i++)
                {
                    combox_serialPorts.Items.Add(m_strListSerialPorts[i]);
                }
                string strVal = m_xml.CustomizeGetAttribute(OpXML._XML_PATH, this.Name, _NODE_SERIAL_PORT, "Value", "");
                if (combox_serialPorts.Items.Count > 0)
                {
                    combox_serialPorts.SelectedIndex = (combox_serialPorts.Items.IndexOf(strVal) >= 0) ? combox_serialPorts.Items.IndexOf(strVal) : 0;
                }
                strVal = m_xml.CustomizeGetAttribute(OpXML._XML_PATH, this.Name, _NODE_BOUDRATE, "Value", "");
                combox_boudrate.SelectedIndex = (combox_boudrate.Items.IndexOf(strVal) >= 0) ? combox_boudrate.Items.IndexOf(strVal) : 4;

                strVal = m_xml.CustomizeGetAttribute(OpXML._XML_PATH, this.Name, _NODE_DATABIT, "Value", "");
                combox_databit.SelectedIndex = (combox_databit.Items.IndexOf(strVal) >= 0) ? combox_databit.Items.IndexOf(strVal) : 3;

                strVal = m_xml.CustomizeGetAttribute(OpXML._XML_PATH, this.Name, _NODE_PARITY, "Value", "");
                combox_parity.SelectedIndex = (combox_parity.Items.IndexOf(strVal) >= 0) ? combox_parity.Items.IndexOf(strVal) : 0;

                strVal = m_xml.CustomizeGetAttribute(OpXML._XML_PATH, this.Name, _NODE_STOPBIT, "Value", "");
                combox_stopbit.SelectedIndex = (combox_stopbit.Items.IndexOf(strVal) >= 0) ? combox_stopbit.Items.IndexOf(strVal) : 0;
            }
            catch (System.Exception ex)
            {
                CommLogDisplay(ex.ToString(), emMsgType.EXCEPTION_TEXT);
            }
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.rtboxCMLog.Clear();
        }
    }
}

