using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuanTanUmbraco.ClassHelper;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class MemberFrontEndController : SurfaceController
    {
        [HttpPost]
        public ActionResult MemberLogin(LoginForm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                }
                var member = Services.MemberService.GetByEmail(model.Username);
                if (member != null && member.IsApproved)
                {
                    var culture = CultureInfo.CurrentCulture;
                    var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
                    var link = "/";
                    if (currentHomePage != null)
                    {
                        link = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
                    }
                    if (!Members.Login(model.Username, model.Password))
                    {
                        model.IsSuccess = false;
                        model.IsRunScripts = true;
                        model.ResponseText = Umbraco.GetDictionaryValue("Login.Failure");
                        model.ResponseHeader = "Error!!";
                        return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                    }
                    model.IsSuccess = true;
                    model.IsRunScripts = true;
                    model.ResponseText = Umbraco.GetDictionaryValue("Login.Success");
                    model.ResponseHeader = "Success!!";
                    model.RedirectLink = link;
                    return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                }
                model.IsSuccess = false;
                model.IsRunScripts = true;
                model.ResponseText = Umbraco.GetDictionaryValue("Account.NotExits");
                model.ResponseHeader = "Error!!";
                return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult MemberForgotPassword(LoginForm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/User/ForgotPassword/_Form.cshtml", model);
                }
                var member = Services.MemberService.GetByEmail(model.Username);
                if (member != null && member.IsApproved)
                {
                    var newPassword = Utils.CreateRandomPassword(10);
                    Services.MemberService.SavePassword(member, newPassword);
                }

                model.IsSuccess = false;
                model.ResponseText = Umbraco.GetDictionaryValue("Account.NotExits");
                return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            var culture = CultureInfo.CurrentCulture;
            var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
            var link = "/";
            if (currentHomePage != null)
            {
                link = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
            }
            Members.Logout();
            return Redirect(link);
        }
    }
}