using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace ThuanTanUmbraco
{
    public class OnSavingEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //RouteTable.Routes.MapRoute(
            //    name: "VerifySignUp",
            //    url: "Member/VerifySignUp/{query}",
            //    defaults: new
            //    {
            //        controller = "Member",
            //        action = "VerifySignUp",
            //        query = UrlParameter.Optional
            //    });
        }
    }
}