using System.Diagnostics;
using System.Windows.Forms;
using SteamNotifier;

namespace SteamNotifier.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void lblDevLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://samjas.co.uk");
        }

        private void lblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://github.com/avinch/steamnotifier");
        }

        private void lblLicenceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Avinch/SteamNotifier/blob/master/LICENSE");
        }

        private void lblVersion_Click(object sender, System.EventArgs e)
        {

        }

		private void About_Load(object sender, System.EventArgs e)
		{
			lblVersion.Text = $"v{Helpers.Assembly.Version}";
		}
	}
}