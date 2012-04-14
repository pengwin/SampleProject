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
using SampleProject.Controllers;
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
            "BlueprintCreate",
            "Ajax/Blueprint/",
            new { controller = "BlueprintAjax", action = "Create" },
            new { httpMethod = new HttpMethodConstraint("POST") }
            );

            routes.MapRoute(
           "BlueprintRead",
           "Ajax/Blueprint/{id}",
           new { controller = "BlueprintAjax", action = "Read" },
           new { httpMethod = new HttpMethodConstraint("GET") }
           );

            routes.MapRoute(
            "BlueprintUpdate",
            "Ajax/Blueprint/{id}",
            new { controller = "BlueprintAjax", action = "Update" },
            new { httpMethod = new HttpMethodConstraint("PUT") }
            );





            /* routes.MapRoute(
            "Blueprint_default",
            "Ajax/Blueprint/{id}",
            new { controller = "BlueprintAjax", action = "Blueprint", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
              "Editor_default",
              "Editor/{id}",
              new { controller = "Editor", action = "Index", id = UrlParameter.Optional }
              );


            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "SampleProject.Controllers" }
                );

        }

        private void InitDbCodeFirst()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            var connectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", "", connectionString);
            Database.DefaultConnectionFactory = connectionFactory;
            Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
            //Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseAlways<DatabaseContext>());
        }

        private IUserAuthService _userInfoService;

        public override void Init()
        {
            _userInfoService = new UserAuthService();
            //PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
            AuthenticateRequest += new EventHandler(MvcApplication_AuthenticateRequest);
            base.Init();
        }

        void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            _userInfoService.SetCurrentUserInfo();
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
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new MainNinjectModule());
            return kernel;
        }

    }
}