using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class AF3Status : Form
    {

        public AF3Status()
        {
            InitializeComponent();

            Utilities.ThemeManager.ApplyThemeTo(this);

            timer1.Start();

            label2.BackColor = label3.BackColor = label4.BackColor = Color.FromArgb(0x33, 0x33, 0x33);
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

            updateTelemLabel(lbRFC1TELEM, telem[0]);
            updateTelemLabel(lbRFC2TELEM, telem[1]);
            updateTelemLabel(lbRFC3TELEM, telem[2]);

            int activeRFC = (int)MainV2.comPort.MAV.cs.af3.activeRFC;

            updateActiveRfcLabel(lbRFC1Active, (activeRFC == 0));
            updateActiveRfcLabel(lbRFC2Active, (activeRFC == 1));
            updateActiveRfcLabel(lbRFC3Active, (activeRFC == 2));

            // Update/Create ESC info labels

            int epCount = MainV2.comPort.MAV.cs.af3.getEndpointCount();

            for (int i = 0; i < epCount; i++)
            {
                var item = MainV2.comPort.MAV.cs.af3.getEndpoint(i);

                epInfo.UpdateItem(item);

            }

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }
    }
}