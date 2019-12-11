namespace System.Speech.Synthesis
{
	public class SpeakStartedEventArgs : PromptEventArgs
	{
		internal SpeakStartedEventArgs(Prompt prompt)
			: base(prompt)
		{
		}
	}
}
