using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SN_Utility {
    public partial class About : Form {
        public About()
        {
            InitializeComponent();
        }

        private void lblDevLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://samjas.co.uk");
        }

        private void lblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/avinch/steamnotifier");
        }

        private void lblLicenceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Avinch/SteamNotifier/blob/master/LICENSE");
        }
    }
}
