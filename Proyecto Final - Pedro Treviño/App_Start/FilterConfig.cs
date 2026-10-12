using System.Web;
using System.Web.Mvc;

namespace Proyecto_Final___Pedro_Treviño
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
