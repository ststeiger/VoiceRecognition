using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicrophoneTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new System.Collections.Generic.List<string>()
                {
                   "index.htm", "index.html"
                }
            });



            // https://stackoverflow.com/questions/38231739/how-to-disable-browser-cache-in-asp-net-core-rc2
            // https://stackoverflow.com/questions/33342643/how-does-javascript-version-asp-append-version-work-in-asp-net-core-mvc
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream",
                ContentTypeProvider = new MicrophoneTest.ExtensionContentTypeProvider(),

                OnPrepareResponse = delegate (Microsoft.AspNetCore.StaticFiles.StaticFileResponseContext context)
                {
                    // https://stackoverflow.com/questions/49547/how-do-we-control-web-page-caching-across-all-browsers

                    // The Cache-Control is per the HTTP 1.1 spec for clients and proxies
                    // If you don't care about IE6, then you could omit Cache-Control: no-cache.
                    // (some browsers observe no-store and some observe must-revalidate)
                    context.Context.Response.Headers["Cache-Control"] =
                        "no-cache, no-store, must-revalidate, max-age=0";
                    // Other Cache-Control parameters such as max-age are irrelevant 
                    // if the abovementioned Cache-Control parameters (no-cache,no-store,must-revalidate) are specified.


                    // Expires is per the HTTP 1.0 and 1.1 specs for clients and proxies. 
                    // In HTTP 1.1, the Cache-Control takes precedence over Expires, so it's after all for HTTP 1.0 proxies only.
                    // If you don't care about HTTP 1.0 proxies, then you could omit Expires.
                    context.Context.Response.Headers["Expires"] = "-1, 0, Tue, 01 Jan 1980 1:00:00 GMT";

                    // The Pragma is per the HTTP 1.0 spec for prehistoric clients, such as Java WebClient
                    // If you don't care about IE6 nor HTTP 1.0 clients 
                    // (HTTP 1.1 was introduced 1997), then you could omit Pragma.
                    context.Context.Response.Headers["pragma"] = "no-cache";


                    // On the other hand, if the server auto-includes a valid Date header, 
                    // then you could theoretically omit Cache-Control too and rely on Expires only.

                    // Date: Wed, 24 Aug 2016 18:32:02 GMT
                    // Expires: 0

                    // But that may fail if e.g. the end-user manipulates the operating system date 
                    // and the client software is relying on it.
                    // https://stackoverflow.com/questions/21120882/the-date-time-format-used-in-http-headers
                }

            });


            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    } // End Class 


}
