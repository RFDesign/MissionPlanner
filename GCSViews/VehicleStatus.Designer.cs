namespace MissionPlanner.GCSViews
{
    partial class VehicleStatus
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleStatus));
            this.backstageView = new MissionPlanner.Controls.BackstageView.BackstageView();
            this.SuspendLayout();
            // 
            // backstageView
            // 
            resources.ApplyResources(this.backstageView, "backstageView");
            this.backstageView.HighlightColor1 = System.Drawing.SystemColors.Highlight;
            this.backstageView.HighlightColor2 = System.Drawing.SystemColors.MenuHighlight;
            this.backstageView.Name = "backstageView";
            this.backstageView.WidthMenu = 203;
            // 
            // VehicleStatus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.backstageView);
            this.Name = "VehicleStatus";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        private Controls.BackstageView.BackstageView backstageView;
    }
}