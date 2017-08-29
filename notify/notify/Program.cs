using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using RegistryUtils;
using System.Threading;

namespace SteamNotifier {

    class Program {

        public static NotifyIcon ni = new NotifyIcon();

        public static string logPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\debug.log";

        public static EventWaitHandle waitHandle;

        [STAThread]
        public static void Main()
        {

            // WILL TO THIS AT A LATER DATE

            //ContextMenuStrip menu = new ContextMenuStrip();

            //menu.Items.Add("About");
            //menu.Items.Add("Exit");

            //ni.ContextMenuStrip = menu;

            File.WriteAllText(logPath, String.Empty);

            waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            RegistryMonitor monitor = new RegistryMonitor(RegistryHive.CurrentUser, @"SOFTWARE\Valve\Steam\Apps\");

            monitor.RegChanged += new EventHandler(regChanged);

            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(exit);

            log("Starting logging..");

            try
            {

                monitor.Start();

            }
            catch
            {

                log("FAILED TO START REGISTRY MONITOR");

            }
            finally
            {

                log("Successfully started the registry monitor");
                log("Waiting for registry updates");

            }


            //ni.BalloonTipClicked += new EventHandler(balloonClicked);
            ni.Visible = true;
            ni.Icon = Properties.Resources.icon_bg;


            Console.ReadLine();
            waitHandle.WaitOne();

        }

        public static void regChanged(object sender, EventArgs e)
        {

            log("Registry change detected");
            notify();

        }

        public static void notify()
        {

            log("Notify method called");

            RegistryKey steamKey = null;

            try
            {

                steamKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey(@"Software\Valve\Steam\Apps\");

            }
            catch
            {

                log("FAILED TO RETRIEVE STEAM BASE KEY");

            }
            finally
            {

                log("Steam base key retrieved");

            }

            string appName = (string)updateCheck(steamKey)[0];
            string appId = (string)updateCheck(steamKey)[1];
            bool isUpdating = (bool)updateCheck(steamKey)[2];

            if ((bool)updateCheck(steamKey)[2])
            {

                log("Detected update for " + appName);
                ni.ShowBalloonTip(100, "Steam has started a download", "An update for " + appName + " has started downloading", ToolTipIcon.Info);

            }
            else if (!(bool)updateCheck(steamKey)[2])
            {

                //ni.ShowBalloonTip(100, "Steam has stopped a download", "An update for " + appName + " has stopped downloading", ToolTipIcon.None);

            }

        }

        /*
         * 
         * updateCheck method
         * borrowed from
         * https://github.com/benjibobs/Steam-Shutdown
         * under GNU v3.0 licence
         * 
         * Modified to suit needs
         * 
         */

        static object[] updateCheck(RegistryKey key)
        {

            log("updateCheck method called");

            // 0: App Name
            // 1: App ID
            // 2: Updating Status
            object[] appInfo = { null, null, false };

            log("Checking each sub key from the Steam base key");

            /* http://stackoverflow.com/a/2915990/5893567 */

            foreach (string sub in key.GetSubKeyNames())
            {

                log("Checking sub key " + sub + "..");

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

                    log("Found an updating app");

                    appInfo[2] = true;
                    appInfo[0] = getAppName(appid);
                    appInfo[1] = appid;

                    break;

                }

            }

            return appInfo;

        }

        static string getAppName(string id)
        {

            log("Retrieving name for app " + id);

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

                appName = "Unknown App";

            }
            finally

            {
                log("Retrieved name for app  " + id + " (" + appName + ")");

            }

            return appName;
        }

        static void exit()
        {

            ni.Icon = null;
            ni.Visible = false;
            ni.Dispose();
            Application.Exit();

        }

        public static void log(string message)
        {

            string logMsg = DateTime.Now + " || " + message;

            Console.WriteLine(logMsg);

            string logToFile = logMsg + Environment.NewLine;

            File.AppendAllText(logPath, logToFile);

        }

    }

}
