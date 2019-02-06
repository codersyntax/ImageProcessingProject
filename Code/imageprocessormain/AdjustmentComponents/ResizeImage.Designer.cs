namespace ImageProcessorMain.AdjustmentComponents
{
    partial class ResizeImage
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
            this.h_trackBar = new System.Windows.Forms.TrackBar();
            this.v_trackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.p_control = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.h_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v_trackBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p_control)).BeginInit();
            this.SuspendLayout();
            // 
            // h_trackBar
            // 
            this.h_trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.h_trackBar.Location = new System.Drawing.Point(114, 12);
            this.h_trackBar.Maximum = 100;
            this.h_trackBar.Name = "h_trackBar";
            this.h_trackBar.Size = new System.Drawing.Size(640, 90);
            this.h_trackBar.TabIndex = 0;
            this.h_trackBar.Value = 100;
            this.h_trackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // v_trackBar
            // 
            this.v_trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.v_trackBar.Location = new System.Drawing.Point(18, 82);
            this.v_trackBar.Maximum = 100;
            this.v_trackBar.Name = "v_trackBar";
            this.v_trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.v_trackBar.Size = new System.Drawing.Size(90, 365);
            this.v_trackBar.TabIndex = 1;
            this.v_trackBar.Value = 100;
            this.v_trackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.p_control);
            this.panel1.Location = new System.Drawing.Point(114, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 352);
            this.panel1.TabIndex = 2;
            // 
            // p_control
            // 
            this.p_control.Location = new System.Drawing.Point(3, 3);
            this.p_control.Name = "p_control";
            this.p_control.Size = new System.Drawing.Size(441, 253);
            this.p_control.TabIndex = 0;
            this.p_control.TabStop = false;
            // 
            // ResizeImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 542);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.v_trackBar);
            this.Controls.Add(this.h_trackBar);
            this.Name = "ResizeImage";
            this.Text = "ResideImage";
            this.Load += new System.EventHandler(this.ResizeImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.h_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.v_trackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p_control)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar h_trackBar;
        private System.Windows.Forms.TrackBar v_trackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox p_control;
    }
}