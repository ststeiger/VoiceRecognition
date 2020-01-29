
using System.Windows.Forms;


namespace RasterImageAsSvg
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }


        // https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
        // https://stackoverflow.com/questions/86477/does-c-sharp-have-an-equivalent-to-javascripts-encodeuricomponent/86484
        public static System.Data.DataTable EncodeChars()
        {
            char[] encodingSet = new char[]{
                 'A'
                ,'B'
                ,'a'
                ,'b'
                ,'0'
                ,'1'
                ,' ' // Space
                 // https://en.wikipedia.org/wiki/Escape_character
                ,'\t'
                ,'\r'
                ,'\n'
                ,'\v' // vertical tab
                ,'\f' // form feed
                ,'\b' // backspace
                 ,'\0' // NULL
                 ,'\xFF' // byte "FF"
                ,'!'
                //,'"	%22	%22	"'
                ,'#'
                ,'$'
                ,'%'
                ,'&'
                ,'\''
                ,'('
                ,')'
                ,'*'
                ,'+'
                ,','
                ,'-'
                ,'.'
                ,'/'
                ,':'
                ,';'
                ,'<'
                ,'='
                ,'>'
                ,'?'
                ,'@'
                ,'['
                ,'\\'
                ,']'
                ,'^'
                ,'_'
                ,'`'
                ,'{'
                ,'|'
                ,'}'
                ,'~'
                ,'Ā'
                ,'ā'
                ,'Ē'
                ,'ē'
                ,'Ī'
                ,'ī'
                ,'Ō'
                ,'ō'
                ,'Ū'
                ,'ū'
                };

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Unencoded", typeof(string));
            dt.Columns.Add("JS escape", typeof(string));
            dt.Columns.Add("JS encodeURIComponent", typeof(string));
            dt.Columns.Add("JS encodeURI", typeof(string));


            dt.Columns.Add("HttpUtility UrlEncode", typeof(string));
            dt.Columns.Add("HttpUtility UrlEncodeUnicode", typeof(string));
            dt.Columns.Add("HttpUtility UrlPathEncode", typeof(string));

            dt.Columns.Add("Uri EscapeDataString", typeof(string));
            dt.Columns.Add("Uri EscapeUriString", typeof(string));
            dt.Columns.Add("Uri HexEscape", typeof(string));

            foreach (char c in encodingSet)
            {
                System.Data.DataRow dr = dt.NewRow();

                string t = c.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string display = t;

                switch (c)
                {
                    case ' ': // Space
                        display = "[SPACE]";
                        break;
                    case '\t': // Tab
                        display = "[TAB]";
                        break;
                    case '\r': // CarriageReturn
                        display = "[CARRIAGE_RETURN]";
                        break;
                    case '\n': // n
                        display = "[NEWLINE]";
                        break;
                    case '\v': // vertical tab
                        display = "[VERTICAL_TAB]";
                        break;

                    case '\f': // form feed
                        display = "[FORM_FEED]";
                        break;
                    case '\b': // backspace
                        display = "[BACKSPACE]";
                        break;
                    case '\0': // NULL
                        display = "[NULL]";
                        break;
                    case '\xFF': // NULL
                        display = "[0xFF]";
                        break;
                }


                dr["Unencoded"] = display;
                dr["JS escape"] = JavaScript.Global.escape(t);
                dr["JS encodeURIComponent"] = JavaScript.Global.encodeURIComponent(t);
                dr["JS encodeURI"] = JavaScript.Global.encodeURI(t);

                // JavaScript.Global.XmlAttributeEscape(t);


                dr["HttpUtility UrlEncode"] = System.Web.HttpUtility.UrlEncode(t);
                dr["HttpUtility UrlEncodeUnicode"] = System.Web.HttpUtility.UrlEncodeUnicode(t);
                dr["HttpUtility UrlPathEncode"] = System.Web.HttpUtility.UrlPathEncode(t);

                dr["Uri EscapeDataString"] = System.Uri.EscapeDataString(t);
                dr["Uri EscapeUriString"] = System.Uri.EscapeUriString(t);
                try
                {
                    dr["Uri HexEscape"] = System.Uri.HexEscape(c);
                }
                catch (System.Exception ex)
                {
                    System.Console.Write("Error: \"");
                    System.Console.Write(c);
                    System.Console.Write("\": ");
                    System.Console.WriteLine(ex.Message);
                }
                
                dt.Rows.Add(dr);
            }

            return dt;

            // System.Web.HttpUtility.HtmlAttributeEncode
            // System.Web.HttpUtility.HtmlEncode

            // System.Web.HttpUtility.UrlEncode
            // System.Web.HttpUtility.UrlEncodeUnicode
            // System.Web.HttpUtility.UrlPathEncode
            // System.Uri.EscapeDataString
            // System.Uri.EscapeUriString
            // System.Uri.HexEscape


            // System.Web.HttpUtility.HtmlDecode
            // System.Web.HttpUtility.UrlDecode
            // System.Uri.UnescapeDataString
            // System.Uri.HexUnescape


            // JavaScript.Global.escape
            // JavaScript.Global.encodeURIComponent
            // JavaScript.Global.encodeURI

            // JavaScript.Global.unescape
            // JavaScript.Global.decodeURIComponent
            // JavaScript.Global.decodeURI
        }


        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.dataGridView1.DataSource = EncodeChars();
        }


    }


}
