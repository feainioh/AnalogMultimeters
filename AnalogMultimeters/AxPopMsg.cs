using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalogMultimeters
{
    /// <summary>
    /// 自定义消息弹窗
    /// </summary>
    public class AxPopMsg
    {
        /// <summary>
        /// 提示消息显示
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Message(string str)
        {
            return MessageBox.Show(str, "TIPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 询问用户操作
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Question(string str)
        {
            return MessageBox.Show(str, "QUESTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 错误提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Error(string str)
        {
            return MessageBox.Show(str, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 警告提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Warning(string str)
        {
            return MessageBox.Show(str, "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 异常提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Exception(string str)
        {
            return MessageBox.Show(str, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
