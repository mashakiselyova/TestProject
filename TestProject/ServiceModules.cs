using Autofac;
using TestProject.BL.Services;
using TestProject.DAL.Repositories;

namespace TestProject
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
        }
    }
}
