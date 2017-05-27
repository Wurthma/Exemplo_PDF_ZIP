using System.Web;
using System.Web.Mvc;

namespace Exemplo_PDF_e_ZIP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
