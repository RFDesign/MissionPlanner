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
        private int refreshCyclesCount = 0;

        public AF3Status()
        {
            InitializeComponent();

            Utilities.ThemeManager.ApplyThemeTo(this);

            timer1.Start();

            label2.BackColor = label3.BackColor = label4.BackColor = Color.FromArgb(0x55, 0x55, 0x55);

            lbRFCTelem.Add(lbRFC1TELEM);
            lbRFCTelem.Add(lbRFC2TELEM);
            lbRFCTelem.Add(lbRFC3TELEM);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void updateTelemLabel(Label lb, bool telemPresent)
        {
            if (telemPresent)
            {
                lb.Text = "TELEM";
                lb.BackColor = BackColor;
            }
            else
            {
                lb.Text = "NO TELEM";
                lb.BackColor = Color.Red;
            }
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

            for (int i = 0; i < MainV2.comPort.MAV.cs.af3.number_rfcs; i++)
            {
                updateTelemLabel(lbRFCTelem[i], telem[i]);
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

                epInfo.UpdateItem(item, lsErrorList);
            }

            refreshCyclesCount++;

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }
    }
}