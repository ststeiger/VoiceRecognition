
namespace MicrophoneTest
{


    public class ExtensionContentTypeProvider
        : Microsoft.AspNetCore.StaticFiles.IContentTypeProvider
    {
        // private static readonly ILog Log; 

        private readonly Microsoft.AspNetCore.StaticFiles.IContentTypeProvider m_defaultContentTypeProvider;
        private readonly System.Collections.Generic.Dictionary<string, string> m_mappings;


        public ExtensionContentTypeProvider()
        {
            // this.Log = LogManager.GetLogger(typeof(ExtensionContentTypeProvider));
            this.m_defaultContentTypeProvider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();

            this.m_mappings = new System.Collections.Generic.Dictionary<string, string>(
                System.StringComparer.OrdinalIgnoreCase)
            {
                { "json", "application/json" },
                { "jbc", "application/json" },
                { "ta", "application/json" },
                { "mustache", "text/plain" }
            };
        }


        /// <summary>
        /// Maps an extension to a content type.
        /// </summary>
        /// <param name="extension">The extension to map, without a leading dot.</param>
        /// <param name="contentType">The content type to map to the extension.</param>
        public void AddMapping(string extension, string contentType)
        {
            if (!this.m_mappings.ContainsKey(extension))
                this.m_mappings.Add(extension, contentType);
        }


        private static string AfterLast(string value, string separator)
        {
            int index = value.LastIndexOf(separator);

            if (index == -1)
                return value;

            return value.Substring(index + separator.Length);
        }


        bool Microsoft.AspNetCore.StaticFiles.IContentTypeProvider.
            TryGetContentType(string subpath, out string contentType)
        {
            string extension = AfterLast(subpath, ".");

            if (this.m_mappings.TryGetValue(extension.ToLowerInvariant(), out contentType))
            {
                return true;
            }

            if (this.m_defaultContentTypeProvider.TryGetContentType(subpath, out contentType))
            {
                return true;
            }

            // Log.WarnFormat("Unknown file type. {0}", subpath);
            // contentType = null;
            // return false;

            contentType = "application/octet-stream";
            return true;
        }


    } // End Class ExtensionContentTypeProvider 


} // End Namespace AnySqlWebAdmin 
