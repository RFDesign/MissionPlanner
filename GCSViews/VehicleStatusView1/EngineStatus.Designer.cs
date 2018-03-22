namespace MissionPlanner.GCSViews.VehicleStatusView
{
    partial class EngineStatus
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Speed (rpm)",
            "--"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Throttle Position (%)",
            "--"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Temperature (°C)",
            "--"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Starter Motor (on/off)",
            "--"}, -1);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cbEngineName = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cht = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEngineName
            // 
            this.cbEngineName.FormattingEnabled = true;
            this.cbEngineName.Items.AddRange(new object[] {
            "Engine 1",
            "Engine 2",
            "Engine 3",
            "Engine 4",
            "Engine 5",
            "Engine 6"});
            this.cbEngineName.Location = new System.Drawing.Point(3, 3);
            this.cbEngineName.Name = "cbEngineName";
            this.cbEngineName.Size = new System.Drawing.Size(222, 24);
            this.cbEngineName.TabIndex = 0;
            this.cbEngineName.SelectedIndexChanged += new System.EventHandler(this.cbEngineName_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cbEngineName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cht, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1010, 459);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.lv.Location = new System.Drawing.Point(3, 43);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(344, 413);
            this.lv.TabIndex = 1;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 159;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 50;
            // 
            // cht
            // 
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.Title = "Time (minutes)";
            chartArea1.AxisY.Title = "rpm";
            chartArea1.AxisY2.Title = "%";
            chartArea1.Name = "ChartArea1";
            this.cht.ChartAreas.Add(chartArea1);
            this.cht.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.cht.Legends.Add(legend1);
            this.cht.Location = new System.Drawing.Point(353, 43);
            this.cht.Name = "cht";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Throttle Position";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Speed";
            this.cht.Series.Add(series1);
            this.cht.Series.Add(series2);
            this.cht.Size = new System.Drawing.Size(654, 413);
            this.cht.TabIndex = 2;
            this.cht.Text = "cht";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // EngineStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EngineStatus";
            this.Size = new System.Drawing.Size(1010, 459);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEngineName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}
