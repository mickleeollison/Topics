using Autofac;
using Topics.Data.DAL;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Repositories;
using Topics.Data.Repositories.Interfaces;

namespace Topics.Data.Ioc
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Core.Ioc.IocModule());

            builder.RegisterType<TopicsContext>().As<ITopicsContext>().InstancePerRequest();
            builder.RegisterType<TopicRepository>().As<ITopicRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
        }
    }
}
