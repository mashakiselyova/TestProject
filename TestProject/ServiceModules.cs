using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TestProject.BL.Services;
using TestProject.DAL.Contexts;
using TestProject.DAL.Repositories;

namespace TestProject
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterContext<ApplicationContext>(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
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

        private void RegisterContext<TContext>(ContainerBuilder builder) where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    serverOptions.MigrationsAssembly("TestProject");
                });

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }

}
