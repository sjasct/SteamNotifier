using System;
using System.Reflection;

namespace SteamNotifier.Helpers
{
	public static class Assembly
	{

		public static string Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		
	}
}
