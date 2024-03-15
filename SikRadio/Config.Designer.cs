namespace SikRadio
{
    partial class Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.CMB_SerialPort = new System.Windows.Forms.ComboBox();
            this.CMB_Baudrate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuLoadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.sikradio1 = new MissionPlanner.Radio.Sikradio(ConfigManager);
            this.tabPageTerminal = new System.Windows.Forms.TabPage();
            this.terminal1 = new SikRadio.Terminal();
            this.tabPageRSSI = new System.Windows.Forms.TabPage();
            this.rssi1 = new SikRadio.Rssi();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPageTerminal.SuspendLayout();
            this.tabPageRSSI.SuspendLayout();
            this.SuspendLayout();
            // 
            // CMB_SerialPort
            // 
            this.CMB_SerialPort.FormattingEnabled = true;
            resources.ApplyResources(this.CMB_SerialPort, "CMB_SerialPort");
            this.CMB_SerialPort.Name = "CMB_SerialPort";
            this.CMB_SerialPort.SelectedIndexChanged += new System.EventHandler(this.CMB_SerialPort_SelectedIndexChanged);
            this.CMB_SerialPort.Click += new System.EventHandler(this.CMB_SerialPort_Click);
            // 
            // CMB_Baudrate
            // 
            this.CMB_Baudrate.FormattingEnabled = true;
            this.CMB_Baudrate.Items.AddRange(new object[] {
            resources.GetString("CMB_Baudrate.Items"),
            resources.GetString("CMB_Baudrate.Items1"),
            resources.GetString("CMB_Baudrate.Items2"),
            resources.GetString("CMB_Baudrate.Items3"),
            resources.GetString("CMB_Baudrate.Items4"),
            resources.GetString("CMB_Baudrate.Items5"),
            resources.GetString("CMB_Baudrate.Items6"),
            resources.GetString("CMB_Baudrate.Items7"),
            resources.GetString("CMB_Baudrate.Items8"),
            resources.GetString("CMB_Baudrate.Items9"),
            resources.GetString("CMB_Baudrate.Items10")});
            resources.ApplyResources(this.CMB_Baudrate, "CMB_Baudrate");
            this.CMB_Baudrate.Name = "CMB_Baudrate";
            this.CMB_Baudrate.SelectedIndexChanged += new System.EventHandler(this.CMB_Baudrate_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CMB_SerialPort);
            this.groupBox1.Controls.Add(this.CMB_Baudrate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuSaveFile,
            this.toolStripMenuLoadFile,
            this.toolStripMenuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // toolStripMenuSaveFile
            // 
            this.toolStripMenuSaveFile.Name = "toolStripMenuSaveFile";
            resources.ApplyResources(this.toolStripMenuSaveFile, "toolStripMenuSaveFile");
            // 
            // toolStripMenuLoadFile
            // 
            this.toolStripMenuLoadFile.Name = "toolStripMenuLoadFile";
            resources.ApplyResources(this.toolStripMenuLoadFile, "toolStripMenuLoadFile");
            // 
            // toolStripMenuExit
            // 
            this.toolStripMenuExit.Name = "toolStripMenuExit";
            resources.ApplyResources(this.toolStripMenuExit, "toolStripMenuExit");
            this.toolStripMenuExit.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // btnConnect
            // 
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageTerminal);
            this.tabControl1.Controls.Add(this.tabPageRSSI);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageSettings
            // 
            resources.ApplyResources(this.tabPageSettings, "tabPageSettings");
            this.tabPageSettings.Controls.Add(this.sikradio1);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // sikradio1
            // 
            resources.ApplyResources(this.sikradio1, "sikradio1");
            this.sikradio1.Name = "sikradio1";
            // 
            // tabPageTerminal
            // 
            this.tabPageTerminal.Controls.Add(this.terminal1);
            resources.ApplyResources(this.tabPageTerminal, "tabPageTerminal");
            this.tabPageTerminal.Name = "tabPageTerminal";
            this.tabPageTerminal.UseVisualStyleBackColor = true;
            // 
            // terminal1
            // 
            resources.ApplyResources(this.terminal1, "terminal1");
            this.terminal1.Name = "terminal1";
            // 
            // tabPageRSSI
            // 
            this.tabPageRSSI.Controls.Add(this.rssi1);
            resources.ApplyResources(this.tabPageRSSI, "tabPageRSSI");
            this.tabPageRSSI.Name = "tabPageRSSI";
            this.tabPageRSSI.UseVisualStyleBackColor = true;
            // 
            // rssi1
            // 
            resources.ApplyResources(this.rssi1, "rssi1");
            this.rssi1.Name = "rssi1";
            // 
            // Config
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Config_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageTerminal.ResumeLayout(false);
            this.tabPageRSSI.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMB_SerialPort;
        private System.Windows.Forms.ComboBox CMB_Baudrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSaveFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuLoadFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabPage tabPageTerminal;
        private System.Windows.Forms.TabPage tabPageRSSI;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private Terminal terminal1;
        private MissionPlanner.Radio.Sikradio sikradio1;
        private Rssi rssi1;
    }
}

