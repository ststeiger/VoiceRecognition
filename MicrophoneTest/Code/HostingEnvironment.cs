
namespace System.Web.Hosting
{


    public static class HostingEnvironment
    {

        private static bool s_isHosted;


        static HostingEnvironment()
        {
            s_isHosted = false;
        }


#if NETCOREAPP3_0 
        private static Microsoft.AspNetCore.Hosting.IWebHostEnvironment s_environment;
         
        public static void Configure(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            s_environment = env;
            s_isHosted = true;
        }
#elif NETCOREAPP2_0 || NETCOREAPP2_1  ||NETCOREAPP2_2  || NETSTANDARD2_0
        private static Microsoft.AspNetCore.Hosting.IHostingEnvironment s_environment;

        public static void Configure(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            s_environment = env;
            s_isHosted = true;
        }

#endif



        public static bool IsHosted
        {
            get { return s_isHosted; }
        }



        public static string HtmlEncode(string s)
        {
            return System.Web.HttpUtility.HtmlEncode(s);
        }


        public static string HtmlDecode(string s)
        {
            return System.Web.HttpUtility.HtmlDecode(s);
        }


        public static string HtmlAttributeEncode(string s)
        {
            return System.Web.HttpUtility.HtmlAttributeEncode(s);
        }


        public static string MapContentPath(string virtualPath)
        {
            return System.IO.Path.Combine(s_environment.ContentRootPath, virtualPath);
        }


        public static string MapContentPath(string basePath, string virtualPath)
        {
            return System.IO.Path.Combine(s_environment.ContentRootPath, basePath, virtualPath);
        }


        public static string MapWebRootPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return s_environment.WebRootPath;

            if (virtualPath[0] == '/' || virtualPath[0] == '\\')
                virtualPath = virtualPath.Substring(1);

            string mappedPath = System.IO.Path.Combine(s_environment.WebRootPath, virtualPath);
            mappedPath = System.IO.Path.GetFullPath(mappedPath);

            return mappedPath;
        }


        public static string MapWebRootPath(string basePath, string virtualPath)
        {
            return System.IO.Path.Combine(s_environment.WebRootPath, basePath, virtualPath);
        }


        public static string MapPath(string virtualPath)
        {
            return System.IO.Path.Combine(s_environment.WebRootPath, virtualPath);
        }


    } // End Class HostingEnvironment


} // End Namespace System.Web.Hosting
