using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.Models;

namespace SampleProject
{
    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "SampleProject.Controllers" }
                );
        }

        private void InitDbCodeFirst()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
            var connectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", "", connectionString);
            Database.DefaultConnectionFactory = connectionFactory;
            //Database.SetInitializer<UserContext>(new DropCreateDatabaseIfModelChanges<UserContext>());
            Database.SetInitializer<UserContext>(new DropCreateDatabaseAlways<UserContext>());
        }

        private IUserAuthService _userInfoService;

        public override void Init()
        {
            _userInfoService = new UserAuthService();
            this.PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            _userInfoService.SetCurrentUserInfo();
        }

        protected override void OnApplicationStarted()
        {
            
            InitDbCodeFirst();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new MainNinjectModule());
            return kernel;
        }

    }
}