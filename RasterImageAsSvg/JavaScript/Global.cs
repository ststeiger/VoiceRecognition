
namespace JavaScript
{


    public class Global
    {


        private enum URISetType
        {
            None,
            Reserved,
            Unescaped
        }

        internal static int HexDigit(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - 48;
            }
            if (c >= 'A' && c <= 'F')
            {
                return 10 + c - 65;
            }
            if (c >= 'a' && c <= 'f')
            {
                return 10 + c - 97;
            }
            return -1;
        }

        private static byte HexValue(char ch1, char ch2)
        {
            int num;
            int num2;
            if ((num = HexDigit(ch1)) < 0 || (num2 = HexDigit(ch2)) < 0)
            {
                throw new JavaScriptException(JavaScriptError.URIDecodeError);
            }
            return (byte)((num << 4) | num2);
        }


        private static bool InURISet(char ch, URISetType flags)
        {
            if ((flags & URISetType.Unescaped) != 0)
            {
                if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                {
                    return true;
                }
                switch (ch)
                {
                    case '!':
                    case '\'':
                    case '(':
                    case ')':
                    case '*':
                    case '-':
                    case '.':
                    case '_':
                    case '~':
                        return true;
                }
            }
            if ((flags & URISetType.Reserved) != 0)
            {
                switch (ch)
                {
                    case '#':
                    case '$':
                    case '&':
                    case '+':
                    case ',':
                    case '/':
                    case ':':
                    case ';':
                    case '=':
                    case '?':
                    case '@':
                        return true;
                }
            }
            return false;
        }


