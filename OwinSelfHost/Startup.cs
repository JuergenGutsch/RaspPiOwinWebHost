using Owin;
using Webserver.Options;
using System;
using Webserver.Middleware;
using System.Web.Http;
using System.Net.Http;

namespace OwinSelfHost
{

    public class Startup1
    {
        public void Configuration (IAppBuilder app)
        {
            app.UseHandlerAsync ((request, response, next) => {
                response.Write("Hallo Welt");
                return next ();
            });
        }
    }

	public class Startup2
	{
		public void Configuration (IAppBuilder app)
		{
			var webserverOptions = new WebserverOptions {
				BasePath = Environment.CurrentDirectory
			};

			app.Use (typeof(TraceMiddleware));
			app.UseHandlerAsync ((request, response, next) => {
				response.AddHeader ("Server", "dotnetpro Webserver");
				return next ();
			});

			app.Use (typeof(IsPrivateFolderMiddleware), webserverOptions);
			app.Use (typeof(ResourceExistMiddleware), webserverOptions);
			app.Use (typeof(RazorMiddleware), webserverOptions);
			app.Use (typeof(DefaultMiddleware), webserverOptions);
		}

	}

    public class Startup3
    {
        public void Configuration (IAppBuilder app)
        {
            var config = new HttpConfiguration ();
            config.Routes.MapHttpRoute (
                name: "DefaultApi",
                routeTemplate: "api/{Controller}/{id}",
                defaults: new{id = RouteParameter.Optional}
            );

            app.UseWebApi (config);
        }

    }
}

