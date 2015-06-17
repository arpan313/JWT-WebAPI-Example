using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;

namespace SampleAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {     
        protected void Application_Start()
        {     
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterGlobalFilters(GlobalFilters.Filters);         
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());            
        }
        protected void Application_BeginRequest()
        {
            // We need to enable for https connection
            //if (!Context.Request.IsSecureConnection)
            //    Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));          

        }
     
        protected void Application_OnEnd()
        {
            
        }

        
    }
}
