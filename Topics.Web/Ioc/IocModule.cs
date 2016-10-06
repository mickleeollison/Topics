using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Topics.Web.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Topics.Services.Ioc.IocModule());

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());
        }
    }
}
