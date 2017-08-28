using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SteamNotifier {
    class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {

            NotifyIcon ni = new NotifyIcon();

            ni.Visible = true;
            ni.Icon = Properties.Resources.icon;


            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            start(ni);

        }

        static void start(NotifyIcon ni)
        {

            /* 
             * 
             * this is a REALLY BAD solution
             * and by really bad i mean the worst solution
             * future me will probably regret this
             * in fact i regret this now
             * 
             * TODO: fix this piece of shit
             *
             */

            /*
             * 
             * Restarting itself every 10 mins
             * since I can't figure out how to
             * constantly check registry without
             * having loads of memory and CPU
             * usage taken
             * 
             */

            int count = 0;

            while (true)
            {

                if (count >= 30)
                {
                    exit(ni);
                    break;
                }
                else
                {
                    count += 1;
                    notify(ni);
                }

                System.Threading.Thread.Sleep(10000);
            }

        }

        

        public static void notify(NotifyIcon ni)
        {

            RegistryKey steamKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey(@"SOFTWARE\Valve\Steam\Apps\");

            string appName = (string)updateCheck(steamKey)[0];
            string appId = (string)updateCheck(steamKey)[1];
            bool isUpdating = (bool)updateCheck(steamKey)[2];

            if ((bool)updateCheck(steamKey)[2] && Properties.Settings.Default.notified == false)
            {
                ni.ShowBalloonTip(100, "Steam has started a download", "An update for " + appName + " has started downloading", ToolTipIcon.Info);
                Properties.Settings.Default.notified = true;
            }
            else if (!(bool)updateCheck(steamKey)[2] && Properties.Settings.Default.notified == true)
            {
                Properties.Settings.Default.notified = false;
                //ni.ShowBalloonTip(100, "Check Occurred", "Steam has been checked but nothing is downloading at the moment.", ToolTipIcon.None);
            }
        }


        /*
         * 
         * updateCheck method
         * borrowed from
         * https://github.com/benjibobs/Steam-Shutdown
         * under GNU v3.0 licence
         * 
         * Changes to method referenced
         * by a commented $
         * 
         */

        static object[] updateCheck(RegistryKey key)
        {


            // 0: Game name
            // 1: App ID
            // 2: Updating Status
            object[] appInfo = { null, null, false };

            /* http://stackoverflow.com/a/2915990/5893567 */
            foreach (string sub in key.GetSubKeyNames())
            {

                RegistryKey local = Registry.Users;
                local = key.OpenSubKey(sub, true);

                string[] splitLocalName = local.Name.Split('\\');

                string appid = splitLocalName.Last();

                object updating = local.GetValue("Updating");
                

                /*
                * 232330 = CS:GO Server
                * 
                * Showing as updating in
                * registry but I don't
                * even have access to 
                * it on Steam
                * 
                * ¯\_(ツ)_/¯
                */

                if (updating != null && (int)updating == 1 && appid != "232330")
                {
                    
                    appInfo[0] = getAppName(appid);
                    appInfo[1] = appid;
                    appInfo[2] = true; 

                }

            }

            return appInfo;

        }

        static string getAppName(string id)
        {
            string appName = "Unknown App";

            string requestURL = "http://store.steampowered.com/api/appdetails?appids=" + id;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);

            WebResponse response = null;

            try
            {
                response = request.GetResponse();
            }
            catch (System.Net.WebException)
            {
                // idk
            }

            Stream respStream = response.GetResponseStream();

            StreamReader read = new StreamReader(respStream);

            string contents = read.ReadToEnd();

            dynamic jsonContents = JObject.Parse(contents);

            try
            {
                appName = jsonContents[id]["data"]["name"];
            }
            catch
            {
                // idk
            }

            return appName;
        }

        static void exit(NotifyIcon ni)
        {
            ni.Icon = null;
            ni.Visible = false;
            ni.Dispose();
            Application.Restart();
        }

    }

}
