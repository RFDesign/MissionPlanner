using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews.VehicleStatusView
{
    public partial class Arbitration : UserControl
    {
        RFD.MonsterCopter.GUI.CANDiagram.TDiagram _Diagram;

        public Arbitration()
        {
            InitializeComponent();
            _Diagram = new RFD.MonsterCopter.GUI.CANDiagram.TDiagram(this.Font);
            tmrUpdate.Enabled = true;
        }

        MAVLinkInterface comPort
        {
            get
            {
                return MissionPlanner.MainV2.comPort;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Redraw();
        }

        void Redraw()
        {
            try
            {
                System.Drawing.Bitmap BMP = new Bitmap(this.Width, this.Height);
                var G = System.Drawing.Graphics.FromImage(BMP);
                _Diagram.Draw(G, comPort.MAV.cs.Arbitration.GetStatus());
                this.CreateGraphics().DrawImage(BMP, 0, 0);
            }
            catch
            {

            }
        }

        private void TmrUpdate_Tick(object sender, EventArgs e)
        {
            Redraw();
        }
    }
}
