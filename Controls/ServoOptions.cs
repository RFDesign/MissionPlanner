using MissionPlanner.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class ServoOptions : UserControl
    {
        private static System.Collections.Generic.List<ServoOptions> OptionsList = new System.Collections.Generic.List<ServoOptions>();

        [System.ComponentModel.Browsable(true)]
        public String ChannValue
        {
            get { return TXT_rcchannel.Text; }
            set { TXT_rcchannel.Text = value; }
        }

        public ServoOptions()
        {
            InitializeComponent();

            loadSettings();

            TXT_rcchannel.BackColor = Color.Gray;

            // store list of the controls in the Class Static private that is accessable to all instances.
            //.Contains(i);
            ServoOptions.OptionsList.Add(this);
        }

        void loadSettings()
        {
            string desc = Settings.Instance["Servo" + ChannValue + "_desc"];
            string low = Settings.Instance["Servo" + ChannValue + "_low"];
            string high = Settings.Instance["Servo" + ChannValue + "_high"];

            string highdesc = Settings.Instance["Servo" + ChannValue + "_highdesc"];
            string lowdesc = Settings.Instance["Servo" + ChannValue + "_lowdesc"];

            if (!string.IsNullOrEmpty(low))
            {
                TXT_pwm_low.Text = low;
            }

            if (!string.IsNullOrEmpty(high))
            {
                TXT_pwm_high.Text = high;
            }

            if (!string.IsNullOrEmpty(desc))
            {
                TXT_rcchannel.Text = desc;
            }

            if (!string.IsNullOrEmpty(highdesc))
            {
                BUT_High.Text = highdesc;
            }

            if (!string.IsNullOrEmpty(lowdesc))
            {
                BUT_Low.Text = lowdesc;
            }
        }

        private void BUT_Low_Click(object sender, EventArgs e)
        {
            String channum = this.ChannValue; //"5"

            // first we act on the current button that hte user pressed.
            bool Error = false;

            try
            {
                int x = Int32.Parse(TXT_rcchannel.Text);
                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, int.Parse(TXT_pwm_low.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Red;
                }
                else
                {
                    CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
                    Error = true;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(Strings.CommandFailed + ex.ToString(), Strings.ERROR);
                Error = true;
            }
            // then we act on any other button in the Set with the same name to keep their state matched up.
            if (Error == false)
            {
                foreach (var so in ServoOptions.OptionsList)
                {
                    if (so.ChannValue == ChannValue)
                    {
                        so.TXT_rcchannel.BackColor = Color.Red;
                    }
                }
            }
        }

        private void BUT_High_Click(object sender, EventArgs e)
        {
            String channum = this.ChannValue; //"5"

            // first we act on the current button that hte user pressed.
            bool Error = false;

            try
            {
                int x = Int32.Parse(TXT_rcchannel.Text);

                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, int.Parse(TXT_pwm_high.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Green;
                }
                else
                {
                    CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
                    Error = true;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(Strings.CommandFailed + ex.ToString(), Strings.ERROR);
                Error = true;
            }

            // then we act on any other button in the Set with the same name to keep their state matched up.
            if (Error == false)
            {
                foreach (var so in ServoOptions.OptionsList)
                {
                    if (so.ChannValue == ChannValue)
                    {
                        so.TXT_rcchannel.BackColor = Color.Green;
                    }
                }
            }
        }

        private void BUT_Repeat_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Int32.Parse(TXT_rcchannel.Text);

                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, int.Parse(TXT_pwm_low.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Red;
                }

                Application.DoEvents();
                System.Threading.Thread.Sleep(200);

                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, int.Parse(TXT_pwm_high.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Green;
                }

                Application.DoEvents();
                System.Threading.Thread.Sleep(200);

                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, int.Parse(TXT_pwm_low.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(Strings.CommandFailed + ex.ToString(), Strings.ERROR);
            }
            // MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, int.Parse(TXT_rcchannel.Text), int.Parse(TXT_pwm_high.Text), 10, 1000, 0, 0, 0);         
        }

        private void TXT_pwm_low_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance["Servo" + ChannValue + "_low"] = TXT_pwm_low.Text;
        }

        private void TXT_pwm_high_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance["Servo" + ChannValue + "_high"] = TXT_pwm_high.Text;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control sourcectl = ((ContextMenuStrip)renameToolStripMenuItem.Owner).SourceControl;

            string desc = sourcectl.Text;
            //MissionPlanner.Controls.InputBox.Show("Description", "Enter new Description", ref desc);
            //sourcectl.Text = desc;

            if (sourcectl == BUT_High)
            {
                Settings.Instance["Servo" + ChannValue + "_highdesc"] = desc;
            }
            else if (sourcectl == BUT_Low)
            {
                Settings.Instance["Servo" + ChannValue + "_lowdesc"] = desc;
            }
            else if (sourcectl == TXT_rcchannel)
            {
                //Settings.Instance["Servo" + thisservo + "_desc"] = desc;
            }
        }

        private void But_mid_Click(object sender, EventArgs e)
        {
            String channum = this.ChannValue; //"5"

            // first we act on the current button that hte user pressed.
            bool Error = false;

            try
            {
                int x = Int32.Parse(TXT_rcchannel.Text);
                if (MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, x, (int.Parse(TXT_pwm_high.Text) - int.Parse(TXT_pwm_low.Text)) / 2 + int.Parse(TXT_pwm_low.Text), 0, 0,
                    0, 0, 0))
                {
                    TXT_rcchannel.BackColor = Color.Orange;
                }
                else
                {
                    CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
                    Error = true;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(Strings.CommandFailed + ex.ToString(), Strings.ERROR);
                Error = true;
            }

            // then we act on any other button in the Set with the same name to keep their state matched up.
            if (Error == false)
            {
                foreach (var so in ServoOptions.OptionsList)
                {
                    if (so.ChannValue == ChannValue)
                    {
                        so.TXT_rcchannel.BackColor = Color.Orange;
                    }
                }
            }
        }
    }
}