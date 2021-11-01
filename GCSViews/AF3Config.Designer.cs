
namespace MissionPlanner.GCSViews
{
    partial class AF3Config
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
            this.lbActiveRFC = new System.Windows.Forms.Label();
            this.btnCheckIntegrity = new System.Windows.Forms.Button();
            this.btnSaveSnapshot = new System.Windows.Forms.Button();
            this.gbInteg = new System.Windows.Forms.GroupBox();
            this.lblIntegStatus = new System.Windows.Forms.Label();
            this.gbInteg.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbActiveRFC
            // 
            this.lbActiveRFC.AutoSize = true;
            this.lbActiveRFC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbActiveRFC.Location = new System.Drawing.Point(27, 26);
            this.lbActiveRFC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbActiveRFC.Name = "lbActiveRFC";
            this.lbActiveRFC.Size = new System.Drawing.Size(153, 17);
            this.lbActiveRFC.TabIndex = 10;
            this.lbActiveRFC.Text = "Active Flight Controller:";
            // 
            // btnCheckIntegrity
            // 
            this.btnCheckIntegrity.Location = new System.Drawing.Point(17, 79);
            this.btnCheckIntegrity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCheckIntegrity.Name = "btnCheckIntegrity";
            this.btnCheckIntegrity.Size = new System.Drawing.Size(149, 50);
            this.btnCheckIntegrity.TabIndex = 11;
            this.btnCheckIntegrity.Text = "Check Integrity";
            this.btnCheckIntegrity.UseVisualStyleBackColor = true;
            this.btnCheckIntegrity.Click += new System.EventHandler(this.btnCheckIntegrity_Click);
            // 
            // btnSaveSnapshot
            // 
            this.btnSaveSnapshot.Location = new System.Drawing.Point(17, 22);
            this.btnSaveSnapshot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveSnapshot.Name = "btnSaveSnapshot";
            this.btnSaveSnapshot.Size = new System.Drawing.Size(149, 50);
            this.btnSaveSnapshot.TabIndex = 12;
            this.btnSaveSnapshot.Text = "Save Snapshot";
            this.btnSaveSnapshot.UseVisualStyleBackColor = true;
            this.btnSaveSnapshot.Click += new System.EventHandler(this.btnSaveSnapshot_Click);
            // 
            // gbInteg
            // 
            this.gbInteg.Controls.Add(this.lblIntegStatus);
            this.gbInteg.Controls.Add(this.btnSaveSnapshot);
            this.gbInteg.Controls.Add(this.btnCheckIntegrity);
            this.gbInteg.Location = new System.Drawing.Point(30, 57);
            this.gbInteg.Name = "gbInteg";
            this.gbInteg.Size = new System.Drawing.Size(409, 149);
            this.gbInteg.TabIndex = 15;
            this.gbInteg.TabStop = false;
            this.gbInteg.Text = "Integrity System";
            // 
            // lblIntegStatus
            // 
            this.lblIntegStatus.AutoSize = true;
            this.lblIntegStatus.Location = new System.Drawing.Point(173, 22);
            this.lblIntegStatus.Name = "lblIntegStatus";
            this.lblIntegStatus.Size = new System.Drawing.Size(52, 17);
            this.lblIntegStatus.TabIndex = 13;
            this.lblIntegStatus.Text = "Status:";
            // 
            // AF3Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInteg);
            this.Controls.Add(this.lbActiveRFC);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AF3Config";
            this.Size = new System.Drawing.Size(615, 251);
            this.gbInteg.ResumeLayout(false);
            this.gbInteg.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbActiveRFC;
        private System.Windows.Forms.Button btnCheckIntegrity;
        private System.Windows.Forms.Button btnSaveSnapshot;
        private System.Windows.Forms.GroupBox gbInteg;
        private System.Windows.Forms.Label lblIntegStatus;
    }
}
