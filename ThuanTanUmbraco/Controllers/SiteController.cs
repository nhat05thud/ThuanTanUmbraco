using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Mvc;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class SiteController : SurfaceController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult HandleContactForm(ContactForm model)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(model.CultureLCID);

            //Check if the dat posted is valid (All required's & email set in email field)
            if (!ModelState.IsValid)
            {
                //Not valid - so lets return the user back to the view with the data they entered still prepopulated
                model.ErrorMsg = "Đã có lỗi xảy ra, Vui lòng thử lại sau !!";
                return PartialView("~/Views/Partials/Contact/_Form.cshtml", model);
            }
            var emailReceive = WebConfigurationManager.AppSettings["EmailContactReceive"];
            var messageString = "<h3>ĐÔNG MINH LIÊN HỆ</h3>";
            messageString += "<b>Họ Tên: </b>" + model.Name + "<br />";
            messageString += "<b>Email: </b>" + model.Email + "<br />";
            messageString += "<b>Điện thoại: </b>" + model.Phone + "<br />";
            messageString += "<b>Nội dung tin nhắn: </b>" + model.Message;

            //Generate an email message object to send
            var email = new MailMessage
            {
                Subject = "Nội dung liên hệ",
                Body = messageString,
                IsBodyHtml = true,
                To = { emailReceive }
            };
            try
            {
                //Connect to SMTP credentials set in web.config
                var smtp = new SmtpClient();

                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //Throw an exception if there is a problem sending the email
                throw ex;
            }


            //All done - lets redirect to the current page & show our thanks/success message
            model.Name = "";
            model.Email = "";
            model.Phone = "";
            model.Message = "";
            model.ErrorMsg = "Tin nhắn đã được gửi thành công !!!";
            ModelState.Clear();
            return PartialView("~/Views/Partials/Contact/_Form.cshtml", model);
        }
    }
}