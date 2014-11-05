using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Webserver.Middleware
{
public class TraceMiddleware : OwinMiddleware
{
    public TraceMiddleware(OwinMiddleware next) : base(next)
    {
    }

    public override Task Invoke(IOwinContext context)
    {
        Trace.WriteLine(context.Request.Path);
        return Next.Invoke(context);
    }
}
}
