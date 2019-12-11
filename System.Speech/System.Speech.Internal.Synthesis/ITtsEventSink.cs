namespace System.Speech.Internal.Synthesis
{
	internal interface ITtsEventSink
	{
		void AddEvent(TTSEvent evt);

		void FlushEvent();
	}
}
