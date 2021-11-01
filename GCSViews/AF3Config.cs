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
        RFDLib.Telemetry.TWrappedTimestamped<MissionPlanner.Utilities.AF3.IntegrityStatus> _StatusWrapper;
        TIntegReader _IntegReader;

        public AF3Config()
        {
            InitializeComponent();

            MainV2.comPort.OnPacketReceived += MavOnOnPacketReceived;

            timer1.Tick += (sender, args) => Update();

            timer1.Start();

            this.VisibleChanged += VisibleChangedEvtHdlr;

            _StatusWrapper = new RFDLib.Telemetry.TWrappedTimestamped<Utilities.AF3.IntegrityStatus>(
                () => MainV2.comPort.MAV.cs.af3IntegrityStatus);

            _IntegReader = new TIntegReader(_StatusWrapper);
            _IntegReader.Status.Updated += () => this.BeginInvoke(new Action(IntegStatusUpdatedEvtHdlr));
            _IntegReader.TimedOut += () => this.BeginInvoke(new Action(IntegStatusUpdatedEvtHdlr));
        }

        void VisibleChangedEvtHdlr(object sender, EventArgs e)
        {
            _IntegReader.Enabled = Visible;
        }

        void IntegStatusUpdatedEvtHdlr()
        {
            if (_IntegReader.Status.IsValid)
            {
                var Status = _IntegReader.Status.Value;

                btnSaveSnapshot.Enabled = Status.Action == MAVLink.AF3_INTEGRITY_ACTION.IDLE;
                btnCheckIntegrity.Enabled = btnSaveSnapshot.Enabled && Status.GotSnapshot;

                string StatusText = "Status:\n";

                StatusText += "\tSnapshot:  " + (Status.GotSnapshot ? "Saved" : "None") + "\n";
                StatusText += "\tIntegrity:  " + (Status.IntegrityOK ? "Checked OK" : "Not Checked") + "\n";

                switch (Status.Action)
                {
                    case MAVLink.AF3_INTEGRITY_ACTION.GETTING_SNAPSHOT:
                        StatusText += "\tGetting Snapshot..." + Status.Progress.ToString() + "%";
                        break;
                    case MAVLink.AF3_INTEGRITY_ACTION.CHECKING_INTEGRITY:
                        StatusText += "\tChecking Integrity..." + Status.Progress.ToString() + "%";
                        break;
                }

                lblIntegStatus.Text = StatusText;

                gbInteg.Enabled = true;
            }
            else
            {
                gbInteg.Enabled = false;
            }
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
            _StatusWrapper.GetWrapped();

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

        private void btnCheckIntegrity_Click(object sender, EventArgs e)
        {
            MainV2.comPort.doCommandInt(MainV2.comPort.MAV.sysid,
                    MainV2.comPort.MAV.compid,
                    (MAVLink.MAV_CMD)MAVLink.AF3_COMMANDS.CHECK_INTEGRITY, 0, 0, 0, 0, 0, 0, 0, false);
        }

        private void btnSaveSnapshot_Click(object sender, EventArgs e)
        {
            MainV2.comPort.doCommandInt(MainV2.comPort.MAV.sysid,
                    MainV2.comPort.MAV.compid,
                    (MAVLink.MAV_CMD)MAVLink.AF3_COMMANDS.GET_SNAPSHOT, 0, 0, 0, 0, 0, 0, 0, false);
        }

        class TIntegReader : RFDLib.Telemetry.TReader<MissionPlanner.Utilities.AF3.IntegrityStatus>
        {
            public TIntegReader(RFDLib.Telemetry.TGeneralTimestamped<Utilities.AF3.IntegrityStatus> x)
                : base(x)
            {
            }

            protected override void Request()
            {
                MainV2.comPort.doCommandInt(MainV2.comPort.MAV.sysid,
                    MainV2.comPort.MAV.compid,
                    (MAVLink.MAV_CMD)MAVLink.AF3_COMMANDS.REPORT_INTEGRITY_STATUS, 0, 0, 0, 0, 0, 0, 0, false);    
            }
        }
    }
}
