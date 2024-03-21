using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MissionPlanner;
using MissionPlanner.MsgBox;
using MissionPlanner.Radio;
using ZedGraph;
using RFDCommon.Interface;
using MissionPlanner.Comms;
using RFDCommon;

namespace SikRadio
{
    public partial class Rssi : UserControl, IRFDConfigForm
    {
        
        private readonly RollingPointPairList plotdatanoicel = new RollingPointPairList(1200);
        private readonly RollingPointPairList plotdatanoicer = new RollingPointPairList(1200);

        private readonly RollingPointPairList plotdatarssil = new RollingPointPairList(1200);
        private readonly RollingPointPairList plotdatarssir = new RollingPointPairList(1200);
        private int tickStart;
        //RFD.RFD900.TSession _Session;
        private IModemComms _comms;
        private bool _started = false;

        public Rssi()
        {
            InitializeComponent();            

            zedGraphControl1.GraphPane.AddCurve("RSSI Local", plotdatarssil, Color.Red, SymbolType.None);
            zedGraphControl1.GraphPane.AddCurve("RSSI Remote", plotdatarssir, Color.Green, SymbolType.None);
            zedGraphControl1.GraphPane.AddCurve("Noise Local", plotdatanoicel, Color.Blue, SymbolType.None);
            zedGraphControl1.GraphPane.AddCurve("Noise Remote", plotdatanoicer, Color.Orange, SymbolType.None);

            zedGraphControl1.GraphPane.Title.Text = "RSSI";

            Terminal.SetupStreamWriter();
        }

       

        public void Start(IModemComms modemComms)
        {
            Visible = true;
            _started = true;
            _comms = modemComms;

            // Listen to connection state changes
            _comms.ConnectionStateChanged += ModemComms_ConnectionStateChanged;

            
            if (!_comms.IsConnected())
            {
                //_comms.Connect();
                // Connect to begin?

                return;
            } 
            else
            {
                StartRSSI();
            }
        }

        
        private void ModemComms_ConnectionStateChanged(object sender, EventArgs e)
        {
            if (_comms.IsConnected())
            {
                // start up rssi?
                StartRSSI();
            } 
            else
            {

            }
        }

        private void StartRSSI()
        {
            zedGraphControl1.Refresh();
            if (RFDLib.Utils.Retry(() =>
            {
                
                return _comms.PutIntoATCommandMode() == RFD.RFD900.TSession.TMode.AT_COMMAND;
            }
                , 3))
            {
                var session = _comms.GetSession();
                if (RFDLib.Utils.Retry(() => session.ATCClient.DoQuery("AT&T=RSSI", true).Contains("RSSI"), 3))
                {
                    session.AssumeMode(RFD.RFD900.TSession.TMode.TRANSPARENT);

                    tickStart = Environment.TickCount;

                    timer1.Start();
                }
                else
                {
                    var ATIReply = session.ATCClient.DoQuery("ATI", true);
                    if (RFDLib.Text.Contains(ATIReply, "async"))
                    {
                        MissionPlanner.MsgBox.CustomMessageBox.Show("Firmware doesn't support RSSI reporting");
                    }
                    else
                    {
                        MissionPlanner.MsgBox.CustomMessageBox.Show("Failed to enter RSSI reporting mode.");
                    }
                }
            }
            else
            {
                MissionPlanner.MsgBox.CustomMessageBox.Show("Failed to put modem into AT command mode.");
            }
        }

        public void Stop()
        {
            Visible = false;

            if (_started)
            {
                _started = false;
                timer1.Stop();
                if (_comms.IsConnected())
                {
                    _comms.GetSession().ATCClient.DoQuery("AT&T", true);
                }
                // Unsub to connection state changes
                _comms.ConnectionStateChanged -= ModemComms_ConnectionStateChanged;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var comPort = _comms.GetSession().Port;// SikRadio.Config.comPort;

            if ((comPort != null) && comPort.IsOpen)
            {
                comPort.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

                if (comPort.BytesToRead < 50)
                    return;

                try
                {
                    var line = comPort.ReadLine();

                    /*
    L/R RSSI: 12/0  L/R noise: 17/0 pkts: 0  txe=0 rxe=0 stx=0 srx=0 ecc=0/0 temp=61 dco=0
    L/R RSSI: 12/0  L/R noise: 16/0 pkts: 0  txe=0 rxe=0 stx=0 srx=0 ecc=0/0 temp=61 dco=0
                     */

                    var rssi = new Regex(@"RSSI: ([0-9]+)/([0-9]+)\s+L/R noise: ([0-9]+)/([0-9]+)");

                    var match = rssi.Match(line);

                    if (match.Success)
                    {
                        var time = (Environment.TickCount - tickStart) / 1000.0;

                        plotdatarssil.Add(time, double.Parse(match.Groups[1].Value));
                        plotdatarssir.Add(time, double.Parse(match.Groups[2].Value));
                        plotdatanoicel.Add(time, double.Parse(match.Groups[3].Value));
                        plotdatanoicer.Add(time, double.Parse(match.Groups[4].Value));


                        // Make sure the Y axis is rescaled to accommodate actual data
                        zedGraphControl1.AxisChange();

                        // Force a redraw

                        zedGraphControl1.Invalidate();

                        if (Terminal.sw != null)
                        {
                            Terminal.sw.Write(line);
                            Terminal.sw.Flush();
                        }
                    }
                }
                catch
                {
                }
            }
        }        
    }
}