
namespace MicrophoneTest.Code
{

    
    using Microsoft.AspNetCore.Builder; // For app.UseMvc
    using Microsoft.Extensions.DependencyInjection; // For ServiceProvider.GetService<IMemoryCache> 
    using Microsoft.Extensions.Caching.Memory; // for _cache.Set


    public interface ICachedRouteDataProvider<TPrimaryKey>
    {
        System.Collections.Generic.IDictionary<string, TPrimaryKey> GetPageToIdMap();
    }


    public class CmsCachedRouteDataProvider : ICachedRouteDataProvider<int>
    {

        public System.Collections.Generic.IDictionary<string, int> GetPageToIdMap()
        {
            return new System.Collections.Generic.Dictionary<string, int>();

            /*
            // Lookup the pages in DB
            return (from page in DbContext.Pages
                    select new KeyValuePair<string, int>(
                        page.Url.TrimStart('/').TrimEnd('/'),
                        page.Id)
                    ).ToDictionary(pair => pair.Key, pair => pair.Value);
                    */
        }


        private static void HowToAddInStartup(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.Routes.Add(
                    new CachedRoute<int>(
                        controller: "Cms",
                        action: "Index",
                        dataProvider: new CmsCachedRouteDataProvider(),
                        cache: routes.ServiceProvider.GetService<Microsoft.Extensions.Caching.Memory.IMemoryCache>(),
                        target: routes.DefaultHandler)
                    {
                        CacheTimeoutInSeconds = 900
                    });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }



    // https://stackoverflow.com/questions/36718499/dynamically-add-route-value-parameter-from-database-in-asp-net-core?noredirect=1&lq=1
    // https://stackoverflow.com/questions/32565768/change-route-collection-of-mvc6-after-startup
    // https://stackoverflow.com/questions/32582232/imlementing-a-custom-irouter-in-asp-net-5-vnext-mvc-6
    // https://intellitect.com/asp-net-core-dynamic-routing-with-constraints/
    // https://stackoverflow.com/questions/36179304/dynamic-url-rewriting-with-mvc-and-asp-net-core
    // https://www.strathweb.com/2019/08/dynamic-controller-routing-in-asp-net-core-3-0/
    public class CachedRoute<TPrimaryKey> : Microsoft.AspNetCore.Routing.IRouter
    {
        private readonly string _controller;
        private readonly string _action;
        private readonly ICachedRouteDataProvider<TPrimaryKey> _dataProvider;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private readonly Microsoft.AspNetCore.Routing.IRouter _target;
        private readonly string _cacheKey;
        private object _lock = new object();

        public CachedRoute(
            string controller,
            string action,
            ICachedRouteDataProvider<TPrimaryKey> dataProvider,
            Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
            Microsoft.AspNetCore.Routing.IRouter target)
        {
            if (string.IsNullOrWhiteSpace(controller))
                throw new System.ArgumentNullException("controller");
            if (string.IsNullOrWhiteSpace(action))
                throw new System.ArgumentNullException("action");
            if (dataProvider == null)
                throw new System.ArgumentNullException("dataProvider");
            if (cache == null)
                throw new System.ArgumentNullException("cache");
            if (target == null)
                throw new System.ArgumentNullException("target");

            _controller = controller;
            _action = action;
            _dataProvider = dataProvider;
            _cache = cache;
            _target = target;

            // Set Defaults
            CacheTimeoutInSeconds = 900;
            _cacheKey = "__" + this.GetType().Name + "_GetPageList_" + _controller + "_" + _action;
        }

        public int CacheTimeoutInSeconds { get; set; }

        public async System.Threading.Tasks.Task RouteAsync(Microsoft.AspNetCore.Routing.RouteContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value;

            if (!string.IsNullOrEmpty(requestPath) && requestPath[0] == '/')
            {
                // Trim the leading slash
                requestPath = requestPath.Substring(1);
            }

            // Get the page id that matches.
            TPrimaryKey id;

            //If this returns false, that means the URI did not match
            if (!GetPageList().TryGetValue(requestPath, out id))
            {
                return;
            }

            //Invoke MVC controller/action
            var routeData = context.RouteData;

            // TODO: You might want to use the page object (from the database) to
            // get both the controller and action, and possibly even an area.
            // Alternatively, you could create a route for each table and hard-code
            // this information.
            routeData.Values["controller"] = _controller;
            routeData.Values["action"] = _action;

            // This will be the primary key of the database row.
            // It might be an integer or a GUID.
            routeData.Values["id"] = id;

            await _target.RouteAsync(context);
        }

        public Microsoft.AspNetCore.Routing.VirtualPathData GetVirtualPath(
            Microsoft.AspNetCore.Routing.VirtualPathContext context)
        {
            Microsoft.AspNetCore.Routing.VirtualPathData result = null;
            string virtualPath;

            if (TryFindMatch(GetPageList(), context.Values, out virtualPath))
            {
                result = new Microsoft.AspNetCore.Routing.VirtualPathData(this, virtualPath);
            }

            return result;
        }

        private bool TryFindMatch(
              System.Collections.Generic.IDictionary<string, TPrimaryKey> pages
            , System.Collections.Generic.IDictionary<string, object> values, out string virtualPath)
        {
            virtualPath = string.Empty;
            TPrimaryKey id;
            object idObj;
            object controller;
            object action;

            if (!values.TryGetValue("id", out idObj))
            {
                return false;
            }

            id = SafeConvert<TPrimaryKey>(idObj);
            values.TryGetValue("controller", out controller);
            values.TryGetValue("action", out action);

            // The logic here should be the inverse of the logic in 
            // RouteAsync(). So, we match the same controller, action, and id.
            // If we had additional route values there, we would take them all 
            // into consideration during this step.
            if (action.Equals(_action) && controller.Equals(_controller))
            {
                // The 'OrDefault' case returns the default value of the type you're 
                // iterating over. For value types, it will be a new instance of that type. 
                // Since KeyValuePair<TKey, TValue> is a value type (i.e. a struct), 
                // the 'OrDefault' case will not result in a null-reference exception. 
                // Since TKey here is string, the .Key of that new instance will be null.

                virtualPath = System.Linq.Enumerable.FirstOrDefault(pages, x => x.Value.Equals(id)).Key;
                if (!string.IsNullOrEmpty(virtualPath))
                {
                    return true;
                }
            }
            return false;
        }

        private System.Collections.Generic.IDictionary<string, TPrimaryKey> GetPageList()
        {
            System.Collections.Generic.IDictionary<string, TPrimaryKey> pages;

            if (!_cache.TryGetValue(_cacheKey, out pages))
            {
                // Only allow one thread to poplate the data
                lock (_lock)
                {
                    if (!_cache.TryGetValue(_cacheKey, out pages))
                    {
                        pages = _dataProvider.GetPageToIdMap();
                        
                        _cache.Set(_cacheKey, pages,
                            new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions()
                            {
                                Priority = Microsoft.Extensions.Caching.Memory.CacheItemPriority.NeverRemove,
                                AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(this.CacheTimeoutInSeconds)
                            }
                        );  
                    }
                }
            }

            return pages;
        }

        private static T SafeConvert<T>(object obj)
        {
            if (typeof(T).Equals(typeof(System.Guid)))
            {
                if (obj.GetType() == typeof(string))
                {
                    return (T)(object)new System.Guid(obj.ToString());
                }
                return (T)(object)System.Guid.Empty;
            }
            return (T)System.Convert.ChangeType(obj, typeof(T));
        }


    }


}
