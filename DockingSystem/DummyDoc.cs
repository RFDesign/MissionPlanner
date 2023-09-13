using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using MissionPlanner;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace DockSample
{
    public partial class DummyDoc : DockContent
    {
        private List<MAVLinkInterface> mavVehicle;
        private ConcurrentQueue<MAVLink.MAVLinkMessage> msgQueue = new ConcurrentQueue<MAVLink.MAVLinkMessage>();

        public DummyDoc(List<MAVLinkInterface> mavVehicle)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            DockAreas = DockAreas.Document | DockAreas.Float;
            if (mavVehicle.Count > 0)
            {
                mavVehicle[0].OnPacketReceived += MavOnOnPacketReceived;
                lbSysId.Text = mavVehicle[0].sysidcurrent.ToString() + " " + mavVehicle[0].compidcurrent.ToString();
            }
            else
                lbSysId.Text = "No vehicle connected";

            try
            {                
                gMap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                //gMap.Position = new GMap.NET.PointLatLng(NextFloat(48.0f, 49.0f), NextFloat(2.0f, 3.0f));
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        private void MavOnOnPacketReceived(object o, MAVLink.MAVLinkMessage linkMessage)
        {
            msgQueue.Enqueue(linkMessage);
            //mavi.Add(linkMessage.sysid, linkMessage.compid, linkMessage.msgid, linkMessage, linkMessage.Length);
        }

        private float NextFloat(float min, float max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        private string m_fileName = string.Empty;
        public string FileName
        {
            get	{	return m_fileName;	}
            set
            {
                if (value != string.Empty)
                {
                    Stream s = new FileStream(value, FileMode.Open);

                    FileInfo efInfo = new FileInfo(value);

                    string fext = efInfo.Extension.ToUpper();
                    s.Close();
                }

                m_fileName = value;
                this.ToolTipText = value;
            }
        }

        // workaround of RichTextbox control's bug:
        // If load file before the control showed, all the text format will be lost
        // re-load the file after it get showed.
        private bool m_resetText = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (m_resetText)
            {
                m_resetText = false;
                FileName = FileName;
            }
        }

        protected override string GetPersistString()
        {
            // Add extra information into the persist string for this document
            // so that it is available when deserialized.
            return GetType().ToString() + "," + FileName + "," + Text;
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("This is to demostrate menu item has been successfully merged into the main form. Form Text=" + Text);
        }

        private void menuItemCheckTest_Click(object sender, System.EventArgs e)
        {
            menuItemCheckTest.Checked = !menuItemCheckTest.Checked;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged (e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MAVLink.MAVLinkMessage msg;
            while (msgQueue.TryDequeue(out msg))
            {
                if (msg.msgid == 33)
                {
                    var pos = (MAVLink.mavlink_global_position_int_t)msg.data;
                    gMap.Position = new GMap.NET.PointLatLng(pos.lat, pos.lon);
                }
            }
        }
    }
}