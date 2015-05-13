using System.Web.Http;

namespace en.AndrewTorski.CineOS.Client.CineOsWebApiService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
