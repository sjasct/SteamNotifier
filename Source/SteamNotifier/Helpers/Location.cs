using System.IO;
using System.Reflection;

namespace SteamNotifier.Helpers
{
    public static class Location
    {
        /// <summary>
        /// </summary>
        public static string CurrentFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// </summary>
        public static string LogPath
        {
            get
            {
                string path = Path.Combine(CurrentFolder, "Logs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
    }
}