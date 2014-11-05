using System;
using System.Collections.Generic;
using Owin;
using Webserver.Middleware;
using Webserver.Options;

namespace OwinSelfHost
{
	public class Startup
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

	public class Startup2
	{
		public void Configuration (IAppBuilder app)
		{
			app.UseHandlerAsync ((request, response, next) => {
				response.Write("Hallo Welt");
				return next ();
			});
		}
	}
}

