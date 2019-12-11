namespace System.Speech.Internal.SrgsParser
{
	internal interface IToken : IElement
	{
		string Text
		{
			set;
		}

		string Display
		{
			set;
		}

		string Pronunciation
		{
			set;
		}
	}
}
