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
        TAlarmLevel _OverallHealth;
        string _EngineDesignator = "";

        public EngineHealth()
        {
            InitializeComponent();
            OverallHealth = TAlarmLevel.NONE;
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


        public TAlarmLevel OverallHealth
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
                    case TAlarmLevel.NONE:
                        BackColor = Color.Green;
                        break;
                    case TAlarmLevel.WARNING:
                        BackColor = Color.Orange;
                        break;
                    case TAlarmLevel.ALERT:
                        BackColor = Color.Red;
                        break;
                }
            }
        }
    }

    public enum TAlarmLevel
    {
        NONE,
        WARNING,
        ALERT
    }
}
