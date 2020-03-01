using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class CartController : SurfaceController
    {
        public ActionResult RenderCartItem(string model)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(model);
            return PartialView("~/Views/Partials/Cart/_CartItem.cshtml", cart);
        }
    }
}