
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;


namespace MicrophoneTest
{


    public class PolyfillMiddleWare
    {
        protected readonly Microsoft.AspNetCore.Http.RequestDelegate m_next;
        protected Microsoft.AspNetCore.Hosting.IHostingEnvironment m_env;

        private static readonly System.Collections.Generic.Dictionary<string, string> s_files;


        static PolyfillMiddleWare()
        {
            // cd /d FOLDER
            // dir / w / b
            s_files = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.Ordinal);
            
            s_files.Add("es6-promise-2.0.0.min", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/es6-promise-2.0.0.min.js"));
            // s_files.Add("es6-fetch", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/fes6-etch.js"));
            s_files.Add("es6-fetch-ie8", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/es6-fetch-ie8.js"));
            
            s_files.Add("array-includes",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/array-includes.js"));
            s_files.Add("array-forEach",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/array-forEach.js"));
            s_files.Add("array-indexOf",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/array-indexOf.js"));
            s_files.Add("array-isArray",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/array-isArray.js"));
            
            s_files.Add("string-trim",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/string-trim.js"));
            s_files.Add("string-trimStart",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/string-trimStart.js"));
            s_files.Add("string-trimEnd",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/string-trimEnd.js"));

            s_files.Add("object-setPrototypeOf-ie9", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/object-setprototypeof-ie9.js"));
            s_files.Add("object-getOwnPropertyNames",  System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/object-getOwnPropertyNames.js"));

            s_files.Add("json3.min", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/json3.min.js"));
            s_files.Add("classList", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/classList.js"));
            
            // s_files.Add("classlist_polyfill", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/classlist_polyfill.js")); // bad 
            // s_files.Add("common", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/common.js"));
            // s_files.Add("console", System.Web.Hosting.HostingEnvironment.MapPath("js/polyfills/console.js")); // console is default-polyfilled

            foreach (string key in new System.Collections.Generic.List<string>(s_files.Keys))
            {
                if(System.IO.File.Exists( s_files[key]))
                    s_files[key] = System.IO.File.ReadAllText(s_files[key]);
            } // Next key 

        } // End Static Constructor 
        
        
        public PolyfillMiddleWare(Microsoft.AspNetCore.Http.RequestDelegate next, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.m_next = next;
            this.m_env = env;
        } // End Constructor 
        
        
        public async System.Threading.Tasks.Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            string[] files = null;
            
            try
            {
                string json = context.Request.Query["polyfills"];
                string ext = context.Request.Query["ext"];
                
                if (string.IsNullOrEmpty(json))
                {
                    context.Response.StatusCode = 422;
                    context.Response.ContentType = "text/plain";
                    
                    await context.Response.WriteAsync("Missing parameter 'polyfills'.");
                    return;
                } // End if (string.IsNullOrEmpty(json)) 

                if (string.IsNullOrEmpty(ext))
                {
                    context.Response.StatusCode = 422;
                    context.Response.ContentType = "text/plain";
                    
                    await context.Response.WriteAsync("Missing parameter 'ext'.");
                    return;
                } // End if (string.IsNullOrEmpty(ext)) 

                files = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(json);
                
                foreach (string file in files)
                {
                    if (string.IsNullOrEmpty(file))
                        continue;

                    if(!s_files.ContainsKey(file))
                        throw new System.IO.FileNotFoundException(file);
                } // Next file 

                bool isJs = ".js".Equals(ext, System.StringComparison.Ordinal);
                string contentType = isJs ? "application/javascript; charset=utf-8" : "text/css; charset=utf-8";
                
                context.Response.StatusCode = 200;
                context.Response.ContentType = contentType;
                
                foreach (string file in files)
                {
                    if (string.IsNullOrEmpty(file))
                        continue;

                    await context.Response.WriteAsync(s_files[file]);
                } // Next file 

            } // End Try 
            catch (System.Exception e)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Error processing polyfills.\r\n");
                await context.Response.WriteAsync(e.Message);
                await context.Response.WriteAsync("\r\n");
                await context.Response.WriteAsync(e.StackTrace);
                return;
            } // End Catch 
            
        } // End Task Invoke 
        
        
    } // End Class PolyfillMiddleWare
    
    
    public static class PolyfillMiddleWareExtensions
    {
        public static Microsoft.AspNetCore.Builder.IApplicationBuilder UsePolyfillMiddleware(
            this Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            // app.UseWhen(context => context.Request.Path.StartsWithSegments("/blob"), appBuilder => { }
            
            // https://www.devtrends.co.uk/blog/conditional-middleware-based-on-request-in-asp.net-core
            app.UseWhen(
                delegate (Microsoft.AspNetCore.Http.HttpContext context)
                {
                    return context.Request.Path.StartsWithSegments("/js/polyfills.ashx");
                }
                , delegate (Microsoft.AspNetCore.Builder.IApplicationBuilder appBuilder)
                {
                    // appBuilder.UseStatusCodePagesWithReExecute("/apierror/{0}");
                    appBuilder.UseMiddleware<PolyfillMiddleWare>();
                    //appBuilder.UseExceptionHandler("/apierror/500");
                }
            );
            
            return app;
        } // End Function UsePolyfillMiddleware 
        
        
    } // End Class PolyfillMiddleWareExtensions 
    
    
} // End Namespace 
