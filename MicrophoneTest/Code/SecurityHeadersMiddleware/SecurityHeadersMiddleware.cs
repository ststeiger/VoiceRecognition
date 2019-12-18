
// https://andrewlock.net/adding-default-security-headers-in-asp-net-core/
// https://github.com/andrewlock/blog-examples/blob/master/adding-default-security-headers/src/AddingDefaultSecurityHeaders/Middleware/SecurityHeadersBuilder.cs
// https://github.com/andrewlock/blog-examples/tree/master/adding-default-security-headers/src/AddingDefaultSecurityHeaders/Middleware/Constants
namespace Microsoft.AspNetCore.SecurityHeadersPolicy
{ 


    public class SecurityHeadersMiddleware
    {
        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;
        private readonly SecurityHeadersPolicy _policy;

        public SecurityHeadersMiddleware(Microsoft.AspNetCore.Http.RequestDelegate next, SecurityHeadersPolicy policy)
        {
            if (next == null)
            {
                throw new System.ArgumentNullException(nameof(next));
            }

            if (next == null)
            {
                throw new System.ArgumentNullException(nameof(policy));
            }

            _next = next;
            _policy = policy;
        }

        public async System.Threading.Tasks.Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }

            var response = context.Response;

            if (response == null)
            {
                throw new System.ArgumentNullException(nameof(response));
            }

            var headers = response.Headers;

            foreach (var headerValuePair in _policy.SetHeaders)
            {
                headers[headerValuePair.Key] = headerValuePair.Value;
            }

            foreach (var header in _policy.RemoveHeaders)
            {
                headers.Remove(header);
            }

            await _next(context);
        }
    }
    

}
