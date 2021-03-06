[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Foodsy.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Foodsy.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Foodsy.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using Foodsy.Data;
    using Foodsy.Data.Repositories;
    using Foodsy.Web.Infrastructure.Caching;
    using Foodsy.Web.Infrastructure.Populators;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IFoodsyData>().To<FoodsyData>().WithConstructorArgument("context", c => new FoodsyDbContext());

            kernel.Bind<DbContext>().To<FoodsyDbContext>();

            kernel.Bind(typeof(IDeletableEntityRepository<>))
                .To(typeof(DeletableEntityRepository<>));

            kernel.Bind(typeof(Foodsy.Data.Repositories.IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<ICacheService>().To<InMemoryCache>();

            kernel.Bind<IDropDownListPopulator>().To<DropDownListPopulator>();
        }
    }
}
