namespace System.Speech.Internal.SrgsParser
{
	internal interface IScript : IElement
	{
		IScript Create(string rule, RuleMethodScript onInit);
	}
}
