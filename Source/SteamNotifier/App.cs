using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamNotifier.Helpers;

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
					_name = GetAppName(ID);
				}

				return _name;
			}
		}

		public bool Updating
		{
			get { return CheckIfUpdating(ID); }
		}

		public bool Ignored
		{
			get { return CheckIfIgnored(ID); }
		}

        #region Static Members

        private static List<string> ignoredAppIDs;

        public static string GetAppName(int id)
        {

            string appName = "Unknown App";

            string requestUrl = string.Format(CultureInfo.InvariantCulture,
                "https://store.steampowered.com/api/appdetails?appids={0}", id);

            var requestContents = string.Empty;

            const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
            const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
            ServicePointManager.SecurityProtocol = Tls12;

            var webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            webRequest.Method = "GET";
            WebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                Logger.Instance.Error("Failed to retrieve app name, defaulting to Unknown App");
                Logger.Instance.Exception(ex);
                return appName;
            }

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                requestContents = streamReader.ReadToEnd();
            }

            try
            {
                dynamic jsonContents = JObject.Parse(requestContents);
                appName = jsonContents[id.ToString()]["data"]["name"];
            }
            catch (JsonReaderException jsonEx)
            {
                Logger.Instance.Error("Failed to read app name");
                Logger.Instance.Exception(jsonEx);
            }
            catch (Exception ex)
            {
                Logger.Instance.Exception(ex);
            }

            return appName;
        }

        public static bool CheckIfUpdating(int id)
        {
            RegistryKey steamKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey(@"Software\Valve\Steam\Apps\");
            RegistryKey appKey = steamKey.OpenSubKey(id.ToString(), true);

            if (appKey == null)
            {
                return false;
            }

            object updatingValue = appKey.GetValue("Updating");

            if (updatingValue == null || (int)updatingValue != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckIfIgnored(int id)
        {
            if (ignoredAppIDs == null)
            {
                LoadIgnoredList();
            }

            return (ignoredAppIDs.Count(x => x == id.ToString()) > 0);

        }

        private static void LoadIgnoredList()
        {
            ignoredAppIDs = new List<string>();
            using (StreamReader reader = new StreamReader("ignoredappids.txt"))
            {
                foreach (string ID in reader.ReadToEnd().Split(','))
                {
                    ignoredAppIDs.Add(ID);
                }
            }
        }
        #endregion
    }
}
