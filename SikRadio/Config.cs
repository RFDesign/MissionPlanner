using System;
using System.Diagnostics;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.Comms;
using MissionPlanner.Radio;
using MissionPlanner.Utilities;
using Microsoft.VisualBasic;
using RFDCommon.Interface;
using RFDCommon;
using System.Threading.Tasks;
using RFD.RFD900;
using System.Drawing;
using FontAwesome.Sharp;

namespace SikRadio
{
    public partial class Config : Form
    {
        
        //ISikRadioForm _CurrentForm;
        static ICommsSerial _comPort;        
        public static IModemComms _modemComms = new ModemComms();
        public ConfigManager ConfigManager = new ConfigManager(_modemComms);
        private int _selectedTabIndex = 0;   
        Color btnBackDefault = Color.FromArgb(255, 21, 29, 46);
        Color btnBackSelected = Color.FromArgb(255, 73, 82, 110);

        public Config()
        {
            InitializeComponent();

            sikradio1.Init(ConfigManager);
            
            // Handle connection state changes
            _modemComms.ConnectionStateChanged += _modemComms_ConnectionStateChanged;

            var Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            Text = "RFD900 Tools " + Version.Minor.ToString() + "." + Version.Build.ToString() + " - RFDesign";

            CMB_SerialPort.Items.AddRange(SerialPort.GetPortNames());
            CMB_SerialPort.Items.Add("TCP");

            if (CMB_SerialPort.Items.Count > 0)
                CMB_SerialPort.SelectedIndex = 0;

            // default
            CMB_Baudrate.SelectedIndex = CMB_Baudrate.Items.IndexOf("57600");

            MissionPlanner.Comms.CommsBase.InputBoxShow += CommsBaseOnInputBoxShow;

            //settingsToolStripMenuItem_Click(null, null);
            
            if (SikRadio.Program.Manufacturer)
            {
                //loadManufacturing();
                //ToolStripMenuItem ManItem = new ToolStripMenuItem("Manufacturing");
                //ManItem.Click += ManufacturerToolStripMenuItem_Click;

                //menuStrip1.Items.Add(ManItem);
            }

            this.configManagerBindingSource.DataSource = ConfigManager;

            SwitchForms(sikradio1, btnConfigPage);
        }

        private async void _modemComms_ConnectionStateChanged(object sender, EventArgs e)
        {
            if (_modemComms.IsConnected())
            {
                // Just Connected - Find out who is home?
                ConfigManager.QueryModems();
            } 
            else
            {

            }
        }

        private async void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var previousForm = tabControl1.TabPages[_selectedTabIndex].Controls[0] as IRFDConfigForm;
            //if (previousForm != null)
            //    previousForm.Stop();

            //// Update index for next time...
            //_selectedTabIndex = tabControl1.SelectedIndex;

            //// Start new form
            //var selectedForm = tabControl1.SelectedTab.Controls[0] as IRFDConfigForm;
            //if (selectedForm == null)
            //    return;

            //selectedForm.Start(_modemComms);            
        }

        /// <summary>
        /// Shows a dialog box in which to enter comms information.
        /// </summary>
        /// <param name="title">The title of the dialog box.</param>
        /// <param name="prompttext">The text to display in the dialog box.</param>
        /// <param name="text">The text to return.</param>
        /// <returns></returns>
        public static inputboxreturn CommsBaseOnInputBoxShow(string title, string prompttext, ref string text)
        {
            text = Interaction.InputBox(prompttext, title, "");

            return inputboxreturn.OK;
        }

        //public static ICommsSerial comPort
        //{
        //    get
        //    {
        //        return _comPort;
        //    }
        //}

        //private ISikRadioForm loadSettings()
        //{
        //    //Terminal.threadrun = false;

        //    //panel1.Controls.Clear();

        //    var form = new Sikradio();
        //    //form.Enabled = false;
        //    form.DoDisconnectReconnect += DoDisconnectReconnect;

        //    tabPageSettings.Controls.Add(form);

        //    ThemeManager.SetTheme(ThemeManager.Themes.None);

        //    ThemeManager.ApplyThemeTo(this);

        //    return form;
        //}

        //private ISikRadioForm loadTerminal()
        //{
        //    //panel1.Controls.Clear();

        //    var form = new Terminal();
        //    //form.Enabled = false;

        //    form.Dock = DockStyle.Fill;

        //    tabPageTerminal.Controls.Add(form);

        //    ThemeManager.SetTheme(ThemeManager.Themes.None);

        //    ThemeManager.ApplyThemeTo(this);

        //    return form;
        //}

        //private ISikRadioForm loadRssi()
        //{
        //    //Terminal.threadrun = false;

        //    //panel1.Controls.Clear();

        //    var form = new Rssi();
        //    //form.Enabled = false;

        //    form.Dock = DockStyle.Fill;

        //    tabPageRSSI.Controls.Add(form);

