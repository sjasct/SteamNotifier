using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamNotifier.Properties;

namespace SteamNotifier.Helpers
{
	static class TrayIcon
	{

		private static NotifyIcon _trayIcon;

		private static ContextMenu _trayIconMenu;

		private static MenuItem _trayMenuItemMute;
		private static MenuItem _trayMenuItemAbout;
		private static MenuItem _trayMenuItemExit;

		private static NotifyIcon Icon
		{
			get
			{
				if (_trayIcon != null)
				{
					return _trayIcon;
				}
				else
				{
					throw new NullReferenceException("Tray Icon has not been created");
				}
			}
			set { _trayIcon = value; }
		}

		private static ContextMenu Menu
		{
			get
			{
				if (_trayIconMenu != null)
				{
					return _trayIconMenu;
				}
				else
				{
					throw new NullReferenceException("Tray Icon has not been created");
				}
			}
			set { _trayIconMenu = value; }
		}

		private static MenuItem Mute
		{
			get
			{
				if (_trayMenuItemMute != null)
				{
					return _trayMenuItemMute;
				}
				else
				{
					throw new NullReferenceException("Tray Icon has not been created");
				}
			}
			set { _trayMenuItemMute = value; }
		}

		private static MenuItem Exit
		{
			get
			{
				if (_trayMenuItemExit != null)
				{
					return _trayMenuItemExit;
				}
				else
				{
					throw new NullReferenceException("Tray Icon has not been created");
				}
			}
			set { _trayMenuItemExit = value; }
		}

		private static MenuItem About
		{
			get
			{
				if (_trayMenuItemAbout != null)
				{
					return _trayMenuItemAbout;
				}
				else
				{
					throw new NullReferenceException("Tray Icon has not been created");
				}
			}
			set { _trayMenuItemAbout = value; }
		}

		private static void TrayMenuClick_Exit(object sender, EventArgs e)
		{
			Assembly.ExitApplication();
		}

		private static void TrayMenuClick_About(object sender, EventArgs e)
		{
			new Forms.Settings().Show();
		}

		private static void TrayMenuClick_Mute(object sender, EventArgs e)
		{
			Settings.ToggleMute();
		}

		public static void Create()
		{
			Thread trayIconThread = new Thread(

				delegate ()
				{
					string muteText = Settings.Muted ? "Unmute" : "Mute";

					Mute = new MenuItem($"{muteText} Notifications");
					Mute.Click += TrayMenuClick_Mute;

					About = new MenuItem("Settings");
					About.Click += TrayMenuClick_About;

					Exit = new MenuItem("Exit");
					Exit.Click += TrayMenuClick_Exit;

					Menu = new ContextMenu();
					Menu.MenuItems.Add(0, Mute);
					Menu.MenuItems.Add(1, About);
					Menu.MenuItems.Add(2, Exit);

					Icon = new NotifyIcon();
					Icon.Icon = Resources.Icon_CircleBG_ICO;
					Icon.Text = "SteamNotifier";
					Icon.ContextMenu = Menu;

					Icon.Visible = true;
					Application.Run();

				}

			);

			trayIconThread.Start();
		}

		public static void Destroy()
		{
			Icon.Icon = null;
			Icon.Visible = false;
			Icon.Dispose();
		}

		public static void SendNotification(string title, string message)
		{
			Icon.ShowBalloonTip(100, title, message, ToolTipIcon.Info);
		}

		public static void UpdateMuteMenuItem()
		{
			string muteText = Settings.Muted ? "Unmute" : "Mute";
			Mute.Text = $"{muteText} Notifications";
		}

	}
}
