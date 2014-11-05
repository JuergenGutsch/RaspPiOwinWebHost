using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Webserver.Infrastructure;
using Webserver.Options;
using Yahoo.Yui.Compressor;

namespace Webserver.Middleware
{
    class MinifyMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public MinifyMiddleware(OwinMiddleware next, WebserverOptions options)
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
                Trace.WriteLine("Invoke MinifyMiddleware");
                
                if (path.Contains(".min."))
                {
                    return Next.Invoke(context);
                }

                var compressed = String.Empty;
                context.Response.StatusCode = 200;
                context.Response.ContentType = MimeTypeService.GetMimeType(ext);

                if (ext.Equals(".css"))
                {
                    compressed = CompressCSS(path);
                }
                if (ext.Equals(".js"))
                {
                    compressed = CompressJs(path);
                }

                context.Response.Write(compressed);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        private static string CompressJs(string path)
        {
            var rawjs = File.ReadAllText(path);
            var jsCompressor = new JavaScriptCompressor();
            var compressed = jsCompressor.Compress(rawjs);
            return compressed;
        }

        private static string CompressCSS(string path)
        {
            var rawcss = File.ReadAllText(path);
            var cssCompressor = new CssCompressor();
            var compressed = cssCompressor.Compress(rawcss);
            return compressed;
        }

        private static bool CanHandle(string ext)
        {
            return !String.IsNullOrWhiteSpace(ext) &&
                (ext.Equals(".css") ||
                ext.Equals(".js"));
        }
    }
}
