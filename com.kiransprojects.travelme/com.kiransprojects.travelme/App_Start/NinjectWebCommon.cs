[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(com.kiransprojects.travelme.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(com.kiransprojects.travelme.App_Start.NinjectWebCommon), "Stop")]

namespace com.kiransprojects.travelme.App_Start
{
    using com.kiransprojects.travelme.DataAccess;
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.DataAccess.Repositories;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Config
            kernel.Bind<IDatabaseConfig>().To<NhibernateConfigurationSingleton>();
            kernel.Bind<INhibernateHelper>().To<NhibernateHelper>();

            //Repository
            kernel.Bind<IUserEntityRepository>().To<UserEntityRepository>();
            kernel.Bind<ITripRepository>().To<TripRepository>();
            kernel.Bind<IRepository<Post>>().To<RepositoryBase<Post>>();
            kernel.Bind<IRepository<Media>>().To<MediaRepository>();
            kernel.Bind<IRepository<Log>>().To<LogRepository>();

            //Services
            kernel.Bind<ILoginService>().To<LoginService>();
            kernel.Bind<ILoggerService>().To<LoggerService>();
            kernel.Bind<IFileService>().To<IFileService>();
            kernel.Bind<IMailService>().To<MailService>();
            kernel.Bind<IMediaService>().To<MediaService>();
            kernel.Bind<IPasswordService>().To<PasswordService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<ITripService>().To<TripService>();
            kernel.Bind<IUserService>().To<UserService>();
        }        
    }
}
