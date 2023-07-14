using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NotIMDb.Api.AuthRepo;
using NotIMDb.Repository;
using NotIMDb.Service;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(NotIMDb.Api.App_Start.Startup))]

namespace NotIMDb.Api.App_Start
{
    public class Startup
    {

        public static OAuthAuthorizationServerOptions OAuthOptions { get; set; }
        public void Configuration(IAppBuilder app)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<UserService>();
            var container = builder.Build();
            app.UseAutofacMiddleware(container);
            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new AuthServerProvider(new UserRepository())
            };

            OAuthOptions = options;
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
