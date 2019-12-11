namespace System.Speech.Internal.SrgsParser
{
	internal interface IElement
	{
		void PostParse(IElement parent);
	}
}
