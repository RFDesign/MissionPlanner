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
            this.GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB
            // 
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
            this.GB.Size = new System.Drawing.Size(382, 112);
            this.GB.TabIndex = 0;
            this.GB.TabStop = false;
            this.GB.Text = "groupBox1";
            // 
            // lblFTSTermState
            // 
            this.lblFTSTermState.AutoSize = true;
            this.lblFTSTermState.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTermState.Location = new System.Drawing.Point(145, 90);
            this.lblFTSTermState.Name = "lblFTSTermState";
            this.lblFTSTermState.Size = new System.Drawing.Size(13, 13);
            this.lblFTSTermState.TabIndex = 22;
            this.lblFTSTermState.Text = "--";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(6, 90);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Term. State:";
            // 
            // btnFTSManualTerminate
            // 
            this.btnFTSManualTerminate.BackColor = System.Drawing.Color.Red;
            this.btnFTSManualTerminate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnFTSManualTerminate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTSManualTerminate.Location = new System.Drawing.Point(225, 19);
            this.btnFTSManualTerminate.Name = "btnFTSManualTerminate";
            this.btnFTSManualTerminate.Size = new System.Drawing.Size(139, 71);
            this.btnFTSManualTerminate.TabIndex = 20;
            this.btnFTSManualTerminate.Text = "Terminate\r\nFlight";
            this.btnFTSManualTerminate.UseVisualStyleBackColor = false;
            this.btnFTSManualTerminate.Click += new System.EventHandler(this.BtnFTSManualTerminate_Click);
            // 
            // lblFTSTermHealth
            // 
            this.lblFTSTermHealth.AutoSize = true;
            this.lblFTSTermHealth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTermHealth.Location = new System.Drawing.Point(145, 73);
            this.lblFTSTermHealth.Name = "lblFTSTermHealth";
            this.lblFTSTermHealth.Size = new System.Drawing.Size(13, 13);
            this.lblFTSTermHealth.TabIndex = 19;
            this.lblFTSTermHealth.Text = "--";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(6, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Term. Health:";
            // 
            // lblFTSRxRSSI
            // 
            this.lblFTSRxRSSI.AutoSize = true;
            this.lblFTSRxRSSI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSRxRSSI.Location = new System.Drawing.Point(145, 56);
            this.lblFTSRxRSSI.Name = "lblFTSRxRSSI";
            this.lblFTSRxRSSI.Size = new System.Drawing.Size(13, 13);
            this.lblFTSRxRSSI.TabIndex = 17;
            this.lblFTSRxRSSI.Text = "--";
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
            this.lblFTSTxRSSI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSTxRSSI.Location = new System.Drawing.Point(145, 39);
            this.lblFTSTxRSSI.Name = "lblFTSTxRSSI";
            this.lblFTSTxRSSI.Size = new System.Drawing.Size(13, 13);
            this.lblFTSTxRSSI.TabIndex = 15;
            this.lblFTSTxRSSI.Text = "--";
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
            this.lblFTSLinkStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFTSLinkStatus.Location = new System.Drawing.Point(145, 22);
            this.lblFTSLinkStatus.Name = "lblFTSLinkStatus";
            this.lblFTSLinkStatus.Size = new System.Drawing.Size(13, 13);
            this.lblFTSLinkStatus.TabIndex = 13;
            this.lblFTSLinkStatus.Text = "--";
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
            // ucFTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GB);
            this.Name = "ucFTS";
            this.Size = new System.Drawing.Size(382, 112);
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
    }
}
