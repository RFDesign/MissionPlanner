namespace MissionPlanner.Radio
{
    partial class Sikradio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sikradio));
            this.Progressbar = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.RSSI = new System.Windows.Forms.TextBox();
            this.configManagerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.RTSCTS = new System.Windows.Forms.CheckBox();
            this.MAX_FREQ = new System.Windows.Forms.ComboBox();
            this.NUM_CHANNELS = new System.Windows.Forms.ComboBox();
            this.LBT_RSSI = new System.Windows.Forms.ComboBox();
            this.MIN_FREQ = new System.Windows.Forms.ComboBox();
            this.DUTY_CYCLE = new System.Windows.Forms.ComboBox();
            this.GPO1_1R_COUT = new System.Windows.Forms.CheckBox();
            this.GPI1_1R_CIN = new System.Windows.Forms.CheckBox();
            this.MAVLINK = new System.Windows.Forms.ComboBox();
            this.SERIAL_SPEED = new System.Windows.Forms.ComboBox();
            this.AIR_SPEED = new System.Windows.Forms.ComboBox();
            this.NETID = new System.Windows.Forms.ComboBox();
            this.TXPOWER = new System.Windows.Forms.ComboBox();
            this.OPPRESEND = new System.Windows.Forms.CheckBox();
            this.ENCRYPTION_LEVEL = new System.Windows.Forms.ComboBox();
            this.FSFRAMELOSS = new System.Windows.Forms.ComboBox();
            this.FORMAT = new System.Windows.Forms.TextBox();
            this.comboSyncMode = new System.Windows.Forms.ComboBox();
            this.AUXSER_SPEED = new System.Windows.Forms.ComboBox();
            this.AIR_FRAMELEN = new System.Windows.Forms.ComboBox();
            this.RSSI_IN_DBM = new System.Windows.Forms.CheckBox();
            this.BUT_SetPPMFailSafe = new MissionPlanner.Controls.MyButton();
            this.BUT_savesettings = new MissionPlanner.Controls.MyButton();
            this.BUT_getcurrent = new MissionPlanner.Controls.MyButton();
            this.MAX_WINDOW = new System.Windows.Forms.ComboBox();
            this.GPO1_3STATLED = new System.Windows.Forms.CheckBox();
            this.GPO1_0TXEN485 = new System.Windows.Forms.CheckBox();
            this.ATI = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.ATI3 = new System.Windows.Forms.TextBox();
            this.linkLabel_mavlink = new System.Windows.Forms.LinkLabel();
            this.linkLabel_lowlatency = new System.Windows.Forms.LinkLabel();
            this.label54 = new System.Windows.Forms.Label();
            this.GPO1_3AUXOUT = new System.Windows.Forms.CheckBox();
            this.lblGPO1_3AUXOUT = new System.Windows.Forms.Label();
            this.GPI1_2AUXIN = new System.Windows.Forms.CheckBox();
            this.lblGPI1_2AUXIN = new System.Windows.Forms.Label();
            this.lblGPIO1_1FUNC = new System.Windows.Forms.Label();
            this.GPIO1_1FUNC = new System.Windows.Forms.ComboBox();
            this.lblGPO1_0TXEN485 = new System.Windows.Forms.Label();
            this.lblGPO1_3STATLED = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.RATE_FREQBAND = new System.Windows.Forms.ComboBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.lblRX_ENCAP_METHOD = new System.Windows.Forms.Label();
            this.RX_ENCAP_METHOD = new System.Windows.Forms.ComboBox();
            this.lblTX_ENCAP_METHOD = new System.Windows.Forms.Label();
            this.TX_ENCAP_METHOD = new System.Windows.Forms.ComboBox();
            this.lblDESTID = new System.Windows.Forms.Label();
            this.lblNODEID = new System.Windows.Forms.Label();
            this.DESTID = new System.Windows.Forms.ComboBox();
            this.NODEID = new System.Windows.Forms.ComboBox();
            this.lblGPO1_1R_COUT = new System.Windows.Forms.Label();
            this.lblGPI1_1R_CIN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNETID = new System.Windows.Forms.Label();
            this.lblTXPOWER = new System.Windows.Forms.Label();
            this.lblOPPRESEND = new System.Windows.Forms.Label();
            this.lblMAVLINK = new System.Windows.Forms.Label();
            this.lblANT_MODE = new System.Windows.Forms.Label();
            this.ANT_MODE = new System.Windows.Forms.ComboBox();
            this.lblSER_BRK_DETMS = new System.Windows.Forms.Label();
            this.lblGLOBAL_RETRIES = new System.Windows.Forms.Label();
            this.lblMAX_RETRIES = new System.Windows.Forms.Label();
            this.SER_BRK_DETMS = new System.Windows.Forms.ComboBox();
            this.GLOBAL_RETRIES = new System.Windows.Forms.ComboBox();
            this.MAX_RETRIES = new System.Windows.Forms.ComboBox();
            this.MAX_DATA = new System.Windows.Forms.ComboBox();
            this.lblMAX_DATA = new System.Windows.Forms.Label();
            this.lblENCRYPTION_LEVEL = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.AESKEY = new System.Windows.Forms.TextBox();
            this.lblRTSCTS = new System.Windows.Forms.Label();
            this.lblMAX_WINDOW = new System.Windows.Forms.Label();
            this.lblMIN_FREQ = new System.Windows.Forms.Label();
            this.lblLBT_RSSI = new System.Windows.Forms.Label();
            this.lblDUTY_CYCLE = new System.Windows.Forms.Label();
            this.lblNUM_CHANNELS = new System.Windows.Forms.Label();
            this.lblMAX_FREQ = new System.Windows.Forms.Label();
            this.ATI2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.listDevices = new System.Windows.Forms.ListBox();
            this.modemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupFirmware = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BUT_loadcustom = new MissionPlanner.Controls.MyButton();
            this.groupRadio = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupData = new System.Windows.Forms.GroupBox();
            this.groupGPIO = new System.Windows.Forms.GroupBox();
            this.GPO1_3SBUSIN = new System.Windows.Forms.CheckBox();
            this.lblSBUSIN = new System.Windows.Forms.Label();
            this.lblSBUSOUT = new System.Windows.Forms.Label();
            this.GPO1_3SBUSOUT = new System.Windows.Forms.ComboBox();
            this.GPO1_1SBUSIN = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GPO1_1SBUSOUT = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BUT_upload = new MissionPlanner.Controls.MyButton();
            this.BUT_Syncoptions = new MissionPlanner.Controls.MyButton();
            this.btnLoadFromFile = new MissionPlanner.Controls.MyButton();
            this.btnSaveToFile = new MissionPlanner.Controls.MyButton();
            this.BUT_resettodefault = new MissionPlanner.Controls.MyButton();
            this.flowLayoutActions = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutMain = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).BeginInit();
            this.groupFirmware.SuspendLayout();
            this.groupRadio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupData.SuspendLayout();
            this.groupGPIO.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutActions.SuspendLayout();
            this.flowLayoutSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Progressbar
            // 
            resources.ApplyResources(this.Progressbar, "Progressbar");
            this.Progressbar.Name = "Progressbar";
            this.Progressbar.Click += new System.EventHandler(this.Progressbar_Click);
            // 
            // RSSI
            // 
            this.RSSI.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "RSSI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.RSSI, "RSSI");
            this.RSSI.Name = "RSSI";
            this.RSSI.ReadOnly = true;
            this.toolTip1.SetToolTip(this.RSSI, resources.GetString("RSSI.ToolTip"));
            // 
            // configManagerBindingSource
            // 
            this.configManagerBindingSource.DataSource = typeof(RFDCommon.ConfigManager);
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabel1, resources.GetString("linkLabel1.ToolTip"));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // RTSCTS
            // 
            resources.ApplyResources(this.RTSCTS, "RTSCTS");
            this.RTSCTS.Name = "RTSCTS";
            this.toolTip1.SetToolTip(this.RTSCTS, resources.GetString("RTSCTS.ToolTip"));
            // 
            // MAX_FREQ
            // 
            this.MAX_FREQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MAX_FREQ, "MAX_FREQ");
            this.MAX_FREQ.FormattingEnabled = true;
            this.MAX_FREQ.Items.AddRange(new object[] {
            resources.GetString("MAX_FREQ.Items"),
            resources.GetString("MAX_FREQ.Items1"),
            resources.GetString("MAX_FREQ.Items2"),
            resources.GetString("MAX_FREQ.Items3"),
            resources.GetString("MAX_FREQ.Items4"),
            resources.GetString("MAX_FREQ.Items5"),
            resources.GetString("MAX_FREQ.Items6"),
            resources.GetString("MAX_FREQ.Items7"),
            resources.GetString("MAX_FREQ.Items8")});
            this.MAX_FREQ.Name = "MAX_FREQ";
            this.toolTip1.SetToolTip(this.MAX_FREQ, resources.GetString("MAX_FREQ.ToolTip"));
            // 
            // NUM_CHANNELS
            // 
            this.NUM_CHANNELS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.NUM_CHANNELS, "NUM_CHANNELS");
            this.NUM_CHANNELS.FormattingEnabled = true;
            this.NUM_CHANNELS.Items.AddRange(new object[] {
            resources.GetString("NUM_CHANNELS.Items"),
            resources.GetString("NUM_CHANNELS.Items1"),
            resources.GetString("NUM_CHANNELS.Items2"),
            resources.GetString("NUM_CHANNELS.Items3"),
            resources.GetString("NUM_CHANNELS.Items4"),
            resources.GetString("NUM_CHANNELS.Items5"),
            resources.GetString("NUM_CHANNELS.Items6"),
            resources.GetString("NUM_CHANNELS.Items7"),
            resources.GetString("NUM_CHANNELS.Items8"),
            resources.GetString("NUM_CHANNELS.Items9"),
            resources.GetString("NUM_CHANNELS.Items10"),
            resources.GetString("NUM_CHANNELS.Items11"),
            resources.GetString("NUM_CHANNELS.Items12"),
            resources.GetString("NUM_CHANNELS.Items13"),
            resources.GetString("NUM_CHANNELS.Items14"),
            resources.GetString("NUM_CHANNELS.Items15"),
            resources.GetString("NUM_CHANNELS.Items16"),
            resources.GetString("NUM_CHANNELS.Items17"),
            resources.GetString("NUM_CHANNELS.Items18")});
            this.NUM_CHANNELS.Name = "NUM_CHANNELS";
            this.toolTip1.SetToolTip(this.NUM_CHANNELS, resources.GetString("NUM_CHANNELS.ToolTip"));
            // 
            // LBT_RSSI
            // 
            this.LBT_RSSI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.LBT_RSSI, "LBT_RSSI");
            this.LBT_RSSI.FormattingEnabled = true;
            this.LBT_RSSI.Items.AddRange(new object[] {
            resources.GetString("LBT_RSSI.Items"),
            resources.GetString("LBT_RSSI.Items1"),
            resources.GetString("LBT_RSSI.Items2"),
            resources.GetString("LBT_RSSI.Items3"),
            resources.GetString("LBT_RSSI.Items4"),
            resources.GetString("LBT_RSSI.Items5"),
            resources.GetString("LBT_RSSI.Items6")});
            this.LBT_RSSI.Name = "LBT_RSSI";
            this.toolTip1.SetToolTip(this.LBT_RSSI, resources.GetString("LBT_RSSI.ToolTip"));
            // 
            // MIN_FREQ
            // 
            this.MIN_FREQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MIN_FREQ, "MIN_FREQ");
            this.MIN_FREQ.FormattingEnabled = true;
            this.MIN_FREQ.Name = "MIN_FREQ";
            this.toolTip1.SetToolTip(this.MIN_FREQ, resources.GetString("MIN_FREQ.ToolTip"));
            // 
            // DUTY_CYCLE
            // 
            this.DUTY_CYCLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DUTY_CYCLE, "DUTY_CYCLE");
            this.DUTY_CYCLE.FormattingEnabled = true;
            this.DUTY_CYCLE.Items.AddRange(new object[] {
            resources.GetString("DUTY_CYCLE.Items"),
            resources.GetString("DUTY_CYCLE.Items1"),
            resources.GetString("DUTY_CYCLE.Items2"),
            resources.GetString("DUTY_CYCLE.Items3"),
            resources.GetString("DUTY_CYCLE.Items4"),
            resources.GetString("DUTY_CYCLE.Items5"),
            resources.GetString("DUTY_CYCLE.Items6"),
            resources.GetString("DUTY_CYCLE.Items7"),
            resources.GetString("DUTY_CYCLE.Items8"),
            resources.GetString("DUTY_CYCLE.Items9")});
            this.DUTY_CYCLE.Name = "DUTY_CYCLE";
            this.toolTip1.SetToolTip(this.DUTY_CYCLE, resources.GetString("DUTY_CYCLE.ToolTip"));
            // 
            // GPO1_1R_COUT
            // 
            resources.ApplyResources(this.GPO1_1R_COUT, "GPO1_1R_COUT");
            this.GPO1_1R_COUT.Name = "GPO1_1R_COUT";
            this.toolTip1.SetToolTip(this.GPO1_1R_COUT, resources.GetString("GPO1_1R_COUT.ToolTip"));
            // 
            // GPI1_1R_CIN
            // 
            resources.ApplyResources(this.GPI1_1R_CIN, "GPI1_1R_CIN");
            this.GPI1_1R_CIN.Name = "GPI1_1R_CIN";
            this.toolTip1.SetToolTip(this.GPI1_1R_CIN, resources.GetString("GPI1_1R_CIN.ToolTip"));
            // 
            // MAVLINK
            // 
            this.MAVLINK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MAVLINK, "MAVLINK");
            this.MAVLINK.FormattingEnabled = true;
            this.MAVLINK.Name = "MAVLINK";
            this.toolTip1.SetToolTip(this.MAVLINK, resources.GetString("MAVLINK.ToolTip"));
            // 
            // SERIAL_SPEED
            // 
            this.SERIAL_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.SERIAL_SPEED, "SERIAL_SPEED");
            this.SERIAL_SPEED.FormattingEnabled = true;
            this.SERIAL_SPEED.Items.AddRange(new object[] {
            resources.GetString("SERIAL_SPEED.Items"),
            resources.GetString("SERIAL_SPEED.Items1"),
            resources.GetString("SERIAL_SPEED.Items2"),
            resources.GetString("SERIAL_SPEED.Items3"),
            resources.GetString("SERIAL_SPEED.Items4"),
            resources.GetString("SERIAL_SPEED.Items5"),
            resources.GetString("SERIAL_SPEED.Items6"),
            resources.GetString("SERIAL_SPEED.Items7"),
            resources.GetString("SERIAL_SPEED.Items8")});
            this.SERIAL_SPEED.Name = "SERIAL_SPEED";
            this.toolTip1.SetToolTip(this.SERIAL_SPEED, resources.GetString("SERIAL_SPEED.ToolTip"));
            // 
            // AIR_SPEED
            // 
            this.AIR_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.AIR_SPEED, "AIR_SPEED");
            this.AIR_SPEED.FormattingEnabled = true;
            this.AIR_SPEED.Items.AddRange(new object[] {
            resources.GetString("AIR_SPEED.Items"),
            resources.GetString("AIR_SPEED.Items1"),
            resources.GetString("AIR_SPEED.Items2"),
            resources.GetString("AIR_SPEED.Items3"),
            resources.GetString("AIR_SPEED.Items4"),
            resources.GetString("AIR_SPEED.Items5"),
            resources.GetString("AIR_SPEED.Items6"),
            resources.GetString("AIR_SPEED.Items7"),
            resources.GetString("AIR_SPEED.Items8"),
            resources.GetString("AIR_SPEED.Items9"),
            resources.GetString("AIR_SPEED.Items10"),
            resources.GetString("AIR_SPEED.Items11"),
            resources.GetString("AIR_SPEED.Items12")});
            this.AIR_SPEED.Name = "AIR_SPEED";
            this.toolTip1.SetToolTip(this.AIR_SPEED, resources.GetString("AIR_SPEED.ToolTip"));
            // 
            // NETID
            // 
            this.NETID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.NETID, "NETID");
            this.NETID.FormattingEnabled = true;
            this.NETID.Items.AddRange(new object[] {
            resources.GetString("NETID.Items"),
            resources.GetString("NETID.Items1"),
            resources.GetString("NETID.Items2"),
            resources.GetString("NETID.Items3"),
            resources.GetString("NETID.Items4"),
            resources.GetString("NETID.Items5"),
            resources.GetString("NETID.Items6"),
            resources.GetString("NETID.Items7"),
            resources.GetString("NETID.Items8"),
            resources.GetString("NETID.Items9"),
            resources.GetString("NETID.Items10"),
            resources.GetString("NETID.Items11"),
            resources.GetString("NETID.Items12"),
            resources.GetString("NETID.Items13"),
            resources.GetString("NETID.Items14"),
            resources.GetString("NETID.Items15"),
            resources.GetString("NETID.Items16"),
            resources.GetString("NETID.Items17"),
            resources.GetString("NETID.Items18"),
            resources.GetString("NETID.Items19"),
            resources.GetString("NETID.Items20"),
            resources.GetString("NETID.Items21"),
            resources.GetString("NETID.Items22"),
            resources.GetString("NETID.Items23"),
            resources.GetString("NETID.Items24"),
            resources.GetString("NETID.Items25"),
            resources.GetString("NETID.Items26"),
            resources.GetString("NETID.Items27"),
            resources.GetString("NETID.Items28"),
            resources.GetString("NETID.Items29")});
            this.NETID.Name = "NETID";
            this.toolTip1.SetToolTip(this.NETID, resources.GetString("NETID.ToolTip"));
            // 
            // TXPOWER
            // 
            this.TXPOWER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.TXPOWER, "TXPOWER");
            this.TXPOWER.FormattingEnabled = true;
            this.TXPOWER.Items.AddRange(new object[] {
            resources.GetString("TXPOWER.Items"),
            resources.GetString("TXPOWER.Items1"),
            resources.GetString("TXPOWER.Items2"),
            resources.GetString("TXPOWER.Items3"),
            resources.GetString("TXPOWER.Items4"),
            resources.GetString("TXPOWER.Items5"),
            resources.GetString("TXPOWER.Items6"),
            resources.GetString("TXPOWER.Items7")});
            this.TXPOWER.Name = "TXPOWER";
            this.toolTip1.SetToolTip(this.TXPOWER, resources.GetString("TXPOWER.ToolTip"));
            // 
            // OPPRESEND
            // 
            resources.ApplyResources(this.OPPRESEND, "OPPRESEND");
            this.OPPRESEND.Name = "OPPRESEND";
            this.toolTip1.SetToolTip(this.OPPRESEND, resources.GetString("OPPRESEND.ToolTip"));
            // 
            // ENCRYPTION_LEVEL
            // 
            this.ENCRYPTION_LEVEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ENCRYPTION_LEVEL, "ENCRYPTION_LEVEL");
            this.ENCRYPTION_LEVEL.FormattingEnabled = true;
            this.ENCRYPTION_LEVEL.Items.AddRange(new object[] {
            resources.GetString("ENCRYPTION_LEVEL.Items"),
            resources.GetString("ENCRYPTION_LEVEL.Items1"),
            resources.GetString("ENCRYPTION_LEVEL.Items2"),
            resources.GetString("ENCRYPTION_LEVEL.Items3"),
            resources.GetString("ENCRYPTION_LEVEL.Items4"),
            resources.GetString("ENCRYPTION_LEVEL.Items5"),
            resources.GetString("ENCRYPTION_LEVEL.Items6"),
            resources.GetString("ENCRYPTION_LEVEL.Items7"),
            resources.GetString("ENCRYPTION_LEVEL.Items8"),
            resources.GetString("ENCRYPTION_LEVEL.Items9"),
            resources.GetString("ENCRYPTION_LEVEL.Items10"),
            resources.GetString("ENCRYPTION_LEVEL.Items11"),
            resources.GetString("ENCRYPTION_LEVEL.Items12"),
            resources.GetString("ENCRYPTION_LEVEL.Items13"),
            resources.GetString("ENCRYPTION_LEVEL.Items14"),
            resources.GetString("ENCRYPTION_LEVEL.Items15"),
            resources.GetString("ENCRYPTION_LEVEL.Items16"),
            resources.GetString("ENCRYPTION_LEVEL.Items17"),
            resources.GetString("ENCRYPTION_LEVEL.Items18"),
            resources.GetString("ENCRYPTION_LEVEL.Items19"),
            resources.GetString("ENCRYPTION_LEVEL.Items20"),
            resources.GetString("ENCRYPTION_LEVEL.Items21"),
            resources.GetString("ENCRYPTION_LEVEL.Items22"),
            resources.GetString("ENCRYPTION_LEVEL.Items23"),
            resources.GetString("ENCRYPTION_LEVEL.Items24"),
            resources.GetString("ENCRYPTION_LEVEL.Items25"),
            resources.GetString("ENCRYPTION_LEVEL.Items26"),
            resources.GetString("ENCRYPTION_LEVEL.Items27"),
            resources.GetString("ENCRYPTION_LEVEL.Items28"),
            resources.GetString("ENCRYPTION_LEVEL.Items29"),
            resources.GetString("ENCRYPTION_LEVEL.Items30")});
            this.ENCRYPTION_LEVEL.Name = "ENCRYPTION_LEVEL";
            this.toolTip1.SetToolTip(this.ENCRYPTION_LEVEL, resources.GetString("ENCRYPTION_LEVEL.ToolTip"));
            // 
            // FSFRAMELOSS
            // 
            this.FSFRAMELOSS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.FSFRAMELOSS, "FSFRAMELOSS");
            this.FSFRAMELOSS.FormattingEnabled = true;
            this.FSFRAMELOSS.Name = "FSFRAMELOSS";
            this.toolTip1.SetToolTip(this.FSFRAMELOSS, resources.GetString("FSFRAMELOSS.ToolTip"));
            // 
            // FORMAT
            // 
            resources.ApplyResources(this.FORMAT, "FORMAT");
            this.FORMAT.Name = "FORMAT";
            this.FORMAT.ReadOnly = true;
            this.toolTip1.SetToolTip(this.FORMAT, resources.GetString("FORMAT.ToolTip"));
            // 
            // comboSyncMode
            // 
            this.comboSyncMode.FormattingEnabled = true;
            this.comboSyncMode.Items.AddRange(new object[] {
            resources.GetString("comboSyncMode.Items"),
            resources.GetString("comboSyncMode.Items1"),
            resources.GetString("comboSyncMode.Items2")});
            resources.ApplyResources(this.comboSyncMode, "comboSyncMode");
            this.comboSyncMode.Name = "comboSyncMode";
            this.toolTip1.SetToolTip(this.comboSyncMode, resources.GetString("comboSyncMode.ToolTip"));
            // 
            // AUXSER_SPEED
            // 
            this.AUXSER_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.AUXSER_SPEED, "AUXSER_SPEED");
            this.AUXSER_SPEED.FormattingEnabled = true;
            this.AUXSER_SPEED.Items.AddRange(new object[] {
            resources.GetString("AUXSER_SPEED.Items"),
            resources.GetString("AUXSER_SPEED.Items1"),
            resources.GetString("AUXSER_SPEED.Items2"),
            resources.GetString("AUXSER_SPEED.Items3"),
            resources.GetString("AUXSER_SPEED.Items4"),
            resources.GetString("AUXSER_SPEED.Items5"),
            resources.GetString("AUXSER_SPEED.Items6"),
            resources.GetString("AUXSER_SPEED.Items7"),
            resources.GetString("AUXSER_SPEED.Items8")});
            this.AUXSER_SPEED.Name = "AUXSER_SPEED";
            this.toolTip1.SetToolTip(this.AUXSER_SPEED, resources.GetString("AUXSER_SPEED.ToolTip"));
            // 
            // AIR_FRAMELEN
            // 
            this.AIR_FRAMELEN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.AIR_FRAMELEN, "AIR_FRAMELEN");
            this.AIR_FRAMELEN.FormattingEnabled = true;
            this.AIR_FRAMELEN.Name = "AIR_FRAMELEN";
            this.toolTip1.SetToolTip(this.AIR_FRAMELEN, resources.GetString("AIR_FRAMELEN.ToolTip"));
            // 
            // RSSI_IN_DBM
            // 
            resources.ApplyResources(this.RSSI_IN_DBM, "RSSI_IN_DBM");
            this.RSSI_IN_DBM.Name = "RSSI_IN_DBM";
            this.toolTip1.SetToolTip(this.RSSI_IN_DBM, resources.GetString("RSSI_IN_DBM.ToolTip"));
            // 
            // BUT_SetPPMFailSafe
            // 
            resources.ApplyResources(this.BUT_SetPPMFailSafe, "BUT_SetPPMFailSafe");
            this.BUT_SetPPMFailSafe.Name = "BUT_SetPPMFailSafe";
            this.toolTip1.SetToolTip(this.BUT_SetPPMFailSafe, resources.GetString("BUT_SetPPMFailSafe.ToolTip"));
            this.BUT_SetPPMFailSafe.UseVisualStyleBackColor = true;
            this.BUT_SetPPMFailSafe.Click += new System.EventHandler(this.BUT_SetPPMFailSafe_Click);
            // 
            // BUT_savesettings
            // 
            resources.ApplyResources(this.BUT_savesettings, "BUT_savesettings");
            this.BUT_savesettings.Name = "BUT_savesettings";
            this.toolTip1.SetToolTip(this.BUT_savesettings, resources.GetString("BUT_savesettings.ToolTip"));
            this.BUT_savesettings.UseVisualStyleBackColor = true;
            this.BUT_savesettings.Click += new System.EventHandler(this.BUT_savesettings_Click);
            // 
            // BUT_getcurrent
            // 
            resources.ApplyResources(this.BUT_getcurrent, "BUT_getcurrent");
            this.BUT_getcurrent.Name = "BUT_getcurrent";
            this.toolTip1.SetToolTip(this.BUT_getcurrent, resources.GetString("BUT_getcurrent.ToolTip"));
            this.BUT_getcurrent.UseVisualStyleBackColor = true;
            this.BUT_getcurrent.Click += new System.EventHandler(this.BUT_getcurrent_Click);
            // 
            // MAX_WINDOW
            // 
            this.MAX_WINDOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MAX_WINDOW, "MAX_WINDOW");
            this.MAX_WINDOW.FormattingEnabled = true;
            this.MAX_WINDOW.Items.AddRange(new object[] {
            resources.GetString("MAX_WINDOW.Items"),
            resources.GetString("MAX_WINDOW.Items1"),
            resources.GetString("MAX_WINDOW.Items2"),
            resources.GetString("MAX_WINDOW.Items3"),
            resources.GetString("MAX_WINDOW.Items4"),
            resources.GetString("MAX_WINDOW.Items5"),
            resources.GetString("MAX_WINDOW.Items6"),
            resources.GetString("MAX_WINDOW.Items7"),
            resources.GetString("MAX_WINDOW.Items8"),
            resources.GetString("MAX_WINDOW.Items9"),
            resources.GetString("MAX_WINDOW.Items10"),
            resources.GetString("MAX_WINDOW.Items11"),
            resources.GetString("MAX_WINDOW.Items12"),
            resources.GetString("MAX_WINDOW.Items13"),
            resources.GetString("MAX_WINDOW.Items14"),
            resources.GetString("MAX_WINDOW.Items15"),
            resources.GetString("MAX_WINDOW.Items16"),
            resources.GetString("MAX_WINDOW.Items17"),
            resources.GetString("MAX_WINDOW.Items18"),
            resources.GetString("MAX_WINDOW.Items19"),
            resources.GetString("MAX_WINDOW.Items20"),
            resources.GetString("MAX_WINDOW.Items21"),
            resources.GetString("MAX_WINDOW.Items22"),
            resources.GetString("MAX_WINDOW.Items23"),
            resources.GetString("MAX_WINDOW.Items24"),
            resources.GetString("MAX_WINDOW.Items25"),
            resources.GetString("MAX_WINDOW.Items26"),
            resources.GetString("MAX_WINDOW.Items27"),
            resources.GetString("MAX_WINDOW.Items28"),
            resources.GetString("MAX_WINDOW.Items29"),
            resources.GetString("MAX_WINDOW.Items30")});
            this.MAX_WINDOW.Name = "MAX_WINDOW";
            // 
            // GPO1_3STATLED
            // 
            resources.ApplyResources(this.GPO1_3STATLED, "GPO1_3STATLED");
            this.GPO1_3STATLED.Name = "GPO1_3STATLED";
            // 
            // GPO1_0TXEN485
            // 
            resources.ApplyResources(this.GPO1_0TXEN485, "GPO1_0TXEN485");
            this.GPO1_0TXEN485.Name = "GPO1_0TXEN485";
            // 
            // ATI
            // 
            this.ATI.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "ATI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.ATI, "ATI");
            this.ATI.Name = "ATI";
            this.ATI.ReadOnly = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // lbl_status
            // 
            this.lbl_status.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_status, "lbl_status");
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.UseMnemonic = false;
            // 
            // ATI3
            // 
            this.ATI3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "FREQ", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.ATI3, "ATI3");
            this.ATI3.Name = "ATI3";
            this.ATI3.ReadOnly = true;
            // 
            // linkLabel_mavlink
            // 
            resources.ApplyResources(this.linkLabel_mavlink, "linkLabel_mavlink");
            this.linkLabel_mavlink.Name = "linkLabel_mavlink";
            this.linkLabel_mavlink.TabStop = true;
            // 
            // linkLabel_lowlatency
            // 
            resources.ApplyResources(this.linkLabel_lowlatency, "linkLabel_lowlatency");
            this.linkLabel_lowlatency.Name = "linkLabel_lowlatency";
            this.linkLabel_lowlatency.TabStop = true;
            // 
            // label54
            // 
            resources.ApplyResources(this.label54, "label54");
            this.label54.Name = "label54";
            // 
            // GPO1_3AUXOUT
            // 
            resources.ApplyResources(this.GPO1_3AUXOUT, "GPO1_3AUXOUT");
            this.GPO1_3AUXOUT.Name = "GPO1_3AUXOUT";
            // 
            // lblGPO1_3AUXOUT
            // 
            resources.ApplyResources(this.lblGPO1_3AUXOUT, "lblGPO1_3AUXOUT");
            this.lblGPO1_3AUXOUT.Name = "lblGPO1_3AUXOUT";
            // 
            // GPI1_2AUXIN
            // 
            resources.ApplyResources(this.GPI1_2AUXIN, "GPI1_2AUXIN");
            this.GPI1_2AUXIN.Name = "GPI1_2AUXIN";
            // 
            // lblGPI1_2AUXIN
            // 
            resources.ApplyResources(this.lblGPI1_2AUXIN, "lblGPI1_2AUXIN");
            this.lblGPI1_2AUXIN.Name = "lblGPI1_2AUXIN";
            // 
            // lblGPIO1_1FUNC
            // 
            resources.ApplyResources(this.lblGPIO1_1FUNC, "lblGPIO1_1FUNC");
            this.lblGPIO1_1FUNC.Name = "lblGPIO1_1FUNC";
            // 
            // GPIO1_1FUNC
            // 
            this.GPIO1_1FUNC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GPIO1_1FUNC, "GPIO1_1FUNC");
            this.GPIO1_1FUNC.FormattingEnabled = true;
            this.GPIO1_1FUNC.Name = "GPIO1_1FUNC";
            // 
            // lblGPO1_0TXEN485
            // 
            resources.ApplyResources(this.lblGPO1_0TXEN485, "lblGPO1_0TXEN485");
            this.lblGPO1_0TXEN485.Name = "lblGPO1_0TXEN485";
            // 
            // lblGPO1_3STATLED
            // 
            resources.ApplyResources(this.lblGPO1_3STATLED, "lblGPO1_3STATLED");
            this.lblGPO1_3STATLED.Name = "lblGPO1_3STATLED";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // txtCountry
            // 
            this.txtCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "COUNTRY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.ReadOnly = true;
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // RATE_FREQBAND
            // 
            this.RATE_FREQBAND.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.RATE_FREQBAND, "RATE_FREQBAND");
            this.RATE_FREQBAND.FormattingEnabled = true;
            this.RATE_FREQBAND.Name = "RATE_FREQBAND";
            // 
            // btnRandom
            // 
            resources.ApplyResources(this.btnRandom, "btnRandom");
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // lblRX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.lblRX_ENCAP_METHOD, "lblRX_ENCAP_METHOD");
            this.lblRX_ENCAP_METHOD.Name = "lblRX_ENCAP_METHOD";
            // 
            // RX_ENCAP_METHOD
            // 
            this.RX_ENCAP_METHOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.RX_ENCAP_METHOD, "RX_ENCAP_METHOD");
            this.RX_ENCAP_METHOD.FormattingEnabled = true;
            this.RX_ENCAP_METHOD.Name = "RX_ENCAP_METHOD";
            // 
            // lblTX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.lblTX_ENCAP_METHOD, "lblTX_ENCAP_METHOD");
            this.lblTX_ENCAP_METHOD.Name = "lblTX_ENCAP_METHOD";
            // 
            // TX_ENCAP_METHOD
            // 
            this.TX_ENCAP_METHOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.TX_ENCAP_METHOD, "TX_ENCAP_METHOD");
            this.TX_ENCAP_METHOD.FormattingEnabled = true;
            this.TX_ENCAP_METHOD.Name = "TX_ENCAP_METHOD";
            // 
            // lblDESTID
            // 
            resources.ApplyResources(this.lblDESTID, "lblDESTID");
            this.lblDESTID.Name = "lblDESTID";
            // 
            // lblNODEID
            // 
            resources.ApplyResources(this.lblNODEID, "lblNODEID");
            this.lblNODEID.Name = "lblNODEID";
            // 
            // DESTID
            // 
            this.DESTID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DESTID, "DESTID");
            this.DESTID.FormattingEnabled = true;
            this.DESTID.Name = "DESTID";
            // 
            // NODEID
            // 
            this.NODEID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.NODEID, "NODEID");
            this.NODEID.FormattingEnabled = true;
            this.NODEID.Name = "NODEID";
            // 
            // lblGPO1_1R_COUT
            // 
            resources.ApplyResources(this.lblGPO1_1R_COUT, "lblGPO1_1R_COUT");
            this.lblGPO1_1R_COUT.Name = "lblGPO1_1R_COUT";
            // 
            // lblGPI1_1R_CIN
            // 
            resources.ApplyResources(this.lblGPI1_1R_CIN, "lblGPI1_1R_CIN");
            this.lblGPI1_1R_CIN.Name = "lblGPI1_1R_CIN";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblNETID
            // 
            resources.ApplyResources(this.lblNETID, "lblNETID");
            this.lblNETID.Name = "lblNETID";
            // 
            // lblTXPOWER
            // 
            resources.ApplyResources(this.lblTXPOWER, "lblTXPOWER");
            this.lblTXPOWER.Name = "lblTXPOWER";
            // 
            // lblOPPRESEND
            // 
            resources.ApplyResources(this.lblOPPRESEND, "lblOPPRESEND");
            this.lblOPPRESEND.Name = "lblOPPRESEND";
            // 
            // lblMAVLINK
            // 
            resources.ApplyResources(this.lblMAVLINK, "lblMAVLINK");
            this.lblMAVLINK.Name = "lblMAVLINK";
            // 
            // lblANT_MODE
            // 
            resources.ApplyResources(this.lblANT_MODE, "lblANT_MODE");
            this.lblANT_MODE.Name = "lblANT_MODE";
            // 
            // ANT_MODE
            // 
            this.ANT_MODE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ANT_MODE, "ANT_MODE");
            this.ANT_MODE.FormattingEnabled = true;
            this.ANT_MODE.Name = "ANT_MODE";
            // 
            // lblSER_BRK_DETMS
            // 
            resources.ApplyResources(this.lblSER_BRK_DETMS, "lblSER_BRK_DETMS");
            this.lblSER_BRK_DETMS.Name = "lblSER_BRK_DETMS";
            // 
            // lblGLOBAL_RETRIES
            // 
            resources.ApplyResources(this.lblGLOBAL_RETRIES, "lblGLOBAL_RETRIES");
            this.lblGLOBAL_RETRIES.Name = "lblGLOBAL_RETRIES";
            // 
            // lblMAX_RETRIES
            // 
            resources.ApplyResources(this.lblMAX_RETRIES, "lblMAX_RETRIES");
            this.lblMAX_RETRIES.Name = "lblMAX_RETRIES";
            // 
            // SER_BRK_DETMS
            // 
            this.SER_BRK_DETMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.SER_BRK_DETMS, "SER_BRK_DETMS");
            this.SER_BRK_DETMS.FormattingEnabled = true;
            this.SER_BRK_DETMS.Name = "SER_BRK_DETMS";
            // 
            // GLOBAL_RETRIES
            // 
            this.GLOBAL_RETRIES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GLOBAL_RETRIES, "GLOBAL_RETRIES");
            this.GLOBAL_RETRIES.FormattingEnabled = true;
            this.GLOBAL_RETRIES.Name = "GLOBAL_RETRIES";
            // 
            // MAX_RETRIES
            // 
            this.MAX_RETRIES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MAX_RETRIES, "MAX_RETRIES");
            this.MAX_RETRIES.FormattingEnabled = true;
            this.MAX_RETRIES.Name = "MAX_RETRIES";
            // 
            // MAX_DATA
            // 
            this.MAX_DATA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.MAX_DATA, "MAX_DATA");
            this.MAX_DATA.FormattingEnabled = true;
            this.MAX_DATA.Name = "MAX_DATA";
            // 
            // lblMAX_DATA
            // 
            resources.ApplyResources(this.lblMAX_DATA, "lblMAX_DATA");
            this.lblMAX_DATA.Name = "lblMAX_DATA";
            // 
            // lblENCRYPTION_LEVEL
            // 
            resources.ApplyResources(this.lblENCRYPTION_LEVEL, "lblENCRYPTION_LEVEL");
            this.lblENCRYPTION_LEVEL.Name = "lblENCRYPTION_LEVEL";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // AESKEY
            // 
            this.AESKEY.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "AESKEY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.AESKEY, "AESKEY");
            this.AESKEY.Name = "AESKEY";
            // 
            // lblRTSCTS
            // 
            resources.ApplyResources(this.lblRTSCTS, "lblRTSCTS");
            this.lblRTSCTS.Name = "lblRTSCTS";
            // 
            // lblMAX_WINDOW
            // 
            resources.ApplyResources(this.lblMAX_WINDOW, "lblMAX_WINDOW");
            this.lblMAX_WINDOW.Name = "lblMAX_WINDOW";
            // 
            // lblMIN_FREQ
            // 
            resources.ApplyResources(this.lblMIN_FREQ, "lblMIN_FREQ");
            this.lblMIN_FREQ.Name = "lblMIN_FREQ";
            // 
            // lblLBT_RSSI
            // 
            resources.ApplyResources(this.lblLBT_RSSI, "lblLBT_RSSI");
            this.lblLBT_RSSI.Name = "lblLBT_RSSI";
            // 
            // lblDUTY_CYCLE
            // 
            resources.ApplyResources(this.lblDUTY_CYCLE, "lblDUTY_CYCLE");
            this.lblDUTY_CYCLE.Name = "lblDUTY_CYCLE";
            // 
            // lblNUM_CHANNELS
            // 
            resources.ApplyResources(this.lblNUM_CHANNELS, "lblNUM_CHANNELS");
            this.lblNUM_CHANNELS.Name = "lblNUM_CHANNELS";
            // 
            // lblMAX_FREQ
            // 
            resources.ApplyResources(this.lblMAX_FREQ, "lblMAX_FREQ");
            this.lblMAX_FREQ.Name = "lblMAX_FREQ";
            // 
            // ATI2
            // 
            this.ATI2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "BOARD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.ATI2, "ATI2");
            this.ATI2.Name = "ATI2";
            this.ATI2.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // dlgSave
            // 
            this.dlgSave.FileName = "*.ini";
            resources.ApplyResources(this.dlgSave, "dlgSave");
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "*.ini";
            resources.ApplyResources(this.dlgOpen, "dlgOpen");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboSyncMode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.listDevices);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // listDevices
            // 
            this.listDevices.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.configManagerBindingSource, "Current", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.listDevices.DataSource = this.modemsBindingSource;
            this.listDevices.DisplayMember = "DisplayName";
            this.listDevices.FormattingEnabled = true;
            resources.ApplyResources(this.listDevices, "listDevices");
            this.listDevices.Name = "listDevices";
            // 
            // modemsBindingSource
            // 
            this.modemsBindingSource.DataMember = "Modems";
            this.modemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // groupFirmware
            // 
            this.groupFirmware.Controls.Add(this.label7);
            this.groupFirmware.Controls.Add(this.label6);
            this.groupFirmware.Controls.Add(this.ATI2);
            this.groupFirmware.Controls.Add(this.Progressbar);
            this.groupFirmware.Controls.Add(this.ATI);
            this.groupFirmware.Controls.Add(this.label11);
            this.groupFirmware.Controls.Add(this.ATI3);
            this.groupFirmware.Controls.Add(this.txtCountry);
            this.groupFirmware.Controls.Add(this.label49);
            this.groupFirmware.Controls.Add(this.label2);
            this.groupFirmware.Controls.Add(this.FORMAT);
            resources.ApplyResources(this.groupFirmware, "groupFirmware");
            this.groupFirmware.Name = "groupFirmware";
            this.groupFirmware.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // BUT_loadcustom
            // 
            resources.ApplyResources(this.BUT_loadcustom, "BUT_loadcustom");
            this.BUT_loadcustom.Name = "BUT_loadcustom";
            this.BUT_loadcustom.UseVisualStyleBackColor = true;
            this.BUT_loadcustom.Click += new System.EventHandler(this.BUT_loadcustom_Click);
            // 
            // groupRadio
            // 
            this.groupRadio.Controls.Add(this.RSSI_IN_DBM);
            this.groupRadio.Controls.Add(this.label12);
            this.groupRadio.Controls.Add(this.RSSI);
            this.groupRadio.Controls.Add(this.label15);
            this.groupRadio.Controls.Add(this.AIR_FRAMELEN);
            this.groupRadio.Controls.Add(this.label14);
            this.groupRadio.Controls.Add(this.MAVLINK);
            this.groupRadio.Controls.Add(this.lblMAVLINK);
            this.groupRadio.Controls.Add(this.MAX_WINDOW);
            this.groupRadio.Controls.Add(this.LBT_RSSI);
            this.groupRadio.Controls.Add(this.OPPRESEND);
            this.groupRadio.Controls.Add(this.lblMAX_WINDOW);
            this.groupRadio.Controls.Add(this.lblMIN_FREQ);
            this.groupRadio.Controls.Add(this.lblOPPRESEND);
            this.groupRadio.Controls.Add(this.lblLBT_RSSI);
            this.groupRadio.Controls.Add(this.lblTXPOWER);
            this.groupRadio.Controls.Add(this.DUTY_CYCLE);
            this.groupRadio.Controls.Add(this.TXPOWER);
            this.groupRadio.Controls.Add(this.lblMAX_FREQ);
            this.groupRadio.Controls.Add(this.lblNETID);
            this.groupRadio.Controls.Add(this.lblNUM_CHANNELS);
            this.groupRadio.Controls.Add(this.NETID);
            this.groupRadio.Controls.Add(this.MIN_FREQ);
            this.groupRadio.Controls.Add(this.lblDUTY_CYCLE);
            this.groupRadio.Controls.Add(this.NUM_CHANNELS);
            this.groupRadio.Controls.Add(this.AIR_SPEED);
            this.groupRadio.Controls.Add(this.MAX_FREQ);
            this.groupRadio.Controls.Add(this.label45);
            this.groupRadio.Controls.Add(this.label3);
            this.groupRadio.Controls.Add(this.RATE_FREQBAND);
            this.groupRadio.Controls.Add(this.ANT_MODE);
            this.groupRadio.Controls.Add(this.lblANT_MODE);
            resources.ApplyResources(this.groupRadio, "groupRadio");
            this.groupRadio.Name = "groupRadio";
            this.groupRadio.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.AUXSER_SPEED);
            this.groupBox1.Controls.Add(this.lblRTSCTS);
            this.groupBox1.Controls.Add(this.SERIAL_SPEED);
            this.groupBox1.Controls.Add(this.RTSCTS);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // groupData
            // 
            this.groupData.Controls.Add(this.lblTX_ENCAP_METHOD);
            this.groupData.Controls.Add(this.lblMAX_DATA);
            this.groupData.Controls.Add(this.MAX_DATA);
            this.groupData.Controls.Add(this.MAX_RETRIES);
            this.groupData.Controls.Add(this.GLOBAL_RETRIES);
            this.groupData.Controls.Add(this.lblSER_BRK_DETMS);
            this.groupData.Controls.Add(this.SER_BRK_DETMS);
            this.groupData.Controls.Add(this.lblRX_ENCAP_METHOD);
            this.groupData.Controls.Add(this.lblMAX_RETRIES);
            this.groupData.Controls.Add(this.RX_ENCAP_METHOD);
            this.groupData.Controls.Add(this.lblGLOBAL_RETRIES);
            this.groupData.Controls.Add(this.TX_ENCAP_METHOD);
            this.groupData.Controls.Add(this.DESTID);
            this.groupData.Controls.Add(this.lblDESTID);
            this.groupData.Controls.Add(this.NODEID);
            this.groupData.Controls.Add(this.lblNODEID);
            resources.ApplyResources(this.groupData, "groupData");
            this.groupData.Name = "groupData";
            this.groupData.TabStop = false;
            // 
            // groupGPIO
            // 
            this.groupGPIO.Controls.Add(this.GPO1_3SBUSIN);
            this.groupGPIO.Controls.Add(this.lblSBUSIN);
            this.groupGPIO.Controls.Add(this.lblSBUSOUT);
            this.groupGPIO.Controls.Add(this.GPO1_3SBUSOUT);
            this.groupGPIO.Controls.Add(this.BUT_SetPPMFailSafe);
            this.groupGPIO.Controls.Add(this.FSFRAMELOSS);
            this.groupGPIO.Controls.Add(this.label54);
            this.groupGPIO.Controls.Add(this.GPO1_1SBUSIN);
            this.groupGPIO.Controls.Add(this.label13);
            this.groupGPIO.Controls.Add(this.label8);
            this.groupGPIO.Controls.Add(this.GPO1_1SBUSOUT);
            this.groupGPIO.Controls.Add(this.lblGPI1_1R_CIN);
            this.groupGPIO.Controls.Add(this.GPI1_1R_CIN);
            this.groupGPIO.Controls.Add(this.GPO1_1R_COUT);
            this.groupGPIO.Controls.Add(this.lblGPO1_1R_COUT);
            this.groupGPIO.Controls.Add(this.lblGPIO1_1FUNC);
            this.groupGPIO.Controls.Add(this.GPO1_3AUXOUT);
            this.groupGPIO.Controls.Add(this.GPIO1_1FUNC);
            this.groupGPIO.Controls.Add(this.lblGPO1_3AUXOUT);
            this.groupGPIO.Controls.Add(this.GPO1_0TXEN485);
            this.groupGPIO.Controls.Add(this.lblGPO1_0TXEN485);
            this.groupGPIO.Controls.Add(this.GPI1_2AUXIN);
            this.groupGPIO.Controls.Add(this.lblGPO1_3STATLED);
            this.groupGPIO.Controls.Add(this.lblGPI1_2AUXIN);
            this.groupGPIO.Controls.Add(this.GPO1_3STATLED);
            resources.ApplyResources(this.groupGPIO, "groupGPIO");
            this.groupGPIO.Name = "groupGPIO";
            this.groupGPIO.TabStop = false;
            // 
            // GPO1_3SBUSIN
            // 
            resources.ApplyResources(this.GPO1_3SBUSIN, "GPO1_3SBUSIN");
            this.GPO1_3SBUSIN.Name = "GPO1_3SBUSIN";
            // 
            // lblSBUSIN
            // 
            resources.ApplyResources(this.lblSBUSIN, "lblSBUSIN");
            this.lblSBUSIN.Name = "lblSBUSIN";
            // 
            // lblSBUSOUT
            // 
            resources.ApplyResources(this.lblSBUSOUT, "lblSBUSOUT");
            this.lblSBUSOUT.Name = "lblSBUSOUT";
            // 
            // GPO1_3SBUSOUT
            // 
            this.GPO1_3SBUSOUT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GPO1_3SBUSOUT, "GPO1_3SBUSOUT");
            this.GPO1_3SBUSOUT.FormattingEnabled = true;
            this.GPO1_3SBUSOUT.Name = "GPO1_3SBUSOUT";
            // 
            // GPO1_1SBUSIN
            // 
            resources.ApplyResources(this.GPO1_1SBUSIN, "GPO1_1SBUSIN");
            this.GPO1_1SBUSIN.Name = "GPO1_1SBUSIN";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // GPO1_1SBUSOUT
            // 
            this.GPO1_1SBUSOUT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GPO1_1SBUSOUT, "GPO1_1SBUSOUT");
            this.GPO1_1SBUSOUT.FormattingEnabled = true;
            this.GPO1_1SBUSOUT.Name = "GPO1_1SBUSOUT";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AESKEY);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.ENCRYPTION_LEVEL);
            this.groupBox3.Controls.Add(this.lblENCRYPTION_LEVEL);
            this.groupBox3.Controls.Add(this.btnRandom);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // BUT_upload
            // 
            resources.ApplyResources(this.BUT_upload, "BUT_upload");
            this.BUT_upload.Name = "BUT_upload";
            this.BUT_upload.UseVisualStyleBackColor = true;
            this.BUT_upload.Click += new System.EventHandler(this.BUT_upload_Click);
            // 
            // BUT_Syncoptions
            // 
            resources.ApplyResources(this.BUT_Syncoptions, "BUT_Syncoptions");
            this.BUT_Syncoptions.Name = "BUT_Syncoptions";
            this.BUT_Syncoptions.UseVisualStyleBackColor = true;
            this.BUT_Syncoptions.Click += new System.EventHandler(this.BUT_Syncoptions_Click);
            // 
            // btnLoadFromFile
            // 
            resources.ApplyResources(this.btnLoadFromFile, "btnLoadFromFile");
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // btnSaveToFile
            // 
            resources.ApplyResources(this.btnSaveToFile, "btnSaveToFile");
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // BUT_resettodefault
            // 
            resources.ApplyResources(this.BUT_resettodefault, "BUT_resettodefault");
            this.BUT_resettodefault.Name = "BUT_resettodefault";
            this.BUT_resettodefault.UseVisualStyleBackColor = true;
            this.BUT_resettodefault.Click += new System.EventHandler(this.BUT_resettodefault_Click);
            // 
            // flowLayoutActions
            // 
            this.flowLayoutActions.Controls.Add(this.BUT_getcurrent);
            this.flowLayoutActions.Controls.Add(this.BUT_savesettings);
            this.flowLayoutActions.Controls.Add(this.btnLoadFromFile);
            this.flowLayoutActions.Controls.Add(this.btnSaveToFile);
            this.flowLayoutActions.Controls.Add(this.BUT_resettodefault);
            this.flowLayoutActions.Controls.Add(this.BUT_loadcustom);
            this.flowLayoutActions.Controls.Add(this.BUT_upload);
            resources.ApplyResources(this.flowLayoutActions, "flowLayoutActions");
            this.flowLayoutActions.Name = "flowLayoutActions";
            // 
            // flowLayoutSettings
            // 
            this.flowLayoutSettings.Controls.Add(this.groupFirmware);
            resources.ApplyResources(this.flowLayoutSettings, "flowLayoutSettings");
            this.flowLayoutSettings.Name = "flowLayoutSettings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_status);
            this.groupBox2.Controls.Add(this.BUT_Syncoptions);
            this.groupBox2.Controls.Add(this.linkLabel_lowlatency);
            this.groupBox2.Controls.Add(this.linkLabel_mavlink);
            this.groupBox2.Controls.Add(this.linkLabel1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // flowLayoutMain
            // 
            this.flowLayoutMain.Controls.Add(this.groupRadio);
            this.flowLayoutMain.Controls.Add(this.groupBox1);
            this.flowLayoutMain.Controls.Add(this.groupGPIO);
            this.flowLayoutMain.Controls.Add(this.groupData);
            this.flowLayoutMain.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.flowLayoutMain, "flowLayoutMain");
            this.flowLayoutMain.Name = "flowLayoutMain";
            // 
            // Sikradio
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutMain);
            this.Controls.Add(this.flowLayoutSettings);
            this.Controls.Add(this.flowLayoutActions);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            resources.ApplyResources(this, "$this");
            this.Name = "Sikradio";
            this.Load += new System.EventHandler(this.Sikradio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).EndInit();
            this.groupFirmware.ResumeLayout(false);
            this.groupFirmware.PerformLayout();
            this.groupRadio.ResumeLayout(false);
            this.groupRadio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            this.groupGPIO.ResumeLayout(false);
            this.groupGPIO.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutActions.ResumeLayout(false);
            this.flowLayoutSettings.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MyButton BUT_upload;
        private System.Windows.Forms.ProgressBar Progressbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox ATI;
        private System.Windows.Forms.TextBox RSSI;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private Controls.MyButton BUT_Syncoptions;
        private System.Windows.Forms.TextBox ATI3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox ATI2;
        private Controls.MyButton BUT_loadcustom;
        private Controls.MyButton BUT_SetPPMFailSafe;
        private System.Windows.Forms.Label lblRX_ENCAP_METHOD;
        private System.Windows.Forms.ComboBox RX_ENCAP_METHOD;
        private System.Windows.Forms.Label lblTX_ENCAP_METHOD;
        private System.Windows.Forms.ComboBox TX_ENCAP_METHOD;
        private System.Windows.Forms.Label lblDESTID;
        private System.Windows.Forms.Label lblNODEID;
        private System.Windows.Forms.ComboBox DESTID;
        private System.Windows.Forms.ComboBox NODEID;
        private System.Windows.Forms.Label lblGPO1_1R_COUT;
        private System.Windows.Forms.CheckBox GPO1_1R_COUT;
        private System.Windows.Forms.Label lblGPI1_1R_CIN;
        private System.Windows.Forms.CheckBox GPI1_1R_CIN;
        private System.Windows.Forms.ComboBox MAVLINK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SERIAL_SPEED;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FORMAT;
        private System.Windows.Forms.ComboBox AIR_SPEED;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox NETID;
        private System.Windows.Forms.Label lblNETID;
        private System.Windows.Forms.ComboBox TXPOWER;
        private System.Windows.Forms.Label lblTXPOWER;
        private System.Windows.Forms.Label lblOPPRESEND;
        private System.Windows.Forms.CheckBox OPPRESEND;
        private System.Windows.Forms.Label lblMAVLINK;
        private System.Windows.Forms.Label lblANT_MODE;
        private System.Windows.Forms.ComboBox ANT_MODE;
        private System.Windows.Forms.Label lblSER_BRK_DETMS;
        private System.Windows.Forms.Label lblGLOBAL_RETRIES;
        private System.Windows.Forms.Label lblMAX_RETRIES;
        private System.Windows.Forms.ComboBox SER_BRK_DETMS;
        private System.Windows.Forms.ComboBox GLOBAL_RETRIES;
        private System.Windows.Forms.ComboBox MAX_RETRIES;
        private System.Windows.Forms.ComboBox MAX_DATA;
        private System.Windows.Forms.Label lblMAX_DATA;
        private System.Windows.Forms.Label lblENCRYPTION_LEVEL;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox AESKEY;
        private System.Windows.Forms.CheckBox RTSCTS;
        private System.Windows.Forms.Label lblRTSCTS;
        private System.Windows.Forms.LinkLabel linkLabel_mavlink;
        private System.Windows.Forms.LinkLabel linkLabel_lowlatency;
        private System.Windows.Forms.Label lblMAX_WINDOW;
        private System.Windows.Forms.ComboBox MAX_WINDOW;
        private System.Windows.Forms.Label lblMIN_FREQ;
        private System.Windows.Forms.ComboBox MAX_FREQ;
        private System.Windows.Forms.ComboBox NUM_CHANNELS;
        private System.Windows.Forms.Label lblLBT_RSSI;
        private System.Windows.Forms.ComboBox LBT_RSSI;
        private System.Windows.Forms.Label lblDUTY_CYCLE;
        private System.Windows.Forms.ComboBox MIN_FREQ;
        private System.Windows.Forms.Label lblNUM_CHANNELS;
        private System.Windows.Forms.Label lblMAX_FREQ;
        private System.Windows.Forms.ComboBox DUTY_CYCLE;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox RATE_FREQBAND;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.CheckBox GPO1_0TXEN485;
        private System.Windows.Forms.Label lblGPO1_0TXEN485;
        private System.Windows.Forms.CheckBox GPO1_3STATLED;
        private System.Windows.Forms.Label lblGPO1_3STATLED;
        private System.Windows.Forms.ComboBox ENCRYPTION_LEVEL;
        private System.Windows.Forms.Label lblGPIO1_1FUNC;
        private System.Windows.Forms.ComboBox GPIO1_1FUNC;
        private System.Windows.Forms.CheckBox GPO1_3AUXOUT;
        private System.Windows.Forms.Label lblGPO1_3AUXOUT;
        private System.Windows.Forms.CheckBox GPI1_2AUXIN;
        private System.Windows.Forms.Label lblGPI1_2AUXIN;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox FSFRAMELOSS;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboSyncMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupFirmware;
        private System.Windows.Forms.GroupBox groupRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox AUXSER_SPEED;
        private System.Windows.Forms.GroupBox groupData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupGPIO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox GPO1_1SBUSOUT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox GPO1_1SBUSIN;
        private System.Windows.Forms.ComboBox AIR_FRAMELEN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox RSSI_IN_DBM;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox GPO1_3SBUSIN;
        private System.Windows.Forms.Label lblSBUSIN;
        private System.Windows.Forms.Label lblSBUSOUT;
        private System.Windows.Forms.ComboBox GPO1_3SBUSOUT;
        private Controls.MyButton btnLoadFromFile;
        private Controls.MyButton btnSaveToFile;
        private Controls.MyButton BUT_resettodefault;
        private Controls.MyButton BUT_savesettings;
        private Controls.MyButton BUT_getcurrent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutActions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutSettings;
        private System.Windows.Forms.BindingSource configManagerBindingSource;
        private System.Windows.Forms.ListBox listDevices;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutMain;
        private System.Windows.Forms.BindingSource modemsBindingSource;
    }
}