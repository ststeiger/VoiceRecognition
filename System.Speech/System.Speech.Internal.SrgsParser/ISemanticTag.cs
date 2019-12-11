namespace System.Speech.Internal.SrgsParser
{
	internal interface ISemanticTag : IElement
	{
		void Content(IElement parent, string value, int line);
	}
}
