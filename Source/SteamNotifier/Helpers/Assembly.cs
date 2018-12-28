using System;
using System.Reflection;

namespace SteamNotifier.Helpers
{
	public static class Assembly
	{

		public static string Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public static void ExitApplication()
		{
			TrayIcon.Destroy();
			Logger.Instance.Dispose();
			Environment.Exit(0);
		}
		
	}
}
