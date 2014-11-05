using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    public class DefaultMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public DefaultMiddleware(OwinMiddleware next, WebserverOptions options)
            : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {
            Trace.WriteLine("Invoke DefaultMiddleware");

            var path = FileSystem.MapPath(_options.BasePath, context.Request.Path.Value);

            context.Response.StatusCode = 200;
            context.Response.ContentType = MimeTypeService.GetMimeType(Path.GetExtension(path));

            using (var inputStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                StreamHandler.CopyStream(context.Response.Body, inputStream);
            }

            return Globals.CompletedTask;
        }
    }
}
