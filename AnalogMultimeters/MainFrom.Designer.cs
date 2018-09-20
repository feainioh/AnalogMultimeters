namespace AnalogMultimeters
{
    partial class MainFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_StartJobs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Adminlogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SetParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ClearAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsll_ConnectDown = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsll_ConnectMultimeters = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsll_SoftVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox_DownStatue = new System.Windows.Forms.GroupBox();
            this.rtboxCMLog = new System.Windows.Forms.RichTextBox();
            this.groupBox_ConnectDown = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_modbusConnect = new System.Windows.Forms.Button();
            this.lbl_modbusIP = new System.Windows.Forms.Label();
            this.txt_modbusPort = new System.Windows.Forms.TextBox();
            this.lbl_modbusPort = new System.Windows.Forms.Label();
            this.txt_mobbusIP = new System.Windows.Forms.TextBox();
            this.groupBox_setfunction = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ProjectName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_comfirm = new System.Windows.Forms.Button();
            this.groupBox_connect = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_IPadress = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txt_IPadress = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_AddTestProject = new System.Windows.Forms.Button();
            this.btn_SaveData = new System.Windows.Forms.Button();
            this.btn_StarTest = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox_DownStatue.SuspendLayout();
            this.groupBox_ConnectDown.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox_setfunction.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBox_connect.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_StartJobs,
            this.tsmi_Adminlogin,
            this.tsmi_SetParameter,
            this.tsmi_ClearAlarm});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1091, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_StartJobs
            // 
            this.tsmi_StartJobs.Name = "tsmi_StartJobs";
            this.tsmi_StartJobs.Size = new System.Drawing.Size(68, 21);
            this.tsmi_StartJobs.Text = "允许作业";
            this.tsmi_StartJobs.Click += new System.EventHandler(this.tsmi_StartJobs_Click);
            // 
            // tsmi_Adminlogin
            // 
            this.tsmi_Adminlogin.Name = "tsmi_Adminlogin";
            this.tsmi_Adminlogin.Size = new System.Drawing.Size(80, 21);
            this.tsmi_Adminlogin.Text = "管理员登录";
            // 
            // tsmi_SetParameter
            // 
            this.tsmi_SetParameter.Name = "tsmi_SetParameter";
            this.tsmi_SetParameter.Size = new System.Drawing.Size(68, 21);
            this.tsmi_SetParameter.Text = "参数设置";
            this.tsmi_SetParameter.Click += new System.EventHandler(this.tsmi_SetParameter_Click);
            // 
            // tsmi_ClearAlarm
            // 
            this.tsmi_ClearAlarm.Name = "tsmi_ClearAlarm";
            this.tsmi_ClearAlarm.Size = new System.Drawing.Size(68, 21);
            this.tsmi_ClearAlarm.Text = "清除异常";
            this.tsmi_ClearAlarm.Click += new System.EventHandler(this.tsmi_ClearAlarm_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsll_ConnectDown,
            this.tsll_ConnectMultimeters,
            this.toolStripStatusLabel1,
            this.tsll_SoftVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1091, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsll_ConnectDown
            // 
            this.tsll_ConnectDown.BackColor = System.Drawing.Color.Red;
            this.tsll_ConnectDown.Name = "tsll_ConnectDown";
            this.tsll_ConnectDown.Size = new System.Drawing.Size(140, 17);
            this.tsll_ConnectDown.Text = "      下位机未连接         ";
            // 
            // tsll_ConnectMultimeters
            // 
            this.tsll_ConnectMultimeters.BackColor = System.Drawing.Color.Red;
            this.tsll_ConnectMultimeters.Name = "tsll_ConnectMultimeters";
            this.tsll_ConnectMultimeters.Size = new System.Drawing.Size(120, 17);
            this.tsll_ConnectMultimeters.Text = "     万用表未连接     ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(733, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tsll_SoftVersion
            // 
            this.tsll_SoftVersion.Name = "tsll_SoftVersion";
            this.tsll_SoftVersion.Size = new System.Drawing.Size(83, 17);
            this.tsll_SoftVersion.Text = "当前程序版本:";
            // 
            // groupBox_DownStatue
            // 
            this.groupBox_DownStatue.Controls.Add(this.rtboxCMLog);
            this.groupBox_DownStatue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_DownStatue.Location = new System.Drawing.Point(3, 315);
            this.groupBox_DownStatue.Name = "groupBox_DownStatue";
            this.groupBox_DownStatue.Size = new System.Drawing.Size(259, 163);
            this.groupBox_DownStatue.TabIndex = 2;
            this.groupBox_DownStatue.TabStop = false;
            this.groupBox_DownStatue.Text = "通讯记录";
            // 
            // rtboxCMLog
            // 
            this.rtboxCMLog.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rtboxCMLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtboxCMLog.Location = new System.Drawing.Point(3, 17);
            this.rtboxCMLog.Name = "rtboxCMLog";
            this.rtboxCMLog.Size = new System.Drawing.Size(253, 143);
            this.rtboxCMLog.TabIndex = 0;
            this.rtboxCMLog.Text = "";
            // 
            // groupBox_ConnectDown
            // 
            this.groupBox_ConnectDown.Controls.Add(this.tableLayoutPanel9);
            this.groupBox_ConnectDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ConnectDown.Location = new System.Drawing.Point(3, 195);
            this.groupBox_ConnectDown.Name = "groupBox_ConnectDown";
            this.groupBox_ConnectDown.Size = new System.Drawing.Size(259, 114);
            this.groupBox_ConnectDown.TabIndex = 1;
            this.groupBox_ConnectDown.TabStop = false;
            this.groupBox_ConnectDown.Text = "基本配置";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.btn_modbusConnect, 1, 2);
            this.tableLayoutPanel9.Controls.Add(this.lbl_modbusIP, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.txt_modbusPort, 1, 1);
            this.tableLayoutPanel9.Controls.Add(this.lbl_modbusPort, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.txt_mobbusIP, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(253, 94);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // btn_modbusConnect
            // 
            this.btn_modbusConnect.AutoSize = true;
            this.btn_modbusConnect.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_modbusConnect.Location = new System.Drawing.Point(68, 65);
            this.btn_modbusConnect.Name = "btn_modbusConnect";
            this.btn_modbusConnect.Size = new System.Drawing.Size(91, 22);
            this.btn_modbusConnect.TabIndex = 4;
            this.btn_modbusConnect.Text = "连接";
            this.btn_modbusConnect.UseVisualStyleBackColor = false;
            this.btn_modbusConnect.Click += new System.EventHandler(this.btn_modbusConnect_Click);
            // 
            // lbl_modbusIP
            // 
            this.lbl_modbusIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_modbusIP.AutoSize = true;
            this.lbl_modbusIP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_modbusIP.Location = new System.Drawing.Point(3, 9);
            this.lbl_modbusIP.Name = "lbl_modbusIP";
            this.lbl_modbusIP.Size = new System.Drawing.Size(59, 12);
            this.lbl_modbusIP.TabIndex = 0;
            this.lbl_modbusIP.Text = "下位机IP:";
            // 
            // txt_modbusPort
            // 
            this.txt_modbusPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_modbusPort.Location = new System.Drawing.Point(68, 36);
            this.txt_modbusPort.Name = "txt_modbusPort";
            this.txt_modbusPort.Size = new System.Drawing.Size(182, 21);
            this.txt_modbusPort.TabIndex = 3;
            this.txt_modbusPort.TextChanged += new System.EventHandler(this.txt_modbusPort_TextChanged);
            // 
            // lbl_modbusPort
            // 
            this.lbl_modbusPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_modbusPort.AutoSize = true;
            this.lbl_modbusPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_modbusPort.Location = new System.Drawing.Point(3, 40);
            this.lbl_modbusPort.Name = "lbl_modbusPort";
            this.lbl_modbusPort.Size = new System.Drawing.Size(47, 12);
            this.lbl_modbusPort.TabIndex = 2;
            this.lbl_modbusPort.Text = "Port口:";
            // 
            // txt_mobbusIP
            // 
            this.txt_mobbusIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_mobbusIP.Location = new System.Drawing.Point(68, 5);
            this.txt_mobbusIP.Name = "txt_mobbusIP";
            this.txt_mobbusIP.Size = new System.Drawing.Size(182, 21);
            this.txt_mobbusIP.TabIndex = 1;
            this.txt_mobbusIP.TextChanged += new System.EventHandler(this.txt_mobbusIP_TextChanged);
            // 
            // groupBox_setfunction
            // 
            this.groupBox_setfunction.Controls.Add(this.tableLayoutPanel8);
            this.groupBox_setfunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_setfunction.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_setfunction.Location = new System.Drawing.Point(3, 99);
            this.groupBox_setfunction.Name = "groupBox_setfunction";
            this.groupBox_setfunction.Size = new System.Drawing.Size(259, 90);
            this.groupBox_setfunction.TabIndex = 0;
            this.groupBox_setfunction.TabStop = false;
            this.groupBox_setfunction.Text = "项目名称";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.lbl_ProjectName, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btn_comfirm, 1, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(253, 70);
            this.tableLayoutPanel8.TabIndex = 5;
            // 
            // lbl_ProjectName
            // 
            this.lbl_ProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ProjectName.AutoSize = true;
            this.lbl_ProjectName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ProjectName.Location = new System.Drawing.Point(3, 11);
            this.lbl_ProjectName.Name = "lbl_ProjectName";
            this.lbl_ProjectName.Size = new System.Drawing.Size(59, 12);
            this.lbl_ProjectName.TabIndex = 1;
            this.lbl_ProjectName.Text = "  名 称 :";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(68, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_comfirm
            // 
            this.btn_comfirm.Location = new System.Drawing.Point(68, 38);
            this.btn_comfirm.Name = "btn_comfirm";
            this.btn_comfirm.Size = new System.Drawing.Size(91, 29);
            this.btn_comfirm.TabIndex = 3;
            this.btn_comfirm.Text = "确定";
            this.btn_comfirm.UseVisualStyleBackColor = true;
            this.btn_comfirm.Click += new System.EventHandler(this.btn_comfirm_Click);
            // 
            // groupBox_connect
            // 
            this.groupBox_connect.Controls.Add(this.tableLayoutPanel7);
            this.groupBox_connect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_connect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_connect.Location = new System.Drawing.Point(3, 3);
            this.groupBox_connect.Name = "groupBox_connect";
            this.groupBox_connect.Size = new System.Drawing.Size(259, 90);
            this.groupBox_connect.TabIndex = 0;
            this.groupBox_connect.TabStop = false;
            this.groupBox_connect.Text = "万用表连接";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.lbl_IPadress, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btn_Connect, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.txt_IPadress, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(253, 70);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // lbl_IPadress
            // 
            this.lbl_IPadress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_IPadress.AutoSize = true;
            this.lbl_IPadress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_IPadress.Location = new System.Drawing.Point(3, 11);
            this.lbl_IPadress.Name = "lbl_IPadress";
            this.lbl_IPadress.Size = new System.Drawing.Size(59, 12);
            this.lbl_IPadress.TabIndex = 1;
            this.lbl_IPadress.Text = "万用表IP:";
            // 
            // btn_Connect
            // 
            this.btn_Connect.AutoSize = true;
            this.btn_Connect.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Connect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Connect.Location = new System.Drawing.Point(68, 38);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(91, 22);
            this.btn_Connect.TabIndex = 0;
            this.btn_Connect.Text = "连接";
            this.btn_Connect.UseVisualStyleBackColor = false;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txt_IPadress
            // 
            this.txt_IPadress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_IPadress.Location = new System.Drawing.Point(68, 7);
            this.txt_IPadress.Name = "txt_IPadress";
            this.txt_IPadress.Size = new System.Drawing.Size(182, 21);
            this.txt_IPadress.TabIndex = 2;
            this.txt_IPadress.TextChanged += new System.EventHandler(this.txt_IPadress_TextChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1091, 487);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox_DownStatue, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.groupBox_setfunction, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.groupBox_ConnectDown, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.groupBox_connect, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(265, 481);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(274, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(814, 481);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btn_AddTestProject, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_SaveData, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_StarTest, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(572, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(239, 475);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_AddTestProject
            // 
            this.btn_AddTestProject.Location = new System.Drawing.Point(3, 3);
            this.btn_AddTestProject.Name = "btn_AddTestProject";
            this.btn_AddTestProject.Size = new System.Drawing.Size(75, 23);
            this.btn_AddTestProject.TabIndex = 0;
            this.btn_AddTestProject.Text = "增加";
            this.btn_AddTestProject.UseVisualStyleBackColor = true;
            this.btn_AddTestProject.Click += new System.EventHandler(this.btn_AddTestProject_Click);
            // 
            // btn_SaveData
            // 
            this.btn_SaveData.Location = new System.Drawing.Point(3, 86);
            this.btn_SaveData.Name = "btn_SaveData";
            this.btn_SaveData.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveData.TabIndex = 1;
            this.btn_SaveData.Text = "保存";
            this.btn_SaveData.UseVisualStyleBackColor = true;
            this.btn_SaveData.Click += new System.EventHandler(this.btn_SaveData_Click);
            // 
            // btn_StarTest
            // 
            this.btn_StarTest.Location = new System.Drawing.Point(3, 169);
            this.btn_StarTest.Name = "btn_StarTest";
            this.btn_StarTest.Size = new System.Drawing.Size(75, 23);
            this.btn_StarTest.TabIndex = 2;
            this.btn_StarTest.Text = "开始测试";
            this.btn_StarTest.UseVisualStyleBackColor = true;
            this.btn_StarTest.Click += new System.EventHandler(this.btn_StarTest_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(563, 475);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 534);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "MainFrom";
            this.Text = "Gentex车载加热";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrom_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox_DownStatue.ResumeLayout(false);
            this.groupBox_ConnectDown.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.groupBox_setfunction.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.groupBox_connect.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;      
        private System.Windows.Forms.ToolStripMenuItem tsmi_StartJobs;
        private System.Windows.Forms.ToolStripStatusLabel tsll_ConnectDown;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsll_SoftVersion;
        private System.Windows.Forms.ToolStripStatusLabel tsll_ConnectMultimeters;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SetParameter;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label lbl_IPadress;
        private System.Windows.Forms.TextBox txt_IPadress;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Adminlogin;
        private System.Windows.Forms.GroupBox groupBox_connect;
        private System.Windows.Forms.GroupBox groupBox_setfunction;
        private System.Windows.Forms.Label lbl_ProjectName;
        private System.Windows.Forms.Label lbl_modbusIP;
        private System.Windows.Forms.TextBox txt_mobbusIP;
        private System.Windows.Forms.Label lbl_modbusPort;
        private System.Windows.Forms.TextBox txt_modbusPort;
        private System.Windows.Forms.Button btn_modbusConnect;
        private System.Windows.Forms.GroupBox groupBox_ConnectDown;
        private System.Windows.Forms.GroupBox groupBox_DownStatue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ClearAlarm;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_comfirm;
        private System.Windows.Forms.RichTextBox rtboxCMLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_AddTestProject;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_SaveData;
        private System.Windows.Forms.Button btn_StarTest;
    }
}

