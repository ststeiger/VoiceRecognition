
using Microsoft.AspNetCore.Builder; // for UseMiddleware<SecurityHeadersMiddleware>(policy);


namespace Microsoft.AspNetCore.SecurityHeadersPolicy
{

    public static class MiddlewareExtensions
    {
        public static Microsoft.AspNetCore.Builder.IApplicationBuilder UseSecurityHeadersMiddleware(
            this Microsoft.AspNetCore.Builder.IApplicationBuilder app, SecurityHeadersBuilder builder)
        {
            SecurityHeadersPolicy policy = builder.Build();
            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }
    }

}
