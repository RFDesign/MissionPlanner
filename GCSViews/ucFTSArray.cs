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
    public partial class ucFTSArray : UserControl
    {
        List<ucFTS> _FTSControls = new List<ucFTS>();

        public ucFTSArray()
        {
            InitializeComponent();
        }


        bool DoesControlsContain(MissionPlanner.MAVState MS)
        {
            foreach (var FTSC in _FTSControls)
            {
                if (FTS.Manager.AreMavStatesTheSame(MS, FTSC.Manager.MS))
                {
                    return true;
                }
            }

            return false;
        }

        bool DoesListContain(List<MissionPlanner.MAVState> L, MissionPlanner.MAVState MS)
        {
            foreach (var LMS in L)
            {
                if (FTS.Manager.AreMavStatesTheSame(LMS, MS))
                {
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
            var MavStates = FTS.Manager.GetMavStates();

            foreach (var MS in MavStates)
            {
                if (!DoesControlsContain(MS))
                {
                    ucFTS C = new ucFTS(new FTS.TSingleFTSManager(MS));
                    AddFTS(C);
                }
            }

            List<ucFTS> ToRemove = new List<ucFTS>();

            foreach (var C in _FTSControls)
            {
                if (DoesListContain(MavStates, C.Manager.MS))
                {
                    C.Update();
                }
                else
                {
                    ToRemove.Add(C);
                }
            }

            foreach (var TR in ToRemove)
            {
                RemoveFTS(TR);
            }
        }

        void AddFTS(ucFTS FTS)
        {
            FTS.Top = FTS.Height * _FTSControls.Count;
            FTS.Left = 0;

            pnlMain.Controls.Add(FTS);

            _FTSControls.Add(FTS);
        }

        void RemoveFTS(ucFTS FTS)
        {
            int StartIndex = _FTSControls.IndexOf(FTS);

            _FTSControls.Remove(FTS);

            for (int n = StartIndex; n < _FTSControls.Count; n++)
            {
                _FTSControls[n].Top -= FTS.Height;
            }

            pnlMain.Controls.Remove(FTS);
        }
    }
}
