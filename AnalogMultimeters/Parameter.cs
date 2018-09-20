using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalogMultimeters
{
    public partial class Parameter : Form
    {
        public Parameter()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestOneSuperiorLimit = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestOneLowerLimit = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestTwoSuperiorLimit = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestTwoLowerLimit = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestThreeSuperiorLimit = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestThreeLowerLimit = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestFourSuperiorLimit = textBox7.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_TestFourLowerLimit = textBox8.Text;
        }

        private void Parameter_Load(object sender, EventArgs e)
        {
            textBox1.Text = GlobalVar.gl_TestOneSuperiorLimit;
            textBox2.Text = GlobalVar.gl_TestOneLowerLimit;
            textBox3.Text = GlobalVar.gl_TestTwoSuperiorLimit;
            textBox4.Text = GlobalVar.gl_TestTwoLowerLimit;
            textBox5.Text = GlobalVar.gl_TestThreeSuperiorLimit;
            textBox6.Text = GlobalVar.gl_TestThreeLowerLimit;
            textBox7.Text = GlobalVar.gl_TestFourSuperiorLimit;
            textBox8.Text = GlobalVar.gl_TestFourLowerLimit;
        }

        private void btn_Comfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_addProject_Click(object sender, EventArgs e)
        {
            int ControlCount = this.flowLayoutPanel1.Controls.Count;
            string ControlName = "btn" + ControlCount.ToString();

            SetParamer setpara = new SetParamer();
            setpara.Name = ControlName;
            setpara.Width = flowLayoutPanel1.Width - 30;
            this.flowLayoutPanel1.Controls.Add(setpara);
            //flowLayoutPanel1.Controls.SetChildIndex(setpara, 1);
            setpara.modifybtnText("测试项" + (ControlCount + 1));
            //Button button = new Button();
            //button.Name = ControlName;
            //button.Text = "测试项" + (ControlCount + 1);
            //button.Width = flowLayoutPanel1.Width - 30;
            //this.flowLayoutPanel1.Controls.Add(button);
        }

    }
}
