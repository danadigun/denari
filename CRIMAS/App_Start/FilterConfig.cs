using Bugsnag.Clients;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Filters;

namespace CRIMAS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(WebMVCClient.ErrorHandler());
        }
    }
}