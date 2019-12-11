namespace System.Speech.Recognition
{
	[Serializable]
	public class SpeechHypothesizedEventArgs : RecognitionEventArgs
	{
		internal SpeechHypothesizedEventArgs(RecognitionResult result)
			: base(result)
		{
		}
	}
}
