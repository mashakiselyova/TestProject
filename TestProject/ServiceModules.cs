using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using TestProject.Mappers;
using TestProject.Models;

namespace TestProject
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterContext(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterMappers(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PostService>().As<IPostService>();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
        }

        private static void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<Mappers.EditPostMapper>().As<Mappers.IMapper<Models.EditPostModel, BL.Models.EditPostModel>>();
            builder.RegisterType<DisplayPostMapper>().As<Mappers.IMapper<PostDisplayModel, PostModel>>();
            builder.RegisterType<BL.Mappers.EditPostMapper>().As<BL.Mappers.IMapper<BL.Models.EditPostModel, Post>>();
            builder.RegisterType<PostMapper>().As<BL.Mappers.IMapper<PostModel, Post>>();
            builder.RegisterType<LoginMapper>().As<Mappers.IMapper<LoginModel, UserLoginModel>>();
            builder.RegisterType<ProfileDisplayMapper>().As<Mappers.IMapper<ProfileDisplayModel, UserProfile>>();
            builder.RegisterType<UserLoginMapper>().As<BL.Mappers.IMapper<UserLoginModel, User>>();
            builder.RegisterType<UserProfileMapper>().As<BL.Mappers.IMapper<UserProfile, User>>();
        }

        private void RegisterContext(ContainerBuilder builder)
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<ApplicationContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    serverOptions.MigrationsAssembly("TestProject");
                });

                return optionsBuilder.Options;
            }).As<DbContextOptions<ApplicationContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<ApplicationContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }

}
