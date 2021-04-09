using MissionPlanner.ArduPilot;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class AF3Status : Form
    {
        private List<System.Windows.Forms.Label> lbRFCTelem = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCFlightMode = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCArmStatus = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCScore = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCCanPres = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCppmVis = new List<System.Windows.Forms.Label>();
        private int refreshCyclesCount = 0;
        private string[] MODE_NAMES = new string[] {"STABILIZE", "ACRO", "ALT HOLD", "AUTO","GUIDED","LOITER","RTL",
            "CIRCLE","","LAND","","DRIFT","","SPORT","FLIP","AUTO TUNE","POS HOLD","BRAKE","THROW","AVOID ADSB","GUIDED NO GPS","SMART RTL","FLOW HOLD",
            "FOLLOW","ZIGZAG","SYSID","HELI AUTOROT"};
        private string[] ARMED_NAMES = new string[] { "ARM UNKNOWN", "ARMED", "DISARMED" };

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public AF3Status()
        {
            InitializeComponent();

            Utilities.ThemeManager.ApplyThemeTo(this);

            timer1.Start();

            label2.BackColor = label3.BackColor = label4.BackColor = Color.FromArgb(0x55, 0x55, 0x55);

            lbRFCTelem.Add(lbRFC1TELEM);
            lbRFCTelem.Add(lbRFC2TELEM);
            lbRFCTelem.Add(lbRFC3TELEM);

            lbRFCFlightMode.Add(lbRFC1FlightMode);
            lbRFCFlightMode.Add(lbRFC2FlightMode);
            lbRFCFlightMode.Add(lbRFC3FlightMode);

            lbRFCArmStatus.Add(lbRFC1ArmStatus);
            lbRFCArmStatus.Add(lbRFC2ArmStatus);
            lbRFCArmStatus.Add(lbRFC3ArmStatus);

            lbRFCCanPres.Add(lbRFC1CanPres);
            lbRFCCanPres.Add(lbRFC2CanPres);
            lbRFCCanPres.Add(lbRFC3CanPres);

            lbRFCScore.Add(lbRFC1Score);
            lbRFCScore.Add(lbRFC2Score);
            lbRFCScore.Add(lbRFC3Score);

            lbRFCppmVis.Add(lbRFC1ppmVis);
            lbRFCppmVis.Add(lbRFC2ppmVis);
            lbRFCppmVis.Add(lbRFC3ppmVis);
        }

        private void updateCanPresLabel(Label lb, uint canPresent)
        {
            if (canPresent < 2)
            {
                lb.Text = "CAN PRESENT";
                lb.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
            }
            else
            {
                lb.Text = "NO CAN";
                lb.BackColor = Color.Red;
            }
        }

        private void updateScoreLabel(Label lb, float score)
        {
            lb.Text = String.Format("{0:N2}", score);
            lb.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
        }

        private void updateTelemLabel(Label lb, bool telemPresent)
        {
            if (telemPresent)
            {
                lb.Text = "TELEM";
                lb.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
            }
            else
            {
                lb.Text = "NO TELEM";
                lb.BackColor = Color.Red;
            }
        }

        private void updatePpmLabel(Label lb, bool ppmVisible)
        {
            if (ppmVisible)
            {
                lb.Text = "PPM OK";
                lb.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
            }
            else
            {
                lb.Text = "NO PPM";
                lb.BackColor = Color.Red;
            }
        }

        private void updateFlightModeLabel(uint flightMode, int rfc_index)
        {
            string mode = flightMode.ToString();
            bool rfHealthy = !MainV2.comPort.MAV.cs.af3.checkFlightModeMismatch(rfc_index);

            // Translate mode number to human-readable
            if (flightMode < MODE_NAMES.Length)
                mode = MODE_NAMES[flightMode];

            var lbFlightMode = lbRFCFlightMode[rfc_index];

            if (rfHealthy)
            {
                lbFlightMode.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
            }
            else
            {
                lbFlightMode.BackColor = Color.FromArgb(0xFF, 0x00, 0x00);
            }
            
            lbFlightMode.Text = mode.ToString();
        }

        private void updateArmedStatusLabel(uint armedStatus, int rfc_index)
        {
            string status = armedStatus.ToString();
            bool rfHealthy = !MainV2.comPort.MAV.cs.af3.checkArmedStatusMismatch(rfc_index);

            // Translate mode number to human-readable
            if (armedStatus < ARMED_NAMES.Length)
                status = ARMED_NAMES[armedStatus];

            var lbArmedStatus = lbRFCArmStatus[rfc_index];

            if (rfHealthy)
            {
                lbArmedStatus.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
            }
            else
            {
                lbArmedStatus.BackColor = Color.FromArgb(0xFF, 0x00, 0x00);
            }

            lbArmedStatus.Text = status.ToString();
        }

        private void updateActiveRfcLabel(Label lb, bool Active)
        {
            if (Active)
            {
                lb.Text = "ACTIVE";
                lb.BackColor = Color.Green;
            }
            else
            {
                lb.Text = "";
                lb.BackColor = BackColor;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool[] telem = MainV2.comPort.MAV.cs.af3.telemRFC;
            bool[] ppmvis = MainV2.comPort.MAV.cs.af3.ppmVisRFC;
            uint[] canPresent = MainV2.comPort.MAV.cs.af3.canElapsedRFC;
            uint[] flightModes = MainV2.comPort.MAV.cs.af3.flightModeRFC;
            uint[] armStatuses = MainV2.comPort.MAV.cs.af3.armedStatRFC;
            float[] scores = MainV2.comPort.MAV.cs.af3.scoreRFC;

            for (int i = 0; i < MainV2.comPort.MAV.cs.af3.number_rfcs; i++)
            {
                updateTelemLabel(lbRFCTelem[i], telem[i]);
                updateCanPresLabel(lbRFCCanPres[i], canPresent[i]);
                updateScoreLabel(lbRFCScore[i], scores[i]);
                updateFlightModeLabel(flightModes[i], i);
                updateArmedStatusLabel(armStatuses[i], i);
                updatePpmLabel(lbRFCppmVis[i], ppmvis[i]);
            }

            int activeRFC = (int)MainV2.comPort.MAV.cs.af3.activeRFC;

            updateActiveRfcLabel(lbRFC1Active, (activeRFC == 0));
            updateActiveRfcLabel(lbRFC2Active, (activeRFC == 1));
            updateActiveRfcLabel(lbRFC3Active, (activeRFC == 2));

            // Update/Create ESC info labels

            int epCount = MainV2.comPort.MAV.cs.af3.getEndpointCount();

            for (int i = 0; i < epCount; i++)
            {
                AF3EndPoint item = MainV2.comPort.MAV.cs.af3.getEndpoint(i);
                uint epEscIndex = item.esc_index;

                string origin = String.Format("EP{0}", item.esc_index);
                List<errorRecord> errLs = MainV2.comPort.MAV.cs.af3.getErrors();
                List<errorRecord> errEp = errLs.FindAll(error => error.origin == origin &&
                    error.resolved == DateTime.MinValue);

                errorRecord worseError = null;

                foreach (var err in errEp)
                {
                    if (worseError == null)
                    {
                        worseError = err;
                        continue;
                    }
                    else
                    {
                        if ((uint)err.state >= (uint)worseError.state)
                        {
                            worseError = err;
                        }
                    }

                }

                epInfo.UpdateItem(item, worseError);
            }

            refreshCyclesCount++;

            List<errorRecord> errList = MainV2.comPort.MAV.cs.af3.getErrors();

            if (errList != null)
            {
                // error inacessible member
                foreach (errorRecord error in errList)
                {
                    bool found = false;

                    foreach (ListViewItem item in lsErrorList.Items)
                    {

                        if ((item.Tag as string).Equals(error.hash))
                        {
                            found = true;
                            item.UseItemStyleForSubItems = false;

                            if (error.resolved == DateTime.MinValue)
                            {
                                double elapsedMillis = DateTime.Now.Subtract(error.timestamp).TotalMilliseconds;
                                item.SubItems[4].Text = MainV2.comPort.MAV.cs.af3.MillisToTime(elapsedMillis);
                                item.SubItems[4].ForeColor = Color.Red;
                            }
                            else
                            {
                                double elapsedMillis = error.resolved.Subtract(error.timestamp).TotalMilliseconds;
                                item.SubItems[4].Text = MainV2.comPort.MAV.cs.af3.MillisToTime(elapsedMillis);
                                item.SubItems[4].ForeColor = Color.White;
                            }

                            break;
                        }
                    }

                    if (!found)
                    {
                        string[] values = new string[] { error.timestamp.ToString("HH:mm:ss"),
                            error.origin,
                            error.state.ToString(),
                            error.message,
                            ""};

                        string hash = error.timestamp.ToBinary().ToString() + values[1] +
                                values[2] + error.failedBuses.ToString();

                        ListViewItem errLine = new ListViewItem(values);
                        errLine.Tag = hash;
                        lsErrorList.Items.Add(errLine);
                    }

                }

            }

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }

    }
}