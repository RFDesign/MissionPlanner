using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class AF3EndpointInfo : UserControl
    {
        private int escLabelRowCount = 1;
        private int escLabelRowColumn = 0;

        private Dictionary<uint, AF3StatusLabels> escLabelCollection = new Dictionary<uint, AF3StatusLabels>();
        public AF3EndpointInfo()
        {
            InitializeComponent();

            lb1.BackColor = lb2.BackColor = lb3.BackColor = lb4.BackColor =
                lb5.BackColor = lb6.BackColor = lb7.BackColor = Color.FromArgb(0x33,0x33,0x33);

            lb1.Margin = lb2.Margin = lb3.Margin = lb4.Margin = lb5.Margin =
                lb6.Margin = lb7.Margin = new Padding(0);
        }

        public void UpdateItem(MissionPlanner.Utilities.AF3EndPoint item)
        {
            if (item != null)
            {

                if ((!escLabelCollection.ContainsKey(item.esc_index)) && escLabelRowCount < 10)
                {
                    escLabelCollection.Add(item.esc_index, new AF3StatusLabels(item.esc_index));
                    int row = escLabelRowColumn;

                    foreach (var label in escLabelCollection[item.esc_index].labels)
                    {
                        tableLayoutPanel1.Controls.Add(label, row, escLabelRowCount);
                        row++;
                    }

                    escLabelRowCount++;
                }

                escLabelCollection[item.esc_index].update(item);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class AF3StatusLabels
    {

        enum lbIndex
        {
            ESC_N = 0,
            RPM = 1,
            VOLT_A = 2,
            VOLT_B = 3,
            CURR_A = 4,
            CURR_B = 5,
            INFO = 6,
            LAST
        }

        public List<System.Windows.Forms.Label> labels = new List<System.Windows.Forms.Label>();
        private List<String> errorMessages = new List<String>();
        private const float lowerVoltageThres = 6800f;
        private const float midVoltageThres = 7200f;
        private const float higherCurrentThres = 2000f;
        private const float midCurrentThres = 1000f;
        private Color badColor = Color.Red;
        private Color attColor = Color.Orange;
        private Color okColor = Color.White;
        private Color goodBgColor;
        private bool first = true;

        private void changeLabelsBackground(Color bgColor)
        {
            foreach (Label lb in labels)
            {
                lb.BackColor = bgColor;
            }
        }
        private void changeLabelsForecolor(Color fgColor)
        {
            foreach (Label lb in labels)
            {
                lb.ForeColor = fgColor;
            }
        }

        public AF3StatusLabels(uint escindex)
        {
            for (int i = 0; i < (int)lbIndex.LAST; i++)
            {
                System.Windows.Forms.Label label = new Label();
                labels.Add(label);
            }

            foreach (Label lb in labels)
            {
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Margin = new Padding(0);
                lb.Dock = DockStyle.Fill;
            }

            labels[(int)lbIndex.INFO].TextAlign = ContentAlignment.MiddleLeft;
            labels[(int)lbIndex.ESC_N].Text = String.Format("{0}", escindex);
        }

        private void setVoltageA(float VoltA)
        {
            if (VoltA < lowerVoltageThres)
            {
                labels[(int)lbIndex.VOLT_A].ForeColor = badColor;
            }
            else if (VoltA < midVoltageThres)
            {
                labels[(int)lbIndex.VOLT_A].ForeColor = attColor;
            }
            else
            {
                labels[(int)lbIndex.VOLT_A].ForeColor = okColor;
            }

            labels[(int)lbIndex.VOLT_A].Text = String.Format("{0:N}", VoltA / 1000);
        }
        private void setVoltageB(float VoltB)
        {
            if (VoltB < lowerVoltageThres)
            {
                labels[(int)lbIndex.VOLT_B].ForeColor = badColor;
            }
            else if (VoltB < midVoltageThres)
            {
                labels[(int)lbIndex.VOLT_B].ForeColor = attColor;
            }
            else
            {
                labels[(int)lbIndex.VOLT_B].ForeColor = okColor;
            }

            labels[(int)lbIndex.VOLT_B].Text = String.Format("{0:N}", VoltB / 1000);

        }

        private void setCurrentA(float CurrA)
        {
            if (CurrA > higherCurrentThres)
            {
                labels[(int)lbIndex.CURR_A].ForeColor = badColor;
            }
            else if (CurrA > midCurrentThres)
            {
                labels[(int)lbIndex.CURR_A].ForeColor = attColor;
            }
            else
            {
                labels[(int)lbIndex.CURR_A].ForeColor = okColor;
            }

            labels[(int)lbIndex.CURR_A].Text = String.Format("{0:N}", CurrA / 1000);
        }

        private void setCurrentB(float CurrB)
        {
            if (CurrB > higherCurrentThres)
            {
                labels[(int)lbIndex.CURR_B].ForeColor = badColor;
            }
            else if (CurrB > midCurrentThres)
            {
                labels[(int)lbIndex.CURR_B].ForeColor = attColor;
            }
            else
            {
                labels[(int)lbIndex.CURR_B].ForeColor = okColor;
            }

            labels[(int)lbIndex.CURR_B].Text = String.Format("{0:N}", CurrB / 1000);
        }

        private void setEscIndex(uint Index)
        {
            labels[(int)lbIndex.ESC_N].Text = String.Format("{0}", Index);
        }
        private void setRpm(int Rpm)
        {
            labels[(int)lbIndex.RPM].Text = String.Format("{0:D}", Rpm);
        }
        public void update(AF3EndPoint endPoint)
        {
            setRpm(endPoint.rpm);
            setVoltageA(endPoint.voltageA);
            setVoltageB(endPoint.voltageB);
            setCurrentA(endPoint.currentA);
            setCurrentB(endPoint.currentB);

            if (first)
            {
                goodBgColor = labels[(int)lbIndex.ESC_N].BackColor;
                first = false;
                return;
            }

            // Check time since last time message was received
            var elapsed = endPoint.isDataStale();
            var busError = endPoint.isBusMissing();

            if (elapsed)
            {
                labels[(int)lbIndex.INFO].Text = String.Format("Endpoint not communicating for {0:0.00} seconds", endPoint.elapsed/1000);
                changeLabelsBackground(badColor);
                changeLabelsForecolor(Color.White);
            }
            else if (busError != 0)
            {
                int err = (int)busError;
                int bus0Error = err & 1;
                int bus1Error = (err & 2) >> 1;
                int bus2Error = (err & 4) >> 2;
                int sumErrors = bus0Error + bus1Error + bus2Error;

                if (sumErrors > 1)
                {
                    labels[(int)lbIndex.INFO].Text = String.Format("Endpoint not communicating in buses: %s%s%s",
                        bus0Error > 0 ? "1 and " : "",
                        bus1Error > 0 ? "2" : "",
                        (bus1Error + bus2Error) > 1 ? " and " : "",
                        bus2Error > 0 ? "3" : "");
                }
                else
                {
                    labels[(int)lbIndex.INFO].Text = String.Format("Endpoint not communicating in bus %s%s%s",
                        bus0Error > 0 ? "1" : "",
                        bus1Error > 0 ? "2" : "",
                        bus2Error > 0 ? "3" : "");
                }
                changeLabelsBackground(badColor);
                changeLabelsForecolor(Color.White);

            }
            else
            {
                changeLabelsBackground(goodBgColor);
            }

        }


    }
}
