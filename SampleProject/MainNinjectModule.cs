using Ninject.Modules;
using SampleProject.Authentication;
using SampleProject.Common;
using SampleProject.Models;
using SampleProject.Models.BlueprintSearch;
using SampleProject.Models.Repositories;

namespace SampleProject
{
    /// <summary>
    /// Binds all dependences in the application.
    /// </summary>
    public class MainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<DebugLogger>().InSingletonScope();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IBlueprintRepository>().To<BlueprintRepository>();
            Bind<DatabaseContext>().To<DatabaseContext>();

            Bind<IBlueprintSearchService>().To<BlueprintSearchService>();

            Bind<IUserAuthService>().To<UserAuthService>();
            Bind<IApiKeyStore>().To<ApiKeyStore>();
        }
    }
}