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

namespace SteamNotifier
{
	static class SteamNotifier
	{

		private static List<App> _apps;

		private static RegistryMonitor _registryMonitor;

		private static string _steamRegistrySubKey = @"SOFTWARE\Valve\Steam\Apps\";

		private static RegistryKey _steamKey;

		private static NotifyIcon _trayIcon;

		private static ContextMenu _trayIconMenu;
		private static MenuItem _trayMenuItemMute;
		private static MenuItem _trayMenuItemAbout;
		private static MenuItem _trayMenuItemExit;

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
				Logger.Instance.Info("Failed to start monitoring registry");
				Logger.Instance.Exception(ex);
				Application.Exit();
			}
			finally
			{
				Logger.Instance.Info("Waiting for registry updates");
			}

			_apps = new List<App>();
			LoadApps();

			InitTrayIcon();

			WaitHandle.WaitOne();
		}

		public static bool Muted
		{
			get { return Properties.Settings.Default.muted; }
			set
			{
				Properties.Settings.Default.muted = value;
				Properties.Settings.Default.Save();
			}
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

			if (Muted)
			{
				return;
			}

			if (_steamKey.SubKeyCount != _apps.Count)
			{
				LoadApps();
			}

			IEnumerable<App> updatingApps = _apps.Where(x => x.Updating == true);

			foreach (var app in updatingApps)
			{
				if (app.Ignored == false)
				{
					Logger.Instance.Info($"Notification: {app.Name} (ID: {app.ID}) found to be updating");
					_trayIcon.ShowBalloonTip(100, "Steam has started a download", $"An update for {app.Name} ({app.ID}) has started downloading", ToolTipIcon.Info);
				}
				else
				{
					Logger.Instance.Info($"{app.Name} (ID: {app.ID}) found to be updating but ignored");
				}
			}

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

		private static void InitTrayIcon()
		{
			Thread trayIconThread = new Thread(

				delegate()
				{
					string muteText = Muted ? "Unmute" : "Mute";
					
					_trayMenuItemMute = new MenuItem($"{muteText} Notifications");
					_trayMenuItemMute.Click += TrayMenuClick_Mute;

					_trayMenuItemAbout = new MenuItem("Settings");
					_trayMenuItemAbout.Click += TrayMenuClick_About;

					_trayMenuItemExit = new MenuItem("Exit");
					_trayMenuItemExit.Click += TrayMenuClick_Exit;

					_trayIconMenu = new ContextMenu();
					_trayIconMenu.MenuItems.Add(0, _trayMenuItemMute);
					_trayIconMenu.MenuItems.Add(1, _trayMenuItemAbout);
					_trayIconMenu.MenuItems.Add(2, _trayMenuItemExit);

					_trayIcon = new NotifyIcon();
					_trayIcon.Icon = Resources.icon_bg;
					_trayIcon.Text = "SteamNotifier";
					_trayIcon.ContextMenu = _trayIconMenu;

					_trayIcon.Visible = true;
					Application.Run();

				}

				);

			trayIconThread.Start();
		}

		private static void TrayMenuClick_Exit(object sender, EventArgs e)
		{
			Exit();
		}

		private static void TrayMenuClick_About(object sender, EventArgs e)
		{
			Logger.Instance.Info("Attempting to launch utility executable");

			try
			{
				Process.Start("SteamNotifierHelper.exe");
				Logger.Instance.Info("Utility executable launched..");
			}
			catch (FileNotFoundException fileNotFoundException)
			{
				_trayIcon.ShowBalloonTip(100, "Could not launch settings!", "Sorry, but the settings executable could not be found. Check the debug.log file for more info", ToolTipIcon.Info);
				Logger.Instance.Error("Failed to find and launch the utility executable");
				Logger.Instance.Exception(fileNotFoundException);
			}
			catch (Exception ex)
			{
				_trayIcon.ShowBalloonTip(100, "Could not launch settings!", "Sorry, but the settings executable could not be launched. Check the debug.log file for more info", ToolTipIcon.Info);
				Logger.Instance.Error("Failed to launch the utility executable");
				Logger.Instance.Exception(ex);
			}
		}

		private static void TrayMenuClick_Mute(object sender, EventArgs e)
		{
			string muteText = Muted ? "Mute" : "Unmute";

			if (Muted)
			{
				Muted = false;
				Logger.Instance.Info("Notifications unmuted");
			}
			else
			{
				Muted = true;
				Logger.Instance.Info("Notifications muted");
			}

			_trayMenuItemMute.Text = $"{muteText} Notifications";

		}

		private static void Exit()
		{
			_trayIcon.Icon = null;
			_trayIcon.Visible = false;
			_trayIcon.Dispose();
			Logger.Instance.Dispose();
			Environment.Exit(0);
		}
	}
}
