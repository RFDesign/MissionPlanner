using System;
using System.Diagnostics;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.Comms;
using MissionPlanner.Radio;
using MissionPlanner.Utilities;

namespace SikRadio
{
    public partial class Config : Form
    {
        ISikRadioForm _CurrentForm;
        static GUISession _GUISession;

        public Config()
        {
            InitializeComponent();

            _GUISession = new GUISession(btnConnect, CMB_SerialPort, CMB_Baudrate);


            settingsToolStripMenuItem_Click(null, null);
        }

        public static ICommsSerial comPort
        {
            get
            {
                return _GUISession.comPort;
            }
        }

        private ISikRadioForm loadSettings()
        {
            //Terminal.threadrun = false;

            //panel1.Controls.Clear();

            var form = new Sikradio();
            form.GUISession = _GUISession;
            form.Enabled = false;

            panel1.Controls.Add(form);

            ThemeManager.SetTheme(ThemeManager.Themes.None);

            ThemeManager.ApplyThemeTo(this);

            return form;
        }

        private ISikRadioForm loadTerminal()
        {
            //panel1.Controls.Clear();

            var form = new Terminal();
            form.Enabled = false;
            form.GUISession = _GUISession;

            form.Dock = DockStyle.Fill;

            panel1.Controls.Add(form);

            ThemeManager.SetTheme(ThemeManager.Themes.None);

            ThemeManager.ApplyThemeTo(this);

            return form;
        }

        private ISikRadioForm loadRssi()
        {
            //Terminal.threadrun = false;

            //panel1.Controls.Clear();

            var form = new Rssi();
            form.GUISession = _GUISession;
            form.Enabled = false;

            form.Dock = DockStyle.Fill;

            panel1.Controls.Add(form);

            ThemeManager.SetTheme(ThemeManager.Themes.None);

            ThemeManager.ApplyThemeTo(this);

            return form;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/ardupilot-mega/wiki/3DRadio");
        }

        private void projectPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/tridge/SiK");
        }

        void ShowForm(Func<ISikRadioForm> Constructor)
        {
            if (_CurrentForm != null)
            {
                _CurrentForm.Disconnect();
                _CurrentForm.Dispose();
            }
            _CurrentForm = Constructor();
            _CurrentForm.Enabled = _GUISession.IsConnected;
            _CurrentForm.Show();
            if (_GUISession.IsConnected)
            {
                _CurrentForm.Connect();
            }
        }

        private void terminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(loadTerminal);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(loadSettings);
        }

        private void rssiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(loadRssi);
        }

        void ConnectedEvtHdlr(bool Connected)
        {
            if (_CurrentForm != null)
            {
                if (Connected)
                {
                    _CurrentForm.Connect();
                }
                else
                {
                    _CurrentForm.Disconnect();
                }
                _CurrentForm.Enabled = Connected;
            }
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CurrentForm != null)
            {
                _CurrentForm.Disconnect();
            }
        }
    }
}