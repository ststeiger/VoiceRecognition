
namespace Microsoft.AspNetCore.SecurityHeadersPolicy
{


    public class SecurityHeadersPolicy
    {
        public System.Collections.Generic.IDictionary<string, string> SetHeaders { get; }

        public System.Collections.Generic.ISet<string> RemoveHeaders { get; }


        public SecurityHeadersPolicy()
        {
            this.SetHeaders = new System.Collections.Generic.Dictionary<string, string>();
            this.RemoveHeaders = new System.Collections.Generic.HashSet<string>();
        }

    }


}
