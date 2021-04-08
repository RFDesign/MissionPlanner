using MissionPlanner.ArduPilot;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class AF3Status : Form
    {
        private List<System.Windows.Forms.Label> lbRFCTelem = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCFlightMode = new List<System.Windows.Forms.Label>();
        private List<System.Windows.Forms.Label> lbRFCArmStatus = new List<System.Windows.Forms.Label>();
        private int refreshCyclesCount = 0;
        private string[] MODE_NAMES = new string[] {"STABILIZE", "ACRO", "ALT HOLD", "AUTO","GUIDED","LOITER","RTL",
            "CIRCLE","","LAND","","DRIFT","","SPORT","FLIP","AUTO TUNE","POS HOLD","BRAKE","THROW","AVOID ADSB","GUIDED NO GPS","SMART RTL","FLOW HOLD",
            "FOLLOW","ZIGZAG","SYSID","HELI AUTOROT"};
        private string[] ARMED_NAMES = new string[] { "ARM UNKNOWN", "ARMED", "DISARMED" };

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
            uint[] flightModes = MainV2.comPort.MAV.cs.af3.flightModeRFC;
            uint[] armStatuses = MainV2.comPort.MAV.cs.af3.armedStatRFC;

            for (int i = 0; i < MainV2.comPort.MAV.cs.af3.number_rfcs; i++)
            {
                updateTelemLabel(lbRFCTelem[i], telem[i]);
                updateFlightModeLabel(flightModes[i], i);
                updateArmedStatusLabel(armStatuses[i], i);
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
                    error.resolved == false);

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
                foreach (errorRecord error in errList)
                {
                    bool found = false;

                    foreach (ListViewItem item in lsErrorList.Items)
                    {
                        /*if ((item.SubItems[0].Text == error.timestamp.ToString("HH:mm:ss")) &&
                            (item.SubItems[1].Text == error.origin) &&
                            (item.SubItems[2].Text == error.state.ToString()) &&
                            (error.state == errorRecord.opState.FULL_FAILURE))
                        {
                            item.SubItems[3].Text = error.message;
                            break;
                        }*/

                        if (item.Tag == error)
                        {
                            found = true;
                            
                            if (error.state == errorRecord.opCode.FULL_FAILURE)
                            {
                                item.SubItems[3].Text = error.message;
                            }

                            break;
                        }
                    }

                    if ((!found))//&&(error.state != errorRecord.opCode.NORMAL))
                    {
                        string[] values = new string[] { error.timestamp.ToString("HH:mm:ss"),
                        error.origin,
                        error.state.ToString(),
                        error.message };

                        ListViewItem errLine = new ListViewItem(values);
                        errLine.Tag = error;
                        lsErrorList.Items.Add(errLine);
                    }

                }

            }

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }

    }
}