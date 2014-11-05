using System.Threading.Tasks;
using Microsoft.Owin;

namespace Webserver.Infrastructure
{
    public class StatusHandler
    {
        public static Task Handle404(IOwinContext context, OwinMiddleware next)
        {
            context.Response.StatusCode = 404;
            context.Response.Write("<!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">");
            context.Response.Write("<head><title>404 Not Found</title></head><body>");
            context.Response.Write("<h1>404 Not Found</h1>");
            context.Response.Write("<p>The resource you have requested could not be found</p>");
            context.Response.Write("</body></html>");

            return Globals.CompletedTask;
        }

        public static Task Handle403(IOwinContext context, OwinMiddleware next)
        {
            context.Response.StatusCode = 403;
            context.Response.Write("<!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">");
            context.Response.Write("<head><title>403 Forbidden</title></head><body>");
            context.Response.Write("<h1>403 Forbidden</h1>");
            context.Response.Write("<p>The access to the resource you have requested is not allowed</p>");
            context.Response.Write("</body></html>");

            return Globals.CompletedTask;
        }
    }
}
