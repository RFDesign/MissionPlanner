using Microsoft.Scripting.Utils;
using MissionPlanner.Utilities;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace MissionPlanner.Controls
{
    public class CameraFeedback : Form
    {
        private Timer timer1;
        private IContainer components;
        PacketInspector<MAVLink.MAVLinkMessage> mavi = new PacketInspector<MAVLink.MAVLinkMessage>();
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel8;
        private System.Windows.Forms.Label lbSecTrigger;
        private System.Windows.Forms.Label label16;
        private Panel panel7;
        private System.Windows.Forms.Label lbDistTrigger;
        private System.Windows.Forms.Label label14;
        private Panel panel6;
        private System.Windows.Forms.Label lbCrossTrackError;
        private System.Windows.Forms.Label label12;
        private Panel panel5;
        private System.Windows.Forms.Label lbAltError;
        private System.Windows.Forms.Label label10;
        private Panel panel4;
        private System.Windows.Forms.Label lbDistWp;
        private System.Windows.Forms.Label label8;
        private Panel panel3;
        private System.Windows.Forms.Label lbNavBearing;
        private System.Windows.Forms.Label label6;
        private Panel panel2;
        private System.Windows.Forms.Label lbGroundCourse;
        private System.Windows.Forms.Label label4;
        private Panel panel1;
        private System.Windows.Forms.Label lbNextWp;
        private System.Windows.Forms.Label label1;
        private MAVLinkInterface mav;

        public CameraFeedback(MAVLinkInterface mav)
        {
            InitializeComponent();

            this.mav = mav;

            mav.OnPacketReceived += MavOnOnPacketReceived;

            timer1.Tick += (sender, args) => Update();

            timer1.Start();

            ThemeManager.ApplyThemeTo(this);
        }

        private void MavOnOnPacketReceived(object o, MAVLink.MAVLinkMessage linkMessage)
        {
            mavi.Add(linkMessage.sysid, linkMessage.compid, linkMessage.msgid, linkMessage, linkMessage.Length);
        }

        private double lastlat = 0;
        private double lastlng = 0;

        public double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            const double radius_of_earth = 6378100.0;

            double dLat1InRad = lat1 * (Math.PI / 180);
            double dLong1InRad = lng1 * (Math.PI / 180);
            double dLat2InRad = lat2 * (Math.PI / 180);
            double dLong2InRad = lng2 * (Math.PI / 180);
            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;
            double a = Math.Pow(Math.Sin(dLatitude / 2), 2) + Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) * Math.Pow(Math.Sin(dLongitude / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius_of_earth * c;
        }

        DateTime lastShotTimestamp;
        Int64 lastShotUsec = 0;

        public new void Update()
        {

            bool added = false;

            foreach (var mavLinkMessage in mavi.GetPacketMessages())
            {
                if ((mavLinkMessage.msgid != (uint)MAVLink.MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT) &&
                    (mavLinkMessage.msgid != (uint)MAVLink.MAVLINK_MSG_ID.CAMERA_FEEDBACK))
                {
                    continue;
                }

                var minfo = MAVLink.MAVLINK_MESSAGE_INFOS.GetMessageInfo(mavLinkMessage.msgid);
                if (minfo.@type == null)
                    continue;               

                foreach (var field in minfo.type.GetFields())
                {
                    object value = field.GetValue(mavLinkMessage.data);

                    if (field.Name == "time_unix_usec")
                    {
                        DateTime date1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        try
                        {
                            value = date1.AddMilliseconds((ulong)value / 1000);
                        }
                        catch
                        {
                        }
                    }

                    if (mavLinkMessage.msgid == (uint)MAVLink.MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT)
                    {
                        if (field.Name == "nav_bearing")
                        {
                            int navBearing = Convert.ToInt32(value);
                            if (navBearing < 0) navBearing = 360 + navBearing;
                            lbNavBearing.Text = navBearing.ToString();
                        }

                        if (field.Name == "alt_error") lbAltError.Text = String.Format("{0:N2}", value);

                        if (field.Name == "wp_dist") lbDistWp.Text = String.Format("{0:N2}", value);
                    }

                    if (mavLinkMessage.msgid == (uint)MAVLink.MAVLINK_MSG_ID.CAMERA_FEEDBACK)
                    {
                        if (field.Name == "time_usec")
                        {
                            Int64 timestamp = Convert.ToInt64(value);

                            if (timestamp != lastShotUsec)
                            {
                                var elapsed = DateTime.Now - lastShotTimestamp;
                                lastShotTimestamp = DateTime.Now;
                                lastShotUsec = timestamp;
                            }
                        }                            
                    }

                    if (field.FieldType.IsArray)
                    {
                        var subtype = value.GetType();

                        var value2 = (Array) value;

                        if (field.Name == "param_id" || field.Name == "text" || field.Name == "model_name" ||
                            field.Name == "vendor_name" || field.Name == "uri" ||
                            field.Name == "cam_definition_uri") // param_value
                        {
                            value = Encoding.ASCII.GetString((byte[]) value2);
                        }
                        else
                        {
                            value = value2.Cast<object>().Aggregate((a, b) => a + "," + b);
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbSecTrigger = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbDistTrigger = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbCrossTrackError = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbAltError = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbDistWp = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbNavBearing = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbGroundCourse = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbNextWp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Controls.Add(this.panel8, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(955, 416);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lbSecTrigger);
            this.panel8.Controls.Add(this.label16);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(321, 279);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(312, 134);
            this.panel8.TabIndex = 7;
            // 
            // lbSecTrigger
            // 
            this.lbSecTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSecTrigger.AutoSize = true;
            this.lbSecTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbSecTrigger.Location = new System.Drawing.Point(6, 50);
            this.lbSecTrigger.Name = "lbSecTrigger";
            this.lbSecTrigger.Size = new System.Drawing.Size(53, 73);
            this.lbSecTrigger.TabIndex = 1;
            this.lbSecTrigger.Text = "-";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label16.Location = new System.Drawing.Point(10, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(243, 31);
            this.label16.TabIndex = 0;
            this.label16.Text = "Seconds to Trigger";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lbDistTrigger);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 279);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(312, 134);
            this.panel7.TabIndex = 6;
            // 
            // lbDistTrigger
            // 
            this.lbDistTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDistTrigger.AutoSize = true;
            this.lbDistTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbDistTrigger.Location = new System.Drawing.Point(6, 50);
            this.lbDistTrigger.Name = "lbDistTrigger";
            this.lbDistTrigger.Size = new System.Drawing.Size(53, 73);
            this.lbDistTrigger.TabIndex = 1;
            this.lbDistTrigger.Text = "-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label14.Location = new System.Drawing.Point(10, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(214, 31);
            this.label14.TabIndex = 0;
            this.label14.Text = "Distance Trigger";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lbCrossTrackError);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(639, 141);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(313, 132);
            this.panel6.TabIndex = 5;
            // 
            // lbCrossTrackError
            // 
            this.lbCrossTrackError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCrossTrackError.AutoSize = true;
            this.lbCrossTrackError.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbCrossTrackError.Location = new System.Drawing.Point(6, 50);
            this.lbCrossTrackError.Name = "lbCrossTrackError";
            this.lbCrossTrackError.Size = new System.Drawing.Size(53, 73);
            this.lbCrossTrackError.TabIndex = 1;
            this.lbCrossTrackError.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label12.Location = new System.Drawing.Point(10, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(300, 31);
            this.label12.TabIndex = 0;
            this.label12.Text = "Cross-track Error (XTE)";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbAltError);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(321, 141);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(312, 132);
            this.panel5.TabIndex = 4;
            // 
            // lbAltError
            // 
            this.lbAltError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAltError.AutoSize = true;
            this.lbAltError.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbAltError.Location = new System.Drawing.Point(6, 50);
            this.lbAltError.Name = "lbAltError";
            this.lbAltError.Size = new System.Drawing.Size(53, 73);
            this.lbAltError.TabIndex = 1;
            this.lbAltError.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label10.Location = new System.Drawing.Point(10, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(172, 31);
            this.label10.TabIndex = 0;
            this.label10.Text = "Altitude Error";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbDistWp);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 141);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(312, 132);
            this.panel4.TabIndex = 3;
            // 
            // lbDistWp
            // 
            this.lbDistWp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDistWp.AutoSize = true;
            this.lbDistWp.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbDistWp.Location = new System.Drawing.Point(6, 50);
            this.lbDistWp.Name = "lbDistWp";
            this.lbDistWp.Size = new System.Drawing.Size(53, 73);
            this.lbDistWp.TabIndex = 1;
            this.lbDistWp.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label8.Location = new System.Drawing.Point(10, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(241, 31);
            this.label8.TabIndex = 0;
            this.label8.Text = "Distance Waypoint";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbNavBearing);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(639, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(313, 132);
            this.panel3.TabIndex = 2;
            // 
            // lbNavBearing
            // 
            this.lbNavBearing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNavBearing.AutoSize = true;
            this.lbNavBearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbNavBearing.Location = new System.Drawing.Point(6, 50);
            this.lbNavBearing.Name = "lbNavBearing";
            this.lbNavBearing.Size = new System.Drawing.Size(53, 73);
            this.lbNavBearing.TabIndex = 1;
            this.lbNavBearing.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "Nav Bearing";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbGroundCourse);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(321, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 132);
            this.panel2.TabIndex = 1;
            // 
            // lbGroundCourse
            // 
            this.lbGroundCourse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroundCourse.AutoSize = true;
            this.lbGroundCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbGroundCourse.Location = new System.Drawing.Point(6, 50);
            this.lbGroundCourse.Name = "lbGroundCourse";
            this.lbGroundCourse.Size = new System.Drawing.Size(53, 73);
            this.lbGroundCourse.TabIndex = 1;
            this.lbGroundCourse.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "GND Course/TRK";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbNextWp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 132);
            this.panel1.TabIndex = 0;
            // 
            // lbNextWp
            // 
            this.lbNextWp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNextWp.AutoSize = true;
            this.lbNextWp.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F);
            this.lbNextWp.Location = new System.Drawing.Point(6, 50);
            this.lbNextWp.Name = "lbNextWp";
            this.lbNextWp.Size = new System.Drawing.Size(53, 73);
            this.lbNextWp.TabIndex = 1;
            this.lbNextWp.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Next Waypoint";
            // 
            // CameraFeedback
            // 
            this.ClientSize = new System.Drawing.Size(955, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CameraFeedback";
            this.Text = "Camera Feedback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraFeedback_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void CameraFeedback_FormClosing(object sender, FormClosingEventArgs e)
        {
            mav.OnPacketReceived -= MavOnOnPacketReceived;
            mav.OnPacketSent -= MavOnOnPacketReceived;

            timer1.Stop();
        }

        private (string msgid, string name) selectedmsgid;

        private void timer1_Tick(object sender, EventArgs e)
        {

            // Refresh info from current state
            lbNextWp.Text = MainV2.comPort.MAV.cs.wpno.ToString();
            lbCrossTrackError.Text = String.Format("{0:N2}", MainV2.comPort.MAV.cs.xtrack_error);
            lbGroundCourse.Text = String.Format("{0:N2}", MainV2.comPort.MAV.cs.groundcourse);
            //lbSecTrigger.Text = String.Format("{0:N2}", MainV2.comPort.MAV.cs.timesincelastshot);

            // Calculated fields
            var sinceLastShot = DateTime.Now.Subtract(lastShotTimestamp).TotalSeconds;
            var untilNext = MainV2.comPort.MAV.cs.timesincelastshot - sinceLastShot;

            double tmp_dist = GetDistance(MainV2.comPort.MAV.cs.lat, MainV2.comPort.MAV.cs.lng, lastlat, lastlng);
            lastlat = MainV2.comPort.MAV.cs.lat;
            lastlng = MainV2.comPort.MAV.cs.lng;

            if ((untilNext >= 0) && (untilNext < 1000))
            {
                lbSecTrigger.Text = String.Format("{0:N2}", untilNext);
                lbDistTrigger.Text = String.Format("{0:N2}", tmp_dist * untilNext);
            }
        }
    }
}
