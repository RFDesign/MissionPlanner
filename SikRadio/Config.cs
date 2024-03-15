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

namespace SikRadio
{
    public partial class Config : Form
    {
        
        //ISikRadioForm _CurrentForm;
        static ICommsSerial _comPort;        
        public static IModemComms _modemComms = new ModemComms();
        public ConfigManager ConfigManager = new ConfigManager(_modemComms);
        private int _selectedTabIndex = 0;

        public Config()
        {
            InitializeComponent();
            
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

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

            tabControl1.SelectedIndex = 0;            
        }

        private void _modemComms_ConnectionStateChanged(object sender, EventArgs e)
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
            var previousForm = tabControl1.TabPages[_selectedTabIndex].Controls[0] as IRFDConfigForm;
            if (previousForm != null)
                previousForm.Stop();

            // Update index for next time...
            _selectedTabIndex = tabControl1.SelectedIndex;

            // Start new form
            var selectedForm = tabControl1.SelectedTab.Controls[0] as IRFDConfigForm;
            if (selectedForm == null)
                return;

            selectedForm.Start(_modemComms);            
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
                _modemComms.Disconnect();   
                
                // Update local control state
                btnConnect.Text = "Connect";                
                CMB_Baudrate.Enabled = true;
                CMB_SerialPort.Enabled = true;
            }
            else
            {
                if (_modemComms.Connect())
                {        
                    // Update local control state
                    btnConnect.Text = "Disconnect";
                    CMB_Baudrate.Enabled = (_comPort is SerialPort);
                    CMB_SerialPort.Enabled = false;
                }
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {            
            await ToggleConnect();            
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            _modemComms.ConnectionStateChanged -= _modemComms_ConnectionStateChanged;
            _modemComms.Disconnect();            
        }

        
        
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}