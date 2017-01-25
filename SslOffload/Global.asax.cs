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
            Console.WriteLine("Begin request");
            HttpContext.Current.Request.ServerVariables["HTTPS"] = "on";
        }
    }
}