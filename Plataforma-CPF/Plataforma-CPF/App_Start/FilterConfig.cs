using System.Web;
using System.Web.Mvc;

namespace Plataforma_CPF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filters.VerifySesion());
        }
    }
}
