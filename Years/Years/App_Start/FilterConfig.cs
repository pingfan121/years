using System.Web;
using System.Web.Mvc;
using Years.WebCore.Filters;

namespace Years.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // filters.Add(new HandleErrorAttribute());
            filters.Add(new ExpFilter());
        }
    }
}
