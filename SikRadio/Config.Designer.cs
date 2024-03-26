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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.modemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.flowLayoutButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnConfigPage = new FontAwesome.Sharp.IconButton();
            this.btnTerminal = new FontAwesome.Sharp.IconButton();
            this.btnRSSI = new FontAwesome.Sharp.IconButton();
            this.btnManufacturer = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.CMB_Baudrate = new System.Windows.Forms.ComboBox();
            this.CMB_SerialPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new FontAwesome.Sharp.IconButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.textConsole = new System.Windows.Forms.TextBox();
            this.configManagerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sikradio1 = new MissionPlanner.Radio.Sikradio();
            this.terminal1 = new SikRadio.Terminal();
            this.rssi1 = new SikRadio.Rssi();
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).BeginInit();
            this.panelNavigation.SuspendLayout();
            this.flowLayoutButtonPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // modemsBindingSource
            // 
            this.modemsBindingSource.DataMember = "Modems";
            this.modemsBindingSource.DataSource = this.configManagerBindingSource;
            // 
            // panelNavigation
            // 
            this.panelNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.panelNavigation.Controls.Add(this.flowLayoutButtonPanel);
            this.panelNavigation.Controls.Add(this.panel3);
            resources.ApplyResources(this.panelNavigation, "panelNavigation");
            this.panelNavigation.Name = "panelNavigation";
            // 
            // flowLayoutButtonPanel
            // 
            this.flowLayoutButtonPanel.Controls.Add(this.btnConfigPage);
            this.flowLayoutButtonPanel.Controls.Add(this.btnTerminal);
            this.flowLayoutButtonPanel.Controls.Add(this.btnRSSI);
            this.flowLayoutButtonPanel.Controls.Add(this.btnManufacturer);
            resources.ApplyResources(this.flowLayoutButtonPanel, "flowLayoutButtonPanel");
            this.flowLayoutButtonPanel.Name = "flowLayoutButtonPanel";
            // 
            // btnConfigPage
            // 
            resources.ApplyResources(this.btnConfigPage, "btnConfigPage");
            this.btnConfigPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigPage.FlatAppearance.BorderSize = 0;
            this.btnConfigPage.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnConfigPage.IconChar = FontAwesome.Sharp.IconChar.Gear;
            this.btnConfigPage.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnConfigPage.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConfigPage.IconSize = 32;
            this.btnConfigPage.Name = "btnConfigPage";
            this.btnConfigPage.UseVisualStyleBackColor = true;
            this.btnConfigPage.Click += new System.EventHandler(this.btnConfigPage_Click);
            // 
            // btnTerminal
            // 
            resources.ApplyResources(this.btnTerminal, "btnTerminal");
            this.btnTerminal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminal.FlatAppearance.BorderSize = 0;
            this.btnTerminal.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnTerminal.IconChar = FontAwesome.Sharp.IconChar.Terminal;
            this.btnTerminal.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnTerminal.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnTerminal.IconSize = 32;
            this.btnTerminal.Name = "btnTerminal";
            this.btnTerminal.UseVisualStyleBackColor = true;
            this.btnTerminal.Click += new System.EventHandler(this.btnTerminal_Click);
            // 
            // btnRSSI
            // 
            resources.ApplyResources(this.btnRSSI, "btnRSSI");
            this.btnRSSI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRSSI.FlatAppearance.BorderSize = 0;
            this.btnRSSI.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnRSSI.IconChar = FontAwesome.Sharp.IconChar.LineChart;
            this.btnRSSI.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnRSSI.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnRSSI.IconSize = 32;
            this.btnRSSI.Name = "btnRSSI";
            this.btnRSSI.UseVisualStyleBackColor = true;
            this.btnRSSI.Click += new System.EventHandler(this.btnRSSI_Click);
            // 
            // btnManufacturer
            // 
            resources.ApplyResources(this.btnManufacturer, "btnManufacturer");
            this.btnManufacturer.FlatAppearance.BorderSize = 0;
            this.btnManufacturer.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnManufacturer.IconChar = FontAwesome.Sharp.IconChar.Industry;
            this.btnManufacturer.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnManufacturer.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnManufacturer.IconSize = 32;
            this.btnManufacturer.Name = "btnManufacturer";
            this.btnManufacturer.UseVisualStyleBackColor = true;
            this.btnManufacturer.Click += new System.EventHandler(this.btnManufacturer_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Name = "label9";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CMB_Baudrate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CMB_SerialPort, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // CMB_Baudrate
            // 
            resources.ApplyResources(this.CMB_Baudrate, "CMB_Baudrate");
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
            this.CMB_Baudrate.Name = "CMB_Baudrate";
            this.CMB_Baudrate.SelectedIndexChanged += new System.EventHandler(this.CMB_Baudrate_SelectedIndexChanged);
            // 
            // CMB_SerialPort
            // 
            resources.ApplyResources(this.CMB_SerialPort, "CMB_SerialPort");
            this.CMB_SerialPort.FormattingEnabled = true;
            this.CMB_SerialPort.Name = "CMB_SerialPort";
            this.CMB_SerialPort.SelectedIndexChanged += new System.EventHandler(this.CMB_SerialPort_SelectedIndexChanged);
            this.CMB_SerialPort.Click += new System.EventHandler(this.CMB_SerialPort_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnConnect
            // 
            resources.ApplyResources(this.btnConnect, "btnConnect");
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.IconChar = FontAwesome.Sharp.IconChar.SatelliteDish;
            this.btnConnect.IconColor = System.Drawing.Color.Black;
            this.btnConnect.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConnect.IconSize = 24;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click_1);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.textConsole);
            this.panelMain.Controls.Add(this.sikradio1);
            this.panelMain.Controls.Add(this.terminal1);
            this.panelMain.Controls.Add(this.rssi1);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // textConsole
            // 
            resources.ApplyResources(this.textConsole, "textConsole");
            this.textConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(49)))), ((int)(((byte)(66)))));
            this.textConsole.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configManagerBindingSource, "Log", true));
            this.textConsole.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.textConsole.Name = "textConsole";
            this.textConsole.ReadOnly = true;
            this.textConsole.TabStop = false;
            this.textConsole.TextChanged += new System.EventHandler(this.textConsole_TextChanged);
            // 
            // configManagerBindingSource
            // 
            this.configManagerBindingSource.DataSource = typeof(RFDCommon.ConfigManager);
            // 
            // sikradio1
            // 
            resources.ApplyResources(this.sikradio1, "sikradio1");
            this.sikradio1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.sikradio1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sikradio1.ForeColor = System.Drawing.Color.White;
            this.sikradio1.Name = "sikradio1";
            // 
            // terminal1
            // 
            resources.ApplyResources(this.terminal1, "terminal1");
            this.terminal1.Name = "terminal1";
            // 
            // rssi1
            // 
            resources.ApplyResources(this.rssi1, "rssi1");
            this.rssi1.Name = "rssi1";
            // 
            // Config
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelNavigation);
            this.Name = "Config";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Config_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.modemsBindingSource)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            this.flowLayoutButtonPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configManagerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource configManagerBindingSource;
        private System.Windows.Forms.BindingSource modemsBindingSource;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnConnect;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutButtonPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CMB_SerialPort;
        private System.Windows.Forms.ComboBox CMB_Baudrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnConfigPage;
        private FontAwesome.Sharp.IconButton btnTerminal;
        private FontAwesome.Sharp.IconButton btnRSSI;
        private FontAwesome.Sharp.IconButton btnManufacturer;
        private System.Windows.Forms.Panel panelMain;
        private MissionPlanner.Radio.Sikradio sikradio1;
        private System.Windows.Forms.TextBox textConsole;
        private Terminal terminal1;
        private Rssi rssi1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

