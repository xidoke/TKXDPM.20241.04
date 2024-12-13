using System.ComponentModel;

namespace AIMS.Views
{
    partial class NavBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.NavBarPanel = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.NavBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // NavBarPanel
            // 
            this.NavBarPanel.Controls.Add(this.logo);
            this.NavBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavBarPanel.Location = new System.Drawing.Point(0, 0);
            this.NavBarPanel.Name = "NavBarPanel";
            this.NavBarPanel.Size = new System.Drawing.Size(1010, 50);
            this.NavBarPanel.TabIndex = 0;
            // 
            // logo
            // 
            this.logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.logo.Cursor = System.Windows.Forms.Cursors.Default;
            this.logo.Image = global::AIMS.Properties.Resources.logo;
            this.logo.InitialImage = global::AIMS.Properties.Resources.logo;
            this.logo.Location = new System.Drawing.Point(8, 5);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(40, 40);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // NavBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NavBarPanel);
            this.Name = "NavBar";
            this.Size = new System.Drawing.Size(1010, 50);
            this.Load += new System.EventHandler(this.NavBar_Load);
            this.NavBarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox logo;

        private System.Windows.Forms.Panel NavBarPanel;

        #endregion
    }
}