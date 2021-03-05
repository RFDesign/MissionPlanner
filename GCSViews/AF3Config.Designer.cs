
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
            this.SuspendLayout();
            // 
            // lbActiveRFC
            // 
            this.lbActiveRFC.AutoSize = true;
            this.lbActiveRFC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbActiveRFC.Location = new System.Drawing.Point(20, 21);
            this.lbActiveRFC.Name = "lbActiveRFC";
            this.lbActiveRFC.Size = new System.Drawing.Size(115, 13);
            this.lbActiveRFC.TabIndex = 10;
            this.lbActiveRFC.Text = "Active Flight Controller:";
            // 
            // AF3Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbActiveRFC);
            this.Name = "AF3Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbActiveRFC;
    }
}
