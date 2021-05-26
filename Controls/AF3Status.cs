using MissionPlanner.ArduPilot;
using MissionPlanner.Utilities;
using MissionPlanner.Utilities.AF3;
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
        private AF3ErrorLog errorLog = new AF3ErrorLog();
        private Color defaultBgColor = Color.FromArgb(0x33, 0x33, 0x33);

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

            label1.BackColor = lbRFC1.BackColor = lbRFC2.BackColor = 
                lbRFC3.BackColor = Color.FromArgb(0x55, 0x55, 0x55);

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

            if (MainV2.comPort.MAV.cs.af3.number_rfcs == 2)
            {
                lbRFC1.Text = "RFC1 CAN1";
                lbRFC2.Text = "RFC1 CAN2";
                lbRFC3.Text = "";
                lbRFC3.BackColor = this.BackColor;
            }
        }

        private void updateCanPresLabel(Label lb, uint canPresent)
        {
            if (canPresent < 2)
            {
                lb.Text = "CAN PRESENT";
                lb.BackColor = defaultBgColor;
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
            lb.BackColor = defaultBgColor;
        }

        private void updateTelemLabel(Label lb, bool telemPresent)
        {
            if (telemPresent)
            {
                lb.Text = "TELEM";
                lb.BackColor = defaultBgColor;
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
                lb.BackColor = defaultBgColor;
            }
            else
            {
                lb.Text = "NO PPM";
                lb.BackColor = Color.Red;
            }
        }

        private void updateVfcStats(int cpuLoading, float temperature, float voltA, float voltB,
            float rxAux, float txAux, float rxAf3, float txAf3)
        {
            if (MainV2.comPort.MAV.cs.af3.number_rfcs == 0) return;

            lbVfcLoading.Text = String.Format("CPU LOAD {0}%", cpuLoading);
            lbVfcLoading.BackColor = cpuLoading > Constants.maxCpuLoading ? Color.Red : defaultBgColor;

            lbVfcTemp.Text = String.Format("CPU TEMP {0} °C", temperature);
            lbVfcTemp.BackColor = cpuLoading > Constants.maxCpuTemperature ? Color.Red : defaultBgColor;

            lbVfcVoltA.Text = String.Format("VFC VOLT A {0:N} V", voltA / 1000);
            lbVfcVoltA.BackColor = voltA < Constants.lowerVoltageThres ? Color.Red : defaultBgColor;
            lbVfcVoltB.Text = String.Format("VFC VOLT B {0:N} V", voltB / 1000);
            lbVfcVoltB.BackColor = voltB < Constants.lowerVoltageThres ? Color.Red : defaultBgColor;

            lbRxAf3.Text = String.Format("VFC RX AF3 {0:N} kbps", rxAf3 / 1000);
            lbTxAf3.Text = String.Format("VFC TX AF3 {0:N} kbps", txAf3 / 1000);
            lbRxAux.Text = String.Format("VFC RX AUX {0:N} kbps", rxAf3 / 1000);
            lbTxAux.Text = String.Format("VFC TX AUX {0:N} kbps", txAf3 / 1000);

            lbRxAf3.BackColor = defaultBgColor;
            lbTxAf3.BackColor = defaultBgColor;
            lbRxAux.BackColor = defaultBgColor;
            lbTxAux.BackColor = defaultBgColor;
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
                lbFlightMode.BackColor = defaultBgColor;
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
                lbArmedStatus.BackColor = defaultBgColor;
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

            for (int i = 0; i < Constants.maxNumCanBuses; i++)
            {
                if (i < MainV2.comPort.MAV.cs.af3.number_rfcs)
                {
                    updateTelemLabel(lbRFCTelem[i], telem[i]);
                    updateCanPresLabel(lbRFCCanPres[i], canPresent[i]);
                    updateScoreLabel(lbRFCScore[i], scores[i]);
                    updateFlightModeLabel(flightModes[i], i);
                    updateArmedStatusLabel(armStatuses[i], i);
                    updatePpmLabel(lbRFCppmVis[i], ppmvis[i]);
                }
                else
                {
                    lbRFCTelem[i].Text = "";
                    lbRFCCanPres[i].Text = "";
                    lbRFCScore[i].Text = "";
                    lbRFCFlightMode[i].Text = "";
                    lbRFCArmStatus[i].Text = "";
                    lbRFCppmVis[i].Text = "";
                }
            }

            updateVfcStats(MainV2.comPort.MAV.cs.af3.vfcCpuLoading,
                MainV2.comPort.MAV.cs.af3.vfcCpuTemperature,
                MainV2.comPort.MAV.cs.af3.vfcPsVoltageA,
                MainV2.comPort.MAV.cs.af3.vfcPsVoltageB,
                MainV2.comPort.MAV.cs.af3.vfcBitrateRxAux,
                MainV2.comPort.MAV.cs.af3.vfcBitrateTxAux,
                MainV2.comPort.MAV.cs.af3.vfcBitrateRxAf3,
                MainV2.comPort.MAV.cs.af3.vfcBitrateTxAf3);

            int activeRFC = (int)MainV2.comPort.MAV.cs.af3.activeRFC;

            updateActiveRfcLabel(lbRFC1Active, (activeRFC == 0));
            updateActiveRfcLabel(lbRFC2Active, (activeRFC == 1));
            updateActiveRfcLabel(lbRFC3Active, (activeRFC == 2));

            // Update/Create ESC info labels

            int epCount = MainV2.comPort.MAV.cs.af3.getEndpointCount();

            for (int i = 0; i < epCount; i++)
            {
                EndPoint item = MainV2.comPort.MAV.cs.af3.getEndpoint(i);
                uint epEscIndex = item.esc_index;

                string origin = String.Format("EP{0}", item.esc_index);
                List<errorRecord> errLs = MainV2.comPort.MAV.cs.af3.getErrors();
                List<errorRecord> errEp = errLs.FindAll(error => error.origin == origin &&
                    !error.resolved);

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

            populateEcam();

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }

        private void populateEcam()
        {

            List<ecamErrorRecord> ecamErrList = MainV2.comPort.MAV.cs.af3.getEcamErrors();

            if (ecamErrList != null && ecamList != null)
            {
                // Only update is the ecam List has changed

                if (ecamErrList.Count == ecamList.Items.Count)
                {
                    bool equal = true;

                    for (int i = 0; i < ecamErrList.Count; i++)
                    {

                        if (ecamList.Items[i].Text != ecamErrList[i].message)
                        {
                            equal = false;
                            break;
                        }

                    }

                    if (equal) return;
                }

                ecamList.Items.Clear();

                foreach (var item in ecamErrList)
                {
                    var lsItem = ecamList.Items.Add(item.message);
                    Color clr = Color.Green;
                    
                    switch (item.severity)
                    {
                        case ecamErrorRecord.severityType.ALERT:
                            clr = Color.Orange;
                            break;
                        case ecamErrorRecord.severityType.CRITICAL:
                            clr = Color.Red;
                            break;
                    }

                    lsItem.ForeColor = clr;
                }

            }

        }

        private void AF3Status_Load(object sender, EventArgs e)
        {

        }

        private void ecamList_DoubleClick(object sender, EventArgs e)
        {
            errorLog.Show();
        }

        private void clearAllMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainV2.comPort.MAV.cs.af3.ClearEcamMessages();
        }
    }
}