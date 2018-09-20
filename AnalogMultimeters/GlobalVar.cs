using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using ModbusDll;

namespace AnalogMultimeters
{
    class GlobalVar
    {
        #region--------------- 全局变量----------------

        public static IntPtr gl_IntPtr_MainWindow;
        public static Modbus gl_Portcon = null; //定义一个modbus
        public static int gl_adminType = 0; //0表示非管理员模式，1表示管理员模式
        public static string gl_Result = "";//测试结果      
        public static bool gl_UpperMonitor = false; //是否连接万用表
        public static bool gl_LowerMachine = false;//是否连接下位机
        public static string gl_TestOne = "Wait";//第一次测试结果
        public static bool gl_TestOneComplete = false;//第一次测试状态
        public static string gl_TestTwo = "Wait";//第二次测试结果
        public static bool gl_TestTwoComplete = false;//第二次测试状态
        public static string gl_TestThree = "Wait";//第三次测试结果
        public static bool gl_TestThreeComplete = false;//第三次测试状态
        public static string gl_TestFour = "Wait";//第四次测试结果
        public static bool gl_TestFourComplete = false;//第四次测试状态
        public static bool gl_TestComplete = true;//全部测试状态,true 表示测试完成   
        public static string gl_TestOneSuperiorLimit = "0";//测试项一上限
        public static string gl_TestOneLowerLimit = "0";//测试项一下限
        public static string gl_TestTwoSuperiorLimit = "0";//测试项二上限
        public static string gl_TestTwoLowerLimit = "0";//测试项二下限
        public static string gl_TestThreeSuperiorLimit = "0";//测试项三上限
        public static string gl_TestThreeLowerLimit = "0";//测试项三下限
        public static string gl_TestFourSuperiorLimit = "0";//测试项四上限
        public static string gl_TestFourLowerLimit = "0";//测试项四下限
        public static string gl_MultimetersIP = "";//万用表IP地址
        public static string gl_ModBusIP = "";//下位机IP地址
        public static string gl_ModBusPort = "";//下位机Port口
        public static string gl_password = "12345.0";
        public static string gl_ChooseFunction = "";//模式选择
        public static string gl_ChooseRange = "";//量程选择
        public static string gl_TestPcsCount = "0";//测试数量
        public static string gl_TestPcsPassCount = "0";//测试OK数量
        public static string gl_TestPcsFailCount = "0";//测试NG数量
        public static string gl_AppName = "Gentex车载加热";

        public static string gl_ControlName = "";//当前测试项ID
        public static string gl_ProjectName = "";//项目名称
        #endregion



        #region----------------文件中的信息------------
        public static int gl_FilTotaltime = 0; //测试结束时间
        public static string gl_FilestartTime = "";//测试开始时间
        public static string gl_FileEndtime = ""; //测试结束时间
        public static string gl_FileMachineTime = ""; //机动时间
        public static string gl_FileErrorTime = ""; //异常时间
        public static string gl_FileFreeTime = "";//空闲时间
        public static string gl_FileTestTime = ""; //测试时间
        #endregion-----------------------------
        #region----------------配置文件----------------
        //----------------*保存在本地的信息*------------------- 
        public const string gl_iniTBS_FileName = "CONFIG.INI";
        public const string gl_inisection_Global = "Global";
        public const string gl_iniKey_MultimetersIP = "MultimetersIP";              //万用表IP地址
        public const string gl_iniKey_ModBusIP = "ModBusIP";            //ModbusIP地址       
        public const string gl_iniKey_ModBusPort = "ModBusPort";       //ModbusPort口
        public const string gl_iniKey_TestOneSuperiorLimit = "TestOneSuperiorLimit";//测试项一上限
        public const string gl_iniKey_TestOneLowerLimit = "TestOneLowerLimit";//测试项一下限
        public const string gl_iniKey_TestTwoSuperiorLimit = "TestTwoSuperiorLimit";//测试项二上限
        public const string gl_iniKey_TestTwoLowerLimit = "TestTwoLowerLimit";//测试项二下限
        public const string gl_iniKey_TestThreeSuperiorLimit = "TestThreeSuperiorLimit";//测试项三上限
        public const string gl_iniKey_TestTThreeLowerLimit = "TestTThreeLowerLimit";//测试项三下限
        public const string gl_iniKey_TestFourSuperiorLimit = "TestFourSuperiorLimit";//测试项四上限
        public const string gl_iniKey_TestFoueLowerLimit = "TestFoueLowerLimit";//测试项四下限
        public const string gl_iniKey_TestPcsCount = "TestPcsCount";      //测试条码数量
        public const string gl_iniKey_TestPcsPassCount = "TestPcsPassCount";      //测试OK数量
        public const string gl_iniKey_TestPcsFailCount = "TestPcsFailCount";      //测试NG数量
        //-----------------------------------------------------
        #endregion

