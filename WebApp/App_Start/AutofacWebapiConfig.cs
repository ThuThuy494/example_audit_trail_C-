using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using WebApp.Core;
using WebApp.Domain;
using WebApp.Domain.Imp;
using WebApp.HistoryTracking;
using WebApp.Infrastructure;
using WebApp.Model;

namespace WebApp.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            var container = RegisterServices(new ContainerBuilder());
            Initialize(config, container);
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());  //Register your Web API controllers.

            builder.RegisterType<ApiCommandProcessor>().As<ICommandProcessor>().InstancePerRequest();
            builder.RegisterType<AuditTrailDbContext>().AsSelf().InstancePerLifetimeScope();
            //History
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
               .OnPreparing(p => p.Parameters = p.Parameters.Concat(new[] { new NamedParameter("container", null) }))
               .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorkEFPlus>().As<IUnitOfWorkEFPlus>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
              .Where(a =>
                  a.FullName.StartsWith("WebApp.Handler")
              )
              .AsImplementedInterfaces()
              .InstancePerRequest();

            //Register your Web API controllers.  
            builder.RegisterAssemblyTypes(Assembly.Load("WebApp"))
                .As(typeof(RepositoryBaseImplementation))
                .Keyed(k => k.FullName, typeof(RepositoryBaseImplementation))
                .InstancePerRequest();
            //IComponentContext componentContext
            

            //repo
            builder.RegisterType<PersonService>().As<IPersonService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PersonDetailService>().As<IPersonDetailService>()
               .InstancePerLifetimeScope();
            Container = builder.Build();

            return Container;
        }
    }
}