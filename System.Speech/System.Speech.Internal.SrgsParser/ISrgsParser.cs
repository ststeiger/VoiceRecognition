namespace System.Speech.Internal.SrgsParser
{
	internal interface ISrgsParser
	{
		IElementFactory ElementFactory
		{
			set;
		}

		void Parse();
	}
}
