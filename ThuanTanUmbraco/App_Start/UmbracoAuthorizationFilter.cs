using System.Web;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Umbraco.Core.Security;

namespace ThuanTanUmbraco
{
    public class UmbracoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var auth = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();
            return auth != null;
        }

    }
}