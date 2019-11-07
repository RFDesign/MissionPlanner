namespace MissionPlanner.GCSViews
{
    partial class ucFTS
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GB = new System.Windows.Forms.GroupBox();
            this.lblFTSTermState = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnFTSManualTerminate = new System.Windows.Forms.Button();
            this.lblFTSTermHealth = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblFTSRxRSSI = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblFTSTxRSSI = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblFTSLinkStatus = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFTSFenceEnabled = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB
            // 
            this.GB.Controls.Add(this.label8);
            this.GB.Controls.Add(this.label9);
            this.GB.Controls.Add(this.label11);
            this.GB.Controls.Add(this.label20);
            this.GB.Controls.Add(this.label7);
            this.GB.Controls.Add(this.label6);
            this.GB.Controls.Add(this.label5);
            this.GB.Controls.Add(this.label4);
            this.GB.Controls.Add(this.label3);
            this.GB.Controls.Add(this.label2);
            this.GB.Controls.Add(this.lblFTSFenceEnabled);
            this.GB.Controls.Add(this.label1);
            this.GB.Controls.Add(this.lblFTSTermState);
            this.GB.Controls.Add(this.label19);
            this.GB.Controls.Add(this.btnFTSManualTerminate);
            this.GB.Controls.Add(this.lblFTSTermHealth);
            this.GB.Controls.Add(this.label15);
            this.GB.Controls.Add(this.lblFTSRxRSSI);
            this.GB.Controls.Add(this.label18);
            this.GB.Controls.Add(this.lblFTSTxRSSI);
            this.GB.Controls.Add(this.label16);
            this.GB.Controls.Add(this.lblFTSLinkStatus);
            this.GB.Controls.Add(this.label13);
            this.GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GB.Location = new System.Drawing.Point(0, 0);
            this.GB.Name = "GB";
            this.GB.Size = new System.Drawing.Size(369, 165);
            this.GB.TabIndex = 0;
            this.GB.TabStop = false;
            this.GB.Text = "groupBox1";
            // 
            // lblFTSTermState
            // 
            this.lblFTSTermState.AutoSize = true;
            this.lblFTSTermState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTSTermState.ForeColor = System.Drawing.Color.Green;
            this.lblFTSTermState.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTermState.Location = new System.Drawing.Point(289, 22);
            this.lblFTSTermState.Name = "lblFTSTermState";
            this.lblFTSTermState.Size = new System.Drawing.Size(46, 13);
            this.lblFTSTermState.TabIndex = 22;
            this.lblFTSTermState.Text = "Normal";
            this.lblFTSTermState.Click += new System.EventHandler(this.LblFTSTermState_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(185, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Term. State:";
            // 
            // btnFTSManualTerminate
            // 
            this.btnFTSManualTerminate.BackColor = System.Drawing.Color.Red;
            this.btnFTSManualTerminate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnFTSManualTerminate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTSManualTerminate.Location = new System.Drawing.Point(188, 44);
            this.btnFTSManualTerminate.Name = "btnFTSManualTerminate";
            this.btnFTSManualTerminate.Size = new System.Drawing.Size(164, 71);
            this.btnFTSManualTerminate.TabIndex = 20;
            this.btnFTSManualTerminate.Text = "Terminate\r\nFlight";
            this.btnFTSManualTerminate.UseVisualStyleBackColor = false;
            this.btnFTSManualTerminate.Click += new System.EventHandler(this.BtnFTSManualTerminate_Click);
            // 
            // lblFTSTermHealth
            // 
            this.lblFTSTermHealth.AutoSize = true;
            this.lblFTSTermHealth.ForeColor = System.Drawing.Color.Green;
            this.lblFTSTermHealth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTermHealth.Location = new System.Drawing.Point(110, 73);
            this.lblFTSTermHealth.Name = "lblFTSTermHealth";
            this.lblFTSTermHealth.Size = new System.Drawing.Size(22, 13);
            this.lblFTSTermHealth.TabIndex = 19;
            this.lblFTSTermHealth.Text = "OK";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(6, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Term. Heartbeat:";
            // 
            // lblFTSRxRSSI
            // 
            this.lblFTSRxRSSI.AutoSize = true;
            this.lblFTSRxRSSI.ForeColor = System.Drawing.Color.Green;
            this.lblFTSRxRSSI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSRxRSSI.Location = new System.Drawing.Point(110, 56);
            this.lblFTSRxRSSI.Name = "lblFTSRxRSSI";
            this.lblFTSRxRSSI.Size = new System.Drawing.Size(43, 13);
            this.lblFTSRxRSSI.TabIndex = 17;
            this.lblFTSRxRSSI.Text = "-79dBm";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(6, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Rx RSSI:";
            // 
            // lblFTSTxRSSI
            // 
            this.lblFTSTxRSSI.AutoSize = true;
            this.lblFTSTxRSSI.ForeColor = System.Drawing.Color.Green;
            this.lblFTSTxRSSI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTxRSSI.Location = new System.Drawing.Point(110, 39);
            this.lblFTSTxRSSI.Name = "lblFTSTxRSSI";
            this.lblFTSTxRSSI.Size = new System.Drawing.Size(43, 13);
            this.lblFTSTxRSSI.TabIndex = 15;
            this.lblFTSTxRSSI.Text = "-78dBm";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(6, 39);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Tx RSSI:";
            // 
            // lblFTSLinkStatus
            // 
            this.lblFTSLinkStatus.AutoSize = true;
            this.lblFTSLinkStatus.ForeColor = System.Drawing.Color.Green;
            this.lblFTSLinkStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSLinkStatus.Location = new System.Drawing.Point(110, 22);
            this.lblFTSLinkStatus.Name = "lblFTSLinkStatus";
            this.lblFTSLinkStatus.Size = new System.Drawing.Size(27, 13);
            this.lblFTSLinkStatus.TabIndex = 13;
            this.lblFTSLinkStatus.Text = "99%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(6, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Link Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Fence Func. State:";
            // 
            // lblFTSFenceEnabled
            // 
            this.lblFTSFenceEnabled.AutoSize = true;
            this.lblFTSFenceEnabled.ForeColor = System.Drawing.Color.Green;
            this.lblFTSFenceEnabled.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSFenceEnabled.Location = new System.Drawing.Point(110, 107);
            this.lblFTSFenceEnabled.Name = "lblFTSFenceEnabled";
            this.lblFTSFenceEnabled.Size = new System.Drawing.Size(46, 13);
            this.lblFTSFenceEnabled.TabIndex = 24;
            this.lblFTSFenceEnabled.Text = "Enabled";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Fence Load. State:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "GPS 1 Detected:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(6, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "GPS 1 Sats/HDOP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(110, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Loaded";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(110, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Yes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(110, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "9/1.35";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Green;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(289, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Yes";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(185, 124);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(88, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "GPS 2 Detected:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(289, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "9/1.35";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(185, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "GPS 2 Sats/HDOP:";
            // 
            // ucFTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GB);
            this.Name = "ucFTS";
            this.Size = new System.Drawing.Size(369, 165);
            this.GB.ResumeLayout(false);
            this.GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB;
        private System.Windows.Forms.Label lblFTSTermState;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnFTSManualTerminate;
        private System.Windows.Forms.Label lblFTSTermHealth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblFTSRxRSSI;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblFTSTxRSSI;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblFTSLinkStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFTSFenceEnabled;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}
