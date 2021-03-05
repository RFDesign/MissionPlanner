using System;
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
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rf1Health.Value = (int)(MainV2.comPort.MAV.cs.ekfvelv * 100);
            rf2Health.Value = (int)(MainV2.comPort.MAV.cs.ekfposhor * 100);
            rf3Health.Value = (int)(MainV2.comPort.MAV.cs.ekfposvert * 100);

            lbRFC1TELEM.Text = MainV2.comPort.MAV.cs.af3rf1telem ? "TELEM" : "NO TELEM";
            lbRFC2TELEM.Text = MainV2.comPort.MAV.cs.af3rf2telem ? "TELEM" : "NO TELEM";
            lbRFC3TELEM.Text = MainV2.comPort.MAV.cs.af3rf3telem ? "TELEM" : "NO TELEM";

            int activeRFC = (int)MainV2.comPort.MAV.cs.af3rfcactive;

            lbRFC1Active.Text = (activeRFC == 0) ? "ACTIVE" : "";
            lbRFC2Active.Text = (activeRFC == 1) ? "ACTIVE" : "";
            lbRFC3Active.Text = (activeRFC == 2) ? "ACTIVE" : "";

            // restore colours
            Utilities.ThemeManager.ApplyThemeTo(this);

            foreach (var item in new VerticalProgressBar2[] { rf1Health, rf2Health, rf3Health })
            {
                if (item.Value > 50)
                    item.ValueColor = Color.Orange;

                if (item.Value > 80)
                    item.ValueColor = Color.Red;
            }

            int idx = 0;
            for (int bitvalue = 1; bitvalue <= (int)MAVLink.EKF_STATUS_FLAGS.EKF_UNINITIALIZED; bitvalue = bitvalue << 1)
            {
                int currentbit = (MainV2.comPort.MAV.cs.ekfflags & bitvalue);

                var currentflag = (MAVLink.EKF_STATUS_FLAGS)Enum.Parse(typeof(MAVLink.EKF_STATUS_FLAGS), bitvalue.ToString());

                if (flowLayoutPanel1.Controls.Count <= idx)
                {
                    flowLayoutPanel1.Controls.Add(new Label() { Height = 13, Width = flowLayoutPanel1.Width });
                }

                flowLayoutPanel1.Controls[idx].Text = currentflag.ToString().Replace("EKF_", "").ToLower() + " " +
                                                         (currentbit > 0 ? "On " : "Off") + "\r\n";

                flowLayoutPanel1.Controls[idx].ForeColor = ForeColor;

                if ((currentflag == MAVLink.EKF_STATUS_FLAGS.EKF_VELOCITY_HORIZ ||
                     currentflag == MAVLink.EKF_STATUS_FLAGS.EKF_POS_HORIZ_ABS ||
                     currentflag == MAVLink.EKF_STATUS_FLAGS.EKF_POS_VERT_ABS) && currentbit == 0)
                {
                    flowLayoutPanel1.Controls[idx].ForeColor = Color.Red;
                }

                idx++;
            }
        }
    }
}