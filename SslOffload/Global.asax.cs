using System;
using System.Reflection;
using System.Web;
using NR = NewRelic.Api.Agent;

namespace SslOffload
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            NR.NewRelic.SetApplicationName("SslOffload");
            NR.NewRelic.StartAgent();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // New Relic event
            NR.NewRelic.IncrementCounter("beginrequest");

            // Stash the original URL away before modification
            HttpContext.Current.Items["OriginalUrl"] = Request.Url;

            // SSL offload?
            if ("https".Equals(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_PROTO"],
                StringComparison.InvariantCultureIgnoreCase))
            {
                // This forces the IIS7WorkerRequest to generate a https URL
                // https://github.com/Microsoft/referencesource/blob/4fe4349175f4c5091d972a7e56ea12012f1e7170/System.Web/Hosting/IIS7WorkerRequest.cs#L402
                HttpContext.Current.Request.ServerVariables["HTTPS"] = "on";

                // Force the HttpRequest object to rebuild its cached Url value
                // Its possible an HttpModule has called Request.Url before we get here
                FieldInfo urlField = typeof(HttpRequest).GetField("_url", BindingFlags.NonPublic | BindingFlags.Instance);
                if (urlField != null)
                {
                    urlField.SetValue(HttpContext.Current.Request, null);
                }
            }
        }
    }
}