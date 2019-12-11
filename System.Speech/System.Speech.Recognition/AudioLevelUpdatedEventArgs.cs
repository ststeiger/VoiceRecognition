namespace System.Speech.Recognition
{
	public class AudioLevelUpdatedEventArgs : EventArgs
	{
		private int _audioLevel;

		public int AudioLevel => _audioLevel;

		internal AudioLevelUpdatedEventArgs(int audioLevel)
		{
			_audioLevel = audioLevel;
		}
	}
}
