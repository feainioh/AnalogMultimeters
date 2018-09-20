using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalogMultimeters
{
    public partial class SetParamer : UserControl
    {
        public SetParamer()
        {
            InitializeComponent();          
        }
        public void modifybtnText(string txtname)
        {

            textBox6.Text= txtname;
           

        }
        public void LoadContrlovalue(string model,string rang,string uppadress,string dwnadress,string delatime,string uppvalue,string dwnvalue,string number)
        {
            comboBox1.Text = model;
            comboBox2.Items.Add(rang);
            comboBox2.SelectedIndex = 0;
            textBox1.Text = uppadress;
            textBox2.Text = dwnadress;
            textBox3.Text = delatime;
            textBox4.Text = uppvalue;
            textBox5.Text = dwnvalue;
            textBox6.Text = number;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }      

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RemoveControl, (IntPtr)0, (IntPtr)0);
        }        
        private void textBox6_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                GlobalVar.gl_ControlName = this.Name;
                contextMenuStrip1.Show(this, e.Location);
            }
        }

        private void textBox6_MouseLeave(object sender, EventArgs e)
        {
            textBox6.ReadOnly = true;
            textBox6.BackColor = Color.Turquoise;
        }

        private void textBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox6.ReadOnly = false;
            textBox6.BackColor = Color.White;
        }    

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalVar.gl_ChooseFunction = comboBox1.SelectedItem.ToString();
            comboBox2.Items.Clear();
            switch (GlobalVar.gl_ChooseFunction)
            {
                case "4W电阻":
                    comboBox2.Items.Add("1G");
                    comboBox2.Items.Add("100M");
                    comboBox2.Items.Add("10M");
                    comboBox2.Items.Add("1M");
                    comboBox2.Items.Add("100K");
                    comboBox2.Items.Add("10K");
                    comboBox2.Items.Add("1K");
                    comboBox2.Items.Add("100");
                    break;
                case "电容":
                    comboBox2.Items.Add("10uF");
                    comboBox2.Items.Add("1uF");
                    comboBox2.Items.Add("100nF");
                    comboBox2.Items.Add("10nF");
                    comboBox2.Items.Add("1nF");
                    break;
                case "直流电压":
                    comboBox2.Items.Add("1000V");
                    comboBox2.Items.Add("100V");
                    comboBox2.Items.Add("10V");
                    comboBox2.Items.Add("1V");
                    comboBox2.Items.Add("100mV");
                    break;
                case "直流电流":
                case "交流电流":
                    comboBox2.Items.Add("3A");
                    comboBox2.Items.Add("1A");
                    comboBox2.Items.Add("100mA");
                    comboBox2.Items.Add("10mA");
                    comboBox2.Items.Add("1mA");
                    comboBox2.Items.Add("100uA");
                    break;
                case "二极管":
                    comboBox2.Items.Add("AUTO");
                    break;
                case "交流电压":
                    comboBox2.Items.Add("750V");
                    comboBox2.Items.Add("100V");
                    comboBox2.Items.Add("10V");
                    comboBox2.Items.Add("1V");
                    comboBox2.Items.Add("100mV");
                    break;

                default:
                    break;
            }
        }

       
    }
}
