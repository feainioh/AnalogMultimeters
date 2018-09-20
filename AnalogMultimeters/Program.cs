using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace AnalogMultimeters
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            using (System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    //处理未捕获的异常
                    //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainFrom());
                }
                else
                {
                    MessageBox.Show("只能同时运行一个程序实例！");
                }
            }
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // e.Exception 可通过判断e的Exception来查找异常。
            try
            {
                string errorMsg = "Main Windows窗体线程异常 : \n\n";
                writlog(errorMsg + e.Exception.ToString() + "\r\n" + e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            }
            catch
            {
                writlog("Main不可恢复的Windows窗体异常，应用程序将退出！");
            }
        }
        private static void writlog(string date)
        {
            try
            {
                String path = System.Windows.Forms.Application.StartupPath + "\\Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter writer = new StreamWriter(System.Windows.Forms.Application.StartupPath + "\\Log\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
                writer.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + date);
                writer.Close();                                                                     //写如错误日志
                return;
            }
            catch
            {
                return;
            }
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //e.ExceptionObject
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "Main非窗体线程异常 : \n\n";
                writlog(errorMsg + ex.ToString() + "\r\n" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch
            {
                writlog("Main不可恢复的非Windows窗体线程异常，应用程序将退出！");
            }
        }
    }
}
