using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Webserver.Infrastructure;
using Webserver.Options;

namespace Webserver.Middleware
{
    public class IsPrivateFolderMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public IsPrivateFolderMiddleware(OwinMiddleware next, WebserverOptions options)
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
            Trace.WriteLine("Invoke IsPrivateFolderMiddleware");

            var path = FileSystem.MapPath(_options.BasePath, context.Request.Path.Value);
            if (path.Contains(@"\.") || path.Contains(@"\_") || path.Contains(@"\ssl\"))
            {
                return StatusHandler.Handle403(context, Next);
            }
            
            return Next.Invoke(context);
        }
    }
}
