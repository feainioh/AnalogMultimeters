using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AnalogMultimeters
{
    class UpdateClass : MyBaseClass
    {
        static String UpdataBase = "Data Source=192.168.208.207;database=BASEDATA;uid=suzmektec;pwd=suzmek;Timeout=5";
        public String NowVersion = "";
        public String dataBaseVersion = "";
        public String Name = "";
        public String HttpPath = "";
        string m_strUpdateLog = "";
        public bool GetVersion()
        {
            string strFullPath = Application.ExecutablePath;
            string AppName = System.IO.Path.GetFileName(strFullPath);
            Name = AppName.Substring(0, AppName.Length - 4);  //取得.exe前面的程序名
            if (Name.Length == GlobalVar.gl_AppName.Length)
                Name = GlobalVar.gl_AppName;
            else
            {
                MessageBox.Show("程序退出，请将程序名称修改成:" + GlobalVar.gl_AppName);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            NowVersion = GlobalVar.GetAssemblyVersion();
            dataBaseVersion = getVesionFromDataBse(AppName);
            if (dataBaseVersion == "")
            {
                return false;
            }
            if (NowVersion == dataBaseVersion)
            {
                return true;
            }
            if (NowVersion != dataBaseVersion)
            {
                if (DialogResult.Cancel ==
                    MessageBox.Show("检测到新版本:" + dataBaseVersion + "\n\r" + "更新内容：" + m_strUpdateLog, "Message", MessageBoxButtons.OKCancel))
                {
                    return false;//放弃更新
                }
                setUpdataStringList(HttpPath);
            }
            for (int i = 0; i < updataStringList.Count; i++)
            {
                String[] Strs = updataStringList[i].Split(',');
                if (!Download(Strs[0] + Strs[1], Application.StartupPath + "\\" + Strs[1]))
                {
                    MessageBox.Show("版本更新失败", "Error");
                    return false;
                }
            }
            UpdateVesion(Name + ".exe", Name + ".rar");
            return true;
        }


        List<String> updataStringList = new List<string>();
        public void setUpdataStringList(String http)
        {
            String Sql = "SELECT HttpPath,ServerFile FROM ProgramVersion where HttpPath='" + http + "'";
            SqlConnection con = new SqlConnection(UpdataBase);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(Sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    updataStringList.Add(reader.GetValue(0).ToString() + "," + reader.GetValue(1).ToString());
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                if (reader != null)
                    reader.Close();
                con.Close();
            }
        }

        public String getVesionFromDataBse(String AppName)
        {

            String Sql = "SELECT Version,HttpPath,Memo FROM ProgramVersion where ProgramName='" + AppName + "'";
            SqlConnection con = new SqlConnection(UpdataBase);
            SqlDataReader reader = null;
            String version = "";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(Sql, con);
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    version = reader.GetValue(0).ToString();
                    HttpPath = reader.GetValue(1).ToString();
                    m_strUpdateLog = reader.GetValue(2).ToString();
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                m_strLastError = ex.Message;
                MessageBox.Show("连接数据库失败，请确认网络是否连接");
                if (reader != null)
                    reader.Close();
                con.Close();
            }
            return version;
        }

        public static bool Download(string Add, string savePath)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(Add, savePath);
                wc.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                wc.Dispose();
                MessageBox.Show(Add + "下载失败!\n" + "Exception:" + ex.Message);
                return false;
            }
        }

        public void UpdateVesion(String AppName, String FFileName)
        {


            string str = "taskkill /F /IM " + AppName;
            str += "\r\n" + "@ping 127.0.0.1 -n 2 > nul";
            str += "\r\n" + "del " + AppName;
            str += "\r\n" + "@ping 127.0.0.1 -n 1 > nul";
            str += "\r\n" + "ren " + FFileName + " " + AppName;
            str += "\r\n" + AppName;
            str += "\r\n" + "del update.bat";

            File.WriteAllText("update.bat", str, ASCIIEncoding.Default);
            System.Diagnostics.Process.Start("update.bat");
        }
    }
}
