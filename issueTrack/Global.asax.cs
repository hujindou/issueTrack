using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace issueTrack
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //HandleErrorAttribute
            //GlobalFilters.Filters.Add(new NLogExceptionHandlerAttribute());
        }

        //protected void Application_Error()
        //{
        //    //var ex = Server.GetLastError();
        //    //log the error!
        //    //_Logger.Error(ex);
        //    System.Diagnostics.Debug.WriteLine("Application_Error executed ....");
        //}
    }

    public class NLogExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            // log error to NLog
            base.OnException(filterContext);
        }
    }
}
