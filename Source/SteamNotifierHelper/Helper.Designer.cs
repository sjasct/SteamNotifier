namespace SteamNotifierHelper {
    partial class Helper {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Helper));
            this.lblHead = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.ckbStartup = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.btnOpenIgnored = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.Location = new System.Drawing.Point(91, 21);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(160, 29);
            this.lblHead.TabIndex = 1;
            this.lblHead.Text = "SteamNotifier";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(93, 50);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(59, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "version 1.4";
            // 
            // ckbStartup
            // 
            this.ckbStartup.AutoSize = true;
            this.ckbStartup.Location = new System.Drawing.Point(13, 97);
            this.ckbStartup.Name = "ckbStartup";
            this.ckbStartup.Size = new System.Drawing.Size(238, 17);
            this.ckbStartup.TabIndex = 3;
            this.ckbStartup.Text = "Run SteamNotifier on startup (recommended)";
            this.ckbStartup.UseVisualStyleBackColor = true;
            this.ckbStartup.CheckedChanged += new System.EventHandler(this.ckbStartup_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(12, 163);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(246, 23);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.Image = global::SteamNotifierHelper.Properties.Resources.icon_bg2;
            this.imgIcon.Location = new System.Drawing.Point(12, 12);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(73, 67);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgIcon.TabIndex = 0;
            this.imgIcon.TabStop = false;
            // 
            // btnOpenIgnored
            // 
            this.btnOpenIgnored.Location = new System.Drawing.Point(12, 132);
            this.btnOpenIgnored.Name = "btnOpenIgnored";
            this.btnOpenIgnored.Size = new System.Drawing.Size(245, 23);
            this.btnOpenIgnored.TabIndex = 6;
            this.btnOpenIgnored.Text = "Change Ignored Apps";
            this.btnOpenIgnored.UseVisualStyleBackColor = true;
            this.btnOpenIgnored.Click += new System.EventHandler(this.btnOpenIgnored_Click);
            // 
            // Helper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 199);
            this.Controls.Add(this.btnOpenIgnored);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.ckbStartup);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.imgIcon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Helper";
            this.Text = "Steam Notifier";
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox ckbStartup;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnOpenIgnored;
    }
}

