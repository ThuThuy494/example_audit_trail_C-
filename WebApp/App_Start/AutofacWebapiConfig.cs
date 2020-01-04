using Autofac;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using WebApp.Domain;
using WebApp.HistoryTracking;
using WebApp.Infrastructure;

namespace WebApp.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetEntryAssembly());  //Register your Web API controllers.
            //Register your Web API controllers.  
            builder.RegisterAssemblyTypes(Assembly.Load("AuditTrail_Console.HistoryTracking"))
                .As(typeof(RepositoryBaseImplementation))
                .Keyed(k => k.FullName, typeof(RepositoryBaseImplementation))
                .InstancePerRequest();

            //History
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
               .OnPreparing(p => p.Parameters = p.Parameters.Concat(new[] { new NamedParameter("container", null) }))
               .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorkEFPlus>().As<IUnitOfWorkEFPlus>()
                .InstancePerLifetimeScope();

            //repo
            builder.RegisterType<PersonService>().As<IPersonService>()
                .InstancePerLifetimeScope();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}