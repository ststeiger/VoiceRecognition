namespace System.Speech.Recognition
{
	[Serializable]
	public class SpeechRecognitionRejectedEventArgs : RecognitionEventArgs
	{
		internal SpeechRecognitionRejectedEventArgs(RecognitionResult result)
			: base(result)
		{
		}
	}
}
