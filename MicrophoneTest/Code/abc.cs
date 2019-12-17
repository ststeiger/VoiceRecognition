
using Microsoft.Net.Http.Headers;

namespace MicrophoneTest
{
    
    using System.Linq;
    
    // https://stackoverflow.com/questions/15486/sorting-an-ilist-in-c-sharp    
    public static class SortExtensions
    {
        //  Sorts an IList<T> in place.
        public static System.Collections.Generic.IList<T> Sort<T>(this System.Collections.Generic.IList<T> list, System.Comparison<T> comparison)
        {
            System.Collections.ArrayList.Adapter((System.Collections.IList)list).Sort(new ComparisonComparer<T>(comparison));
            return list;
        }

        // Convenience method on IEnumerable<T> to allow passing of a
        // Comparison<T> delegate to the OrderBy method.
        public static System.Collections.Generic.IEnumerable<T> OrderBy<T>(
            this System.Collections.Generic.IEnumerable<T> list, System.Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }
    }

    // Wraps a generic Comparison<T> delegate in an IComparer to make it easy
    // to use a lambda expression for methods that take an IComparer or IComparer<T>
    public class ComparisonComparer<T> : System.Collections.Generic.IComparer<T>, System.Collections.IComparer
    {
        private readonly System.Comparison<T> _comparison;

        public ComparisonComparer(System.Comparison<T> comparison)
        {
            _comparison = comparison;
        }
        
        public int Compare(T x, T y)
        {
            return _comparison(x, y);
        }

        public int Compare(object o1, object o2)
        {
            return _comparison((T)o1, (T)o2);
        }
    }
    
    
    // https://madskristensen.net/blog/get-language-and-country-from-a-browser-in-aspnet/
    // https://stackoverflow.com/questions/18826282/detecting-browser-display-language
    // https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
    public class CountryInfo
    {

        public static void TestGetHeader()
        {
            Microsoft.AspNetCore.Http.HttpContext ctx = new Microsoft.AspNetCore.Http.DefaultHttpContext();

            ctx.Request.Headers["device-id"] = "20317";
            ctx.Request.Headers["Accept-Language"] = "en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";

            GetHeader(ctx);

        }



        // https://stackoverflow.com/questions/9927871/need-an-example-on-how-to-get-preferred-language-from-accept-language-request-he
        private static void GetHeader(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // RequestHeaders.AcceptLanguage P
            string header = context.Request.Headers["Accept-Language"];
            // string header = "en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";

            string def = 
            Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.ParseList(header.Split(';')).Sort(
                delegate(StringWithQualityHeaderValue a, StringWithQualityHeaderValue b)
                {
                    return a.Quality.GetValueOrDefault(1).CompareTo(b.Quality.GetValueOrDefault(1));
                }
            )[0].ToString();
            

            Microsoft.Net.Http.Headers.StringWithQualityHeaderValue defLang = 
                Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.ParseList(header.Split(';'))
                .OrderByDescending(x => x.Quality.GetValueOrDefault(1))
                .FirstOrDefault();
            
            // Microsoft.Net.Http.Headers.StringWithQualityHeaderValue.Parse(new StringSegment(v));

            
            
            // Query syntax
            //System.Linq.IOrderedEnumerable<System.Net.Http.Headers.StringWithQualityHeaderValue> languages1 =
            System.Net.Http.Headers.StringWithQualityHeaderValue defaultLanguage =
            (from headerParts in header.Split(',')
            select System.Net.Http.Headers.StringWithQualityHeaderValue.Parse(headerParts) into acceptLanguages
            orderby acceptLanguages.Quality.GetValueOrDefault(1) descending
            select acceptLanguages
            ).FirstOrDefault();
            
            // Method syntax
            System.Linq.IOrderedEnumerable < System.Net.Http.Headers.StringWithQualityHeaderValue > languages = header.Split(',')
                .Select(System.Net.Http.Headers.StringWithQualityHeaderValue.Parse)
                .OrderByDescending(s => s.Quality.GetValueOrDefault(1));
        }



        public static System.Globalization.CultureInfo ResolveCulture(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // string[] languages = HttpContext.Current.Request.UserLanguages;
            string[] languages = context.Request.Headers["Accept-Language"].ToArray(); ;
            
            if (languages == null || languages.Length == 0)
                return null;

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return System.Globalization.CultureInfo.CreateSpecificCulture(language);
            }
            catch (System.ArgumentException)
            {
                return null;
            }

        }

        public static System.Globalization.RegionInfo ResolveCountry(Microsoft.AspNetCore.Http.HttpContext context)
        {
            System.Globalization.CultureInfo culture = ResolveCulture(context);

            if (culture != null)
                return new System.Globalization.RegionInfo(culture.LCID);

            return null;
        }



        /// <summary>
        /// method for generating a country list, say for populating
        /// a ComboBox, with country options. We return the
        /// values in a Generic List<t>
        /// </t></summary>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetCountryList()
        {
            //create a new Generic list to hold the country names returned
            System.Collections.Generic.List<string> cultureList = new System.Collections.Generic.List<string>();

            //create an array of CultureInfo to hold all the cultures found, these include the users local cluture, and all the
            //cultures installed with the .Net Framework
            System.Globalization.CultureInfo[] cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures & ~System.Globalization.CultureTypes.NeutralCultures);

            //loop through all the cultures found
            foreach (System.Globalization.CultureInfo culture in cultures)
            {
                //pass the current culture's Locale ID (http://msdn.microsoft.com/en-us/library/0h88fahh.aspx)
                //to the RegionInfo contructor to gain access to the information for that culture
                System.Globalization.RegionInfo region = new System.Globalization.RegionInfo(culture.LCID);

                //make sure out generic list doesnt already
                //contain this country
                if (!(cultureList.Contains(region.EnglishName)))
                    //not there so add the EnglishName (http://msdn.microsoft.com/en-us/library/system.globalization.regioninfo.englishname.aspx)
                    //value to our generic list
                    cultureList.Add(region.EnglishName);
            }
            return cultureList;
        }


        public static void GetList()
        {

            //Example usage
            foreach (string country in GetCountryList())
            {
                //comboBox1.Items.Add(country);
            }
        }


    }


}
