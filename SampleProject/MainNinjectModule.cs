using Ninject.Modules;
using SampleProject.Common;

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
        }
    }
}