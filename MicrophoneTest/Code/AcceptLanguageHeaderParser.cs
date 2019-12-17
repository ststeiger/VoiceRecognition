
namespace MicrophoneTest
{


    public class AcceptLanguageHeaderParser
    {


        // string header = "en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";
        public static System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>>
            GetAcceptedLanguages(string header, string defaultLanguage, string[] allowedValues)
        {
            System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>> ls = 
                new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>>();


            System.Func<System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>>> defaultLanguageList = delegate ()
            {
                ls.Add(
                    new System.Collections.Generic.KeyValuePair<string, double>(defaultLanguage, 1.0)
                );

                return ls;
            };


            if (header == null)
            {
                return defaultLanguageList();
            }


            header = System.Text.RegularExpressions.Regex.Replace(header, @"\s+", "");

            if (header == string.Empty)
            {
                return defaultLanguageList();
            }

            string[] languages = header.Split(',');

            for (int i = 0; i < languages.Length; ++i)
            {
                string[] kvp = languages[i].Split(';');

                if (kvp.Length < 1)
                    continue;

                string key = kvp[0];
                if (key == null)
                    continue;


                int ind = key.IndexOf('-');
                if (ind != -1)
                {
                    key = key.Substring(0, ind);
                }

                key = key.ToLower(System.Globalization.CultureInfo.InvariantCulture);

                if (allowedValues != null)
                {
                    int pos = System.Array.IndexOf(allowedValues, key);
                    if (pos == -1)
                    {
                        continue;
                    }
                } // End if (allowedValues != null) 


                double quality = 1.0;

                if (kvp.Length > 1)
                {
                    string qual = kvp[1];

                    if (qual != null && qual.StartsWith("q="))
                        qual = qual.Substring(2);

                    // if (!double.TryParse(qual, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out quality)) { quality = 1.0; }
                    double.TryParse(qual, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out quality);
                    // Set quality to 0 if invalid, 1 if not present 
                } // End if (kvp.Length > 1) 

                // Note to self: 
                // Here we introduce bias - if the default-language is in the accepted languages list. 
                // Ensure default-language is taken, if it is among the accepted languages
                if (defaultLanguage.Equals(key))
                    quality = 100;

                ls.Add(
                    new System.Collections.Generic.KeyValuePair<string, double>(key, quality)
                );


                ls.Sort(delegate (System.Collections.Generic.KeyValuePair<string, double> a, System.Collections.Generic.KeyValuePair<string, double> b)
                {
                    // return a.Value.CompareTo(b.Value); // Ascending 
                    return b.Value.CompareTo(a.Value); // Ascending 
                });

            } // Next i 

            if (ls.Count == 0)
            {
                return defaultLanguageList();
            }

            return ls;
        } // End Function GetAcceptedLanguages 


        private static string GetAcceptHeaderValue(Microsoft.AspNetCore.Http.HttpContext context)
        {
            string acceptHeader = null;

            if (context != null && context.Request != null && context.Request.Headers != null)
            {
                acceptHeader = context.Request.Headers["Accept-Language"];
            }

            return acceptHeader;
        } // End Function GetAcceptHeaderValue 


        public static System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>>
            GetAcceptedLanguages(Microsoft.AspNetCore.Http.HttpContext context, string defaultLanguage, string[] allowedValues)
        {
            string acceptHeader = GetAcceptHeaderValue(context);

            return GetAcceptedLanguages(acceptHeader, defaultLanguage, allowedValues);
        }

        
        public static string GetDefaultLanguage(string header, string defaultValue, string[] allowedValues)
        {
            System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, double>> ls = GetAcceptedLanguages(header, defaultValue, allowedValues);
            defaultValue = ls[0].Key;
            ls.Clear();
            ls = null;

            return defaultValue;
        }


        public static string GetDefaultLanguage(string acceptHeader, string defaultLanguage)
        {
            return GetDefaultLanguage(acceptHeader, defaultLanguage, null);
        }
        

        public static string GetDefaultLanguage(Microsoft.AspNetCore.Http.HttpContext context, string defaultLanguage)
        {
            string acceptHeader = GetAcceptHeaderValue(context);

            return GetDefaultLanguage(acceptHeader, defaultLanguage, null);
        }


        public static string GetDefaultLanguage(string acceptHeader)
        {
            string[] allowedValues = new string[] { "de", "fr", "it", "en" };

            return GetDefaultLanguage(acceptHeader, "de", allowedValues);
        }


        public static string GetDefaultLanguage(Microsoft.AspNetCore.Http.HttpContext context)
        {
            string acceptHeader = GetAcceptHeaderValue(context);

            return GetDefaultLanguage(acceptHeader);
        }
        

        public static void Test()
        {
            // var language = window.navigator.userLanguage || window.navigator.language;

            Microsoft.AspNetCore.Http.HttpContext ctx = new Microsoft.AspNetCore.Http.DefaultHttpContext();

            ctx.Request.Headers["device-id"] = "20317";
            ctx.Request.Headers["Accept-Language"] = "it;de-CH,en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";
            // ctx.Request.Headers["Accept-Language"] = "";

            string defLang = GetDefaultLanguage(ctx, "de");
            System.Console.WriteLine(defLang);
        } // End Sub Test 


    } // End Class AcceptLanguageHeaderParser 


}