        #region----------------全局消息----------------

        public const int WM_ResultPass = 0x0400 + 1;//发送测试OK 
        public const int WM_ResultFail = 0x0400 + 2;//发送测试NG
        public const int WM_StartTest = 0x0400 + 3;//发送开始测试
        public const int WM_RemoveControl = 0x0400 + 4;//移除测试项
        public const int WM_RenamControl = 0x0400 + 5;//控件重新命名
        #endregion

        #region----------------Modbus参数地址----------
        public static int gl_WordLength = 1;
        public static int gl_WordLongLength = 2;
        public static int gl_ErrorWordLongLength = 3;
        public static int gl_AlarmLength = 1;
        public static bool modbusStatue = false;

        public static int gl_PermitWork = 00000;	//作业允许
        public static int gl_StartTest = gl_PermitWork + 1;	//启动按钮信号
        public static int gl_SolenoidvalveSignal = gl_StartTest + 1;	//电磁阀动作信号
        public static int gl_PhotocouplerSignal = gl_SolenoidvalveSignal + 1;	//槽型光耦信号
        public static int gl_RasterProtectSignal = gl_PhotocouplerSignal + 1;	//光栅保护信号



        public static int gl_RelayOne = 200;	            //继电器1
        public static int gl_RelayTwo = gl_RelayOne + 1;	//继电器2
        public static int gl_RelayThree = gl_RelayTwo + 1;	//继电器3
        public static int gl_RelayFour = gl_RelayThree + 1;	//继电器3


        public static int gl_CompleteTest = 400;//完成信号
        public static int gl_TestResult = gl_CompleteTest + 1;//测试结果
        //public static int gl_******* =        1134;	//待定
        //public static int gl_******* =        1135;	//待定
        //public static int gl_******* =        1136;	//待定
        //public static int gl_******* =        1137;	//待定
        //public static int gl_******* =        1191;	//待定
        //public static int gl_******* =        1192;	//待定
        //public static int gl_******* =        1193;	//待定
        //public static int gl_******* =        1194;	//待定
        #endregion--------------------------------------

        #region----------------Modbus参数--------------
        public static string g_Portnumber = ""; //串口名称
        public static int g_nBaudRate = 115200;//波特率
        public static int g_nDataBits = 8;// 数据位
        public static Parity g_Parity = Parity.None;// 校验位
        public static StopBits g_StopBits = StopBits.One;// 停止位

        //------------MODBUS功能码---------------------
        public static byte g_ReadCoil = 1;
        public static byte g_ReadHoldReg = 3;
        public static byte g_WriteSingleHoldReg = 6;
        public static byte g_WriteMultiCoils = 15;
        public static byte g_WriteMultiHoldReg = 16;

        //------------MODBUS报警协议---------------------
        //串口Scanner扫描失败  1013;
        //串口Scanner连接超时 1014;
        //网口Scanner发送失败 1015;
        //网口Scanner连接超时 1016;
        //网口Scanner扫码错误 1017;
        public static string[] gl_ScanFailErrorCodeList = { "1013" };    //扫描失败Pub 错误代码   
        public static string[] gl_ScanErrorCodeList = { "1017" };    //扫码错误Pub 错误代码 
        public static string[] gl_ConncetErrorCodeList = { "1014", "1016" };   //连接超时Pub 错误代码   
        public static string[] gl_SendErrorCodeList = { "1015" };            //发送失败Pub 错误代码   
        public static byte[] gl_UpperDisconnect = { 0x00, 0x01 };//上位机未连接
        public static byte[] gl_ScanFail = { 0x00, 0x04 };    //扫描失败Pub  1013
        public static byte[] gl_ScanError = { 0x00, 0x08 };//扫码错误Pub  1017
        public static byte[] gl_ConnectError = { 0x00, 0x10 };//连接超时Pub 1014   1016
        public static byte[] gl_SendError = { 0x00, 0x20 };//发送失败Pub 1015
        public static byte[] gl_OtherError = { 0x80, 0x00 };//未定义的其他异常
        public static byte[] gl_ClearAll = { 0x00, 0x00 };         //清空报警
        #endregion--------------------------------------

        #region----------------程序参数----------------
        //获取程序版本号
        public static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        //获取文件版本号
        public static string GetFileVersion()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(asm.Location);
            return fvi.FileVersion;
        }
        #endregion----------
    }
}
