using MissionPlanner.Utilities;
using MissionPlanner.Utilities.AF3;
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
        private int neededHeight { get { return escLabelRowCount * 20; } }

        private Dictionary<uint, AF3StatusLabels> escLabelCollection = new Dictionary<uint, AF3StatusLabels>();
        public AF3EndpointInfo()
        {
            InitializeComponent();

            lb1.BackColor = lb2.BackColor = lb3.BackColor = lb4.BackColor =
                lb5.BackColor = lb6.BackColor = lb7.BackColor = Color.FromArgb(0x55,0x55,0x55);

            lb1.Margin = lb2.Margin = lb3.Margin = lb4.Margin = lb5.Margin =
                lb6.Margin = lb7.Margin = new Padding(0);
        }

        public void UpdateItem(MissionPlanner.Utilities.AF3.EndPoint item, errorRecord error)
        {
            if (item != null)
            {

                if ((!escLabelCollection.ContainsKey(item.esc_index)) && escLabelRowCount < 10)
                {
                    escLabelCollection.Add(item.esc_index, new AF3StatusLabels(item.esc_index, icons));
                    int row = escLabelRowColumn;

                    foreach (var label in escLabelCollection[item.esc_index].lineControls)
                    {
                        tableLayoutPanel1.Controls.Add(label, row, escLabelRowCount);
                        row++;
                    }

                    escLabelRowCount++;
                }

                escLabelCollection[item.esc_index].update(item, error);

            }
        }

        public List<KeyValuePair<uint, errorRecord>> getErrors()
        {
            List<KeyValuePair<uint,errorRecord>> fullErrorList = new List<KeyValuePair<uint, errorRecord>>();

            foreach (KeyValuePair<uint, AF3StatusLabels> item in escLabelCollection)
            {
                uint escIndex = item.Key;

                foreach (errorRecord err in item.Value.errorList)
                {
                    fullErrorList.Add(new KeyValuePair<uint, errorRecord>(escIndex, err));
                }
            }

            return fullErrorList;
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
            ICON = 6,
            INFO = 7,
            LAST
        }

        public List<System.Windows.Forms.Control> lineControls = new List<System.Windows.Forms.Control>();
        public List<errorRecord> errorList = new List<errorRecord>();
        private Color badColor = Color.Red;
        private Color attColor = Color.Orange;
        private Color okColor = Color.White;
        private Color goodBgColor;
        private bool first = true;
        private System.Windows.Forms.ImageList icons;
        private ListView listviewError;
        private uint escIndex;

        private void changeLabelsBackground(Color bgColor)
        {
            foreach (Control lb in lineControls)
            {
                lb.BackColor = bgColor;
            }
        }
        private void changeLabelsForecolor(Color fgColor)
        {
            foreach (Control lb in lineControls)
            {
                lb.ForeColor = fgColor;
            }
        }

        public AF3StatusLabels(uint escindex, ImageList imgList)
        {
            escIndex = escindex;
            icons = imgList;

            for (int i = 0; i < (int)lbIndex.LAST; i++)
            {
                System.Windows.Forms.Label ct = new Label(); ;

                if (i == (int)lbIndex.ICON)
                {
                    ct.Image = icons.Images[0];
                }

                lineControls.Add(ct);
            }

            foreach (Control ct in lineControls)
            {
                if (ct is Label)
                {
                    Label lb = (ct as Label);
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    lb.Margin = new Padding(0);
                    lb.Dock = DockStyle.Fill;
                }
            }

            (lineControls[(int)lbIndex.INFO] as System.Windows.Forms.Label).TextAlign = ContentAlignment.MiddleLeft;
            (lineControls[(int)lbIndex.ESC_N] as System.Windows.Forms.Label).Text = String.Format("{0}", escindex);
        }

        private void setVoltageA(float VoltA)
        {
            if (VoltA < Constants.lowerVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_A].ForeColor = badColor;
            }
            else if (VoltA < Constants.midVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_A].ForeColor = attColor;
            }
            else
            {
                lineControls[(int)lbIndex.VOLT_A].ForeColor = okColor;
            }

            lineControls[(int)lbIndex.VOLT_A].Text = String.Format("{0:N}", VoltA / 1000);
        }
        private void setVoltageB(float VoltB)
        {
            if (VoltB < Constants.lowerVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_B].ForeColor = badColor;
            }
            else if (VoltB < Constants.midVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_B].ForeColor = attColor;
            }
            else
            {
                lineControls[(int)lbIndex.VOLT_B].ForeColor = okColor;
            }

            lineControls[(int)lbIndex.VOLT_B].Text = String.Format("{0:N}", VoltB / 1000);

        }

        private void setCurrentA(float CurrA)
        {
            if (CurrA > Constants.higherCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_A].ForeColor = badColor;
            }
            else if (CurrA > Constants.midCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_A].ForeColor = attColor;
            }
            else
            {
                lineControls[(int)lbIndex.CURR_A].ForeColor = okColor;
            }

            lineControls[(int)lbIndex.CURR_A].Text = String.Format("{0:N}", CurrA / 1000);
        }

        private void setCurrentB(float CurrB)
        {
            if (CurrB > Constants.higherCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_B].ForeColor = badColor;
            }
            else if (CurrB > Constants.midCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_B].ForeColor = attColor;
            }
            else
            {
                lineControls[(int)lbIndex.CURR_B].ForeColor = okColor;
            }

            lineControls[(int)lbIndex.CURR_B].Text = String.Format("{0:N}", CurrB / 1000);
        }

        private void setEscIndex(uint Index)
        {
            lineControls[(int)lbIndex.ESC_N].Text = String.Format("{0}", Index);
        }

        private void setRpm(int Rpm)
        {
            lineControls[(int)lbIndex.RPM].Text = String.Format("{0:D}", Rpm);
        }

        public void update(EndPoint endPoint, errorRecord error)
        {
            setRpm(endPoint.rpm);
            setVoltageA(endPoint.voltageA);
            setVoltageB(endPoint.voltageB);
            setCurrentA(endPoint.currentA);
            setCurrentB(endPoint.currentB);

            if (first)
            {
                goodBgColor = lineControls[(int)lbIndex.ESC_N].BackColor;
                first = false;
                return;
            }

            if (endPoint.esc_index == 1)
                Console.WriteLine("stop here");

            if (error == null)
            {
                lineControls[(int)lbIndex.INFO].Text = "";
                changeLabelsBackground(goodBgColor);
            }
            else
            {
                if (error.state == errorRecord.opCode.BUS_ERROR && error.failedBuses != 7)
                {
                    (lineControls[(int)lbIndex.ICON] as Label).Image = icons.Images[1];
                    changeLabelsBackground(attColor);
                }
                else if (error.state == errorRecord.opCode.BUS_ERROR && error.failedBuses == 7)
                {
                    (lineControls[(int)lbIndex.ICON] as Label).Image = icons.Images[2];
                    changeLabelsBackground(badColor);
                }

                lineControls[(int)lbIndex.INFO].Text = error.message;
                changeLabelsForecolor(Color.White);

            }

        }

    }

}
