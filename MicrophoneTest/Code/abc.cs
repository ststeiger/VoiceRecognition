
namespace MicrophoneTest
{
    
    using System.Linq;

    // https://madskristensen.net/blog/get-language-and-country-from-a-browser-in-aspnet/
    // https://stackoverflow.com/questions/18826282/detecting-browser-display-language
    // https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
    public class CountryInfo
    {


        private static void GetHeader(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // RequestHeaders.AcceptLanguage P
            string header = context.Request.Headers["Accept-Language"];
            // string header = "en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";

            // Query syntax
            System.Linq.IOrderedEnumerable<System.Net.Http.Headers.StringWithQualityHeaderValue> languages1 =
            from s in header.Split(',')
            select System.Net.Http.Headers.StringWithQualityHeaderValue.Parse(s) into foo
            orderby foo.Quality.GetValueOrDefault(1) descending
            select foo
            ;

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
