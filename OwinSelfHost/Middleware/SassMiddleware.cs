using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using NSass;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    class SassMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public SassMiddleware(OwinMiddleware next, WebserverOptions options)
            : base(next)
        {
            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {
            var path = FileSystem.MapPath(_options.BasePath, context.Request.Path.Value);
            var ext = Path.GetExtension(path);
            if (ExtToHandle(ext))
            {
                Trace.WriteLine("Invoke SassMiddleware");

               context.Response.StatusCode = 200;
                context.Response.ContentType = MimeTypeService.GetMimeType(".css");

                var compiler = new SassCompiler();
                var compressed = compiler.CompileFile(path, OutputStyle.Compressed, false);
                compressed = compressed.Replace(";}", "}").Replace(" {", "{");

                context.Response.Write(compressed);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        private static bool ExtToHandle(string ext)
        {
            return !String.IsNullOrWhiteSpace(ext) &&
                (ext.Equals(".sass") || ext.Equals(".scss"));
        }
    }
}
