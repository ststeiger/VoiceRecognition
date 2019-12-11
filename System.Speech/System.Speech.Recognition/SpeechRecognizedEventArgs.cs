namespace System.Speech.Recognition
{
	[Serializable]
	public class SpeechRecognizedEventArgs : RecognitionEventArgs
	{
		internal SpeechRecognizedEventArgs(RecognitionResult result)
			: base(result)
		{
		}
	}
}
