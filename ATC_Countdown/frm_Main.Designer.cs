namespace ATC_Countdown
{
    partial class frm_Main
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
            this.ptb_Main = new System.Windows.Forms.PictureBox();
            this.lb_Countdown = new System.Windows.Forms.Label();
            this.tmr_Countdown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // ptb_Main
            // 
            this.ptb_Main.Image = global::ATC_Countdown.Properties.Resources.QooBee1_Feeling_good;
            this.ptb_Main.Location = new System.Drawing.Point(0, 0);
            this.ptb_Main.Name = "ptb_Main";
            this.ptb_Main.Size = new System.Drawing.Size(60, 60);
            this.ptb_Main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptb_Main.TabIndex = 0;
            this.ptb_Main.TabStop = false;
            this.ptb_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ptb_Main_MouseDown);
            // 
            // lb_Countdown
            // 
            this.lb_Countdown.AutoSize = true;
            this.lb_Countdown.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Countdown.ForeColor = System.Drawing.Color.LawnGreen;
            this.lb_Countdown.Location = new System.Drawing.Point(61, 0);
            this.lb_Countdown.Name = "lb_Countdown";
            this.lb_Countdown.Size = new System.Drawing.Size(206, 56);
            this.lb_Countdown.TabIndex = 1;
            this.lb_Countdown.Text = "-19:-12";
            this.lb_Countdown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_Countdown_MouseDown);
            // 
            // tmr_Countdown
            // 
            this.tmr_Countdown.Interval = 1000;
            this.tmr_Countdown.Tick += new System.EventHandler(this.tmr_Countdown_Tick);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(327, 60);
            this.ControlBox = false;
            this.Controls.Add(this.lb_Countdown);
            this.Controls.Add(this.ptb_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ptb_Main;
        private System.Windows.Forms.Label lb_Countdown;
        private System.Windows.Forms.Timer tmr_Countdown;
    }
}

