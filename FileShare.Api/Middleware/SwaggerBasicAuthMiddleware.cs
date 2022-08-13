using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace FileShare.Api.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate next;

        public SwaggerBasicAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api") is false)
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader is not null && authHeader.StartsWith("Basic "))
                {
                    var header = AuthenticationHeaderValue.Parse(authHeader);
                    var inBytes = Convert.FromBase64String(header.Parameter);
                    var credentials = Encoding.UTF8.GetString(inBytes).Split(':');

                    if (credentials[0].Equals("admin") && credentials[1].Equals("12345"))
                    {
                        await next.Invoke(context).ConfigureAwait(false);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }
    }
}