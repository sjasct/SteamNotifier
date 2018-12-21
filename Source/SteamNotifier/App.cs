using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SteamNotifier
{
	class App
	{

		private int _id;
		private string _name;

		public App(int id)
		{

			_id = id;
			_name = null;

		}

		public int ID
		{
			get { return _id; }
		}

		public string Name
		{
			get
			{
				if (_name == null)
				{
					_name = GetAppName();
				}

				return _name;
			}
		}

		public bool Updating
		{
			get { return CheckIfUpdating(); }
		}

		public bool Ignored
		{
			get { return CheckIfIgnored(); }
		}

		public string GetAppName()
		{
			return AppUtils.GetAppName(ID);
		}

		public bool CheckIfUpdating()
		{
			return AppUtils.CheckIfUpdating(ID);
		}

		public bool CheckIfIgnored()
		{
			return AppUtils.CheckIfIgnored(ID);
		}

	}
}
