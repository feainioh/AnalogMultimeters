using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using AnalogMultimeters;
using System.Data;
using System.Management;
using System.Threading;

namespace AnalogMultimeters
{
    class MyFunctions
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(
                            IntPtr hwnd,
                            int wMsg,
                            IntPtr wParam,
                            IntPtr lParam);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);
        public static Mutex Wrimutex = new Mutex();
        /// <summary>
        /// 获得接收消息标志字符  E\H\R....
        /// </summary>
        /// <param name="str">需要带前缀?或!</param>
        /// <returns></returns>
        public string getTagString(string str)
        {
            return str.Substring(1, 1);
        }

        /// <summary>
        /// 调试输出文本
        /// </summary>
        /// <param name="str">输出内容</param>
        internal static void Output(string str)
        {
            Console.WriteLine("{0}\t{1}", DateTime.Now.ToString("HH:mm:ss:fff"), str);
        }
        //获得校验码
        public string getCRCCode(string str)
        {
            try
            {
                return str.Substring(str.IndexOf('#') + 1, 4);
            }
            catch { return ""; }
        }

        //获得收到信息中的有效字串(除?/@和#+校验码外的字符)
        public string getValidStringMsg(string str)
        {
            try
            {
                str = str.Substring(1);
                str = str.Substring(0, str.IndexOf("#"));
                return str;
            }
            catch { return ""; }
        }
        /// <summary>
        /// 获取本机所有的IP4地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public IPAddress[] GetAllIP4()
        {
            List<IPAddress> ipAddr = new List<IPAddress>();
            IPAddress[] arrIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIP)
            {
                string[] ipAdress = ip.ToString().Split('.');
                if (ipAdress[0].ToString() == "172")
                {
                    ipAddr.Add(ip);
                }

            }
            return ipAddr.ToArray();
        }

        /// <summary>
        /// 获取该IP地址下的子网掩码
        /// </summary>
        /// <param name="IPAddr">IP地址</param>
        /// <returns></returns>
        internal string GetSubnet(string IPAddr)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    string mac = nic["MacAddress"].ToString();//Mac地址
                    string ip = (nic["IPAddress"] as String[])[0];//IP地址
                    string ipsubnet = (nic["IPSubnet"] as String[])[0];//子网掩码
                    //string ipgateway = (nic["DefaultIPGateway"] as String[])[0];//默认网关
                    if (ip == IPAddr) return ipsubnet;
                }
            }
            return string.Empty;
        }
        public static void deleOldLog()
        {
            try
            {
                String path = System.Windows.Forms.Application.StartupPath + "\\Log\\";
                DirectoryInfo info = new DirectoryInfo(path);
                foreach (FileInfo fileinfo in info.GetFiles())
                {
                    TimeSpan ts = DateTime.Now.Subtract(fileinfo.CreationTime);
                    if (ts.Days > 60)
                    {
                        File.Delete(fileinfo.FullName);
                    }
                }
            }
            catch { }
        }

        public void writelog(string str)
        {
            try
            {
                deleOldLog();
                String path = System.Windows.Forms.Application.StartupPath + "\\Log\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter writer = new StreamWriter(path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
                writer.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + "\t" + str);
                writer.Close();        //写如错误日志
                return;
            }
            catch
            {
                return;
            }
        }
        public void writValue(string str)//写入测试数据
        {
            try
            {
                String path = System.Windows.Forms.Application.StartupPath + "\\ValueInfo\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "value.txt";
                if (File.Exists(path))
                    File.Delete(path);
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(str);
                writer.Close();
                return;
            }
            catch
            {
                return;
            }
        }

        public static void Write(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public static string Read(string Section, string Key, string path)
        {
            StringBuilder temp = new StringBuilder(5000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 5000, path);
            return temp.ToString();
        }

        //CRC8位校验
        public string CRC8(string str)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(str);
            short crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= (Int16)(buffer[j] << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) > 0)
                    {
                        crc = (Int16)((crc << 1) ^ 0x1021);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }
            return string.Format(Convert.ToString(crc, 16).ToUpper().PadLeft(4, '0'), "0000");
        }

        public Bitmap copyImage(Bitmap sourceBmp, int startX, int startY, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            if ((startX + width >= sourceBmp.Width)
                || (startY + height >= sourceBmp.Height))
            {
                return bitmap;
            }
            try
            {
                Graphics g = Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(sourceBmp, new Rectangle(0, 0, bitmap.Width, bitmap.Height)
                    , startX, startY, width, height, GraphicsUnit.Pixel);
                g.Save();
                return bitmap;
            }
            catch { return bitmap; }
            finally { }
        }

        public static void appendNewLogMessage(string str)
        {
            try
            {
                string dirName = Application.StartupPath + "\\LOG\\";
                if (!Directory.Exists(dirName))
                { Directory.CreateDirectory(dirName); }

                string _logfile = dirName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                FileStream FS = new FileStream(_logfile, FileMode.Append);
                string str_record = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\0\0\0\0" + str;
                StreamWriter SW = new StreamWriter(FS);
                SW.WriteLine(str_record);
                SW.Close();
                SW.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// 检验输入是否合法
        /// </summary>
        /// <param name="str">检测字串</param>
        /// <param name="checkType">1：数字  2：英文字符  3：数字+英文字符+"-"</param>
        /// <returns></returns>
        public bool checkStringIsLegal(string str, int checkType)
        {
            bool result = true;
            if (checkType == 1)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((c >= 48) && (c <= 57));
                }
            }
            else if (checkType == 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= (((c >= 65) && (c <= 90))
                        || ((c >= 97) && (c <= 122)));
                }
            }
            else if (checkType == 3)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((((c >= 65) && (c <= 90)) || ((c >= 97) && (c <= 122)))
                        || ((c >= 48) && (c <= 57)) || ((c == '-')));
                }
            }
            return result;
        }

        /// <summary>
        /// 获得本机IP
        /// </summary>
        /// <returns></returns>
        public IPAddress getHostIP()
        {
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork") //排除IPV6
                {
                    return _IPAddress;
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }

        //判斷IP是否合格
        public bool checkIPStringIsLegal(string str)
        {
            try
            {
                Regex reg = new Regex(@"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
                if (!reg.IsMatch(str))
                {
                    return false;
                }
                string[] array_str = str.Split('.');
                for (int i = 0; i < array_str.Length; i++)
                {
                    if (Convert.ToInt32(array_str[i]) > 255) { return false; }
                }
                return true;
            }
            catch
            { return false; }
        }

        //保存设置到TBS.INI
        public void WriteGlobalInfoToTBS()
        {
            try
            {
                string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                if (!File.Exists(iniFilePath)) { File.Create(iniFilePath); }
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_MultimetersIP, GlobalVar.gl_MultimetersIP.ToString(), iniFilePath);      //万用表IP地址               
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_ModBusIP, GlobalVar.gl_ModBusIP.ToString(), iniFilePath);     //ModbusIP 
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_ModBusPort, GlobalVar.gl_ModBusPort.ToString(), iniFilePath);  //ModbusPort


                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestOneSuperiorLimit, GlobalVar.gl_TestOneSuperiorLimit.ToString(), iniFilePath);  //测试项一上限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestOneLowerLimit, GlobalVar.gl_TestOneLowerLimit, iniFilePath);//测试项一下限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTwoSuperiorLimit, GlobalVar.gl_TestTwoSuperiorLimit.ToString(), iniFilePath);  //测试项二上限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTwoLowerLimit, GlobalVar.gl_TestTwoLowerLimit, iniFilePath);//测试项二下限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestThreeSuperiorLimit, GlobalVar.gl_TestThreeSuperiorLimit.ToString(), iniFilePath);  //测试项三上限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTThreeLowerLimit, GlobalVar.gl_TestThreeLowerLimit, iniFilePath);//测试项三下限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestFourSuperiorLimit, GlobalVar.gl_TestFourSuperiorLimit.ToString(), iniFilePath);  //测试项四上限
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestFoueLowerLimit, GlobalVar.gl_TestFourLowerLimit, iniFilePath);//测试项四下限

                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsCount, GlobalVar.gl_TestPcsCount, iniFilePath);//测试总数量
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsPassCount, GlobalVar.gl_TestPcsPassCount, iniFilePath);//测试OK数量
                WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsFailCount, GlobalVar.gl_TestPcsFailCount, iniFilePath);//测试NG数量

            }
            catch (Exception ex)
            {
                writelog("写入文件时" + ex.ToString());
            }
        }


        //读取TBS.INI配置信息
        public void ReadGlobalInfoFromTBS()
        {
            try
            {
                StringBuilder str_tmp = new StringBuilder(100);
                string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                if (!File.Exists(iniFilePath)) { return; }
                //读取配置信息
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_MultimetersIP, "", str_tmp, 50, iniFilePath);
                GlobalVar.gl_MultimetersIP = str_tmp.ToString().Trim(); //万用表IP地址 
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_ModBusIP, "", str_tmp, 50, iniFilePath);
                GlobalVar.gl_ModBusIP = str_tmp.ToString().Trim();  //ModbusIP 
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_ModBusPort, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_ModBusPort = str_tmp.ToString().Trim();//ModbusPort


                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestOneSuperiorLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestOneSuperiorLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项一上限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestOneLowerLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestOneLowerLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项一下限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTwoSuperiorLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestTwoSuperiorLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项二上限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTwoLowerLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestTwoLowerLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项二下限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestThreeSuperiorLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestThreeSuperiorLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项三上限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestTThreeLowerLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestThreeLowerLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项三下限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestFourSuperiorLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestFourSuperiorLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项四上限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestFoueLowerLimit, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestFourLowerLimit = (str_tmp.ToString().Trim() == "") ? "0" : str_tmp.ToString().Trim();//测试项四下限
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsCount, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestPcsCount = str_tmp.ToString().Trim() == "" ? "0" : str_tmp.ToString().Trim();//测试条码数量
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsPassCount, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestPcsPassCount = str_tmp.ToString().Trim() == "" ? "0" : str_tmp.ToString().Trim();//测试OK数量
                GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_TestPcsFailCount, "", str_tmp, 100, iniFilePath);
                GlobalVar.gl_TestPcsFailCount = str_tmp.ToString().Trim() == "" ? "0" : str_tmp.ToString().Trim();//测试NG数量
            }
            catch (Exception ex)
            {
                writelog("读取文件时" + ex.ToString());
            }
        }


        //网络文件夹
        public void LoadShare()
        {
            Process proc = new Process();
            try
            {
                String path = System.Windows.Forms.Application.StartupPath + "\\Log";
                string dosLine = @"cacls  " + path;
                string doLine = @"net share Log=" + path + "/grant:Everyone,read";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine(doLine);
                proc.StandardInput.WriteLine("exit");
            }
            catch (Exception ex)
            {
                writelog("共享文件夹失败" + ex.ToString());
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
        }


    }
}
