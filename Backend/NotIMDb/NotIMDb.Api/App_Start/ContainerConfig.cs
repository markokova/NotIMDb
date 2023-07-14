using Autofac;
using Autofac.Integration.WebApi;
using NotIMDb.Api.AuthRepo;
using NotIMDb.Repository;
using NotIMDb.Repository.Common;
using NotIMDb.Service;
using NotIMDb.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace NotIMDb.Api.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            
            builder.RegisterType<UserManagementService>().As<IUserManagementService>();
            builder.RegisterType<UserManagementRepository>().As<IUserManagementRepository>();

            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<ActorService>().As<IActorService>();
            builder.RegisterType<ActorRepository>().As<IActorRepository>();
            
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<MovieService>().As<IMovieService>();

            builder.RegisterType<GenreRepository>().As<IGenreRepository>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<WatchlistRepository>().As<IWatchlistRepository>();
            builder.RegisterType<WatchlistService>().As<IWatchlistService>();

            builder.RegisterType<MovieGenreRepository>().As<IMovieGenreRepository>();
            builder.RegisterType<MovieGenreService>().As<IMovieGenreService>();

            IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}