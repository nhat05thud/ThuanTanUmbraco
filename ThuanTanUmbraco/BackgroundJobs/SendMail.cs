﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Hangfire;
using ThuanTanUmbraco.ClassHelper;
using ThuanTanUmbraco.Mailer;
using ThuanTanUmbraco.TemplateEngine;
using Umbraco.Core;

namespace ThuanTanUmbraco.BackgroundJobs
{
    public class SendMail
    {
        public static string EmailTemplatePath = ConfigurationManager.AppSettings["Mailer.TemplatePath"];
        private readonly DotLiquidTemplate _textTemplate = new DotLiquidTemplate(HostingEnvironment.MapPath(EmailTemplatePath));
        private readonly SmtpEmailSender _emailSender = new SmtpEmailSender(
            smtpHost: ConfigurationManager.AppSettings["SmtpClient.Host"],
            port: Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClient.Port"] ?? "25"),
            useDefaultCredentials: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.UseDefaultCredentials"] ?? "False"),
            userName: ConfigurationManager.AppSettings["SmtpClient.Username"],
            password: ConfigurationManager.AppSettings["SmtpClient.Password"],
            enableSsl: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.EnableSsl"] ?? "False"));

        [DisplayName("[SendEmail] Test Email: {0}")]
        public async Task Send(string email, string title, string content)
        {
            var newMessage = _emailSender.CreateNoReplyEmail();
            newMessage.To = email;
            newMessage.Subject = title;
            newMessage.Body = _textTemplate.Render("blank", new { content });
            await _emailSender.SendAsync(newMessage);
        }
        public static void Enqueue(string email, string title, string content)
        {
            BackgroundJob.Enqueue<SendMail>(s => s.Send(email, title, content));
        }
    }
}