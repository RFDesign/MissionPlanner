using MissionPlanner.Controls;
using MissionPlanner.Properties;
using MissionPlanner.Utilities;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews
{
    public partial class AF3Config : MyUserControl, IActivate
    {
        PacketInspector<MAVLink.MAVLinkMessage> mavi = new PacketInspector<MAVLink.MAVLinkMessage>();

        private int currRFCidx = -1;
        private Timer timer1 = new Timer();

        public AF3Config()
        {
            InitializeComponent();

            MainV2.comPort.OnPacketReceived += MavOnOnPacketReceived;

            timer1.Tick += (sender, args) => Update();

            timer1.Start();
        }

        private void MavOnOnPacketReceived(object o, MAVLink.MAVLinkMessage linkMessage)
        {
            mavi.Add(linkMessage.sysid, linkMessage.compid, linkMessage.msgid, linkMessage, linkMessage.Length);
        }

        public void Activate()
        {
        }

        public new void Update()
        {

            foreach (var mavLinkMessage in mavi.GetPacketMessages())
            {

                if (mavLinkMessage.msgid == (uint)MAVLink.MAVLINK_MSG_ID.AF3_STATUS)
                {
                    object currRFC = mavLinkMessage.data.GetPropertyOrField("active_rfc");
                    currRFCidx = Convert.ToInt32(currRFC);

                    if ((currRFCidx >= 0) && (currRFCidx < 3))
                    {
                        lbActiveRFC.Text = String.Format("Active Flight Controller: {0}", currRFCidx);
                    }
                    else
                    {
                        lbActiveRFC.Text = String.Format("Flight Solution is not avalaible ({0})", currRFCidx); 
                    }

                }
            }

        }

    }
}
