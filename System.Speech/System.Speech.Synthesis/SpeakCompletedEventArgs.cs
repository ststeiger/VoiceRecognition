namespace System.Speech.Synthesis
{
	public class SpeakCompletedEventArgs : PromptEventArgs
	{
		internal SpeakCompletedEventArgs(Prompt prompt)
			: base(prompt)
		{
		}
	}
}
