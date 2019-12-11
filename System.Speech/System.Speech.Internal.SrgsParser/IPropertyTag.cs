namespace System.Speech.Internal.SrgsParser
{
	internal interface IPropertyTag : IElement
	{
		void NameValue(IElement parent, string name, object value);
	}
}