        //    ThemeManager.SetTheme(ThemeManager.Themes.None);

        //    ThemeManager.ApplyThemeTo(this);

        //    return form;
        //}

        //private ISikRadioForm loadManufacturing()
        //{
        //    //panel1.Controls.Clear();

        //    var form = new RFD900Tools.Manufacturing();
        //    form.Enabled = false;

        //    form.Dock = DockStyle.Fill;

        //    //tabPageManufacture.Controls.Add(form);

        //    ThemeManager.SetTheme(ThemeManager.Themes.None);

        //    ThemeManager.ApplyThemeTo(this);

        //    return form;
        //}


        //void DoDisconnectReconnect()
        //{
        //    if (_Connected)
        //    {
        //        Disconnect();
        //        Connect();
        //    }
        //}

        private void CMB_SerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainV2.comPort.BaseStream.PortName = CMB_SerialPort.Text;
            MainV2.comPortName = CMB_SerialPort.Text;
        }

        private void CMB_Baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainV2.comPort.BaseStream.BaudRate = int.Parse(CMB_Baudrate.Text);
            MainV2.comPortBaud = int.Parse(CMB_Baudrate.Text);

            if (_modemComms.IsConnected())
                _modemComms.Reconnect();
            //DoDisconnectReconnect();
        }

        private void CMB_SerialPort_Click(object sender, EventArgs e)
        {
            CMB_SerialPort.Items.Clear();
            CMB_SerialPort.Items.AddRange(SerialPort.GetPortNames());
            CMB_SerialPort.Items.Add("TCP");
       
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/ardupilot-mega/wiki/3DRadio");
        }

        private void projectPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/tridge/SiK");
        }

        //void ShowForm(Func<ISikRadioForm> Constructor)
        //{
        //    if (_CurrentForm != null)
        //    {
        //        _CurrentForm.Disconnect();
        //        _CurrentForm.Dispose();
        //    }
        //    _CurrentForm = Constructor();
        //    _CurrentForm.Enabled = _Connected;
        //    //GB.Text = _CurrentForm.Header;
        //    _CurrentForm.Show();
        //    if (_Connected)
        //    {
        //        _CurrentForm.Connect(comPort);
        //    }
        //}

       
        //void ManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowForm(loadManufacturing);
        //}

        //private void terminalToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowForm(loadTerminal);
        //}

        //private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowForm(loadSettings);
        //}

        //private void rssiToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowForm(loadRssi);
        //}


        private async Task ToggleConnect()
        {
            // NOW to make connect work globally!
            if (_modemComms.IsConnected())
            {
                if (_modemComms.Disconnect())
                {
                    // Update local control state
                    btnConnect.Text = "Connect";
                    CMB_Baudrate.Enabled = true;
                    CMB_SerialPort.Enabled = true;

                    ConfigManager.AddLog($"Disconnected");
                }
            }
            else
            {
                if (_modemComms.Connect())
                {        
                    // Update local control state
                    btnConnect.Text = "Disconnect";
                    CMB_Baudrate.Enabled = (_comPort is SerialPort);
                    CMB_SerialPort.Enabled = false;
                    ConfigManager.AddLog($"Connected");
                }
            }
        }

        

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            _modemComms.ConnectionStateChanged -= _modemComms_ConnectionStateChanged;
            _modemComms.Disconnect();            
        }

        private async void btnConnect_Click_1(object sender, EventArgs e)
        {
            await ToggleConnect();
        }

        private async void SwitchForms(IRFDConfigForm showForm, IconButton button)
        {
            foreach (var item in panelMain.Controls)
            {
                var form = item as IRFDConfigForm;
                if (form == null)
                    continue;
                
                if (form == showForm)
                {
                    form.Start(_modemComms);
                } else
                {
                    form.Stop();
                }                
            }
            foreach (var item in flowLayoutButtonPanel.Controls)
            {
                var btn = item as FontAwesome.Sharp.IconButton;
                if (btn == null)
                    continue;

                if (btn == button)
                {
                    
                    btn.BackColor = btnBackSelected;
                    //btn.IconColor = btnSelected;
                    //btn.ForeColor = btnSelected;
                } 
                else
                {
                    btn.BackColor = btnBackDefault;
                    //btn.IconColor = btnDefault;
                    //btn.ForeColor = btnDefault;
                }
            }
        }

        private void btnConfigPage_Click(object sender, EventArgs e)
        {            
            SwitchForms(sikradio1, sender as IconButton);
        }

        private void btnTerminal_Click(object sender, EventArgs e)
        {
            SwitchForms(terminal1, sender as IconButton);
        }

        private void btnRSSI_Click(object sender, EventArgs e)
        {
            SwitchForms(rssi1, sender as IconButton);
        }

        private void btnManufacturer_Click(object sender, EventArgs e)
        {
            //SwitchForms()
        }
    }
}