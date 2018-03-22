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
    public partial class VehicleOverview : UserControl
    {
        public VehicleOverview()
        {
            InitializeComponent();

            //MainV2.comPort.MAV.cs.
        }

        void AddItem(string Name, string Value)
        {
            ListViewItem LVI = new ListViewItem(Name);
            LVI.SubItems.Add(Value);
            lv.Items.Add(LVI);
        }

        void Update()
        {
            lv.Items.Clear();

            AddItem("Battery Voltage (V)", MainV2.comPort.MAV.cs.battery_voltage.ToString("F2"));
            AddItem("Battery Temperature (°C)", MainV2.comPort.MAV.cs.battery_temp.ToString("F2"));
            AddItem("Battery Remaining (%)", MainV2.comPort.MAV.cs.battery_remaining.ToString("F1"));
            AddItem("HW Voltage (V)", MainV2.comPort.MAV.cs.hwvoltage.ToString("F1"));
            AddItem("Board Voltage (V)", MainV2.comPort.MAV.cs.boardvoltage.ToString("F1"));
            AddItem("Servo Voltage (V)", MainV2.comPort.MAV.cs.servovoltage.ToString("F1"));
        }
    }
}
