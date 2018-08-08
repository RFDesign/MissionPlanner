using System;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.Comms;
using Microsoft.VisualBasic;

namespace SikRadio
{
    public interface ISikRadioForm : IDisposable
    {
        void Connect();
        void Disconnect();
        void Show();
        bool Enabled { get; set; }
    }

    public class GUISession
    {
        bool _Connected = false;
        ICommsSerial _comPort;

        public readonly Button btnConnect;
        public readonly ComboBox cmbCOMPort;
        public readonly ComboBox cmbBaudRate;
        public event Action<bool> Connected;

        public GUISession(Button Connect, ComboBox COMPort, ComboBox BaudRate)
        {
            this.btnConnect = Connect;
            this.cmbCOMPort = COMPort;
            this.cmbBaudRate = BaudRate;

            cmbCOMPort.Items.AddRange(SerialPort.GetPortNames());
            cmbCOMPort.Items.Add("TCP");

            if (cmbCOMPort.Items.Count > 0)
                cmbCOMPort.SelectedIndex = 0;

            // default
            cmbBaudRate.Items.Clear();
            cmbBaudRate.Items.Add("2400");
            cmbBaudRate.Items.Add("4800");
            cmbBaudRate.Items.Add("9600");
            cmbBaudRate.Items.Add("14400");
            cmbBaudRate.Items.Add("19200");
            cmbBaudRate.Items.Add("28800");
            cmbBaudRate.Items.Add("38400");
            cmbBaudRate.Items.Add("57600");
            cmbBaudRate.Items.Add("115200");
            cmbBaudRate.Items.Add("230400");
            cmbBaudRate.Items.Add("460800");
            cmbBaudRate.SelectedIndex = cmbBaudRate.Items.IndexOf("57600");

            btnConnect.Text = "Connect";

            cmbCOMPort.SelectedIndexChanged += cmbCOMPort_SelectedIndexChanged;
            cmbBaudRate.SelectedIndexChanged += cmbBaudRate_SelectedIndexChanged;
            cmbCOMPort.Click += cmbCOMPort_Click;
            btnConnect.Click += btnConnect_Click;

            MissionPlanner.Comms.CommsBase.InputBoxShow += CommsBaseOnInputBoxShow;
        }

        private void cmbCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainV2.comPort.BaseStream.PortName = cmbCOMPort.Text;
            MainV2.comPortName = cmbCOMPort.Text;
        }

        private void cmbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainV2.comPort.BaseStream.BaudRate = int.Parse(cmbBaudRate.Text);
            MainV2.comPortBaud = int.Parse(cmbBaudRate.Text);
        }

        private void cmbCOMPort_Click(object sender, EventArgs e)
        {
            cmbBaudRate.Items.Clear();
            cmbBaudRate.Items.AddRange(SerialPort.GetPortNames());
            cmbBaudRate.Items.Add("TCP");

        }

        public ICommsSerial comPort
        {
            get
            {
                return _comPort;
            }
        }

        public bool IsConnected
        {
            get
            {
                return _Connected;
            }
        }

        void getTelemPortWithRadio(ref ICommsSerial comPort)
        {
            // try telem1

            comPort = new MAVLinkSerialPort(MainV2.comPort, (int)MAVLink.SERIAL_CONTROL_DEV.TELEM1);

            comPort.ReadTimeout = 4000;

            comPort.Open();
        }

        bool Connect()
        {
            try
            {
                if (MainV2.comPort.BaseStream.PortName.Contains("TCP"))
                {
                    _comPort = new TcpSerial();
                    _comPort.BaudRate = MainV2.comPort.BaseStream.BaudRate;
                    _comPort.ReadTimeout = 4000;
                    _comPort.Open();
                }
                else
                {
                    _comPort = new SerialPort();

                    if (MainV2.comPort.BaseStream.IsOpen)
                    {
                        getTelemPortWithRadio(ref _comPort);
                    }
                    else
                    {
                        _comPort.PortName = MainV2.comPort.BaseStream.PortName;
                        _comPort.BaudRate = MainV2.comPort.BaseStream.BaudRate;
                    }

                    _comPort.ReadTimeout = 4000;

                    _comPort.Open();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool Disconnect()
        {
            _comPort.Close();
            _comPort = null;
            return true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_Connected)
            {
                if (Connected != null)
                {
                    Connected(false);
                }
                Disconnect();
                _Connected = false;
                btnConnect.Text = "Connect";
                cmbBaudRate.Enabled = true;
                cmbCOMPort.Enabled = true;
            }
            else
            {
                if (Connect())
                {
                    _Connected = true;
                    btnConnect.Text = "Disconnect";
                    if (Connected != null)
                    {
                        Connected(true);
                    }
                    cmbBaudRate.Enabled = false;
                    cmbCOMPort.Enabled = false;
                }
            }
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


    }
}