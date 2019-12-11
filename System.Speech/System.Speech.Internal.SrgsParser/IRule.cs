namespace System.Speech.Internal.SrgsParser
{
	internal interface IRule : IElement
	{
		string BaseClass
		{
			get;
			set;
		}

		void CreateScript(IGrammar grammar, string rule, string method, RuleMethodScript type);
	}
}
