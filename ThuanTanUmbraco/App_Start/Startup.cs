using System;
using System.Configuration;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using Hangfire;
using Hangfire.SimpleInjector;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using ThuanTanUmbraco.Mailer;
using ThuanTanUmbraco.TemplateEngine;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using Umbraco.Web;

[assembly: OwinStartup(typeof(ThuanTanUmbraco.Startup))]
namespace ThuanTanUmbraco
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        //private Container _container;
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
            //_container = new Container();
            //ConfigureContainer();
            ConfigureHangfire(app);
            app.UseHangfireDashboard();
        }
        public void ConfigureHangfire(IAppBuilder app)
        {
            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new UmbracoAuthorizationFilter()
                }
            };
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "BackgroundJobs",
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(10) });
            //GlobalConfiguration.Configuration
            //    .UseActivator(new SimpleInjectorJobActivator(_container));
            GlobalJobFilters.Filters
                .Add(new AutomaticRetryAttribute { Attempts = 5 });
            app.UseHangfireDashboard("/hangfire", dashboardOptions);
            app.UseHangfireServer();
        }
        //public void ConfigureContainer()
        //{
        //    var emailTemplatePath = ConfigurationManager.AppSettings["Mailer.TemplatePath"];
        //    _container.RegisterInstance<ITextTemplate>(new DotLiquidTemplate(HostingEnvironment.MapPath(emailTemplatePath)));

        //    _container.RegisterInstance<IEmailSender>(new SmtpEmailSender(
        //        smtpHost: ConfigurationManager.AppSettings["SmtpClient.Host"],
        //        port: Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClient.Port"] ?? "25"),
        //        useDefaultCredentials: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.UseDefaultCredentials"] ?? "False"),
        //        userName: ConfigurationManager.AppSettings["SmtpClient.Username"],
        //        password: ConfigurationManager.AppSettings["SmtpClient.Password"],
        //        enableSsl: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.EnableSsl"] ?? "False")));
        //    _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
        //    DependencyResolver.SetResolver(
        //        new SimpleInjectorDependencyResolver(_container));
        //    _container.Verify();
        //}
    }
}