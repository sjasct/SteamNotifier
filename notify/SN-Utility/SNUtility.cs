using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using IO = System.IO;
using System.Diagnostics;

namespace SN_Utility {
    public partial class SNUtility : Form {

        public WshShell shell;

        public static string shortcutName = "SteamNotifier";
        public string shortcutTargetExe = "SteamNotifier";

        public string scPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + shortcutName + ".lnk";

        public SNUtility()
        {
            InitializeComponent();


            // ensures that changing the checked value upon startup does not trigger the event
            ckbStartup.CheckedChanged -= ckbStartup_CheckedChanged;

            if (IO.File.Exists(scPath))
            {
                ckbStartup.Checked = true;
            }
            else
            {
                ckbStartup.Checked = false;
            }

            ckbStartup.CheckedChanged += ckbStartup_CheckedChanged;

        }

        private void ckbStartup_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbStartup.Checked == true)
            { 

                string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup)+ "\\"+ shortcutName + ".lnk";

                string targetExe = AppDomain.CurrentDomain.BaseDirectory + shortcutTargetExe + ".exe";

                shell = new WshShell();

                IWshShortcut shortcut = shell.CreateShortcut(startupPath);
                shortcut.Description = "SteamNotifier";
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.TargetPath = targetExe;
                shortcut.Save();

            }
            else if(ckbStartup.Checked == false)
            {
                
                if (IO.File.Exists(scPath))
                {
                    IO.File.Delete(scPath);
                }
                else
                {
                    MessageBox.Show("Could not remove SteamNotifier from startup!\n\nTry to see if you can find the shortcut in: \n" + Environment.GetFolderPath(Environment.SpecialFolder.Startup));
                }

            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About abtForm = new About();

            abtForm.ShowDialog();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            Process[] processes = Process.GetProcessesByName("SteamNotifier");
     
            if(processes.Length > 0)
            {
                Process snProcess = processes[0];

                try { 
                    snProcess.Kill();
               }
                catch
                {
                    MessageBox.Show("Failed to stop SteamNotifier!");
                }

            }
            else
            {

                MessageBox.Show("The SteamNotifier background process is not running!");

            }

        }
    }
}
