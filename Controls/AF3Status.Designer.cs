using System.Collections.Generic;

namespace MissionPlanner.Controls
{
    partial class AF3Status
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AF3Status));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRFC1TELEM = new System.Windows.Forms.Label();
            this.lbRFC3TELEM = new System.Windows.Forms.Label();
            this.lbRFC2TELEM = new System.Windows.Forms.Label();
            this.lbRFC1Active = new System.Windows.Forms.Label();
            this.lbRFC3Active = new System.Windows.Forms.Label();
            this.lbRFC2Active = new System.Windows.Forms.Label();
            this.lsErrorList = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.epInfo = new MissionPlanner.Controls.AF3EndpointInfo();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lbRFC1TELEM
            // 
            resources.ApplyResources(this.lbRFC1TELEM, "lbRFC1TELEM");
            this.lbRFC1TELEM.Name = "lbRFC1TELEM";
            // 
            // lbRFC3TELEM
            // 
            resources.ApplyResources(this.lbRFC3TELEM, "lbRFC3TELEM");
            this.lbRFC3TELEM.Name = "lbRFC3TELEM";
            // 
            // lbRFC2TELEM
            // 
            resources.ApplyResources(this.lbRFC2TELEM, "lbRFC2TELEM");
            this.lbRFC2TELEM.Name = "lbRFC2TELEM";
            // 
            // lbRFC1Active
            // 
            resources.ApplyResources(this.lbRFC1Active, "lbRFC1Active");
            this.lbRFC1Active.Name = "lbRFC1Active";
            // 
            // lbRFC3Active
            // 
            resources.ApplyResources(this.lbRFC3Active, "lbRFC3Active");
            this.lbRFC3Active.Name = "lbRFC3Active";
            // 
            // lbRFC2Active
            // 
            resources.ApplyResources(this.lbRFC2Active, "lbRFC2Active");
            this.lbRFC2Active.Name = "lbRFC2Active";
            // 
            // lsErrorList
            // 
            this.lsErrorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.tableLayoutPanel1.SetColumnSpan(this.lsErrorList, 3);
            resources.ApplyResources(this.lsErrorList, "lsErrorList");
            this.lsErrorList.HideSelection = false;
            this.lsErrorList.Name = "lsErrorList";
            this.lsErrorList.UseCompatibleStateImageBehavior = false;
            this.lsErrorList.View = System.Windows.Forms.View.Details;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2Active, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3Active, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1Active, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2TELEM, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3TELEM, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1TELEM, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.epInfo, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lsErrorList, 1, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // epInfo
            // 
            resources.ApplyResources(this.epInfo, "epInfo");
            this.tableLayoutPanel1.SetColumnSpan(this.epInfo, 3);
            this.epInfo.Name = "epInfo";
            // 
            // AF3Status
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AF3Status";
            this.ShowIcon = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private AF3EndpointInfo epInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbRFC2Active;
        private System.Windows.Forms.Label lbRFC3Active;
        private System.Windows.Forms.Label lbRFC1Active;
        private System.Windows.Forms.Label lbRFC2TELEM;
        private System.Windows.Forms.Label lbRFC3TELEM;
        private System.Windows.Forms.Label lbRFC1TELEM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lsErrorList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}