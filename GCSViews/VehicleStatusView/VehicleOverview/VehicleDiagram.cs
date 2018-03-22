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
    public partial class VehicleDiagram : UserControl
    {
        public VehicleDiagram()
        {
            InitializeComponent();

            foreach (Control C in Controls)
            {
                C.Left = (int)(1.5 * C.Left);
                C.Top = (int)(1.5 * C.Top);
            }
        }
    }
}
