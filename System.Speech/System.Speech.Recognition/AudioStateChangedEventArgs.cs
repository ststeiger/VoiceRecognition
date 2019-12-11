namespace System.Speech.Recognition
{
	public class AudioStateChangedEventArgs : EventArgs
	{
		private AudioState _audioState;

		public AudioState AudioState => _audioState;

		internal AudioStateChangedEventArgs(AudioState audioState)
		{
			_audioState = audioState;
		}
	}
}
