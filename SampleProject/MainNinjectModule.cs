using Ninject.Modules;
using SampleProject.Authentication;
using SampleProject.Common;
using SampleProject.Models;

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

            Bind<IUserAuthService>().To<UserAuthService>();
        }
    }
}