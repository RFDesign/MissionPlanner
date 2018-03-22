namespace MissionPlanner.GCSViews.VehicleStatusView
{
    partial class VehicleDiagram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleDiagram));
            this.engineHealth1 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            this.engineHealth2 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.engineHealth3 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            this.engineHealth4 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            this.engineHealth6 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            this.engineHealth5 = new MissionPlanner.GCSViews.VehicleStatusView.EngineHealth();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // engineHealth1
            // 
            this.engineHealth1.BackColor = System.Drawing.Color.Red;
            this.engineHealth1.EngineDesignator = "4";
            this.engineHealth1.Location = new System.Drawing.Point(440, 128);
            this.engineHealth1.Name = "engineHealth1";
            this.engineHealth1.OverallHealth = TAlarmLevel.ALERT;
            this.engineHealth1.Size = new System.Drawing.Size(169, 140);
            this.engineHealth1.TabIndex = 2;
            // 
            // engineHealth2
            // 
            this.engineHealth2.BackColor = System.Drawing.Color.Orange;
            this.engineHealth2.EngineDesignator = "6";
            this.engineHealth2.Location = new System.Drawing.Point(440, 357);
            this.engineHealth2.Name = "engineHealth2";
            this.engineHealth2.OverallHealth = TAlarmLevel.WARNING;
            this.engineHealth2.Size = new System.Drawing.Size(169, 140);
            this.engineHealth2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(863, 611);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // engineHealth3
            // 
            this.engineHealth3.BackColor = System.Drawing.Color.Green;
            this.engineHealth3.EngineDesignator = "3";
            this.engineHealth3.Location = new System.Drawing.Point(15, 357);
            this.engineHealth3.Name = "engineHealth3";
            this.engineHealth3.OverallHealth = TAlarmLevel.NONE;
            this.engineHealth3.Size = new System.Drawing.Size(169, 140);
            this.engineHealth3.TabIndex = 4;
            // 
            // engineHealth4
            // 
            this.engineHealth4.BackColor = System.Drawing.Color.Green;
            this.engineHealth4.EngineDesignator = "5";
            this.engineHealth4.Location = new System.Drawing.Point(15, 128);
            this.engineHealth4.Name = "engineHealth4";
            this.engineHealth4.OverallHealth = TAlarmLevel.NONE;
            this.engineHealth4.Size = new System.Drawing.Size(169, 140);
            this.engineHealth4.TabIndex = 5;
            // 
            // engineHealth6
            // 
            this.engineHealth6.BackColor = System.Drawing.Color.Green;
            this.engineHealth6.EngineDesignator = "2";
            this.engineHealth6.Location = new System.Drawing.Point(229, 468);
            this.engineHealth6.Name = "engineHealth6";
            this.engineHealth6.OverallHealth = TAlarmLevel.NONE;
            this.engineHealth6.Size = new System.Drawing.Size(169, 140);
            this.engineHealth6.TabIndex = 7;
            // 
            // engineHealth5
            // 
            this.engineHealth5.BackColor = System.Drawing.Color.Green;
            this.engineHealth5.EngineDesignator = "1";
            this.engineHealth5.Location = new System.Drawing.Point(229, 15);
            this.engineHealth5.Name = "engineHealth5";
            this.engineHealth5.OverallHealth = TAlarmLevel.NONE;
            this.engineHealth5.Size = new System.Drawing.Size(169, 140);
            this.engineHealth5.TabIndex = 6;
            // 
            // VehicleDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.engineHealth6);
            this.Controls.Add(this.engineHealth5);
            this.Controls.Add(this.engineHealth4);
            this.Controls.Add(this.engineHealth3);
            this.Controls.Add(this.engineHealth2);
            this.Controls.Add(this.engineHealth1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "VehicleDiagram";
            this.Size = new System.Drawing.Size(863, 611);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EngineHealth engineHealth1;
        private EngineHealth engineHealth2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private EngineHealth engineHealth3;
        private EngineHealth engineHealth4;
        private EngineHealth engineHealth6;
        private EngineHealth engineHealth5;
    }
}
