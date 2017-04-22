using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using recenzent.Data;

namespace recenzent
{
    public static class DependencyInjectionConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            var autofacResolver = new AutofacDependencyResolver(container);
            //var dependencyResolver = new InjectableDependencyResolver(diContainer, autofacResolver);
            DependencyResolver.SetResolver(autofacResolver);
        }

        public static void RegisterServices(ContainerBuilder c)
        {
            c.RegisterType<DataContext>().InstancePerDependency();
        }
    }
}