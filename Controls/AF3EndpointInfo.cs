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
                lb5.BackColor = lb6.BackColor = lb7.BackColor = Color.FromArgb(0x55,0x55,0x55);

            lb1.Margin = lb2.Margin = lb3.Margin = lb4.Margin = lb5.Margin =
                lb6.Margin = lb7.Margin = new Padding(0);
        }

        public void UpdateItem(MissionPlanner.Utilities.AF3EndPoint item, ListView lsError)
        {
            if (item != null)
            {

                if ((!escLabelCollection.ContainsKey(item.esc_index)) && escLabelRowCount < 10)
                {
                    escLabelCollection.Add(item.esc_index, new AF3StatusLabels(item.esc_index, icons, lsError));
                    int row = escLabelRowColumn;

                    foreach (var label in escLabelCollection[item.esc_index].lineControls)
                    {
                        tableLayoutPanel1.Controls.Add(label, row, escLabelRowCount);
                        row++;
                    }

                    escLabelRowCount++;
                }

                escLabelCollection[item.esc_index].update(item);

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

    public class errorRecord
    {
        public DateTime timestamp;
        public opState state;
        public String message;
        public int failedBuses;
        public ListViewItem lsItem;
        public enum opState
        {
            NORMAL = 0,
            FULL_FAILURE,
            BUS_ERROR,
        }

        public errorRecord(opState st, String msg, int failBusesMask)
        {
            timestamp = DateTime.Now;
            message = msg;
            state = st;
            failedBuses = failBusesMask;
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
        private const float lowerVoltageThres = 6800f;
        private const float midVoltageThres = 7200f;
        private const float higherCurrentThres = 8000f;
        private const float midCurrentThres = 7000f;
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

        public AF3StatusLabels(uint escindex, ImageList imgList, ListView lsError)
        {
            escIndex = escindex;
            icons = imgList;
            listviewError = lsError;

            for (int i = 0; i < (int)lbIndex.LAST; i++)
            {
                System.Windows.Forms.Label ct = new Label(); ;

                if (i == (int)lbIndex.ICON)
                {
                    ct.Cursor = Cursors.Hand;
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
            if (VoltA < lowerVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_A].ForeColor = badColor;
            }
            else if (VoltA < midVoltageThres)
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
            if (VoltB < lowerVoltageThres)
            {
                lineControls[(int)lbIndex.VOLT_B].ForeColor = badColor;
            }
            else if (VoltB < midVoltageThres)
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
            if (CurrA > higherCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_A].ForeColor = badColor;
            }
            else if (CurrA > midCurrentThres)
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
            if (CurrB > higherCurrentThres)
            {
                lineControls[(int)lbIndex.CURR_B].ForeColor = badColor;
            }
            else if (CurrB > midCurrentThres)
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

        private String MillisToTime(double millis)
        {
            String result = "";
            double seconds = millis / 1000;
            double minutes = seconds / 60;
            double hours = minutes / 60;

            if ((int)hours > 0)
            {
                double min = (hours - ((int)hours)) * 60;
                result = String.Format("{0:0}:{1:00}'", hours, min);
            }
            else if ((int)minutes > 0)
            {
                double secs = seconds - (((int)minutes) * 60);
                result = String.Format("{0:0}' {1:00}\"", minutes, secs);
            }
            else
            {
                result = String.Format("{0:0.0} s", seconds);
            }
            return result;
        }

        private errorRecord.opState state = errorRecord.opState.NORMAL;

        private void addError(errorRecord.opState state, String message, int failedBuses)
        {

            if ((errorList.Count == 0) || 
                (errorList.Last().state != state) ||
                (errorList.Last().failedBuses != failedBuses))
            {
                errorRecord errRec = new errorRecord(state, message, failedBuses);
                errorList.Add(errRec);

                if (listviewError != null)
                {
                    string[] values = new string[] { errRec.timestamp.ToString("HH:mm:ss"),
                        escIndex.ToString(),
                        state.ToString(),
                        message };

                    ListViewItem errLine = new ListViewItem(values);
                    errRec.lsItem = listviewError.Items.Add(errLine);
                }

            }
            else if ((errorList.Last().state == state) && 
                (errorList.Last().failedBuses == failedBuses) &&
                (errorList.Last().state != errorRecord.opState.NORMAL))
            {

                errorRecord err = errorList.Last();
                err.message = message;

                if (listviewError != null)
                {
                    if (err.lsItem != null)
                    {
                        err.lsItem.SubItems[3].Text = message;
                    }

                }

            }

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
                goodBgColor = lineControls[(int)lbIndex.ESC_N].BackColor;
                first = false;
                return;
            }

            if (endPoint.esc_index == 1)
                Console.WriteLine("stop here");

            // Check time since last time message was received
            var elapsed = endPoint.isDataStale();
            var busError = endPoint.isBusMissing();

            if (elapsed)
            {
                String errorMessage = String.Format("Endpoint not communicating for {0}", MillisToTime(endPoint.elapsed));
                lineControls[(int)lbIndex.INFO].Text = errorMessage;
                changeLabelsBackground(badColor);
                changeLabelsForecolor(Color.White);
                (lineControls[(int)lbIndex.ICON] as Label).Image = icons.Images[2];

                addError(errorRecord.opState.FULL_FAILURE, errorMessage, 7); // 7 corresponds to all buses failing
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
                    String errorMessage = String.Format("Endpoint not communicating in buses: {0}{1}{2}{3}",
                        bus0Error > 0 ? "1 and " : "",
                        bus1Error > 0 ? "2" : "",
                        (bus1Error + bus2Error) > 1 ? " and " : "",
                        bus2Error > 0 ? "3" : "");

                    lineControls[(int)lbIndex.INFO].Text = errorMessage;
                    addError(errorRecord.opState.BUS_ERROR, errorMessage, err);
                }
                else
                {
                    String errorMessage = String.Format("Endpoint not communicating in bus {0}{1}{2}",
                        bus0Error > 0 ? "1" : "",
                        bus1Error > 0 ? "2" : "",
                        bus2Error > 0 ? "3" : "");

                    lineControls[(int)lbIndex.INFO].Text = errorMessage;
                    addError(errorRecord.opState.BUS_ERROR, errorMessage, err);

                }
                changeLabelsBackground(attColor);
                changeLabelsForecolor(Color.White);
                (lineControls[(int)lbIndex.ICON] as Label).Image = icons.Images[1];

            }
            else
            {
                lineControls[(int)lbIndex.INFO].Text = "";
                changeLabelsBackground(goodBgColor);
                addError(errorRecord.opState.NORMAL, "Endpoint operating normally", 0);
            }

        }


    }
}
