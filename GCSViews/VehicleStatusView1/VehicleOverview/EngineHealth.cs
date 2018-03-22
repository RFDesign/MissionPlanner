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
    public partial class EngineHealth : UserControl
    {
        RFDLib.TAlarmLevel _OverallHealth;
        string _EngineDesignator = "";

        public EngineHealth()
        {
            InitializeComponent();
            OverallHealth = RFDLib.TAlarmLevel.NONE;
        }

        public string EngineDesignator
        {
            get
            {
                return _EngineDesignator;
            }
            set
            {
                _EngineDesignator = value;
                lblName.Text = "Engine " + _EngineDesignator;
            }
        }


        public RFDLib.TAlarmLevel OverallHealth
        {
            get
            {
                return _OverallHealth;
            }
            set
            {
                _OverallHealth = value;
                switch (_OverallHealth)
                {
                    case RFDLib.TAlarmLevel.NONE:
                        BackColor = Color.Green;
                        break;
                    case RFDLib.TAlarmLevel.WARNING:
                        BackColor = Color.Orange;
                        break;
                    case RFDLib.TAlarmLevel.ALERT:
                        BackColor = Color.Red;
                        break;
                }
            }
        }
    }
}
