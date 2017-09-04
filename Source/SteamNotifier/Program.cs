using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using RegistryUtils;
using SteamNotifier.Properties;

namespace SteamNotifier
{
    internal class Program
    {
        public static NotifyIcon ni = new NotifyIcon();

        public static string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string logPath = currentPath + "\\debug.log";

        public static EventWaitHandle waitHandle;

        public static void log(string message)
        {
            string logMsg = DateTime.Now + " || " + message;

            Console.WriteLine(logMsg);

            string logToFile = logMsg + Environment.NewLine;

            File.AppendAllText(logPath, logToFile);
        }

        [STAThread]
        public static void Main()
        {
            // WILL TO THIS AT A LATER DATE

            //ContextMenuStrip menu = new ContextMenuStrip();

            //menu.Items.Add("About");
            //menu.Items.Add("Exit");

            //ni.ContextMenuStrip = menu;

            ni.Click += trayiconClick;

            File.WriteAllText(logPath, string.Empty);

            waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            RegistryMonitor monitor = new RegistryMonitor(RegistryHive.CurrentUser, @"SOFTWARE\Valve\Steam\Apps\");

            monitor.RegChanged += regChanged;

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
            ni.Icon = Resources.icon_bg;

            Console.ReadLine();
            waitHandle.WaitOne();
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

            string appName = (string) updateCheck(steamKey)[0];
            string appId = (string) updateCheck(steamKey)[1];
            bool isUpdating = (bool) updateCheck(steamKey)[2];

            if ((bool) updateCheck(steamKey)[2])
            {
                log("Detected update for " + appName);
                ni.ShowBalloonTip(100, "Steam has started a download", "An update for " + appName + " has started downloading", ToolTipIcon.Info);
            }
            else if (!(bool) updateCheck(steamKey)[2])
            {
                //ni.ShowBalloonTip(100, "Steam has stopped a download", "An update for " + appName + " has stopped downloading", ToolTipIcon.None);
            }
        }

        public static void regChanged(object sender, EventArgs e)
        {
            log("Registry change detected");
            notify();
        }

        public static void trayiconClick(object sender, EventArgs e)
        {
            log("Attempting to launch utility executable");

            try
            {
                Process.Start("SteamNotifierHelper.exe");
                log("Utility executable launched..");
            }
            catch (Exception ex)
            {
                ni.ShowBalloonTip(100, "Could not launch settings!", "Sorry, but the settings executable could not be launched. Check the debug.log file for more info", ToolTipIcon.Info);
                log("FAILED TO LAUNCH UTILITY EXECUTABLE");
                log("DETAILS:" + ex);
            }
        }

        private static void exit()
        {
            ni.Icon = null;
            ni.Visible = false;
            ni.Dispose();
            Application.Exit();
        }

        private static string getAppName(string id)
        {
            log("Retrieving name for app " + id);

            string appName = "Unknown App";

            string requestURL = "http://store.steampowered.com/api/appdetails?appids=" + id;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestURL);

            WebResponse response = null;

            try
            {
                response = request.GetResponse();
            }
            catch (WebException)
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

        private static object[] updateCheck(RegistryKey key)
        {
            log("updateCheck method called");

            // 0: App Name
            // 1: App ID
            // 2: Updating Status
            object[] appInfo = {null, null, false};

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

                if (updating != null && (int) updating == 1 && appid != "232330")
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
    }
}