using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using HangFire;
using CRIMAS.Repository.artifacts;
using HangFire.SqlServer;
using Bugsnag.Clients;

namespace CRIMAS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _server;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //initiate all cron jobs
            JobStorage.Current = new SqlServerStorage("DefaultConnection");
            _server = new BackgroundJobServer();
            _server.Start();

            new DenariCronJobs().initateDividends();
            new DenariCronJobs().generateReconciliation();

            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Catch application Level Exception
            Exception ex = Server.GetLastError();

            //  Notify.  This will get configuration from the web.config
            //  and gather all known errors and report them.  It's just that simple!
            var bugsnag = new BaseClient("486389d17f72b81a6dfdb1239f3e6d18");
            bugsnag.Notify(ex);

            // Clear the error from the server
            Server.ClearError();
        }
    }
}