using Autofac;
using Topics.Services.Services;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Data.Ioc.IocModule());
            builder.RegisterType<TopicService>().As<ITopicService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<RegisterService>().As<IRegisterService>();
        }
    }
}
