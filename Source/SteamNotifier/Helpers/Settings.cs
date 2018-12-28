using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace SteamNotifier.Helpers
{
	public static class Settings
	{

		private static string _shortcutName = "SteamNotifier";
		private static string _shortcutTargetExe = "SteamNotifier";
		private static string _startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + _shortcutName + ".lnk";

		public static bool Muted
		{
			get { return Properties.Settings.Default.Muted; }
			set
			{
				Properties.Settings.Default.Muted = value;
				Save();
			}
		}

		public static bool ShowAppID
		{
			get { return Properties.Settings.Default.ShowAppID; }
			set
			{
				Properties.Settings.Default.ShowAppID = value;
				Save();
			}
		}

		public static bool OpenOnStartup
		{
			get
			{ 
				return System.IO.File.Exists(_startupPath);
			}
			set
			{
				if (value)
				{
					string targetExe = AppDomain.CurrentDomain.BaseDirectory + _shortcutTargetExe + ".exe";

					WshShell Shell = new WshShell();

					IWshShortcut shortcut = Shell.CreateShortcut(_startupPath);
					shortcut.Description = "SteamNotifier";
					shortcut.WorkingDirectory = Location.CurrentFolder;
					shortcut.TargetPath = targetExe;
					shortcut.Save();
				}
				else
				{
					if (System.IO.File.Exists(_startupPath))
					{
						System.IO.File.Delete(_startupPath);
					}
				}
			}
		}

		public static void Save()
		{
			Properties.Settings.Default.Save();
		}

	}
}
