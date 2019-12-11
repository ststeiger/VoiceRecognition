namespace System.Speech.Recognition
{
	[Serializable]
	public abstract class RecognitionEventArgs : EventArgs
	{
		private RecognitionResult _result;

		public RecognitionResult Result => _result;

		internal RecognitionEventArgs(RecognitionResult result)
		{
			_result = result;
		}
	}
}
