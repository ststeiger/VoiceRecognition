namespace System.Speech.Recognition
{
	public class SpeechDetectedEventArgs : EventArgs
	{
		private TimeSpan _audioPosition;

		public TimeSpan AudioPosition => _audioPosition;

		internal SpeechDetectedEventArgs(TimeSpan audioPosition)
		{
			_audioPosition = audioPosition;
		}
	}
}
