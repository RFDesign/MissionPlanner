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
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MissionPlanner.Controls
{
    public class MAVLinkVideoService : Form
    {
        const int MAX_MAV_PAYLOAD = 96;
        const int BUFFER_SIZE = MAX_MAV_PAYLOAD * 100;
        const int MAX_DELAY_MS = 100;

        private IContainer components;
        private MAVLinkInterface mav;
        private Socket udpService;
        IPEndPoint ep;
        byte[] udpBuffer = new byte[BUFFER_SIZE];
        int udpBuffCount = 0;
        private Label label1;
        private Label label2;
        DateTime lastSent;
        //byte _LastSeq = 0;

        MavLinkVideoService.TTCPServer _TCPServer;

        public MAVLinkVideoService(MAVLinkInterface mav)
        {
            InitializeComponent();

            this.mav = mav;

            _TCPServer = new MavLinkVideoService.TTCPServer(7760);

            mav.OnPacketReceived += MavOnOnPacketReceived;
            lastSent = DateTime.Now;

            udpService = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            ep = new IPEndPoint(broadcast, 11000);

            ThemeManager.ApplyThemeTo(this);
        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        private void MavOnOnPacketReceived(object o, MAVLink.MAVLinkMessage linkMessage)
        {
            if (linkMessage.msgid == (uint)172)
            {
                MAVLink.mavlink_data96_t data = (MAVLink.mavlink_data96_t)linkMessage.data;

                if (data.type == 'R')
                {
                    udpService.SendTo(data.data, 0, data.len, SocketFlags.None, ep);

                    //byte Seq = data.data[0];
                    _TCPServer.Output(data.data, 0, data.len);

                    /*if (Seq != _LastSeq + 1)
                    {
                        System.Diagnostics.Debug.WriteLine("Missed packet " + _LastSeq.ToString() + ", " + Seq.ToString());
                    }

                    _LastSeq = Seq;*/

                    /*Buffer.BlockCopy(data.data, 0, udpBuffer, udpBuffCount * MAX_MAV_PAYLOAD, MAX_MAV_PAYLOAD);

                    udpBuffCount++;

                    if (udpBuffCount == BUFFER_SIZE/MAX_MAV_PAYLOAD)
                    {
                        //AppendAllBytes("video.h264", udpBuffer);
                        udpBuffCount = 0;
                        udpService.SendTo(udpBuffer, ep);
                        lastSent = DateTime.Now;
                    }*/
                    /*else if (DateTime.Now.Subtract(lastSent).TotalMilliseconds > MAX_DELAY_MS)
                    {
                        udpBuffCount = 0;
                        udpService.SendTo(udpBuffer, udpBuffCount * MAX_MAV_PAYLOAD, SocketFlags.None, ep);
                        lastSent = DateTime.Now;
                    }*/
                }
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can now minimize this window.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Closing it will cause the video service to be interrupted";
            // 
            // MAVLinkVideoService
            // 
            this.ClientSize = new System.Drawing.Size(420, 222);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MAVLinkVideoService";
            this.Text = "Mavlink Video Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MAVLinkInspector_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MAVLinkInspector_FormClosing(object sender, FormClosingEventArgs e)
        {
            mav.OnPacketReceived -= MavOnOnPacketReceived;
            mav.OnPacketSent -= MavOnOnPacketReceived;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
