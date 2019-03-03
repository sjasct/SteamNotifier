using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace SteamNotifier.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

			SetSettingsValues();
		}

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About abtForm = new About();

            abtForm.ShowDialog();
        }

	    private void btnOpenIgnored_Click(object sender, EventArgs e)
	    {
		    new IgnoredApps().ShowDialog();
	    }

		private void SetSettingsValues()
	    {
		    // ensures that changing the checked value upon startup does not trigger the event
		    ckbStartup.CheckedChanged -= ckbStartup_CheckedChanged;
		    ckbMute.CheckedChanged -= ckbMute_CheckedChanged;
		    ckbAppID.CheckedChanged -= ckbAppID_CheckedChanged;
            ckbNotifyWhileRunning.CheckedChanged -= ckbNotifyWhileRunning_CheckedChanged;

		    ckbStartup.Checked = Helpers.Settings.OpenOnStartup;
		    ckbMute.Checked = Helpers.Settings.Muted;
		    ckbAppID.Checked = Helpers.Settings.ShowAppID;
            ckbNotifyWhileRunning.Checked = Helpers.Settings.NotifyWhileRunning;
            numNotificationWait.Value = Helpers.Settings.NotificationWaitTime;

		    ckbStartup.CheckedChanged += ckbStartup_CheckedChanged;
		    ckbMute.CheckedChanged += ckbMute_CheckedChanged;
		    ckbAppID.CheckedChanged += ckbAppID_CheckedChanged;
            ckbNotifyWhileRunning.CheckedChanged += ckbNotifyWhileRunning_CheckedChanged;
            numNotificationWait.ValueChanged += numNotificationWait_ValueChanged;
        }

        private void numNotificationWait_ValueChanged(object sender, EventArgs e)
        {
            Helpers.Settings.NotificationWaitTime = (int)numNotificationWait.Value;
        }

        private void ckbNotifyWhileRunning_CheckedChanged(object sender, EventArgs e)
        {
            Helpers.Settings.NotifyWhileRunning = ckbNotifyWhileRunning.Checked;
        }

        private void ckbStartup_CheckedChanged(object sender, EventArgs e)
        {
	        Helpers.Settings.OpenOnStartup = ckbStartup.Checked;
        }

		private void ckbMute_CheckedChanged(object sender, EventArgs e)
		{
			Helpers.Settings.Muted = ckbMute.Checked;
		}

		private void ckbAppID_CheckedChanged(object sender, EventArgs e)
		{
			Helpers.Settings.ShowAppID = ckbAppID.Checked;
		}

        private void BtnNotificantionWaitHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Having this set to a value between 3-6 can prevent 'ghost' notifications when Steam starts up." +
                "\n\nHowever, having this too high can stop notifications for small updates.", 
                "Information - Notification wait time setting", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}