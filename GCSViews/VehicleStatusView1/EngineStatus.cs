using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MissionPlanner.GCSViews.VehicleStatusView
{
    public partial class EngineStatus : UserControl
    {
        //public MAVLinkInterface comPort;
        const uint SELECTED_ENGINE_NONE = 1000;
        uint _SelectedEngine = SELECTED_ENGINE_NONE;

        public EngineStatus()
        {
            InitializeComponent();
            tmrUpdate.Enabled = true;
        }

        MAVLinkInterface comPort
        {
            get
            {
                return MissionPlanner.MainV2.comPort;
            }
        }

        void UpdateEngineSelectComboBox()
        {
            cbEngineName.Items.Clear();
            bool Selected = false;

            if (comPort != null)
            {
                foreach (var i in comPort.MAV.cs.EngineStatus.GetEngineNumbers())
                {
                    CBWrapper CBW = new CBWrapper(i);
                    cbEngineName.Items.Add(CBW);
                    if (i == _SelectedEngine)
                    {
                        cbEngineName.SelectedItem = CBW;
                        Selected = true;
                    }
                }
            }

            if (!Selected)
            {
                _SelectedEngine = SELECTED_ENGINE_NONE;
            }
        }

        /// <summary>
        /// Returns engine status for the selected engine or null if nothing selected.
        /// </summary>
        /// <returns></returns>
        RFD.EngineMonitor.TEngineStatus GetEngineStatusForSelected()
        {
            if (comPort == null)
            {
                return null;
            }
            else
            {
                return comPort.MAV.cs.EngineStatus.GetEngineStatus(_SelectedEngine);
            }
        }

        /// <summary>
        /// Handles a change of the engine selection combo box.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbEngineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEngineName.SelectedItem == null)
            {
                _SelectedEngine = SELECTED_ENGINE_NONE;
            }
            else
            {
                _SelectedEngine = ((CBWrapper)cbEngineName.SelectedItem).EngineNumber;
            }
        }

        /// <summary>
        /// Add an item to the list view.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="LVIW"></param>
        void AddItem(string Key, string Value, LVIWrapper LVIW)
        {
            ListViewItem LVI = new ListViewItem(Key);
            LVI.Tag = LVIW;
            LVI.SubItems.Add(Value);
            lv.Items.Add(LVI);
        }

        /// <summary>
        /// Converts the given seconds to hours, minutes and seconds as a string.  
        /// </summary>
        /// <param name="Seconds">The total nuber of seconds.</param>
        /// <returns>The time in hours, minutes and seconds.  Never null.</returns>
        string SecondsToHMS(UInt32 Seconds)
        {
            UInt32 H = Seconds / 60 / 60;
            Seconds -= H * 60 * 60;
            UInt32 M = Seconds / 60;
            Seconds -= M * 60;
            return H.ToString() + "h" + M.ToString() + "m" + Seconds.ToString() + "s";
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateEngineSelectComboBox();
            RFD.EngineMonitor.TEngineStatus ES = GetEngineStatusForSelected();

            lv.Items.Clear();

            if (ES != null)
            {
                if (ES.Data.Valid)
                {
                    AddItem("Engine Temp.", ES.Data.Value.ICEEngDegC.ToString("F1") + "°C", null);
                    AddItem("Exhaust Temp.", ES.Data.Value.ICEExhDegC.ToString("F1") + "°C", null);
                    AddItem("ICE servo", ES.Data.Value.ICEMotServoUS.ToString() + "us", null);
                    AddItem("DC servo", ES.Data.Value.DCMotServoUS.ToString() + "us", null);
                    AddItem("Speed", ES.Data.Value.ICERPM.ToString("F0") + "rpm", null);
                    AddItem("Health", (ES.Data.Value.Health / 655).ToString("F0") + "%", null);
                    AddItem("Service", SecondsToHMS(ES.Data.Value.ServiceSecs), null);
                }
                if (ES.Status.Valid)
                {
                    AddItem("RPM history", "[Graph]",
                        new IntArrayLVIWrapper(ES.Status.Value.ICE1000RPM, 0,
                        ES.Status.Value.ICE1000RPM.Count() * 1000, "RPM"));
                    AddItem("Engine temp history", "[Graph]",
                        new IntArrayLVIWrapper(ES.Status.Value.ICEEng10degC, 0,
                        ES.Status.Value.ICEEng10degC.Count() * 10, "Engine temp history"));
                }
                if (ES.ExhTemp1.Valid && ES.ExhTemp2.Valid)
                {
                    var Temp = RFDLib.Array.Concat(ES.ExhTemp1.Value.ICEExh10degC, 
                        ES.ExhTemp2.Value.ICEExh10degC);

                    AddItem("Exhaust temp history", "[Graph]",
                        new IntArrayLVIWrapper(Temp, 0, 
                        Temp.Length * 10, "Exhaust temp history"));
                }
            }
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count != 0)
            {
                var x = lv.SelectedItems[0];
                if (x.Tag != null)
                {
                    var y = (LVIWrapper)x.Tag;
                    y.UpdateChart(cht);
                }
            }
        }

        class CBWrapper
        {
            uint _EngineNumber;

            public CBWrapper(uint EngineNumber)
            {
                _EngineNumber = EngineNumber;
            }

            public uint EngineNumber
            {
                get
                {
                    return _EngineNumber;
                }
            }

            public override string ToString()
            {
                return _EngineNumber.ToString();
            }
        }

        abstract class LVIWrapper
        {
            public abstract void UpdateChart(System.Windows.Forms.DataVisualization.Charting.Chart C);

            protected void UpdateChart(System.Windows.Forms.DataVisualization.Charting.Chart C,
                UInt32[] Bins, float Lower, float Upper, string Name)
            {
                C.Series.Clear();
                var x = new System.Windows.Forms.DataVisualization.Charting.Series(Name);
                float Step = (Upper - Lower) / (Bins.Length - 1);
                foreach (var i in Bins)
                {
                    x.Points.AddXY(Lower, i);
                    Lower += Step;
                }
                C.Series.Add(x);
            }
        }

        class IntArrayLVIWrapper : LVIWrapper
        {
            UInt32[] _Bins;
            float _Lower;
            float _Upper;
            string _Name;

            public IntArrayLVIWrapper(UInt32[] Bins, float Lower, float Upper, string Name)
            {
                _Bins = Bins;
                _Lower = Lower;
                _Upper = Upper;
                _Name = Name;
            }

            public override void UpdateChart(Chart C)
            {
                base.UpdateChart(C, _Bins, _Lower, _Upper, _Name);
            }
        }
    }
}
