using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using RazorEngine;
using RazorEngine.Templating;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    class RazorMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public RazorMiddleware(OwinMiddleware next, WebserverOptions options)
            : base(next)
        {
            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {

            var path = FileSystem.MapPath(_options.BasePath, context.Request.Path.Value);
            var ext = Path.GetExtension(path);
            if (CanHandle(ext))
            {
                Trace.WriteLine("Invoke RazorMiddleware");

                context.Response.StatusCode = 200;
                context.Response.ContentType = MimeTypeService.GetMimeType(".html");

                var dynamicViewBag = new DynamicViewBag();
                dynamicViewBag.AddValue("Options", _options);

                var razor = File.ReadAllText(path);
                var result = Razor.Parse(razor, null, dynamicViewBag, String.Empty);

                context.Response.Write(result);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        private static bool CanHandle(string ext)
        {
            return !String.IsNullOrWhiteSpace(ext) &&
                (ext.Equals(".cshtml"));
        }
    }
}
