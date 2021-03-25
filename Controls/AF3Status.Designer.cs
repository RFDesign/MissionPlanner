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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.epInfo = new MissionPlanner.Controls.AF3EndpointInfo();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
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
    }
}