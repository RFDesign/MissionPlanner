using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class AF3Status : Form
    {
        private Dictionary<uint, AF3StatusLabels> escLabelCollection = new Dictionary<uint, AF3StatusLabels>();
        private int escLabelRowCount = 4;

        public AF3Status()
        {
            InitializeComponent();

            Utilities.ThemeManager.ApplyThemeTo(this);

            timer1.Start();

            wBar1.BackColor = Color.White;
            wBar2.BackColor = Color.White;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void updateTelemLabel(Label lb, bool telemPresent)
        {
            if (telemPresent)
            {
                lb.Text = "TELEM";
                lb.BackColor = BackColor;
            }
            else
            {
                lb.Text = "NO TELEM";
                lb.BackColor = Color.Red;
            }
        }

        private void updateActiveRfcLabel(Label lb, bool Active)
        {
            if (Active)
            {
                lb.Text = "ACTIVE";
                lb.BackColor = Color.Green;
            }
            else
            {
                lb.Text = "";
                lb.BackColor = BackColor;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool[] telem = MainV2.comPort.MAV.cs.af3.telemRFC;

            updateTelemLabel(lbRFC1TELEM, telem[0]);
            updateTelemLabel(lbRFC2TELEM, telem[1]);
            updateTelemLabel(lbRFC3TELEM, telem[2]);

            int activeRFC = (int)MainV2.comPort.MAV.cs.af3.activeRFC;

            updateActiveRfcLabel(lbRFC1Active, (activeRFC == 0));
            updateActiveRfcLabel(lbRFC2Active, (activeRFC == 1));
            updateActiveRfcLabel(lbRFC3Active, (activeRFC == 2));

            // Update/Create ESC info labels

            int epCount = MainV2.comPort.MAV.cs.af3.getEndpointCount();

            for (int i = 0; i < epCount; i++)
            {
                var item = MainV2.comPort.MAV.cs.af3.getEndpoint(i);

                if (item != null)
                {

                    if ((!escLabelCollection.ContainsKey(item.esc_index)) && escLabelRowCount < 10)
                    {
                        escLabelCollection.Add(item.esc_index, new AF3StatusLabels(item.esc_index));

                        tableLayoutPanel1.Controls.Add(escLabelCollection[item.esc_index].lbIndex, 5, escLabelRowCount);
                        tableLayoutPanel1.Controls.Add(escLabelCollection[item.esc_index].lbRpm, 6, escLabelRowCount);
                        tableLayoutPanel1.Controls.Add(escLabelCollection[item.esc_index].lbVoltageA, 7, escLabelRowCount);
                        tableLayoutPanel1.Controls.Add(escLabelCollection[item.esc_index].lbVoltageB, 8, escLabelRowCount);

                        escLabelRowCount++;
                    }

                    escLabelCollection[item.esc_index].update(item);

                }

            }

            // restore colours
            //Utilities.ThemeManager.ApplyThemeTo(this);
        }
    }

    public class AF3StatusLabels
    {
        public System.Windows.Forms.Label lbIndex = new Label();
        public System.Windows.Forms.Label lbRpm = new Label();
        public System.Windows.Forms.Label lbVoltageA = new Label();
        public System.Windows.Forms.Label lbVoltageB = new Label();

        private const float lowerVoltageThres = 6.8f;
        private const float midVoltageThres = 7.2f;
        private Color badColor = Color.Red;
        private Color attColor = Color.Orange;
        private Color okColor = Color.White;
        private Color goodBgColor;
        private bool first = true;

        public AF3StatusLabels (uint escindex)
        {
            lbIndex.TextAlign = ContentAlignment.MiddleCenter;
            lbRpm.TextAlign = ContentAlignment.MiddleCenter;
            lbVoltageA.TextAlign = ContentAlignment.MiddleCenter;
            lbVoltageB.TextAlign = ContentAlignment.MiddleCenter;

            lbIndex.Text = String.Format("{0}", escindex);
        }

        private void setVoltageA(float VoltA)
        {
            if (VoltA < lowerVoltageThres)
            {
                lbVoltageA.ForeColor = badColor;
            }
            else if (VoltA < midVoltageThres)
            {
                lbVoltageA.ForeColor = attColor;
            }
            else
            {
                lbVoltageA.ForeColor = okColor;
            }

            lbVoltageA.Text = String.Format("{0}", VoltA);
        }
        private void setVoltageB(float VoltB)
        {
            if (VoltB < lowerVoltageThres)
            {
                lbVoltageA.ForeColor = badColor;
            }
            else if (VoltB < midVoltageThres)
            {
                lbVoltageA.ForeColor = attColor;
            }
            else
            {
                lbVoltageA.ForeColor = okColor;
                lbVoltageA.ForeColor = okColor;
            }

            lbVoltageB.Text = String.Format("{0}", VoltB);

        }
        private void setEscIndex(uint Index)
        {
            lbIndex.Text = String.Format("{0}", Index);
        }
        private void setRpm(int Rpm)
        {
            lbRpm.Text = String.Format("{0}", Rpm);
        }
        public void update(AF3EndPoint endPoint)
        {
            setRpm(endPoint.rpm);
            setVoltageA(endPoint.voltageA);
            setVoltageB(endPoint.voltageB);
            
            if (first)
            {
                goodBgColor = lbIndex.BackColor;
                first = false;
                return;
            }

            // Check time since last time message was received
            lbIndex.BackColor = (endPoint.isDataStale()) ? badColor : goodBgColor;
        }


    }
}