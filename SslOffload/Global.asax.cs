using System;
using System.Web;

namespace SslOffload
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // SSL offload?
            if ("https".Equals(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_PROTO"],
                StringComparison.InvariantCultureIgnoreCase))
            {
                // This forces the IIS7WorkerRequest to generate a https URL
                // https://github.com/Microsoft/referencesource/blob/4fe4349175f4c5091d972a7e56ea12012f1e7170/System.Web/Hosting/IIS7WorkerRequest.cs#L402
                HttpContext.Current.Request.ServerVariables["HTTPS"] = "on";
            }
        }
    }
}