using Newtonsoft.Json.Serialization;
using NLog;
using System.Web.Http;

namespace SignupApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        private static bool _LoggedStartup;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            if (!_LoggedStartup)
            {
                _Logger.Info("Application started");
                _LoggedStartup = true;
            }
        }
    }
}