        private static string Decode(string encodedURI, URISetType flags)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < encodedURI.Length; i++)
            {
                char c = encodedURI[i];
                if (c != '%')
                {
                    stringBuilder.Append(c);
                    continue;
                }
                int num = i;
                if (i + 2 >= encodedURI.Length)
                {
                    throw new JavaScriptException(JavaScriptError.URIDecodeError);
                }
                byte b = HexValue(encodedURI[i + 1], encodedURI[i + 2]);
                i += 2;
                char c2;
                if ((b & 0x80) == 0)
                {
                    c2 = (char)b;
                }
                else
                {
                    int j;
                    for (j = 1; ((b << j) & 0x80) != 0; j++)
                    {
                    }
                    if (j == 1 || j > 4 || i + (j - 1) * 3 >= encodedURI.Length)
                    {
                        throw new JavaScriptException(JavaScriptError.URIDecodeError);
                    }
                    int num2 = b & (255 >> j + 1);
                    while (j > 1)
                    {
                        if (encodedURI[i + 1] != '%')
                        {
                            throw new JavaScriptException(JavaScriptError.URIDecodeError);
                        }
                        b = HexValue(encodedURI[i + 2], encodedURI[i + 3]);
                        i += 3;
                        if ((b & 0xC0) != 128)
                        {
                            throw new JavaScriptException(JavaScriptError.URIDecodeError);
                        }
                        num2 = ((num2 << 6) | (b & 0x3F));
                        j--;
                    }
                    if (num2 >= 55296 && num2 < 57344)
                    {
                        throw new JavaScriptException(JavaScriptError.URIDecodeError);
                    }
                    if (num2 >= 65536)
                    {
                        if (num2 > 1114111)
                        {
                            throw new JavaScriptException(JavaScriptError.URIDecodeError);
                        }
                        stringBuilder.Append((char)(((num2 - 65536 >> 10) & 0x3FF) + 55296));
                        stringBuilder.Append((char)(((num2 - 65536) & 0x3FF) + 56320));
                        continue;
                    }
                    c2 = (char)num2;
                }
                if (InURISet(c2, flags))
                {
                    stringBuilder.Append(encodedURI, num, i - num + 1);
                }
                else
                {
                    stringBuilder.Append(c2);
                }
            }
            return stringBuilder.ToString();
        }

        public static string decodeURI(string encodedURI)
        {
            return Decode(encodedURI, URISetType.Reserved);
        }

        public static string decodeURIComponent(string encodedURI)
        {
            return Decode(encodedURI, URISetType.None);
        }

        private static void AppendInHex(System.Text.StringBuilder bs, int value)
        {
            bs.Append('%');
            int num = (value >> 4) & 0xF;
            bs.Append((char)((num >= 10) ? (num - 10 + 65) : (num + 48)));
            num = (value & 0xF);
            bs.Append((char)((num >= 10) ? (num - 10 + 65) : (num + 48)));
        }

        private static string Encode(string uri, URISetType flags)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < uri.Length; i++)
            {
                char c = uri[i];
                if (InURISet(c, flags))
                {
                    stringBuilder.Append(c);
                    continue;
                }
                int num = c;
                if (num >= 0 && num <= 127)
                {
                    AppendInHex(stringBuilder, num);
                    continue;
                }
                if (num >= 128 && num <= 2047)
                {
                    AppendInHex(stringBuilder, (num >> 6) | 0xC0);
                    AppendInHex(stringBuilder, (num & 0x3F) | 0x80);
                    continue;
                }
                if (num < 55296 || num > 57343)
                {
                    AppendInHex(stringBuilder, (num >> 12) | 0xE0);
                    AppendInHex(stringBuilder, ((num >> 6) & 0x3F) | 0x80);
                    AppendInHex(stringBuilder, (num & 0x3F) | 0x80);
                    continue;
                }
                if (num >= 56320 && num <= 57343)
                {
                    throw new JavaScriptException(JavaScriptError.URIEncodeError);
                }
                if (++i >= uri.Length)
                {
                    throw new JavaScriptException(JavaScriptError.URIEncodeError);
                }
                int num2 = uri[i];
                if (num2 < 56320 || num2 > 57343)
                {
                    throw new JavaScriptException(JavaScriptError.URIEncodeError);
                }
                num = (num - 55296 << 10) + num2 + 9216;
                AppendInHex(stringBuilder, (num >> 18) | 0xF0);
                AppendInHex(stringBuilder, ((num >> 12) & 0x3F) | 0x80);
                AppendInHex(stringBuilder, ((num >> 6) & 0x3F) | 0x80);
                AppendInHex(stringBuilder, (num & 0x3F) | 0x80);
            }
            return stringBuilder.ToString();
        }


        public static string encodeURI(string uri)
        {
            return Encode(uri, (URISetType)3);
        }

        public static string encodeURIComponent(string uriComponent)
        {
            return Encode(uriComponent, URISetType.Unescaped);
        }


        public static string escape(string text)
        {
            string text2 = "0123456789ABCDEF";
            int length = text.Length;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(length * 2);
            int num = -1;
            while (++num < length)
            {
                char c = text[num];
                int num2 = c;
                if ((65 > num2 || num2 > 90) && (97 > num2 || num2 > 122) && (48 > num2 || num2 > 57) && c != '@' && c != '*' && c != '_' && c != '+' && c != '-' && c != '.' && c != '/')
                {
                    stringBuilder.Append('%');
                    if (num2 < 256)
                    {
                        stringBuilder.Append(text2[num2 / 16]);
                        c = text2[num2 % 16];
                    }
                    else
                    {
                        stringBuilder.Append('u');
                        stringBuilder.Append(text2[(num2 >> 12) % 16]);
                        stringBuilder.Append(text2[(num2 >> 8) % 16]);
                        stringBuilder.Append(text2[(num2 >> 4) % 16]);
                        c = text2[num2 % 16];
                    }
                }
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }


        /// <summary>Konvertiert Zeichen, für die das Escapezeichen % verwendet wird (@, *, _, +, -, ., /), in die angegebene Zeichenfolge im ursprünglichen Format.Die Escapezeichen werden in Unicode-Schreibweise ausgedrückt.</summary>
		/// <returns>Eine neue Kopie von <paramref name="string" />, in der die Zeichen, für die das Escapezeichen verwendet wird, in ihr ursprüngliches Format konvertiert werden.</returns>
		/// <param name="string">Die zu konvertierende Zeichenfolge.</param>
        public static string unescape(string text)
        {
            int length = text.Length;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(length);
            int num = -1;
            while (++num < length)
            {
                char c = text[num];
                if (c == '%')
                {
                    int num4;
                    int num5;
                    int num2;
                    int num3;
                    if (num + 5 < length && text[num + 1] == 'u' && (num2 = HexDigit(text[num + 2])) != -1 && (num3 = HexDigit(text[num + 3])) != -1 && (num4 = HexDigit(text[num + 4])) != -1 && (num5 = HexDigit(text[num + 5])) != -1)
                    {
                        c = (char)((num2 << 12) + (num3 << 8) + (num4 << 4) + num5);
                        num += 5;
                    }
                    else if (num + 2 < length && (num2 = HexDigit(text[num + 1])) != -1 && (num3 = HexDigit(text[num + 2])) != -1)
                    {
                        c = (char)((num2 << 4) + num3);
                        num += 2;
                    }
                }
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

    }
}
