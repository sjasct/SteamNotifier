using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamNotifier.Helpers
{
	public static class Settings
	{

		public static bool Muted
		{
			get { return Properties.Settings.Default.Muted; }
			set
			{
				Properties.Settings.Default.Muted = value;
				Properties.Settings.Default.Save();
			}
		}

	}
}
