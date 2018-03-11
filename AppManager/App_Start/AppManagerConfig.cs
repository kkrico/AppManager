using System.Configuration;
using System.IO;
using System.Web;
using AppManager.Data.Entity;

namespace AppManager
{
    public class AppManagerConfig
    {
        public static RunMode RunMode => string.IsNullOrEmpty(ApplicationHostConfigFileLocation)
            ? RunMode.Internal
            : RunMode.External;

        public static string ApplicationHostConfigFileLocation { get; private set; }


        public static void RegisterRunMode()
        {
            const string applicationhostconfigfilelocation = "ApplicationHostConfigFileLocation";
            var appHostConfigFile = ConfigurationManager.AppSettings[applicationhostconfigfilelocation];
            if (string.IsNullOrEmpty(appHostConfigFile) || !File.Exists(appHostConfigFile))
                appHostConfigFile = HttpContext.Current.Server.MapPath("App_Data/applicationHost.config");

            ApplicationHostConfigFileLocation = appHostConfigFile;
        }
    }
}