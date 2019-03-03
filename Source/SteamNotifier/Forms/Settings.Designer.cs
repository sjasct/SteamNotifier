namespace SteamNotifier.Forms
{
    partial class Settings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.lblHead = new System.Windows.Forms.Label();
            this.ckbStartup = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.btnOpenIgnored = new System.Windows.Forms.Button();
            this.ckbMute = new System.Windows.Forms.CheckBox();
            this.ckbAppID = new System.Windows.Forms.CheckBox();
            this.ckbNotifyWhileRunning = new System.Windows.Forms.CheckBox();
            this.numNotificationWait = new System.Windows.Forms.NumericUpDown();
            this.lblNotificationWait = new System.Windows.Forms.Label();
            this.btnNotificantionWaitHelp = new System.Windows.Forms.Button();
            this.btnSettingsToDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotificationWait)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.Location = new System.Drawing.Point(91, 33);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(160, 29);
            this.lblHead.TabIndex = 1;
            this.lblHead.Text = "SteamNotifier";
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
            this.btnAbout.Location = new System.Drawing.Point(12, 280);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(319, 23);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.Image = global::SteamNotifier.Properties.Resources.Icon_CircleBG_PNG;
            this.imgIcon.Location = new System.Drawing.Point(12, 12);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(73, 67);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgIcon.TabIndex = 0;
            this.imgIcon.TabStop = false;
            // 
            // btnOpenIgnored
            // 
            this.btnOpenIgnored.Location = new System.Drawing.Point(12, 249);
            this.btnOpenIgnored.Name = "btnOpenIgnored";
            this.btnOpenIgnored.Size = new System.Drawing.Size(319, 23);
            this.btnOpenIgnored.TabIndex = 6;
            this.btnOpenIgnored.Text = "Change Ignored Apps";
            this.btnOpenIgnored.UseVisualStyleBackColor = true;
            this.btnOpenIgnored.Click += new System.EventHandler(this.btnOpenIgnored_Click);
            // 
            // ckbMute
            // 
            this.ckbMute.AutoSize = true;
            this.ckbMute.Location = new System.Drawing.Point(13, 121);
            this.ckbMute.Name = "ckbMute";
            this.ckbMute.Size = new System.Drawing.Size(111, 17);
            this.ckbMute.TabIndex = 7;
            this.ckbMute.Text = "Mute Notifications";
            this.ckbMute.UseVisualStyleBackColor = true;
            this.ckbMute.CheckedChanged += new System.EventHandler(this.ckbMute_CheckedChanged);
            // 
            // ckbAppID
            // 
            this.ckbAppID.AutoSize = true;
            this.ckbAppID.Location = new System.Drawing.Point(13, 145);
            this.ckbAppID.Name = "ckbAppID";
            this.ckbAppID.Size = new System.Drawing.Size(170, 17);
            this.ckbAppID.TabIndex = 8;
            this.ckbAppID.Text = "Show App ID with notifications";
            this.ckbAppID.UseVisualStyleBackColor = true;
            this.ckbAppID.CheckedChanged += new System.EventHandler(this.ckbAppID_CheckedChanged);
            // 
            // ckbNotifyWhileRunning
            // 
            this.ckbNotifyWhileRunning.AutoSize = true;
            this.ckbNotifyWhileRunning.Location = new System.Drawing.Point(13, 168);
            this.ckbNotifyWhileRunning.Name = "ckbNotifyWhileRunning";
            this.ckbNotifyWhileRunning.Size = new System.Drawing.Size(149, 17);
            this.ckbNotifyWhileRunning.TabIndex = 9;
            this.ckbNotifyWhileRunning.Text = "Notify while app is running";
            this.ckbNotifyWhileRunning.UseVisualStyleBackColor = true;
            // 
            // numNotificationWait
            // 
            this.numNotificationWait.Location = new System.Drawing.Point(13, 192);
            this.numNotificationWait.Name = "numNotificationWait";
            this.numNotificationWait.Size = new System.Drawing.Size(42, 20);
            this.numNotificationWait.TabIndex = 10;
            // 
            // lblNotificationWait
            // 
            this.lblNotificationWait.AutoSize = true;
            this.lblNotificationWait.Location = new System.Drawing.Point(61, 194);
            this.lblNotificationWait.Name = "lblNotificationWait";
            this.lblNotificationWait.Size = new System.Drawing.Size(210, 13);
            this.lblNotificationWait.TabIndex = 11;
            this.lblNotificationWait.Text = "Seconds to wait before sending notification";
            // 
            // btnNotificantionWaitHelp
            // 
            this.btnNotificantionWaitHelp.Location = new System.Drawing.Point(277, 192);
            this.btnNotificantionWaitHelp.Name = "btnNotificantionWaitHelp";
            this.btnNotificantionWaitHelp.Size = new System.Drawing.Size(22, 20);
            this.btnNotificantionWaitHelp.TabIndex = 12;
            this.btnNotificantionWaitHelp.Text = "?";
            this.btnNotificantionWaitHelp.UseVisualStyleBackColor = true;
            this.btnNotificantionWaitHelp.Click += new System.EventHandler(this.BtnNotificantionWaitHelp_Click);
            // 
            // btnSettingsToDefault
            // 
            this.btnSettingsToDefault.Location = new System.Drawing.Point(13, 220);
            this.btnSettingsToDefault.Name = "btnSettingsToDefault";
            this.btnSettingsToDefault.Size = new System.Drawing.Size(319, 23);
            this.btnSettingsToDefault.TabIndex = 13;
            this.btnSettingsToDefault.Text = "Reset to Defaults";
            this.btnSettingsToDefault.UseVisualStyleBackColor = true;
            this.btnSettingsToDefault.Click += new System.EventHandler(this.BtnSettingsToDefault_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 317);
            this.Controls.Add(this.btnSettingsToDefault);
            this.Controls.Add(this.btnNotificantionWaitHelp);
            this.Controls.Add(this.lblNotificationWait);
            this.Controls.Add(this.numNotificationWait);
            this.Controls.Add(this.ckbNotifyWhileRunning);
            this.Controls.Add(this.ckbAppID);
            this.Controls.Add(this.ckbMute);
            this.Controls.Add(this.btnOpenIgnored);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.ckbStartup);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.imgIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "SteamNotifier";
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotificationWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.CheckBox ckbStartup;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnOpenIgnored;
		private System.Windows.Forms.CheckBox ckbMute;
		private System.Windows.Forms.CheckBox ckbAppID;
        private System.Windows.Forms.CheckBox ckbNotifyWhileRunning;
        private System.Windows.Forms.NumericUpDown numNotificationWait;
        private System.Windows.Forms.Label lblNotificationWait;
        private System.Windows.Forms.Button btnNotificantionWaitHelp;
        private System.Windows.Forms.Button btnSettingsToDefault;
    }
}

