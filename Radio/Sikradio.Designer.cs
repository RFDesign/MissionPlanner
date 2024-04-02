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
            this.RTSCTS = new System.Windows.Forms.CheckBox();
            this.SERIAL_SPEED = new System.Windows.Forms.ComboBox();
            this.ENCRYPTION_LEVEL = new System.Windows.Forms.ComboBox();
            this.FSFRAMELOSS = new System.Windows.Forms.ComboBox();
            this.FORMAT = new System.Windows.Forms.TextBox();
            this.AUXSER_SPEED = new System.Windows.Forms.ComboBox();
            this.btn_LoadSetting = new FontAwesome.Sharp.IconButton();
            this.NUM_CHANNELS = new System.Windows.Forms.ComboBox();
            this.AIR_SPEED = new System.Windows.Forms.ComboBox();
            this.DUTY_CYCLE = new System.Windows.Forms.ComboBox();
            this.TXPOWER = new System.Windows.Forms.ComboBox();
            this.MAX_FREQ = new System.Windows.Forms.ComboBox();
            this.MIN_FREQ = new System.Windows.Forms.ComboBox();
            this.OPPRESEND = new System.Windows.Forms.CheckBox();
            this.RSSI_IN_DBM = new System.Windows.Forms.CheckBox();
            this.AIR_FRAMELEN = new System.Windows.Forms.ComboBox();
            this.MAVLINK = new System.Windows.Forms.ComboBox();
            this.LBT_RSSI = new System.Windows.Forms.ComboBox();
            this.NETID = new System.Windows.Forms.ComboBox();
            this.GPIO2 = new System.Windows.Forms.ComboBox();
            this.configManagerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pin14ItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GPIO0 = new System.Windows.Forms.ComboBox();
            this.pin13ItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GPIO3 = new System.Windows.Forms.ComboBox();
            this.pin12ItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GPIO1 = new System.Windows.Forms.ComboBox();
            this.pin15ItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxSync = new System.Windows.Forms.CheckBox();
            this.btn_Firmware = new FontAwesome.Sharp.IconButton();
            this.FREQ = new System.Windows.Forms.TextBox();
            this.linkLabel_mavlink = new System.Windows.Forms.LinkLabel();
            this.linkLabel_lowlatency = new System.Windows.Forms.LinkLabel();
            this.lblFailsafe = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.lblRX_ENCAP_METHOD = new System.Windows.Forms.Label();
            this.RX_ENCAP_METHOD = new System.Windows.Forms.ComboBox();
            this.lblTX_ENCAP_METHOD = new System.Windows.Forms.Label();
            this.TX_ENCAP_METHOD = new System.Windows.Forms.ComboBox();
            this.lblDESTID = new System.Windows.Forms.Label();
            this.lblNODEID = new System.Windows.Forms.Label();
            this.DESTID = new System.Windows.Forms.ComboBox();
            this.NODEID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.ATI2 = new System.Windows.Forms.TextBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.groupFirmware = new System.Windows.Forms.GroupBox();
            this.tableLayoutDevice = new System.Windows.Forms.TableLayoutPanel();
            this.FREQ_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ATI = new System.Windows.Forms.TextBox();
            this.comboModemSelection = new System.Windows.Forms.ComboBox();
            this.modemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ATI_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.FORMAT_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.groupRadio = new System.Windows.Forms.GroupBox();
            this.tableRadio = new System.Windows.Forms.TableLayoutPanel();
            this.OPPRESEND_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.RSSI_IN_DBM_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.AIR_FRAMELEN_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.AIR_SPEED_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MAVLINK_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.TXPOWER_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MAX_WINDOW_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.DUTY_CYCLE_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.LBT_RSSI_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.NUM_CHANNELS_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.NETID_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MAX_FREQ_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.ANT_MODE_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MIN_FREQ_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.ANT_MODE = new System.Windows.Forms.ComboBox();
            this.MAX_WINDOW = new System.Windows.Forms.ComboBox();
            this.RSSI = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblMAVLINK = new System.Windows.Forms.Label();
            this.lblOPPRESEND = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblMIN_FREQ = new System.Windows.Forms.Label();
            this.lblMAX_FREQ = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblMAX_WINDOW = new System.Windows.Forms.Label();
            this.lblNUM_CHANNELS = new System.Windows.Forms.Label();
            this.lblDUTY_CYCLE = new System.Windows.Forms.Label();
            this.lblLBT_RSSI = new System.Windows.Forms.Label();
            this.lblTXPOWER = new System.Windows.Forms.Label();
            this.lblNETID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RATE_FREQBAND = new System.Windows.Forms.ComboBox();
            this.lblANT_MODE = new System.Windows.Forms.Label();
            this.groupSerial = new System.Windows.Forms.GroupBox();
            this.tableLayoutSerial = new System.Windows.Forms.TableLayoutPanel();
            this.AUXSER_SPEED_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.SERIAL_SPEED_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.RTSCTS_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupData = new System.Windows.Forms.GroupBox();
            this.tableLayoutData = new System.Windows.Forms.TableLayoutPanel();
            this.DESTID_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.NODEID_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.TX_ENCAP_METHOD_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.RX_ENCAP_METHOD_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MAX_DATA_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.MAX_RETRIES_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.GLOBAL_RETRIES_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.SER_BRK_DETMS_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.groupGPIO = new System.Windows.Forms.GroupBox();
            this.tableLayoutGPIO = new System.Windows.Forms.TableLayoutPanel();
            this.GPIO0_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.GPIO3_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.GPIO2_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.GPIO1_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.groupSecurity = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnGenerateKey = new FontAwesome.Sharp.IconButton();
            this.ENCRYPTION_LEVEL_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.AESKEY_CHECK = new FontAwesome.Sharp.IconPictureBox();
            this.flowLayoutActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_SaveSetting = new FontAwesome.Sharp.IconButton();
            this.btn_LoadFile = new FontAwesome.Sharp.IconButton();
            this.btn_SaveFile = new FontAwesome.Sharp.IconButton();
            this.btn_Reset = new FontAwesome.Sharp.IconButton();
            this.richTextHelp = new System.Windows.Forms.RichTextBox();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin14ItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin13ItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin12ItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin15ItemsBindingSource)).BeginInit();
            this.groupFirmware.SuspendLayout();
            this.tableLayoutDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FREQ_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATI_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FORMAT_CHECK)).BeginInit();
            this.groupRadio.SuspendLayout();
            this.tableRadio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OPPRESEND_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSSI_IN_DBM_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIR_FRAMELEN_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIR_SPEED_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAVLINK_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TXPOWER_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_WINDOW_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DUTY_CYCLE_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBT_RSSI_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_CHANNELS_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NETID_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_FREQ_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANT_MODE_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MIN_FREQ_CHECK)).BeginInit();
            this.groupSerial.SuspendLayout();
            this.tableLayoutSerial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AUXSER_SPEED_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SERIAL_SPEED_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RTSCTS_CHECK)).BeginInit();
            this.groupData.SuspendLayout();
            this.tableLayoutData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DESTID_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NODEID_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TX_ENCAP_METHOD_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RX_ENCAP_METHOD_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_DATA_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_RETRIES_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GLOBAL_RETRIES_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SER_BRK_DETMS_CHECK)).BeginInit();
            this.groupGPIO.SuspendLayout();
            this.tableLayoutGPIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO0_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO3_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO2_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO1_CHECK)).BeginInit();
            this.groupSecurity.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ENCRYPTION_LEVEL_CHECK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AESKEY_CHECK)).BeginInit();
            this.flowLayoutActions.SuspendLayout();
            this.groupInfo.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Progressbar
            // 
            resources.ApplyResources(this.Progressbar, "Progressbar");
            this.Progressbar.Name = "Progressbar";
            this.Progressbar.Click += new System.EventHandler(this.Progressbar_Click);
            // 
            // RTSCTS
            // 
            resources.ApplyResources(this.RTSCTS, "RTSCTS");
            this.RTSCTS.Name = "RTSCTS";
            this.toolTip1.SetToolTip(this.RTSCTS, resources.GetString("RTSCTS.ToolTip"));
            this.RTSCTS.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // SERIAL_SPEED
            // 
            resources.ApplyResources(this.SERIAL_SPEED, "SERIAL_SPEED");
            this.SERIAL_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.SERIAL_SPEED.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // ENCRYPTION_LEVEL
            // 
            resources.ApplyResources(this.ENCRYPTION_LEVEL, "ENCRYPTION_LEVEL");
            this.ENCRYPTION_LEVEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.ENCRYPTION_LEVEL.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // FSFRAMELOSS
            // 
            resources.ApplyResources(this.FSFRAMELOSS, "FSFRAMELOSS");
            this.FSFRAMELOSS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FSFRAMELOSS.FormattingEnabled = true;
            this.FSFRAMELOSS.Name = "FSFRAMELOSS";
            this.toolTip1.SetToolTip(this.FSFRAMELOSS, resources.GetString("FSFRAMELOSS.ToolTip"));
            // 
            // FORMAT
            // 
            resources.ApplyResources(this.FORMAT, "FORMAT");
            this.FORMAT.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FORMAT.Name = "FORMAT";
            this.FORMAT.ReadOnly = true;
            this.toolTip1.SetToolTip(this.FORMAT, resources.GetString("FORMAT.ToolTip"));
            this.FORMAT.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // AUXSER_SPEED
            // 
            resources.ApplyResources(this.AUXSER_SPEED, "AUXSER_SPEED");
            this.AUXSER_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AUXSER_SPEED.FormattingEnabled = true;
            this.AUXSER_SPEED.Name = "AUXSER_SPEED";
            this.toolTip1.SetToolTip(this.AUXSER_SPEED, resources.GetString("AUXSER_SPEED.ToolTip"));
            this.AUXSER_SPEED.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // btn_LoadSetting
            // 
            this.btn_LoadSetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadSetting.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_LoadSetting, "btn_LoadSetting");
            this.btn_LoadSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadSetting.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.btn_LoadSetting.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_LoadSetting.IconSize = 24;
            this.btn_LoadSetting.Name = "btn_LoadSetting";
            this.toolTip1.SetToolTip(this.btn_LoadSetting, resources.GetString("btn_LoadSetting.ToolTip"));
            this.btn_LoadSetting.UseVisualStyleBackColor = true;
            this.btn_LoadSetting.Click += new System.EventHandler(this.btn_LoadSetting_Click);
            // 
            // NUM_CHANNELS
            // 
            resources.ApplyResources(this.NUM_CHANNELS, "NUM_CHANNELS");
            this.NUM_CHANNELS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.NUM_CHANNELS.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // AIR_SPEED
            // 
            resources.ApplyResources(this.AIR_SPEED, "AIR_SPEED");
            this.AIR_SPEED.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.AIR_SPEED.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // DUTY_CYCLE
            // 
            resources.ApplyResources(this.DUTY_CYCLE, "DUTY_CYCLE");
            this.DUTY_CYCLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.DUTY_CYCLE.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // TXPOWER
            // 
            resources.ApplyResources(this.TXPOWER, "TXPOWER");
            this.TXPOWER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.TXPOWER.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MAX_FREQ
            // 
            resources.ApplyResources(this.MAX_FREQ, "MAX_FREQ");
            this.MAX_FREQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.MAX_FREQ.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MIN_FREQ
            // 
            resources.ApplyResources(this.MIN_FREQ, "MIN_FREQ");
            this.MIN_FREQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MIN_FREQ.FormattingEnabled = true;
            this.MIN_FREQ.Name = "MIN_FREQ";
            this.toolTip1.SetToolTip(this.MIN_FREQ, resources.GetString("MIN_FREQ.ToolTip"));
            this.MIN_FREQ.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // OPPRESEND
            // 
            resources.ApplyResources(this.OPPRESEND, "OPPRESEND");
            this.OPPRESEND.Name = "OPPRESEND";
            this.toolTip1.SetToolTip(this.OPPRESEND, resources.GetString("OPPRESEND.ToolTip"));
            this.OPPRESEND.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // RSSI_IN_DBM
            // 
            resources.ApplyResources(this.RSSI_IN_DBM, "RSSI_IN_DBM");
            this.RSSI_IN_DBM.Name = "RSSI_IN_DBM";
            this.toolTip1.SetToolTip(this.RSSI_IN_DBM, resources.GetString("RSSI_IN_DBM.ToolTip"));
            this.RSSI_IN_DBM.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // AIR_FRAMELEN
            // 
            resources.ApplyResources(this.AIR_FRAMELEN, "AIR_FRAMELEN");
            this.AIR_FRAMELEN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AIR_FRAMELEN.FormattingEnabled = true;
            this.AIR_FRAMELEN.Name = "AIR_FRAMELEN";
            this.toolTip1.SetToolTip(this.AIR_FRAMELEN, resources.GetString("AIR_FRAMELEN.ToolTip"));
            this.AIR_FRAMELEN.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MAVLINK
            // 
            resources.ApplyResources(this.MAVLINK, "MAVLINK");
            this.MAVLINK.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.MAVLINK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MAVLINK.FormattingEnabled = true;
            this.MAVLINK.Name = "MAVLINK";
            this.toolTip1.SetToolTip(this.MAVLINK, resources.GetString("MAVLINK.ToolTip"));
            this.MAVLINK.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // LBT_RSSI
            // 
            resources.ApplyResources(this.LBT_RSSI, "LBT_RSSI");
            this.LBT_RSSI.BackColor = System.Drawing.SystemColors.Window;
            this.LBT_RSSI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.LBT_RSSI.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // NETID
            // 
            resources.ApplyResources(this.NETID, "NETID");
            this.NETID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.NETID.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // GPIO2
            // 
            resources.ApplyResources(this.GPIO2, "GPIO2");
            this.GPIO2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.configManagerBindingSource, "GPIO2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.GPIO2.DataSource = this.pin14ItemsBindingSource;
            this.GPIO2.DisplayMember = "Name";
            this.GPIO2.FormattingEnabled = true;
            this.GPIO2.Name = "GPIO2";
            this.toolTip1.SetToolTip(this.GPIO2, resources.GetString("GPIO2.ToolTip"));
            this.GPIO2.ValueMember = "Value";
            this.GPIO2.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // configManagerBindingSource
            // 
            this.configManagerBindingSource.DataSource = typeof(RFDCommon.ConfigManager);
            // 
            // pin14ItemsBindingSource
            // 
            this.pin14ItemsBindingSource.DataMember = "GPIO2_Items";
            this.pin14ItemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // GPIO0
            // 
            resources.ApplyResources(this.GPIO0, "GPIO0");
            this.GPIO0.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.configManagerBindingSource, "GPIO0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.GPIO0.DataSource = this.pin13ItemsBindingSource;
            this.GPIO0.DisplayMember = "Name";
            this.GPIO0.FormattingEnabled = true;
            this.GPIO0.Name = "GPIO0";
            this.toolTip1.SetToolTip(this.GPIO0, resources.GetString("GPIO0.ToolTip"));
            this.GPIO0.ValueMember = "Value";
            this.GPIO0.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // pin13ItemsBindingSource
            // 
            this.pin13ItemsBindingSource.DataMember = "GPIO0_Items";
            this.pin13ItemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // GPIO3
            // 
            resources.ApplyResources(this.GPIO3, "GPIO3");
            this.GPIO3.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.configManagerBindingSource, "GPIO3", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.GPIO3.DataSource = this.pin12ItemsBindingSource;
            this.GPIO3.DisplayMember = "Name";
            this.GPIO3.FormattingEnabled = true;
            this.GPIO3.Name = "GPIO3";
            this.toolTip1.SetToolTip(this.GPIO3, resources.GetString("GPIO3.ToolTip"));
            this.GPIO3.ValueMember = "Value";
            this.GPIO3.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // pin12ItemsBindingSource
            // 
            this.pin12ItemsBindingSource.DataMember = "GPIO3_Items";
            this.pin12ItemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // GPIO1
            // 
            resources.ApplyResources(this.GPIO1, "GPIO1");
            this.GPIO1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.configManagerBindingSource, "GPIO1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.GPIO1.DataSource = this.pin15ItemsBindingSource;
            this.GPIO1.DisplayMember = "Name";
            this.GPIO1.FormattingEnabled = true;
            this.GPIO1.Name = "GPIO1";
            this.toolTip1.SetToolTip(this.GPIO1, resources.GetString("GPIO1.ToolTip"));
            this.GPIO1.ValueMember = "Value";
            this.GPIO1.SelectedIndexChanged += new System.EventHandler(this.GPIO1_SelectedIndexChanged);
            this.GPIO1.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // pin15ItemsBindingSource
            // 
            this.pin15ItemsBindingSource.DataMember = "GPIO1_Items";
            this.pin15ItemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // checkBoxSync
            // 
            resources.ApplyResources(this.checkBoxSync, "checkBoxSync");
            this.checkBoxSync.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.configManagerBindingSource, "AutoSync", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxSync.ForeColor = System.Drawing.Color.White;
            this.checkBoxSync.Name = "checkBoxSync";
            this.toolTip1.SetToolTip(this.checkBoxSync, resources.GetString("checkBoxSync.ToolTip"));
            this.checkBoxSync.UseVisualStyleBackColor = true;
            // 
            // btn_Firmware
            // 
            this.btn_Firmware.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Firmware.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_Firmware, "btn_Firmware");
            this.btn_Firmware.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Firmware.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.btn_Firmware.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Firmware.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_Firmware.IconSize = 24;
            this.btn_Firmware.Name = "btn_Firmware";
            this.toolTip1.SetToolTip(this.btn_Firmware, resources.GetString("btn_Firmware.ToolTip"));
            this.btn_Firmware.UseVisualStyleBackColor = true;
            this.btn_Firmware.Click += new System.EventHandler(this.btn_Firmware_Click);
            // 
            // FREQ
            // 
            resources.ApplyResources(this.FREQ, "FREQ");
            this.FREQ.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FREQ.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "FREQ", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FREQ.Name = "FREQ";
            this.FREQ.ReadOnly = true;
            this.FREQ.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
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
            // lblFailsafe
            // 
            resources.ApplyResources(this.lblFailsafe, "lblFailsafe");
            this.lblFailsafe.Name = "lblFailsafe";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "COUNTRY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.ReadOnly = true;
            this.txtCountry.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // lblRX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.lblRX_ENCAP_METHOD, "lblRX_ENCAP_METHOD");
            this.lblRX_ENCAP_METHOD.Name = "lblRX_ENCAP_METHOD";
            // 
            // RX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.RX_ENCAP_METHOD, "RX_ENCAP_METHOD");
            this.RX_ENCAP_METHOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RX_ENCAP_METHOD.FormattingEnabled = true;
            this.RX_ENCAP_METHOD.Name = "RX_ENCAP_METHOD";
            this.RX_ENCAP_METHOD.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // lblTX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.lblTX_ENCAP_METHOD, "lblTX_ENCAP_METHOD");
            this.lblTX_ENCAP_METHOD.Name = "lblTX_ENCAP_METHOD";
            // 
            // TX_ENCAP_METHOD
            // 
            resources.ApplyResources(this.TX_ENCAP_METHOD, "TX_ENCAP_METHOD");
            this.TX_ENCAP_METHOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TX_ENCAP_METHOD.FormattingEnabled = true;
            this.TX_ENCAP_METHOD.Name = "TX_ENCAP_METHOD";
            this.TX_ENCAP_METHOD.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
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
            resources.ApplyResources(this.DESTID, "DESTID");
            this.DESTID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DESTID.FormattingEnabled = true;
            this.DESTID.Name = "DESTID";
            this.DESTID.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // NODEID
            // 
            resources.ApplyResources(this.NODEID, "NODEID");
            this.NODEID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NODEID.FormattingEnabled = true;
            this.NODEID.Name = "NODEID";
            this.NODEID.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
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
            resources.ApplyResources(this.SER_BRK_DETMS, "SER_BRK_DETMS");
            this.SER_BRK_DETMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SER_BRK_DETMS.FormattingEnabled = true;
            this.SER_BRK_DETMS.Name = "SER_BRK_DETMS";
            this.SER_BRK_DETMS.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // GLOBAL_RETRIES
            // 
            resources.ApplyResources(this.GLOBAL_RETRIES, "GLOBAL_RETRIES");
            this.GLOBAL_RETRIES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GLOBAL_RETRIES.FormattingEnabled = true;
            this.GLOBAL_RETRIES.Name = "GLOBAL_RETRIES";
            this.GLOBAL_RETRIES.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MAX_RETRIES
            // 
            resources.ApplyResources(this.MAX_RETRIES, "MAX_RETRIES");
            this.MAX_RETRIES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MAX_RETRIES.FormattingEnabled = true;
            this.MAX_RETRIES.Name = "MAX_RETRIES";
            this.MAX_RETRIES.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MAX_DATA
            // 
            resources.ApplyResources(this.MAX_DATA, "MAX_DATA");
            this.MAX_DATA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MAX_DATA.FormattingEnabled = true;
            this.MAX_DATA.Name = "MAX_DATA";
            this.MAX_DATA.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
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
            resources.ApplyResources(this.AESKEY, "AESKEY");
            this.AESKEY.Name = "AESKEY";
            // 
            // lblRTSCTS
            // 
            resources.ApplyResources(this.lblRTSCTS, "lblRTSCTS");
            this.lblRTSCTS.Name = "lblRTSCTS";
            // 
            // ATI2
            // 
            resources.ApplyResources(this.ATI2, "ATI2");
            this.ATI2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ATI2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "BOARD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ATI2.Name = "ATI2";
            this.ATI2.ReadOnly = true;
            this.ATI2.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
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
            // groupFirmware
            // 
            resources.ApplyResources(this.groupFirmware, "groupFirmware");
            this.groupFirmware.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupFirmware, 6);
            this.groupFirmware.Controls.Add(this.tableLayoutDevice);
            this.groupFirmware.ForeColor = System.Drawing.Color.White;
            this.groupFirmware.Name = "groupFirmware";
            this.groupFirmware.TabStop = false;
            // 
            // tableLayoutDevice
            // 
            resources.ApplyResources(this.tableLayoutDevice, "tableLayoutDevice");
            this.tableLayoutDevice.Controls.Add(this.FREQ_CHECK, 2, 1);
            this.tableLayoutDevice.Controls.Add(this.label9, 0, 0);
            this.tableLayoutDevice.Controls.Add(this.txtCountry, 4, 2);
            this.tableLayoutDevice.Controls.Add(this.ATI2, 4, 1);
            this.tableLayoutDevice.Controls.Add(this.label49, 3, 2);
            this.tableLayoutDevice.Controls.Add(this.label6, 3, 1);
            this.tableLayoutDevice.Controls.Add(this.label7, 0, 1);
            this.tableLayoutDevice.Controls.Add(this.label11, 3, 0);
            this.tableLayoutDevice.Controls.Add(this.FREQ, 1, 1);
            this.tableLayoutDevice.Controls.Add(this.ATI, 4, 0);
            this.tableLayoutDevice.Controls.Add(this.FORMAT, 1, 2);
            this.tableLayoutDevice.Controls.Add(this.label2, 0, 2);
            this.tableLayoutDevice.Controls.Add(this.comboModemSelection, 1, 0);
            this.tableLayoutDevice.Controls.Add(this.ATI_CHECK, 5, 0);
            this.tableLayoutDevice.Controls.Add(this.FORMAT_CHECK, 2, 2);
            this.tableLayoutDevice.Name = "tableLayoutDevice";
            // 
            // FREQ_CHECK
            // 
            resources.ApplyResources(this.FREQ_CHECK, "FREQ_CHECK");
            this.FREQ_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.FREQ_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.FREQ_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.FREQ_CHECK.IconColor = System.Drawing.Color.Gray;
            this.FREQ_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.FREQ_CHECK.IconSize = 18;
            this.FREQ_CHECK.Name = "FREQ_CHECK";
            this.FREQ_CHECK.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // ATI
            // 
            resources.ApplyResources(this.ATI, "ATI");
            this.ATI.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ATI.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "ATI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ATI.Name = "ATI";
            this.ATI.ReadOnly = true;
            this.ATI.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // comboModemSelection
            // 
            resources.ApplyResources(this.comboModemSelection, "comboModemSelection");
            this.comboModemSelection.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.configManagerBindingSource, "Current", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboModemSelection.DataSource = this.modemsBindingSource;
            this.comboModemSelection.DisplayMember = "DisplayName";
            this.comboModemSelection.FormattingEnabled = true;
            this.comboModemSelection.Name = "comboModemSelection";
            this.comboModemSelection.SelectedIndexChanged += new System.EventHandler(this.comboModemSelection_SelectedIndexChanged);
            this.comboModemSelection.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // modemsBindingSource
            // 
            this.modemsBindingSource.DataMember = "Modems";
            this.modemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // ATI_CHECK
            // 
            resources.ApplyResources(this.ATI_CHECK, "ATI_CHECK");
            this.ATI_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.ATI_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.ATI_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.ATI_CHECK.IconColor = System.Drawing.Color.Gray;
            this.ATI_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ATI_CHECK.IconSize = 18;
            this.ATI_CHECK.Name = "ATI_CHECK";
            this.ATI_CHECK.TabStop = false;
            // 
            // FORMAT_CHECK
            // 
            resources.ApplyResources(this.FORMAT_CHECK, "FORMAT_CHECK");
            this.FORMAT_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.FORMAT_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.FORMAT_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.FORMAT_CHECK.IconColor = System.Drawing.Color.Gray;
            this.FORMAT_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.FORMAT_CHECK.IconSize = 18;
            this.FORMAT_CHECK.Name = "FORMAT_CHECK";
            this.FORMAT_CHECK.TabStop = false;
            // 
            // groupRadio
            // 
            resources.ApplyResources(this.groupRadio, "groupRadio");
            this.groupRadio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupRadio, 6);
            this.groupRadio.Controls.Add(this.tableRadio);
            this.groupRadio.ForeColor = System.Drawing.Color.White;
            this.groupRadio.Name = "groupRadio";
            this.tableLayoutMain.SetRowSpan(this.groupRadio, 2);
            this.groupRadio.TabStop = false;
            // 
            // tableRadio
            // 
            resources.ApplyResources(this.tableRadio, "tableRadio");
            this.tableRadio.Controls.Add(this.OPPRESEND_CHECK, 5, 7);
            this.tableRadio.Controls.Add(this.RSSI_IN_DBM_CHECK, 2, 7);
            this.tableRadio.Controls.Add(this.AIR_FRAMELEN_CHECK, 5, 6);
            this.tableRadio.Controls.Add(this.AIR_SPEED_CHECK, 2, 6);
            this.tableRadio.Controls.Add(this.MAVLINK_CHECK, 5, 5);
            this.tableRadio.Controls.Add(this.TXPOWER_CHECK, 2, 5);
            this.tableRadio.Controls.Add(this.MAX_WINDOW_CHECK, 5, 4);
            this.tableRadio.Controls.Add(this.DUTY_CYCLE_CHECK, 2, 4);
            this.tableRadio.Controls.Add(this.LBT_RSSI_CHECK, 5, 3);
            this.tableRadio.Controls.Add(this.NUM_CHANNELS_CHECK, 2, 3);
            this.tableRadio.Controls.Add(this.NETID_CHECK, 5, 2);
            this.tableRadio.Controls.Add(this.MAX_FREQ_CHECK, 2, 2);
            this.tableRadio.Controls.Add(this.ANT_MODE_CHECK, 5, 1);
            this.tableRadio.Controls.Add(this.MIN_FREQ_CHECK, 2, 1);
            this.tableRadio.Controls.Add(this.ANT_MODE, 4, 1);
            this.tableRadio.Controls.Add(this.NETID, 4, 2);
            this.tableRadio.Controls.Add(this.LBT_RSSI, 4, 3);
            this.tableRadio.Controls.Add(this.MAX_WINDOW, 4, 4);
            this.tableRadio.Controls.Add(this.MAVLINK, 4, 5);
            this.tableRadio.Controls.Add(this.AIR_FRAMELEN, 4, 6);
            this.tableRadio.Controls.Add(this.RSSI, 1, 8);
            this.tableRadio.Controls.Add(this.label14, 3, 6);
            this.tableRadio.Controls.Add(this.RSSI_IN_DBM, 1, 7);
            this.tableRadio.Controls.Add(this.label45, 0, 0);
            this.tableRadio.Controls.Add(this.OPPRESEND, 4, 7);
            this.tableRadio.Controls.Add(this.lblMAVLINK, 3, 5);
            this.tableRadio.Controls.Add(this.lblOPPRESEND, 3, 7);
            this.tableRadio.Controls.Add(this.label12, 0, 8);
            this.tableRadio.Controls.Add(this.lblMIN_FREQ, 0, 1);
            this.tableRadio.Controls.Add(this.lblMAX_FREQ, 0, 2);
            this.tableRadio.Controls.Add(this.label15, 0, 7);
            this.tableRadio.Controls.Add(this.lblMAX_WINDOW, 3, 4);
            this.tableRadio.Controls.Add(this.lblNUM_CHANNELS, 0, 3);
            this.tableRadio.Controls.Add(this.lblDUTY_CYCLE, 0, 4);
            this.tableRadio.Controls.Add(this.lblLBT_RSSI, 3, 3);
            this.tableRadio.Controls.Add(this.lblTXPOWER, 0, 5);
            this.tableRadio.Controls.Add(this.lblNETID, 3, 2);
            this.tableRadio.Controls.Add(this.label3, 0, 6);
            this.tableRadio.Controls.Add(this.RATE_FREQBAND, 1, 0);
            this.tableRadio.Controls.Add(this.MIN_FREQ, 1, 1);
            this.tableRadio.Controls.Add(this.lblANT_MODE, 3, 1);
            this.tableRadio.Controls.Add(this.MAX_FREQ, 1, 2);
            this.tableRadio.Controls.Add(this.TXPOWER, 1, 5);
            this.tableRadio.Controls.Add(this.DUTY_CYCLE, 1, 4);
            this.tableRadio.Controls.Add(this.AIR_SPEED, 1, 6);
            this.tableRadio.Controls.Add(this.NUM_CHANNELS, 1, 3);
            this.tableRadio.Name = "tableRadio";
            // 
            // OPPRESEND_CHECK
            // 
            resources.ApplyResources(this.OPPRESEND_CHECK, "OPPRESEND_CHECK");
            this.OPPRESEND_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.OPPRESEND_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.OPPRESEND_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.OPPRESEND_CHECK.IconColor = System.Drawing.Color.Gray;
            this.OPPRESEND_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.OPPRESEND_CHECK.IconSize = 18;
            this.OPPRESEND_CHECK.Name = "OPPRESEND_CHECK";
            this.OPPRESEND_CHECK.TabStop = false;
            // 
            // RSSI_IN_DBM_CHECK
            // 
            resources.ApplyResources(this.RSSI_IN_DBM_CHECK, "RSSI_IN_DBM_CHECK");
            this.RSSI_IN_DBM_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.RSSI_IN_DBM_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.RSSI_IN_DBM_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.RSSI_IN_DBM_CHECK.IconColor = System.Drawing.Color.Gray;
            this.RSSI_IN_DBM_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.RSSI_IN_DBM_CHECK.IconSize = 18;
            this.RSSI_IN_DBM_CHECK.Name = "RSSI_IN_DBM_CHECK";
            this.RSSI_IN_DBM_CHECK.TabStop = false;
            // 
            // AIR_FRAMELEN_CHECK
            // 
            resources.ApplyResources(this.AIR_FRAMELEN_CHECK, "AIR_FRAMELEN_CHECK");
            this.AIR_FRAMELEN_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.AIR_FRAMELEN_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.AIR_FRAMELEN_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.AIR_FRAMELEN_CHECK.IconColor = System.Drawing.Color.Gray;
            this.AIR_FRAMELEN_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AIR_FRAMELEN_CHECK.IconSize = 18;
            this.AIR_FRAMELEN_CHECK.Name = "AIR_FRAMELEN_CHECK";
            this.AIR_FRAMELEN_CHECK.TabStop = false;
            // 
            // AIR_SPEED_CHECK
            // 
            resources.ApplyResources(this.AIR_SPEED_CHECK, "AIR_SPEED_CHECK");
            this.AIR_SPEED_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.AIR_SPEED_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.AIR_SPEED_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.AIR_SPEED_CHECK.IconColor = System.Drawing.Color.Gray;
            this.AIR_SPEED_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AIR_SPEED_CHECK.IconSize = 18;
            this.AIR_SPEED_CHECK.Name = "AIR_SPEED_CHECK";
            this.AIR_SPEED_CHECK.TabStop = false;
            // 
            // MAVLINK_CHECK
            // 
            resources.ApplyResources(this.MAVLINK_CHECK, "MAVLINK_CHECK");
            this.MAVLINK_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MAVLINK_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MAVLINK_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MAVLINK_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MAVLINK_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MAVLINK_CHECK.IconSize = 18;
            this.MAVLINK_CHECK.Name = "MAVLINK_CHECK";
            this.MAVLINK_CHECK.TabStop = false;
            // 
            // TXPOWER_CHECK
            // 
            resources.ApplyResources(this.TXPOWER_CHECK, "TXPOWER_CHECK");
            this.TXPOWER_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.TXPOWER_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.TXPOWER_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.TXPOWER_CHECK.IconColor = System.Drawing.Color.Gray;
            this.TXPOWER_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TXPOWER_CHECK.IconSize = 18;
            this.TXPOWER_CHECK.Name = "TXPOWER_CHECK";
            this.TXPOWER_CHECK.TabStop = false;
            // 
            // MAX_WINDOW_CHECK
            // 
            resources.ApplyResources(this.MAX_WINDOW_CHECK, "MAX_WINDOW_CHECK");
            this.MAX_WINDOW_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MAX_WINDOW_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MAX_WINDOW_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MAX_WINDOW_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MAX_WINDOW_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MAX_WINDOW_CHECK.IconSize = 18;
            this.MAX_WINDOW_CHECK.Name = "MAX_WINDOW_CHECK";
            this.MAX_WINDOW_CHECK.TabStop = false;
            // 
            // DUTY_CYCLE_CHECK
            // 
            resources.ApplyResources(this.DUTY_CYCLE_CHECK, "DUTY_CYCLE_CHECK");
            this.DUTY_CYCLE_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.DUTY_CYCLE_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.DUTY_CYCLE_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.DUTY_CYCLE_CHECK.IconColor = System.Drawing.Color.Gray;
            this.DUTY_CYCLE_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.DUTY_CYCLE_CHECK.IconSize = 18;
            this.DUTY_CYCLE_CHECK.Name = "DUTY_CYCLE_CHECK";
            this.DUTY_CYCLE_CHECK.TabStop = false;
            // 
            // LBT_RSSI_CHECK
            // 
            resources.ApplyResources(this.LBT_RSSI_CHECK, "LBT_RSSI_CHECK");
            this.LBT_RSSI_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.LBT_RSSI_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.LBT_RSSI_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.LBT_RSSI_CHECK.IconColor = System.Drawing.Color.Gray;
            this.LBT_RSSI_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.LBT_RSSI_CHECK.IconSize = 18;
            this.LBT_RSSI_CHECK.Name = "LBT_RSSI_CHECK";
            this.LBT_RSSI_CHECK.TabStop = false;
            // 
            // NUM_CHANNELS_CHECK
            // 
            resources.ApplyResources(this.NUM_CHANNELS_CHECK, "NUM_CHANNELS_CHECK");
            this.NUM_CHANNELS_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.NUM_CHANNELS_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.NUM_CHANNELS_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.NUM_CHANNELS_CHECK.IconColor = System.Drawing.Color.Gray;
            this.NUM_CHANNELS_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.NUM_CHANNELS_CHECK.IconSize = 18;
            this.NUM_CHANNELS_CHECK.Name = "NUM_CHANNELS_CHECK";
            this.NUM_CHANNELS_CHECK.TabStop = false;
            // 
            // NETID_CHECK
            // 
            resources.ApplyResources(this.NETID_CHECK, "NETID_CHECK");
            this.NETID_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.NETID_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.NETID_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.NETID_CHECK.IconColor = System.Drawing.Color.Gray;
            this.NETID_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.NETID_CHECK.IconSize = 18;
            this.NETID_CHECK.Name = "NETID_CHECK";
            this.NETID_CHECK.TabStop = false;
            // 
            // MAX_FREQ_CHECK
            // 
            resources.ApplyResources(this.MAX_FREQ_CHECK, "MAX_FREQ_CHECK");
            this.MAX_FREQ_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MAX_FREQ_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MAX_FREQ_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MAX_FREQ_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MAX_FREQ_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MAX_FREQ_CHECK.IconSize = 18;
            this.MAX_FREQ_CHECK.Name = "MAX_FREQ_CHECK";
            this.MAX_FREQ_CHECK.TabStop = false;
            // 
            // ANT_MODE_CHECK
            // 
            resources.ApplyResources(this.ANT_MODE_CHECK, "ANT_MODE_CHECK");
            this.ANT_MODE_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.ANT_MODE_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.ANT_MODE_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.ANT_MODE_CHECK.IconColor = System.Drawing.Color.Gray;
            this.ANT_MODE_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ANT_MODE_CHECK.IconSize = 18;
            this.ANT_MODE_CHECK.Name = "ANT_MODE_CHECK";
            this.ANT_MODE_CHECK.TabStop = false;
            // 
            // MIN_FREQ_CHECK
            // 
            resources.ApplyResources(this.MIN_FREQ_CHECK, "MIN_FREQ_CHECK");
            this.MIN_FREQ_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MIN_FREQ_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MIN_FREQ_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MIN_FREQ_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MIN_FREQ_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MIN_FREQ_CHECK.IconSize = 18;
            this.MIN_FREQ_CHECK.Name = "MIN_FREQ_CHECK";
            this.MIN_FREQ_CHECK.TabStop = false;
            // 
            // ANT_MODE
            // 
            resources.ApplyResources(this.ANT_MODE, "ANT_MODE");
            this.ANT_MODE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ANT_MODE.FormattingEnabled = true;
            this.ANT_MODE.Name = "ANT_MODE";
            this.ANT_MODE.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // MAX_WINDOW
            // 
            resources.ApplyResources(this.MAX_WINDOW, "MAX_WINDOW");
            this.MAX_WINDOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MAX_WINDOW.FormattingEnabled = true;
            this.MAX_WINDOW.Name = "MAX_WINDOW";
            this.MAX_WINDOW.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // RSSI
            // 
            this.RSSI.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableRadio.SetColumnSpan(this.RSSI, 4);
            this.RSSI.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "RSSI", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.RSSI, "RSSI");
            this.RSSI.Name = "RSSI";
            this.RSSI.ReadOnly = true;
            this.RSSI.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // lblMAVLINK
            // 
            resources.ApplyResources(this.lblMAVLINK, "lblMAVLINK");
            this.lblMAVLINK.Name = "lblMAVLINK";
            // 
            // lblOPPRESEND
            // 
            resources.ApplyResources(this.lblOPPRESEND, "lblOPPRESEND");
            this.lblOPPRESEND.Name = "lblOPPRESEND";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // lblMIN_FREQ
            // 
            resources.ApplyResources(this.lblMIN_FREQ, "lblMIN_FREQ");
            this.lblMIN_FREQ.Name = "lblMIN_FREQ";
            // 
            // lblMAX_FREQ
            // 
            resources.ApplyResources(this.lblMAX_FREQ, "lblMAX_FREQ");
            this.lblMAX_FREQ.Name = "lblMAX_FREQ";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // lblMAX_WINDOW
            // 
            resources.ApplyResources(this.lblMAX_WINDOW, "lblMAX_WINDOW");
            this.lblMAX_WINDOW.Name = "lblMAX_WINDOW";
            // 
            // lblNUM_CHANNELS
            // 
            resources.ApplyResources(this.lblNUM_CHANNELS, "lblNUM_CHANNELS");
            this.lblNUM_CHANNELS.Name = "lblNUM_CHANNELS";
            // 
            // lblDUTY_CYCLE
            // 
            resources.ApplyResources(this.lblDUTY_CYCLE, "lblDUTY_CYCLE");
            this.lblDUTY_CYCLE.Name = "lblDUTY_CYCLE";
            // 
            // lblLBT_RSSI
            // 
            resources.ApplyResources(this.lblLBT_RSSI, "lblLBT_RSSI");
            this.lblLBT_RSSI.Name = "lblLBT_RSSI";
            // 
            // lblTXPOWER
            // 
            resources.ApplyResources(this.lblTXPOWER, "lblTXPOWER");
            this.lblTXPOWER.Name = "lblTXPOWER";
            // 
            // lblNETID
            // 
            resources.ApplyResources(this.lblNETID, "lblNETID");
            this.lblNETID.Name = "lblNETID";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RATE_FREQBAND
            // 
            resources.ApplyResources(this.RATE_FREQBAND, "RATE_FREQBAND");
            this.tableRadio.SetColumnSpan(this.RATE_FREQBAND, 4);
            this.RATE_FREQBAND.Name = "RATE_FREQBAND";
            this.RATE_FREQBAND.Click += new System.EventHandler(this.Control_Clicked_ShowHelp);
            // 
            // lblANT_MODE
            // 
            resources.ApplyResources(this.lblANT_MODE, "lblANT_MODE");
            this.lblANT_MODE.Name = "lblANT_MODE";
            // 
            // groupSerial
            // 
            this.groupSerial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupSerial, 3);
            this.groupSerial.Controls.Add(this.tableLayoutSerial);
            resources.ApplyResources(this.groupSerial, "groupSerial");
            this.groupSerial.ForeColor = System.Drawing.Color.White;
            this.groupSerial.Name = "groupSerial";
            this.groupSerial.TabStop = false;
            // 
            // tableLayoutSerial
            // 
            resources.ApplyResources(this.tableLayoutSerial, "tableLayoutSerial");
            this.tableLayoutSerial.Controls.Add(this.AUXSER_SPEED_CHECK, 2, 1);
            this.tableLayoutSerial.Controls.Add(this.SERIAL_SPEED_CHECK, 2, 0);
            this.tableLayoutSerial.Controls.Add(this.RTSCTS_CHECK, 2, 2);
            this.tableLayoutSerial.Controls.Add(this.label1, 0, 0);
            this.tableLayoutSerial.Controls.Add(this.RTSCTS, 1, 2);
            this.tableLayoutSerial.Controls.Add(this.lblRTSCTS, 0, 2);
            this.tableLayoutSerial.Controls.Add(this.AUXSER_SPEED, 1, 1);
            this.tableLayoutSerial.Controls.Add(this.label5, 0, 1);
            this.tableLayoutSerial.Controls.Add(this.SERIAL_SPEED, 1, 0);
            this.tableLayoutSerial.Name = "tableLayoutSerial";
            // 
            // AUXSER_SPEED_CHECK
            // 
            resources.ApplyResources(this.AUXSER_SPEED_CHECK, "AUXSER_SPEED_CHECK");
            this.AUXSER_SPEED_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.AUXSER_SPEED_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.AUXSER_SPEED_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.AUXSER_SPEED_CHECK.IconColor = System.Drawing.Color.Gray;
            this.AUXSER_SPEED_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AUXSER_SPEED_CHECK.IconSize = 18;
            this.AUXSER_SPEED_CHECK.Name = "AUXSER_SPEED_CHECK";
            this.AUXSER_SPEED_CHECK.TabStop = false;
            // 
            // SERIAL_SPEED_CHECK
            // 
            resources.ApplyResources(this.SERIAL_SPEED_CHECK, "SERIAL_SPEED_CHECK");
            this.SERIAL_SPEED_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.SERIAL_SPEED_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.SERIAL_SPEED_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.SERIAL_SPEED_CHECK.IconColor = System.Drawing.Color.Gray;
            this.SERIAL_SPEED_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SERIAL_SPEED_CHECK.IconSize = 18;
            this.SERIAL_SPEED_CHECK.Name = "SERIAL_SPEED_CHECK";
            this.SERIAL_SPEED_CHECK.TabStop = false;
            // 
            // RTSCTS_CHECK
            // 
            resources.ApplyResources(this.RTSCTS_CHECK, "RTSCTS_CHECK");
            this.RTSCTS_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.RTSCTS_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.RTSCTS_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.RTSCTS_CHECK.IconColor = System.Drawing.Color.Gray;
            this.RTSCTS_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.RTSCTS_CHECK.IconSize = 18;
            this.RTSCTS_CHECK.Name = "RTSCTS_CHECK";
            this.RTSCTS_CHECK.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // groupData
            // 
            this.groupData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupData, 6);
            this.groupData.Controls.Add(this.tableLayoutData);
            resources.ApplyResources(this.groupData, "groupData");
            this.groupData.ForeColor = System.Drawing.Color.White;
            this.groupData.Name = "groupData";
            this.groupData.TabStop = false;
            // 
            // tableLayoutData
            // 
            resources.ApplyResources(this.tableLayoutData, "tableLayoutData");
            this.tableLayoutData.Controls.Add(this.DESTID_CHECK, 2, 1);
            this.tableLayoutData.Controls.Add(this.NODEID_CHECK, 2, 0);
            this.tableLayoutData.Controls.Add(this.lblNODEID, 0, 0);
            this.tableLayoutData.Controls.Add(this.lblDESTID, 0, 1);
            this.tableLayoutData.Controls.Add(this.NODEID, 1, 0);
            this.tableLayoutData.Controls.Add(this.DESTID, 1, 1);
            this.tableLayoutData.Controls.Add(this.lblTX_ENCAP_METHOD, 3, 0);
            this.tableLayoutData.Controls.Add(this.TX_ENCAP_METHOD, 4, 0);
            this.tableLayoutData.Controls.Add(this.TX_ENCAP_METHOD_CHECK, 5, 0);
            this.tableLayoutData.Controls.Add(this.RX_ENCAP_METHOD, 4, 1);
            this.tableLayoutData.Controls.Add(this.lblRX_ENCAP_METHOD, 3, 1);
            this.tableLayoutData.Controls.Add(this.RX_ENCAP_METHOD_CHECK, 5, 1);
            this.tableLayoutData.Controls.Add(this.lblMAX_DATA, 0, 2);
            this.tableLayoutData.Controls.Add(this.MAX_DATA, 1, 2);
            this.tableLayoutData.Controls.Add(this.MAX_DATA_CHECK, 2, 2);
            this.tableLayoutData.Controls.Add(this.lblMAX_RETRIES, 3, 2);
            this.tableLayoutData.Controls.Add(this.MAX_RETRIES, 4, 2);
            this.tableLayoutData.Controls.Add(this.MAX_RETRIES_CHECK, 5, 2);
            this.tableLayoutData.Controls.Add(this.lblGLOBAL_RETRIES, 3, 3);
            this.tableLayoutData.Controls.Add(this.GLOBAL_RETRIES, 4, 3);
            this.tableLayoutData.Controls.Add(this.GLOBAL_RETRIES_CHECK, 5, 3);
            this.tableLayoutData.Controls.Add(this.lblSER_BRK_DETMS, 0, 3);
            this.tableLayoutData.Controls.Add(this.SER_BRK_DETMS, 1, 3);
            this.tableLayoutData.Controls.Add(this.SER_BRK_DETMS_CHECK, 2, 3);
            this.tableLayoutData.Name = "tableLayoutData";
            // 
            // DESTID_CHECK
            // 
            resources.ApplyResources(this.DESTID_CHECK, "DESTID_CHECK");
            this.DESTID_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.DESTID_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.DESTID_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.DESTID_CHECK.IconColor = System.Drawing.Color.Gray;
            this.DESTID_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.DESTID_CHECK.IconSize = 18;
            this.DESTID_CHECK.Name = "DESTID_CHECK";
            this.DESTID_CHECK.TabStop = false;
            // 
            // NODEID_CHECK
            // 
            resources.ApplyResources(this.NODEID_CHECK, "NODEID_CHECK");
            this.NODEID_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.NODEID_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.NODEID_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.NODEID_CHECK.IconColor = System.Drawing.Color.Gray;
            this.NODEID_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.NODEID_CHECK.IconSize = 18;
            this.NODEID_CHECK.Name = "NODEID_CHECK";
            this.NODEID_CHECK.TabStop = false;
            // 
            // TX_ENCAP_METHOD_CHECK
            // 
            resources.ApplyResources(this.TX_ENCAP_METHOD_CHECK, "TX_ENCAP_METHOD_CHECK");
            this.TX_ENCAP_METHOD_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.TX_ENCAP_METHOD_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.TX_ENCAP_METHOD_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.TX_ENCAP_METHOD_CHECK.IconColor = System.Drawing.Color.Gray;
            this.TX_ENCAP_METHOD_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TX_ENCAP_METHOD_CHECK.IconSize = 18;
            this.TX_ENCAP_METHOD_CHECK.Name = "TX_ENCAP_METHOD_CHECK";
            this.TX_ENCAP_METHOD_CHECK.TabStop = false;
            // 
            // RX_ENCAP_METHOD_CHECK
            // 
            resources.ApplyResources(this.RX_ENCAP_METHOD_CHECK, "RX_ENCAP_METHOD_CHECK");
            this.RX_ENCAP_METHOD_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.RX_ENCAP_METHOD_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.RX_ENCAP_METHOD_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.RX_ENCAP_METHOD_CHECK.IconColor = System.Drawing.Color.Gray;
            this.RX_ENCAP_METHOD_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.RX_ENCAP_METHOD_CHECK.IconSize = 18;
            this.RX_ENCAP_METHOD_CHECK.Name = "RX_ENCAP_METHOD_CHECK";
            this.RX_ENCAP_METHOD_CHECK.TabStop = false;
            // 
            // MAX_DATA_CHECK
            // 
            resources.ApplyResources(this.MAX_DATA_CHECK, "MAX_DATA_CHECK");
            this.MAX_DATA_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MAX_DATA_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MAX_DATA_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MAX_DATA_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MAX_DATA_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MAX_DATA_CHECK.IconSize = 18;
            this.MAX_DATA_CHECK.Name = "MAX_DATA_CHECK";
            this.MAX_DATA_CHECK.TabStop = false;
            // 
            // MAX_RETRIES_CHECK
            // 
            resources.ApplyResources(this.MAX_RETRIES_CHECK, "MAX_RETRIES_CHECK");
            this.MAX_RETRIES_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.MAX_RETRIES_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.MAX_RETRIES_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.MAX_RETRIES_CHECK.IconColor = System.Drawing.Color.Gray;
            this.MAX_RETRIES_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MAX_RETRIES_CHECK.IconSize = 18;
            this.MAX_RETRIES_CHECK.Name = "MAX_RETRIES_CHECK";
            this.MAX_RETRIES_CHECK.TabStop = false;
            // 
            // GLOBAL_RETRIES_CHECK
            // 
            resources.ApplyResources(this.GLOBAL_RETRIES_CHECK, "GLOBAL_RETRIES_CHECK");
            this.GLOBAL_RETRIES_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.GLOBAL_RETRIES_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.GLOBAL_RETRIES_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.GLOBAL_RETRIES_CHECK.IconColor = System.Drawing.Color.Gray;
            this.GLOBAL_RETRIES_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GLOBAL_RETRIES_CHECK.IconSize = 18;
            this.GLOBAL_RETRIES_CHECK.Name = "GLOBAL_RETRIES_CHECK";
            this.GLOBAL_RETRIES_CHECK.TabStop = false;
            // 
            // SER_BRK_DETMS_CHECK
            // 
            resources.ApplyResources(this.SER_BRK_DETMS_CHECK, "SER_BRK_DETMS_CHECK");
            this.SER_BRK_DETMS_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.SER_BRK_DETMS_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.SER_BRK_DETMS_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.SER_BRK_DETMS_CHECK.IconColor = System.Drawing.Color.Gray;
            this.SER_BRK_DETMS_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SER_BRK_DETMS_CHECK.IconSize = 18;
            this.SER_BRK_DETMS_CHECK.Name = "SER_BRK_DETMS_CHECK";
            this.SER_BRK_DETMS_CHECK.TabStop = false;
            // 
            // groupGPIO
            // 
            this.groupGPIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupGPIO, 6);
            this.groupGPIO.Controls.Add(this.tableLayoutGPIO);
            resources.ApplyResources(this.groupGPIO, "groupGPIO");
            this.groupGPIO.ForeColor = System.Drawing.Color.White;
            this.groupGPIO.Name = "groupGPIO";
            this.groupGPIO.TabStop = false;
            // 
            // tableLayoutGPIO
            // 
            resources.ApplyResources(this.tableLayoutGPIO, "tableLayoutGPIO");
            this.tableLayoutGPIO.Controls.Add(this.GPIO0_CHECK, 2, 0);
            this.tableLayoutGPIO.Controls.Add(this.GPIO3_CHECK, 2, 3);
            this.tableLayoutGPIO.Controls.Add(this.GPIO2, 1, 2);
            this.tableLayoutGPIO.Controls.Add(this.GPIO0, 1, 0);
            this.tableLayoutGPIO.Controls.Add(this.GPIO3, 1, 3);
            this.tableLayoutGPIO.Controls.Add(this.label19, 0, 1);
            this.tableLayoutGPIO.Controls.Add(this.FSFRAMELOSS, 4, 1);
            this.tableLayoutGPIO.Controls.Add(this.label16, 0, 0);
            this.tableLayoutGPIO.Controls.Add(this.label17, 0, 3);
            this.tableLayoutGPIO.Controls.Add(this.label18, 0, 2);
            this.tableLayoutGPIO.Controls.Add(this.GPIO1, 1, 1);
            this.tableLayoutGPIO.Controls.Add(this.GPIO2_CHECK, 2, 2);
            this.tableLayoutGPIO.Controls.Add(this.GPIO1_CHECK, 2, 1);
            this.tableLayoutGPIO.Controls.Add(this.lblFailsafe, 3, 1);
            this.tableLayoutGPIO.Name = "tableLayoutGPIO";
            // 
            // GPIO0_CHECK
            // 
            resources.ApplyResources(this.GPIO0_CHECK, "GPIO0_CHECK");
            this.GPIO0_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.GPIO0_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.GPIO0_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.GPIO0_CHECK.IconColor = System.Drawing.Color.Gray;
            this.GPIO0_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GPIO0_CHECK.IconSize = 18;
            this.GPIO0_CHECK.Name = "GPIO0_CHECK";
            this.GPIO0_CHECK.TabStop = false;
            // 
            // GPIO3_CHECK
            // 
            resources.ApplyResources(this.GPIO3_CHECK, "GPIO3_CHECK");
            this.GPIO3_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.GPIO3_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.GPIO3_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.GPIO3_CHECK.IconColor = System.Drawing.Color.Gray;
            this.GPIO3_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GPIO3_CHECK.IconSize = 18;
            this.GPIO3_CHECK.Name = "GPIO3_CHECK";
            this.GPIO3_CHECK.TabStop = false;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // GPIO2_CHECK
            // 
            resources.ApplyResources(this.GPIO2_CHECK, "GPIO2_CHECK");
            this.GPIO2_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.GPIO2_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.GPIO2_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.GPIO2_CHECK.IconColor = System.Drawing.Color.Gray;
            this.GPIO2_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GPIO2_CHECK.IconSize = 18;
            this.GPIO2_CHECK.Name = "GPIO2_CHECK";
            this.GPIO2_CHECK.TabStop = false;
            // 
            // GPIO1_CHECK
            // 
            resources.ApplyResources(this.GPIO1_CHECK, "GPIO1_CHECK");
            this.GPIO1_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.GPIO1_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.GPIO1_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.GPIO1_CHECK.IconColor = System.Drawing.Color.Gray;
            this.GPIO1_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GPIO1_CHECK.IconSize = 18;
            this.GPIO1_CHECK.Name = "GPIO1_CHECK";
            this.GPIO1_CHECK.TabStop = false;
            // 
            // groupSecurity
            // 
            this.groupSecurity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.tableLayoutMain.SetColumnSpan(this.groupSecurity, 3);
            this.groupSecurity.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.groupSecurity, "groupSecurity");
            this.groupSecurity.ForeColor = System.Drawing.Color.White;
            this.groupSecurity.Name = "groupSecurity";
            this.groupSecurity.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnGenerateKey, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.ENCRYPTION_LEVEL_CHECK, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.AESKEY, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblENCRYPTION_LEVEL, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label35, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.ENCRYPTION_LEVEL, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.AESKEY_CHECK, 2, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnGenerateKey
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btnGenerateKey, 3);
            resources.ApplyResources(this.btnGenerateKey, "btnGenerateKey");
            this.btnGenerateKey.ForeColor = System.Drawing.Color.Black;
            this.btnGenerateKey.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.btnGenerateKey.IconColor = System.Drawing.Color.Black;
            this.btnGenerateKey.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGenerateKey.IconSize = 24;
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.UseVisualStyleBackColor = true;
            // 
            // ENCRYPTION_LEVEL_CHECK
            // 
            resources.ApplyResources(this.ENCRYPTION_LEVEL_CHECK, "ENCRYPTION_LEVEL_CHECK");
            this.ENCRYPTION_LEVEL_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.ENCRYPTION_LEVEL_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.ENCRYPTION_LEVEL_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.ENCRYPTION_LEVEL_CHECK.IconColor = System.Drawing.Color.Gray;
            this.ENCRYPTION_LEVEL_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ENCRYPTION_LEVEL_CHECK.IconSize = 18;
            this.ENCRYPTION_LEVEL_CHECK.Name = "ENCRYPTION_LEVEL_CHECK";
            this.ENCRYPTION_LEVEL_CHECK.TabStop = false;
            // 
            // AESKEY_CHECK
            // 
            resources.ApplyResources(this.AESKEY_CHECK, "AESKEY_CHECK");
            this.AESKEY_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.AESKEY_CHECK.ForeColor = System.Drawing.Color.Gray;
            this.AESKEY_CHECK.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.AESKEY_CHECK.IconColor = System.Drawing.Color.Gray;
            this.AESKEY_CHECK.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.AESKEY_CHECK.IconSize = 18;
            this.AESKEY_CHECK.Name = "AESKEY_CHECK";
            this.AESKEY_CHECK.TabStop = false;
            // 
            // flowLayoutActions
            // 
            this.flowLayoutActions.Controls.Add(this.btn_LoadSetting);
            this.flowLayoutActions.Controls.Add(this.btn_SaveSetting);
            this.flowLayoutActions.Controls.Add(this.btn_LoadFile);
            this.flowLayoutActions.Controls.Add(this.btn_SaveFile);
            this.flowLayoutActions.Controls.Add(this.btn_Reset);
            this.flowLayoutActions.Controls.Add(this.btn_Firmware);
            this.flowLayoutActions.Controls.Add(this.checkBoxSync);
            this.flowLayoutActions.Controls.Add(this.Progressbar);
            resources.ApplyResources(this.flowLayoutActions, "flowLayoutActions");
            this.flowLayoutActions.Name = "flowLayoutActions";
            // 
            // btn_SaveSetting
            // 
            this.btn_SaveSetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveSetting.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_SaveSetting, "btn_SaveSetting");
            this.btn_SaveSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveSetting.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.btn_SaveSetting.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_SaveSetting.IconSize = 24;
            this.btn_SaveSetting.Name = "btn_SaveSetting";
            this.btn_SaveSetting.UseVisualStyleBackColor = true;
            this.btn_SaveSetting.Click += new System.EventHandler(this.btn_SaveSetting_Click);
            // 
            // btn_LoadFile
            // 
            this.btn_LoadFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadFile.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_LoadFile, "btn_LoadFile");
            this.btn_LoadFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadFile.IconChar = FontAwesome.Sharp.IconChar.ArrowRightFromFile;
            this.btn_LoadFile.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_LoadFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_LoadFile.IconSize = 24;
            this.btn_LoadFile.Name = "btn_LoadFile";
            this.btn_LoadFile.UseVisualStyleBackColor = true;
            this.btn_LoadFile.Click += new System.EventHandler(this.btn_LoadFile_Click);
            // 
            // btn_SaveFile
            // 
            this.btn_SaveFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveFile.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_SaveFile, "btn_SaveFile");
            this.btn_SaveFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveFile.IconChar = FontAwesome.Sharp.IconChar.FileImport;
            this.btn_SaveFile.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_SaveFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_SaveFile.IconSize = 24;
            this.btn_SaveFile.Name = "btn_SaveFile";
            this.btn_SaveFile.UseVisualStyleBackColor = true;
            this.btn_SaveFile.Click += new System.EventHandler(this.btn_SaveFile_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Reset.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btn_Reset, "btn_Reset");
            this.btn_Reset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Reset.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateBackward;
            this.btn_Reset.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.btn_Reset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_Reset.IconSize = 24;
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // richTextHelp
            // 
            this.richTextHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.richTextHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.richTextHelp, "richTextHelp");
            this.richTextHelp.ForeColor = System.Drawing.Color.White;
            this.richTextHelp.Name = "richTextHelp";
            this.richTextHelp.ReadOnly = true;
            this.richTextHelp.TabStop = false;
            // 
            // groupInfo
            // 
            resources.ApplyResources(this.groupInfo, "groupInfo");
            this.tableLayoutMain.SetColumnSpan(this.groupInfo, 2);
            this.groupInfo.Controls.Add(this.richTextHelp);
            this.groupInfo.ForeColor = System.Drawing.Color.White;
            this.groupInfo.Name = "groupInfo";
            this.tableLayoutMain.SetRowSpan(this.groupInfo, 5);
            this.groupInfo.TabStop = false;
            // 
            // tableLayoutMain
            // 
            resources.ApplyResources(this.tableLayoutMain, "tableLayoutMain");
            this.tableLayoutMain.Controls.Add(this.linkLabel_lowlatency, 7, 5);
            this.tableLayoutMain.Controls.Add(this.groupData, 0, 5);
            this.tableLayoutMain.Controls.Add(this.linkLabel_mavlink, 6, 5);
            this.tableLayoutMain.Controls.Add(this.groupSecurity, 3, 3);
            this.tableLayoutMain.Controls.Add(this.groupSerial, 0, 3);
            this.tableLayoutMain.Controls.Add(this.groupFirmware, 0, 0);
            this.tableLayoutMain.Controls.Add(this.groupRadio, 0, 1);
            this.tableLayoutMain.Controls.Add(this.groupInfo, 6, 0);
            this.tableLayoutMain.Controls.Add(this.groupGPIO, 0, 4);
            this.tableLayoutMain.Name = "tableLayoutMain";
            // 
            // Sikradio
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.flowLayoutActions);
            this.Name = "Sikradio";
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin14ItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin13ItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin12ItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pin15ItemsBindingSource)).EndInit();
            this.groupFirmware.ResumeLayout(false);
            this.tableLayoutDevice.ResumeLayout(false);
            this.tableLayoutDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FREQ_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ATI_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FORMAT_CHECK)).EndInit();
            this.groupRadio.ResumeLayout(false);
            this.groupRadio.PerformLayout();
            this.tableRadio.ResumeLayout(false);
            this.tableRadio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OPPRESEND_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSSI_IN_DBM_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIR_FRAMELEN_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIR_SPEED_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAVLINK_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TXPOWER_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_WINDOW_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DUTY_CYCLE_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBT_RSSI_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_CHANNELS_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NETID_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_FREQ_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ANT_MODE_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MIN_FREQ_CHECK)).EndInit();
            this.groupSerial.ResumeLayout(false);
            this.groupSerial.PerformLayout();
            this.tableLayoutSerial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AUXSER_SPEED_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SERIAL_SPEED_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RTSCTS_CHECK)).EndInit();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            this.tableLayoutData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DESTID_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NODEID_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TX_ENCAP_METHOD_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RX_ENCAP_METHOD_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_DATA_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAX_RETRIES_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GLOBAL_RETRIES_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SER_BRK_DETMS_CHECK)).EndInit();
            this.groupGPIO.ResumeLayout(false);
            this.tableLayoutGPIO.ResumeLayout(false);
            this.tableLayoutGPIO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO0_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO3_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO2_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GPIO1_CHECK)).EndInit();
            this.groupSecurity.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ENCRYPTION_LEVEL_CHECK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AESKEY_CHECK)).EndInit();
            this.flowLayoutActions.ResumeLayout(false);
            this.flowLayoutActions.PerformLayout();
            this.groupInfo.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar Progressbar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox FREQ;
        private System.Windows.Forms.TextBox ATI2;
        private System.Windows.Forms.Label lblRX_ENCAP_METHOD;
        private System.Windows.Forms.ComboBox RX_ENCAP_METHOD;
        private System.Windows.Forms.Label lblTX_ENCAP_METHOD;
        private System.Windows.Forms.ComboBox TX_ENCAP_METHOD;
        private System.Windows.Forms.Label lblDESTID;
        private System.Windows.Forms.Label lblNODEID;
        private System.Windows.Forms.ComboBox DESTID;
        private System.Windows.Forms.ComboBox NODEID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SERIAL_SPEED;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FORMAT;
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
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox ENCRYPTION_LEVEL;
        private System.Windows.Forms.Label lblFailsafe;
        private System.Windows.Forms.ComboBox FSFRAMELOSS;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.GroupBox groupFirmware;
        private System.Windows.Forms.GroupBox groupRadio;
        private System.Windows.Forms.GroupBox groupSerial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox AUXSER_SPEED;
        private System.Windows.Forms.GroupBox groupData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupGPIO;
        private System.Windows.Forms.GroupBox groupSecurity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutActions;
        private System.Windows.Forms.BindingSource configManagerBindingSource;
        private FontAwesome.Sharp.IconButton btn_LoadSetting;
        private FontAwesome.Sharp.IconButton btn_SaveSetting;
        private FontAwesome.Sharp.IconButton btn_LoadFile;
        private FontAwesome.Sharp.IconButton btn_SaveFile;
        private FontAwesome.Sharp.IconButton btn_Reset;
        private FontAwesome.Sharp.IconButton btn_Firmware;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDevice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ATI;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboModemSelection;
        private System.Windows.Forms.BindingSource modemsBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutSerial;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutGPIO;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox GPIO1;
        private System.Windows.Forms.ComboBox GPIO2;
        private System.Windows.Forms.ComboBox GPIO0;
        private System.Windows.Forms.ComboBox GPIO3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutData;
        private System.Windows.Forms.RichTextBox richTextHelp;
        private System.Windows.Forms.GroupBox groupInfo;
        private FontAwesome.Sharp.IconPictureBox ATI_CHECK;
        private FontAwesome.Sharp.IconPictureBox FREQ_CHECK;
        private FontAwesome.Sharp.IconPictureBox FORMAT_CHECK;
        private System.Windows.Forms.TableLayoutPanel tableRadio;
        private FontAwesome.Sharp.IconPictureBox OPPRESEND_CHECK;
        private FontAwesome.Sharp.IconPictureBox RSSI_IN_DBM_CHECK;
        private FontAwesome.Sharp.IconPictureBox AIR_FRAMELEN_CHECK;
        private FontAwesome.Sharp.IconPictureBox AIR_SPEED_CHECK;
        private FontAwesome.Sharp.IconPictureBox MAVLINK_CHECK;
        private FontAwesome.Sharp.IconPictureBox TXPOWER_CHECK;
        private FontAwesome.Sharp.IconPictureBox MAX_WINDOW_CHECK;
        private FontAwesome.Sharp.IconPictureBox DUTY_CYCLE_CHECK;
        private FontAwesome.Sharp.IconPictureBox LBT_RSSI_CHECK;
        private FontAwesome.Sharp.IconPictureBox NUM_CHANNELS_CHECK;
        private FontAwesome.Sharp.IconPictureBox NETID_CHECK;
        private FontAwesome.Sharp.IconPictureBox MAX_FREQ_CHECK;
        private FontAwesome.Sharp.IconPictureBox ANT_MODE_CHECK;
        private FontAwesome.Sharp.IconPictureBox MIN_FREQ_CHECK;
        private System.Windows.Forms.ComboBox ANT_MODE;
        private System.Windows.Forms.ComboBox NETID;
        private System.Windows.Forms.ComboBox LBT_RSSI;
        private System.Windows.Forms.ComboBox MAX_WINDOW;
        private System.Windows.Forms.ComboBox MAVLINK;
        private System.Windows.Forms.ComboBox AIR_FRAMELEN;
        private System.Windows.Forms.TextBox RSSI;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox RSSI_IN_DBM;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.CheckBox OPPRESEND;
        private System.Windows.Forms.Label lblMAVLINK;
        private System.Windows.Forms.Label lblOPPRESEND;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblMIN_FREQ;
        private System.Windows.Forms.Label lblMAX_FREQ;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblMAX_WINDOW;
        private System.Windows.Forms.Label lblNUM_CHANNELS;
        private System.Windows.Forms.Label lblDUTY_CYCLE;
        private System.Windows.Forms.Label lblLBT_RSSI;
        private System.Windows.Forms.Label lblTXPOWER;
        private System.Windows.Forms.Label lblNETID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox RATE_FREQBAND;
        private System.Windows.Forms.ComboBox MIN_FREQ;
        private System.Windows.Forms.Label lblANT_MODE;
        private System.Windows.Forms.ComboBox MAX_FREQ;
        private System.Windows.Forms.ComboBox TXPOWER;
        private System.Windows.Forms.ComboBox DUTY_CYCLE;
        private System.Windows.Forms.ComboBox AIR_SPEED;
        private System.Windows.Forms.ComboBox NUM_CHANNELS;
        private FontAwesome.Sharp.IconPictureBox ENCRYPTION_LEVEL_CHECK;
        private FontAwesome.Sharp.IconPictureBox AESKEY_CHECK;
        private FontAwesome.Sharp.IconPictureBox AUXSER_SPEED_CHECK;
        private FontAwesome.Sharp.IconPictureBox SERIAL_SPEED_CHECK;
        private FontAwesome.Sharp.IconPictureBox RTSCTS_CHECK;
        private FontAwesome.Sharp.IconPictureBox GPIO0_CHECK;
        private FontAwesome.Sharp.IconPictureBox GPIO3_CHECK;
        private FontAwesome.Sharp.IconPictureBox GPIO2_CHECK;
        private FontAwesome.Sharp.IconPictureBox GPIO1_CHECK;
        private FontAwesome.Sharp.IconPictureBox SER_BRK_DETMS_CHECK;
        private FontAwesome.Sharp.IconPictureBox GLOBAL_RETRIES_CHECK;
        private FontAwesome.Sharp.IconPictureBox MAX_RETRIES_CHECK;
        private FontAwesome.Sharp.IconPictureBox MAX_DATA_CHECK;
        private FontAwesome.Sharp.IconPictureBox RX_ENCAP_METHOD_CHECK;
        private FontAwesome.Sharp.IconPictureBox TX_ENCAP_METHOD_CHECK;
        private FontAwesome.Sharp.IconPictureBox DESTID_CHECK;
        private FontAwesome.Sharp.IconPictureBox NODEID_CHECK;
        private System.Windows.Forms.BindingSource pin14ItemsBindingSource;
        private System.Windows.Forms.BindingSource pin13ItemsBindingSource;
        private System.Windows.Forms.BindingSource pin12ItemsBindingSource;
        private System.Windows.Forms.BindingSource pin15ItemsBindingSource;
        private System.Windows.Forms.CheckBox checkBoxSync;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private FontAwesome.Sharp.IconButton btnGenerateKey;
    }
}