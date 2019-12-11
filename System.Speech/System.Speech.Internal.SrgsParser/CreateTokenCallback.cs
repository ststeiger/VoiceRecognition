namespace System.Speech.Internal.SrgsParser
{
	internal delegate IToken CreateTokenCallback(IElement parent, string content, string pronumciation, string display, float reqConfidence);
}
