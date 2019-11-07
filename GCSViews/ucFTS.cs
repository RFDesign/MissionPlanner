using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews
{
    public partial class ucFTS : UserControl
    {
        public readonly FTS.TSingleFTSManager Manager;
        bool _HasShownTermWindowBefore = false;
        TGPSGroup _GPSGroup1;
        TGPSGroup _GPSGroup2;

        public ucFTS(FTS.TSingleFTSManager Manager)
        {
            InitializeComponent();

            _GPSGroup1 = new TGPSGroup(lblGPS1Detected, lblGPS1SatsHDOP);
            _GPSGroup2 = new TGPSGroup(lblGPS2Detected, lblGPS2SatsHDOP);

            this.Manager = Manager;
        }

        void SetRSSILabel(System.Windows.Forms.Label L, float RSSI)
        {
            if (float.IsNaN(RSSI))
            {
                SetLabelText(L, "--", false);
            }
            else
            {
                bool IsOK = RSSI >= -100;
                SetLabelText(L, RSSI.ToString("F0") + "dBm", IsOK);
            }
        }

        bool GetIsStateTerminating(FTS.TSingleFTSManager.TRemoteState RS)
        {
            switch (RS)
            {
                default:
                case FTS.TSingleFTSManager.TRemoteState.ERROR:
                case FTS.TSingleFTSManager.TRemoteState.NORMAL:
                    return false;
                case FTS.TSingleFTSManager.TRemoteState.TERMINATING_GEOFENCE:
                case FTS.TSingleFTSManager.TRemoteState.TERMINATING_MANUAL:
                    return true;
            }
        }

        FTS.TSingleFTSManager.TRemoteState GetRemoteState()
        {
            var NewState = Manager.GetRemoteState();

            if (GetIsStateTerminating(NewState))
            {
                if (!_HasShownTermWindowBefore)
                {
                    _HasShownTermWindowBefore = true;
                    System.Windows.Forms.MessageBox.Show(GetNonNullName() + " has been terminated!");
                }
            }
            else if (NewState == FTS.TSingleFTSManager.TRemoteState.NORMAL)
            {
                _HasShownTermWindowBefore = false;
            }

            return NewState;
        }

        void SetLabelText(Label L, string Text, FTS.TSingleFTSManager.THealthLevel HL)
        {
            L.Text = Text;
            switch (HL)
            {
                case FTS.TSingleFTSManager.THealthLevel.BAD:
                    L.ForeColor = Color.Red;
                    break;
                default:
                case FTS.TSingleFTSManager.THealthLevel.WARNING:
                    L.ForeColor = Color.Orange;
                    break;
                case FTS.TSingleFTSManager.THealthLevel.GOOD:
                    L.ForeColor = Color.Green;
                    break;
            }
        }

        void SetLabelText(Label L, string Text, bool IsOK)
        {
            SetLabelText(L, Text, IsOK ? FTS.TSingleFTSManager.THealthLevel.GOOD : FTS.TSingleFTSManager.THealthLevel.BAD);
        }

        void UpdateLinkStatus()
        {
            float LinkStatus = Manager.GetLinkStatus();
            bool IsOK = LinkStatus >= 50;

            SetLabelText(lblFTSLinkStatus, LinkStatus.ToString("F0") + "%", IsOK);
        }

        void UpdateFTSHealth()
        {
            bool Health = Manager.GetFTSHealth();
            SetLabelText(lblFTSTermHealth, Health ? "OK" : "Failure", Health);
        }

        void UpdateFenceLoadState()
        {
            bool Loaded = Manager.GetGeofenceLoaded();
            SetLabelText(lblFTSFenceLoadState, Loaded ? "Loaded" : "None", Loaded);
        }

        void UpdateFenceEnabledState()
        {
            bool Enabled = Manager.GetGeofenceEnabled();
            SetLabelText(lblFTSFenceEnabled, Enabled ? "Enabled" : "Disabled", Enabled);
        }

        void UpdateFTSTermState()
        {
            var State = GetRemoteState();
            bool IsOK = false;
            switch (State)
            {
                default:
                case FTS.TSingleFTSManager.TRemoteState.ERROR:
                case FTS.TSingleFTSManager.TRemoteState.TERMINATING_GEOFENCE:
                case FTS.TSingleFTSManager.TRemoteState.TERMINATING_MANUAL:
                    IsOK = false;
                    break;
                case FTS.TSingleFTSManager.TRemoteState.NORMAL:
                    IsOK =  true;
                    break;
            }

            SetLabelText(lblFTSTermState, Manager.GetRemoteStateDescription(State), IsOK);
        }

        void UpdateGPSGroup(TGPSGroup Group, FTS.TSingleFTSManager.TGPSStatus Status)
        {
            SetLabelText(Group.lblDetected, Status.IsDetected ? "Yes" : "No", Status.IsDetected);
            bool IsOK = (Status.QTYSats >= 4) && (Status.HDOP <= 10);
            string HDOPString;
            if (Status.QTYSats <= 0)
            {
                HDOPString = "--";
            }
            else
            {
                HDOPString = Status.HDOP.ToString("F2");
            }
            SetLabelText(Group.lblSatCountAndHDOP,
                Status.QTYSats.ToString() + " / " + HDOPString, 
                IsOK);
        }

        void UpdateAFSState()
        {
            string Text = Manager.GetAFSStateText();
            SetLabelText(lblFTSAFSState, Text, FTS.TSingleFTSManager.AFSStateTextToHealthLevel(Text));
        }

        public void UpdateGUI()
        {
            btnFTSManualTerminate.BackColor = Color.Red;
            UpdateLinkStatus();
            SetRSSILabel(lblFTSRxRSSI, Manager.GetRxRSSI());
            SetRSSILabel(lblFTSTxRSSI, Manager.GetTxRSSI());
            UpdateFTSHealth();
            UpdateFTSTermState();
            UpdateFenceLoadState();
            UpdateFenceEnabledState();
            UpdateGPSGroup(_GPSGroup1, Manager.GetGPS1Status());
            UpdateGPSGroup(_GPSGroup2, Manager.GetGPS2Status());
            UpdateAFSState();

            string Name = GetName();
            if (Name != null)
            {
                GB.Text = Name;
                GB.ForeColor = Color.White;
            }
        }

        string GetName()
        {
            MissionPlanner.Controls.ConnectionControl.port_sysid PSI;
            PSI.compid = this.Manager.MS.compid;
            PSI.sysid = this.Manager.MS.sysid;
            PSI.port = MissionPlanner.MainV2.comPort;
            return MissionPlanner.Controls.ConnectionControl.GetConnectionDescription(PSI);
        }

        string GetNonNullName()
        {
            var Name = GetName();
            if (Name == null)
            {
                Name = "this aircraft";
            }
            return Name;
        }

        private void BtnFTSManualTerminate_Click(object sender, EventArgs e)
        {
            var Name = GetNonNullName();

            if (System.Windows.Forms.MessageBox.Show(
                "Do you really want to DESTROY " + Name + " by manually terminating the flight?", "Destory aircraft?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Manager.TerminateFlight();
            }
        }

        private void LblFTSTermState_Click(object sender, EventArgs e)
        {

        }

        public class TGPSGroup
        {
            public readonly Label lblDetected;
            public readonly Label lblSatCountAndHDOP;

            public TGPSGroup(Label lblDetected, Label lblSatCountAndHDOP)
            {
                this.lblDetected = lblDetected;
                this.lblSatCountAndHDOP = lblSatCountAndHDOP;
            }
        }
    }
}
