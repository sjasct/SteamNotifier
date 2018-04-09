using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamNotifierHelper
{
    public partial class frmIgnore : Form
    {

        static string IgnoredFile = "ignoredappids.txt";

        public frmIgnore()
        {
            InitializeComponent();
        }

        private void frmIgnore_Load(object sender, EventArgs e)
        {
            txtContents.Text = LoadIgnored();
        }

        private static string LoadIgnored()
        {
            using (StreamReader Reader = new StreamReader(IgnoredFile))
            {
                return Reader.ReadLine();
            };
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            var regex = new Regex(@"^([0-9]+,)*[0-9]+$");

            if(regex.IsMatch(txtContents.Text) || string.IsNullOrEmpty(txtContents.Text))
            {
                using (StreamWriter Writer = new StreamWriter(IgnoredFile, false))
                {
                    Writer.Write(txtContents.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("AppIDs must be followed by a comma e.g. 1234,5678,901", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
