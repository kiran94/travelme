namespace com.kiransprojects.travelme
{
    using com.kiransprojects.travelme.DataAccess;
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.DataAccess.Repositories;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Ninject;
    using Ninject.Web.Common;
    using System.Reflection;
    using System.Web.Mvc;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        /// <summary>
        /// Application Start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        /// <summary>
        /// Sets up Depdendency Injection
        /// </summary>
        /// <returns></returns>
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

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

            return kernel; 

        }
    }
}