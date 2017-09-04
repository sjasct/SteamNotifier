namespace SteamNotifierHelper {
    partial class About {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblTagline = new System.Windows.Forms.Label();
            this.lblGitHub = new System.Windows.Forms.LinkLabel();
            this.lblDevNotice = new System.Windows.Forms.Label();
            this.lblDevLink = new System.Windows.Forms.LinkLabel();
            this.lblLicenceNotice = new System.Windows.Forms.Label();
            this.lblLicenceLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(93, 50);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(59, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "version 1.2";
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.Location = new System.Drawing.Point(91, 21);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(160, 29);
            this.lblHead.TabIndex = 4;
            this.lblHead.Text = "SteamNotifier";
            // 
            // imgIcon
            // 
            this.imgIcon.Image = global::SteamNotifierHelper.Properties.Resources.icon_bg2;
            this.imgIcon.Location = new System.Drawing.Point(12, 12);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(73, 67);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgIcon.TabIndex = 3;
            this.imgIcon.TabStop = false;
            // 
            // lblTagline
            // 
            this.lblTagline.Location = new System.Drawing.Point(12, 91);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(260, 36);
            this.lblTagline.TabIndex = 6;
            this.lblTagline.Text = "Lightweight background program to notify you when Steam has started downloading s" +
    "omething\r\n";
            this.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGitHub
            // 
            this.lblGitHub.Location = new System.Drawing.Point(12, 127);
            this.lblGitHub.Name = "lblGitHub";
            this.lblGitHub.Size = new System.Drawing.Size(260, 19);
            this.lblGitHub.TabIndex = 7;
            this.lblGitHub.TabStop = true;
            this.lblGitHub.Text = "View on GitHub";
            this.lblGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblGitHub_LinkClicked);
            // 
            // lblDevNotice
            // 
            this.lblDevNotice.Location = new System.Drawing.Point(12, 161);
            this.lblDevNotice.Name = "lblDevNotice";
            this.lblDevNotice.Size = new System.Drawing.Size(173, 23);
            this.lblDevNotice.TabIndex = 8;
            this.lblDevNotice.Text = "Developed by Sam Scott";
            this.lblDevNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDevLink
            // 
            this.lblDevLink.Location = new System.Drawing.Point(191, 160);
            this.lblDevLink.Name = "lblDevLink";
            this.lblDevLink.Size = new System.Drawing.Size(81, 23);
            this.lblDevLink.TabIndex = 9;
            this.lblDevLink.TabStop = true;
            this.lblDevLink.Text = "samjas.co.uk";
            this.lblDevLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDevLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDevLink_LinkClicked);
            // 
            // lblLicenceNotice
            // 
            this.lblLicenceNotice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenceNotice.Location = new System.Drawing.Point(12, 193);
            this.lblLicenceNotice.Name = "lblLicenceNotice";
            this.lblLicenceNotice.Size = new System.Drawing.Size(173, 36);
            this.lblLicenceNotice.TabIndex = 10;
            this.lblLicenceNotice.Text = "Distributed under the GNU v3.0 Licence\r\n";
            this.lblLicenceNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLicenceLink
            // 
            this.lblLicenceLink.Location = new System.Drawing.Point(191, 193);
            this.lblLicenceLink.Name = "lblLicenceLink";
            this.lblLicenceLink.Size = new System.Drawing.Size(81, 23);
            this.lblLicenceLink.TabIndex = 11;
            this.lblLicenceLink.TabStop = true;
            this.lblLicenceLink.Text = "Learn More";
            this.lblLicenceLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLicenceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLicenceLink_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 248);
            this.Controls.Add(this.lblLicenceLink);
            this.Controls.Add(this.lblLicenceNotice);
            this.Controls.Add(this.lblDevLink);
            this.Controls.Add(this.lblDevNotice);
            this.Controls.Add(this.lblGitHub);
            this.Controls.Add(this.lblTagline);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.imgIcon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.Text = "About Steam Notifier";
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.LinkLabel lblGitHub;
        private System.Windows.Forms.Label lblDevNotice;
        private System.Windows.Forms.LinkLabel lblDevLink;
        private System.Windows.Forms.Label lblLicenceNotice;
        private System.Windows.Forms.LinkLabel lblLicenceLink;
    }
}