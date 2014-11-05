using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using dotless.Core;
using dotless.Core.configuration;
using Microsoft.Owin;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    class LessMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public LessMiddleware(OwinMiddleware next, WebserverOptions options)
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
                Trace.WriteLine("Invoke LessMiddleware");

                context.Response.StatusCode = 200;
                context.Response.ContentType = MimeTypeService.GetMimeType(".css");

                var rawLess = File.ReadAllText(path);
                var compressed = Less.Parse(rawLess, new DotlessConfiguration
                {
                    MinifyOutput = true
                });

                context.Response.Write(compressed);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        private static bool CanHandle(string ext)
        {
            return !String.IsNullOrWhiteSpace(ext) &&
                ext.Equals(".less");
        }
    }
}
