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
            this.lbRFC3 = new System.Windows.Forms.Label();
            this.lbRFC2 = new System.Windows.Forms.Label();
            this.lbRFC3TELEM = new System.Windows.Forms.Label();
            this.lbRFC2TELEM = new System.Windows.Forms.Label();
            this.lbRFC3Active = new System.Windows.Forms.Label();
            this.lbRFC2Active = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbVfcVoltB = new System.Windows.Forms.Label();
            this.lbTxAux = new System.Windows.Forms.Label();
            this.lbTxAf3 = new System.Windows.Forms.Label();
            this.lbVfcVoltA = new System.Windows.Forms.Label();
            this.lbRxAux = new System.Windows.Forms.Label();
            this.lbRxAf3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbVfcLoading = new System.Windows.Forms.Label();
            this.lbVfcTemp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRFC3ppmVis = new System.Windows.Forms.Label();
            this.lbRFC2ppmVis = new System.Windows.Forms.Label();
            this.lbRFC1ppmVis = new System.Windows.Forms.Label();
            this.lbRFC3Score = new System.Windows.Forms.Label();
            this.lbRFC2Score = new System.Windows.Forms.Label();
            this.lbRFC1Score = new System.Windows.Forms.Label();
            this.lbRFC3CanPres = new System.Windows.Forms.Label();
            this.lbRFC2CanPres = new System.Windows.Forms.Label();
            this.lbRFC1CanPres = new System.Windows.Forms.Label();
            this.lbRFC1ArmStatus = new System.Windows.Forms.Label();
            this.lbRFC3ArmStatus = new System.Windows.Forms.Label();
            this.lbRFC2ArmStatus = new System.Windows.Forms.Label();
            this.lbRFC3FlightMode = new System.Windows.Forms.Label();
            this.lbRFC2FlightMode = new System.Windows.Forms.Label();
            this.lbRFC1FlightMode = new System.Windows.Forms.Label();
            this.lbRFC1Active = new System.Windows.Forms.Label();
            this.lbRFC1TELEM = new System.Windows.Forms.Label();
            this.lbRFC1 = new System.Windows.Forms.Label();
            this.epInfo = new MissionPlanner.Controls.AF3EndpointInfo();
            this.ecamList = new System.Windows.Forms.ListView();
            this.ecamMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearAllMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.ecamMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbRFC3
            // 
            resources.ApplyResources(this.lbRFC3, "lbRFC3");
            this.lbRFC3.Name = "lbRFC3";
            // 
            // lbRFC2
            // 
            resources.ApplyResources(this.lbRFC2, "lbRFC2");
            this.lbRFC2.Name = "lbRFC2";
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
            this.tableLayoutPanel1.Controls.Add(this.lbVfcVoltB, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbTxAux, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbTxAf3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbVfcVoltA, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbRxAux, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbRxAf3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbVfcLoading, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbVfcTemp, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3ppmVis, 3, 15);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2ppmVis, 2, 15);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1ppmVis, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3Score, 3, 14);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2Score, 2, 14);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1Score, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3CanPres, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2CanPres, 2, 13);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1CanPres, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1ArmStatus, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3ArmStatus, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2ArmStatus, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3FlightMode, 3, 11);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2FlightMode, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1FlightMode, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2Active, 2, 16);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3Active, 3, 16);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1Active, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2TELEM, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3TELEM, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1TELEM, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC1, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC2, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.lbRFC3, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.epInfo, 1, 18);
            this.tableLayoutPanel1.Controls.Add(this.ecamList, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lbVfcVoltB
            // 
            resources.ApplyResources(this.lbVfcVoltB, "lbVfcVoltB");
            this.lbVfcVoltB.Name = "lbVfcVoltB";
            // 
            // lbTxAux
            // 
            resources.ApplyResources(this.lbTxAux, "lbTxAux");
            this.lbTxAux.Name = "lbTxAux";
            // 
            // lbTxAf3
            // 
            resources.ApplyResources(this.lbTxAf3, "lbTxAf3");
            this.lbTxAf3.Name = "lbTxAf3";
            // 
            // lbVfcVoltA
            // 
            resources.ApplyResources(this.lbVfcVoltA, "lbVfcVoltA");
            this.lbVfcVoltA.Name = "lbVfcVoltA";
            // 
            // lbRxAux
            // 
            resources.ApplyResources(this.lbRxAux, "lbRxAux");
            this.lbRxAux.Name = "lbRxAux";
            // 
            // lbRxAf3
            // 
            resources.ApplyResources(this.lbRxAf3, "lbRxAf3");
            this.lbRxAf3.Name = "lbRxAf3";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lbVfcLoading
            // 
            resources.ApplyResources(this.lbVfcLoading, "lbVfcLoading");
            this.lbVfcLoading.Name = "lbVfcLoading";
            // 
            // lbVfcTemp
            // 
            resources.ApplyResources(this.lbVfcTemp, "lbVfcTemp");
            this.lbVfcTemp.Name = "lbVfcTemp";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Name = "label1";
            // 
            // lbRFC3ppmVis
            // 
            resources.ApplyResources(this.lbRFC3ppmVis, "lbRFC3ppmVis");
            this.lbRFC3ppmVis.Name = "lbRFC3ppmVis";
            // 
            // lbRFC2ppmVis
            // 
            resources.ApplyResources(this.lbRFC2ppmVis, "lbRFC2ppmVis");
            this.lbRFC2ppmVis.Name = "lbRFC2ppmVis";
            // 
            // lbRFC1ppmVis
            // 
            resources.ApplyResources(this.lbRFC1ppmVis, "lbRFC1ppmVis");
            this.lbRFC1ppmVis.Name = "lbRFC1ppmVis";
            // 
            // lbRFC3Score
            // 
            resources.ApplyResources(this.lbRFC3Score, "lbRFC3Score");
            this.lbRFC3Score.Name = "lbRFC3Score";
            // 
            // lbRFC2Score
            // 
            resources.ApplyResources(this.lbRFC2Score, "lbRFC2Score");
            this.lbRFC2Score.Name = "lbRFC2Score";
            // 
            // lbRFC1Score
            // 
            resources.ApplyResources(this.lbRFC1Score, "lbRFC1Score");
            this.lbRFC1Score.Name = "lbRFC1Score";
            // 
            // lbRFC3CanPres
            // 
            resources.ApplyResources(this.lbRFC3CanPres, "lbRFC3CanPres");
            this.lbRFC3CanPres.Name = "lbRFC3CanPres";
            // 
            // lbRFC2CanPres
            // 
            resources.ApplyResources(this.lbRFC2CanPres, "lbRFC2CanPres");
            this.lbRFC2CanPres.Name = "lbRFC2CanPres";
            // 
            // lbRFC1CanPres
            // 
            resources.ApplyResources(this.lbRFC1CanPres, "lbRFC1CanPres");
            this.lbRFC1CanPres.Name = "lbRFC1CanPres";
            // 
            // lbRFC1ArmStatus
            // 
            resources.ApplyResources(this.lbRFC1ArmStatus, "lbRFC1ArmStatus");
            this.lbRFC1ArmStatus.Name = "lbRFC1ArmStatus";
            // 
            // lbRFC3ArmStatus
            // 
            resources.ApplyResources(this.lbRFC3ArmStatus, "lbRFC3ArmStatus");
            this.lbRFC3ArmStatus.Name = "lbRFC3ArmStatus";
            // 
            // lbRFC2ArmStatus
            // 
            resources.ApplyResources(this.lbRFC2ArmStatus, "lbRFC2ArmStatus");
            this.lbRFC2ArmStatus.Name = "lbRFC2ArmStatus";
            // 
            // lbRFC3FlightMode
            // 
            resources.ApplyResources(this.lbRFC3FlightMode, "lbRFC3FlightMode");
            this.lbRFC3FlightMode.Name = "lbRFC3FlightMode";
            // 
            // lbRFC2FlightMode
            // 
            resources.ApplyResources(this.lbRFC2FlightMode, "lbRFC2FlightMode");
            this.lbRFC2FlightMode.Name = "lbRFC2FlightMode";
            // 
            // lbRFC1FlightMode
            // 
            resources.ApplyResources(this.lbRFC1FlightMode, "lbRFC1FlightMode");
            this.lbRFC1FlightMode.Name = "lbRFC1FlightMode";
            // 
            // lbRFC1Active
            // 
            resources.ApplyResources(this.lbRFC1Active, "lbRFC1Active");
            this.lbRFC1Active.Name = "lbRFC1Active";
            // 
            // lbRFC1TELEM
            // 
            resources.ApplyResources(this.lbRFC1TELEM, "lbRFC1TELEM");
            this.lbRFC1TELEM.Name = "lbRFC1TELEM";
            // 
            // lbRFC1
            // 
            resources.ApplyResources(this.lbRFC1, "lbRFC1");
            this.lbRFC1.Name = "lbRFC1";
            // 
            // epInfo
            // 
            resources.ApplyResources(this.epInfo, "epInfo");
            this.tableLayoutPanel1.SetColumnSpan(this.epInfo, 3);
            this.epInfo.Name = "epInfo";
            // 
            // ecamList
            // 
            this.ecamList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.ecamList, 3);
            this.ecamList.ContextMenuStrip = this.ecamMenu;
            resources.ApplyResources(this.ecamList, "ecamList");
            this.ecamList.HideSelection = false;
            this.ecamList.Name = "ecamList";
            this.ecamList.UseCompatibleStateImageBehavior = false;
            this.ecamList.View = System.Windows.Forms.View.List;
            this.ecamList.DoubleClick += new System.EventHandler(this.ecamList_DoubleClick);
            // 
            // ecamMenu
            // 
            this.ecamMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ecamMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllMessagesToolStripMenuItem});
            this.ecamMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(this.ecamMenu, "ecamMenu");
            // 
            // clearAllMessagesToolStripMenuItem
            // 
            this.clearAllMessagesToolStripMenuItem.Name = "clearAllMessagesToolStripMenuItem";
            resources.ApplyResources(this.clearAllMessagesToolStripMenuItem, "clearAllMessagesToolStripMenuItem");
            this.clearAllMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearAllMessagesToolStripMenuItem_Click);
            // 
            // AF3Status
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AF3Status";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.AF3Status_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ecamMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbRFC2Active;
        private System.Windows.Forms.Label lbRFC3Active;
        private System.Windows.Forms.Label lbRFC2TELEM;
        private System.Windows.Forms.Label lbRFC3TELEM;
        private System.Windows.Forms.Label lbRFC2;
        private System.Windows.Forms.Label lbRFC3;
        private System.Windows.Forms.Label lbRFC3FlightMode;
        private System.Windows.Forms.Label lbRFC2FlightMode;
        private System.Windows.Forms.Label lbRFC3ArmStatus;
        private System.Windows.Forms.Label lbRFC2ArmStatus;
        private System.Windows.Forms.Label lbRFC3Score;
        private System.Windows.Forms.Label lbRFC2Score;
        private System.Windows.Forms.Label lbRFC3CanPres;
        private System.Windows.Forms.Label lbRFC2CanPres;
        private System.Windows.Forms.Label lbRFC3ppmVis;
        private System.Windows.Forms.Label lbRFC2ppmVis;
        private System.Windows.Forms.Label lbRFC1ppmVis;
        private System.Windows.Forms.Label lbRFC1Score;
        private System.Windows.Forms.Label lbRFC1CanPres;
        private System.Windows.Forms.Label lbRFC1ArmStatus;
        private System.Windows.Forms.Label lbRFC1FlightMode;
        private System.Windows.Forms.Label lbRFC1Active;
        private System.Windows.Forms.Label lbRFC1TELEM;
        private System.Windows.Forms.Label lbRFC1;
        private AF3EndpointInfo epInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView ecamList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbVfcLoading;
        private System.Windows.Forms.Label lbVfcTemp;
        private System.Windows.Forms.ContextMenuStrip ecamMenu;
        private System.Windows.Forms.ToolStripMenuItem clearAllMessagesToolStripMenuItem;
        private System.Windows.Forms.Label lbVfcVoltB;
        private System.Windows.Forms.Label lbTxAux;
        private System.Windows.Forms.Label lbTxAf3;
        private System.Windows.Forms.Label lbVfcVoltA;
        private System.Windows.Forms.Label lbRxAux;
        private System.Windows.Forms.Label lbRxAf3;
    }
}