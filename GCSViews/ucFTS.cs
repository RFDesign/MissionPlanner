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

        public ucFTS(FTS.TSingleFTSManager Manager)
        {
            InitializeComponent();
            this.Manager = Manager;
        }

        void SetRSSILabel(System.Windows.Forms.Label L, float RSSI)
        {
            if (float.IsNaN(RSSI))
            {
                L.Text = "--";
            }
            else
            {
                L.Text = RSSI.ToString("F0") + "dBm";
            }
        }

        public void Update()
        {
            btnFTSManualTerminate.BackColor = Color.Red;
            lblFTSLinkStatus.Text = Manager.GetLinkStatus().ToString("F0") + "%";
            SetRSSILabel(lblFTSRxRSSI, Manager.GetRxRSSI());
            SetRSSILabel(lblFTSTxRSSI, Manager.GetTxRSSI());
            lblFTSTermHealth.Text = Manager.GetFTSHealth() ? "OK" : "Failure";
            lblFTSTermState.Text = Manager.GetRemoteStateDescription(Manager.GetRemoteState());

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

        private void BtnFTSManualTerminate_Click(object sender, EventArgs e)
        {
            var Name = GetName();
            if (Name == null)
            {
                Name = "this aircraft";
            }

            if (System.Windows.Forms.MessageBox.Show(
                "Do you really want to DESTROY " + Name + " by manually terminating the flight?", "Destory aircraft?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Manager.TerminateFlight();
            }
        }
    }
}
