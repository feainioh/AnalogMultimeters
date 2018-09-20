using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace AnalogMultimeters
{
    public class MyBaseClass
    {
        /// <summary>
        ///  从配置文件中读出整个Section的内容 
        /// </summary>
        ///  <param name="section">INI文件中的段落</param>
        ///  <param name="lpReturn">返回的数据数组</param>
        ///  <param name="nSize">返回数据的缓冲区长度</param>
        ///  <param name="strFileName">INI文件的完整的路径(包含文件名)</param>
        ///  <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileSection(string section,
                byte[] lpReturn, int nSize, string strFileName);
        /// <summary>
        /// 将一个整个Section的内容写入ini文件的指定Section中
        /// </summary>
        /// <param name="Section">INI文件中的段落名称</param>
        /// <param name="str">要写入的字符串</param>
        /// <param name="strFilePath">INI文件的完整路径(包含文件名)</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int WritePrivateProfileSection(string Section, string str, string strFilePath);
        /// <summary>
        /// 从ini文件的某个Section取得一个key的字符串
        /// </summary>
        /// <param name = "section">INI文件中的段落名称</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="def">无法读取时候时候的缺省数值</param>
        /// <param name="retVal">读取数值</param>
        /// <param name="size">数值的大小</param>
        /// <param name="filePath">INI文件的完整路径(包含文件名)</param>  
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 将一个Key值写入Win.ini文件的指定Section中
        /// </summary>
        /// <param name="section">INI文件中的段落</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="val">INI文件中关键字的数值</param>
        /// <param name="filePath">INI文件的完整的路径(包含文件名)</param> 
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);
        protected string m_strLastError = "";
        public string GetLastError() 
        {
            return m_strLastError;
        }
    }
}
