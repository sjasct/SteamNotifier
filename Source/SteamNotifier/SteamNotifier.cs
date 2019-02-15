using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using RegistryUtils;
using SteamNotifier.Helpers;
using SteamNotifier.Properties;
using SteamNotifier.Forms;
using SNSettings = SteamNotifier.Helpers.Settings;

namespace SteamNotifier
{
	static class SteamNotifier
	{

		private static List<App> _apps;

		private static RegistryMonitor _registryMonitor;

		private static string _steamRegistrySubKey = @"SOFTWARE\Valve\Steam\Apps\";

		private static RegistryKey _steamKey;

		public static EventWaitHandle WaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

		public static void Main()
		{
			
			_registryMonitor = new RegistryMonitor(RegistryHive.CurrentUser, _steamRegistrySubKey);
			_registryMonitor.RegChanged += RegistryUpdated;

			try
			{
				_registryMonitor.Start();
			}
			catch (Exception ex)
			{
				Logger.Instance.Error("Failed to start monitoring registry");
				Logger.Instance.Exception(ex);
				Application.Exit();
			}
			finally
			{
				Logger.Instance.Info("Waiting for registry updates");
			}

			_apps = new List<App>();
			LoadApps();

			TrayIcon.Create();

			WaitHandle.WaitOne();
		}


		private static void LoadApps()
		{
			_apps.Clear();
			RefreshSteamBaseKey();

			foreach (string subKeyName in _steamKey.GetSubKeyNames())
			{

				int appID = Convert.ToInt32(subKeyName.Split('\\').Last());

				_apps.Add(new App(appID));
			}

		}

		private static void RegistryUpdated(object sender, EventArgs e)
		{

			if (SNSettings.Muted)
			{
				return;
			}

			if (_steamKey.SubKeyCount != _apps.Count)
			{
				LoadApps();
			}

			IEnumerable<App> updatingApps = _apps.Where(x => x.IsUpdating == true);

			foreach (var app in updatingApps)
			{
                if (app.IsRunning)
                {
                    Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be updating but app is also running, not sending notification");
                }
                if (app.IsIgnored)
				{
                    Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be updating but user has ignored this app, not sending notification");
                }
                else
				{
                    Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be updating, confirming if still updating in 3 seconds before sending notification");
                    QueueApp(app);
                }
			}

		}

	    private static void QueueApp(App app)
	    {
	        new Thread(() =>
	            {
	                Thread.CurrentThread.IsBackground = true;

	                Thread.Sleep(3000);

	                if (app.IsUpdating)
	                {
	                    Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be updating after 3 seconds, sending notification");
	                    string appID = SNSettings.ShowAppID ? $" ({app.ID})" : "";
	                    TrayIcon.SendNotification("Steam has started a download", $"An update for {app.Name}{appID} has started downloading");
                    }
	                else
	                {
                        Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be a false update, not sending notification");
	                }
                }
	        ).Start();

	    }

		private static void RefreshSteamBaseKey()
		{
			try
			{
				_steamKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default)
					.OpenSubKey(_steamRegistrySubKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.Error("Failed to refresh Steam base key");
				Logger.Instance.Exception(ex);
			}
		}
	}
}
