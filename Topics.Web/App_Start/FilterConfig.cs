using System.Web;
using System.Web.Mvc;
using Topics.Web.Filters;

namespace Topics.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TopicsErrorHandler());
        }
    }
}
