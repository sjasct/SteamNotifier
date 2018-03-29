using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryUtils;
using SteamNotifier.Helpers;
using SteamNotifier.Properties;

namespace SteamNotifier
{
    internal class Program
    {
        public static NotifyIcon Ni = new NotifyIcon();

        static string[] IgnoredAppIDs;

        public static EventWaitHandle WaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        [STAThread]
        public static void Main()
        {
            
            Ni.Click += TrayIconOnClick;

            RegistryMonitor monitor = new RegistryMonitor(RegistryHive.CurrentUser, @"SOFTWARE\Valve\Steam\Apps\");

            monitor.RegChanged += RegChanged;

            IgnoredAppIDs = LoadIgnored();


            try
            {
                monitor.Start();
            }
            catch
            {
                Logger.Instance.Info("FAILED TO START REGISTRY MONITOR");
            }
            finally
            {
                Logger.Instance.Info("Successfully started the registry monitor");
                Logger.Instance.Info("Waiting for registry updates");
            }

            Ni.Visible = true;
            Ni.Icon = Resources.icon_bg;

            Console.ReadLine();
            WaitHandle.WaitOne();
        }

        public static void Notify()
        {
            Logger.Instance.Info("Notify method called");

            RegistryKey steamKey = null;

            try
            {
                steamKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey(@"Software\Valve\Steam\Apps\");
            }
            catch (Exception e)
            {
                Logger.Instance.Error("FAILED TO RETRIEVE STEAM BASE KEY");
                Logger.Instance.Exception(e);
            }
            finally
            {
                Logger.Instance.Info("Steam base key retrieved");
            }

            AppInfo updatingApp = UpdateCheck(steamKey);
            string appName = updatingApp.AppName;
            string appId = updatingApp.AppId;
            bool isUpdating = updatingApp.UpdatingStatus;
            
            if (!isUpdating)
            {
                return;
            }
            if (CheckIfIgnored(appId))
            {
                return;
            }

            Logger.Instance.Info("Detected update for {0} ({1})", appName, appId);
            Ni.ShowBalloonTip(100, "Steam has started a download", "An update for " + appName + " has started downloading", ToolTipIcon.Info);
        }

        public static void RegChanged(object sender, EventArgs e)
        {
            Logger.Instance.Info("Registry change detected");
            Notify();
        }

        public static void TrayIconOnClick(object sender, EventArgs e)
        {
            Logger.Instance.Info("Attempting to launch utility executable");

            try
            {
                Process.Start("SteamNotifierHelper.exe");
                Logger.Instance.Info("Utility executable launched..");
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Ni.ShowBalloonTip(100, "Could not launch settings!", "Sorry, but the settings executable could not be found. Check the debug.log file for more info", ToolTipIcon.Info);
                Logger.Instance.Error("Failed to find and launch the utility executable");
                Logger.Instance.Exception(fileNotFoundException);
            }
            catch (Exception ex)
            {
                Ni.ShowBalloonTip(100, "Could not launch settings!", "Sorry, but the settings executable could not be launched. Check the debug.log file for more info", ToolTipIcon.Info);
                Logger.Instance.Error("Failed to launch the utility executable");
                Logger.Instance.Exception(ex);
            }
        }

        private static void Exit()
        {
            Ni.Icon = null;
            Ni.Visible = false;
            Ni.Dispose();
            Logger.Instance.Dispose();
            Application.Exit();
        }

        private static string[] LoadIgnored()
        {
            Logger.Instance.Info("Loading ignored App IDs");
            using (StreamReader Reader = new StreamReader("ignoredappids.txt"))
            {
                string UnsplitList = Reader.ReadLine();
                Logger.Instance.Info("Ignored App IDs: {0}", UnsplitList);
                try
                {
                    return UnsplitList.Split(',');
                }
                catch
                {
                    return new string[] { "" };
                }
            };

            
        }

        private static bool CheckIfIgnored(string AppID)
        {

            if (IgnoredAppIDs.Contains(AppID))
            {
                Logger.Instance.Info(String.Format("{0} was found to be updating but is set as ignored", AppID));
                return true;
            }
            else
            {
                return false;
            }
            
        
        }

        private static string GetAppName(string id)
        {
            Logger.Instance.Info("Retrieving name for app {0}", id);

            string appName = "Unknown App";
            string requestUrl = string.Format(CultureInfo.InvariantCulture, "http://store.steampowered.com/api/appdetails?appids={0}", id);
            string contents = string.Empty;

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    contents = webClient.DownloadString(requestUrl);
                }
            }
            catch (WebException webException)
            {
                // TODO: properly handle
            }
            catch (Exception e)
            {
                Logger.Instance.Exception(e);
            }

            try
            {
                dynamic jsonContents = JObject.Parse(contents);
                appName = jsonContents[id]["data"]["name"];
            }
            catch (JsonReaderException jsonReaderException)
            {
                Logger.Instance.Exception(jsonReaderException);
            }
            catch (Exception e)
            {
                Logger.Instance.Exception(e);
                appName = "Unknown App";
            }
            finally
            {
                Logger.Instance.Info("Retrieved name for app  " + id + " (" + appName + ")");
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

        private static AppInfo UpdateCheck(RegistryKey key)
        {
            Logger.Instance.Info("updateCheck method called");

            AppInfo appInfo = new AppInfo();

            Logger.Instance.Info("Checking each sub key from the Steam base key");

            /* http://stackoverflow.com/a/2915990/5893567 */

            //Task.Run(() =>
            //{
                Parallel.ForEach(key.GetSubKeyNames(), (sub, loopState) =>
                {
                    Logger.Instance.Info("Checking sub key " + sub + "..");

                    RegistryKey local = key.OpenSubKey(sub, true);

                    if (local == null)
                    {
                        return;
                    }

                    string[] splitLocalName = local.Name.Split('\\');

                    string appid = splitLocalName.Last();

                    object updating = local.GetValue("Updating");

                    if (updating == null || (int) updating != 1)
                    {
                        return;
                    }

                    Logger.Instance.Info("Found an updating app");

                    appInfo = new AppInfo { AppId = appid, UpdatingStatus = true, AppName = GetAppName(appid) };

                    loopState.Break();
                    
                });
            //});

            return appInfo;
        }

        private struct AppInfo
        {
            public string AppName;
            public string AppId;
            public bool UpdatingStatus;
        }
    }
}