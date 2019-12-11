using System.IO;

namespace System.Speech.Synthesis.TtsEngine
{
	public interface ITtsEngineSite
	{
		int EventInterest
		{
			get;
		}

		int Actions
		{
			get;
		}

		int Rate
		{
			get;
		}

		int Volume
		{
			get;
		}

		void AddEvents(SpeechEventInfo[] events, int count);

		int Write(IntPtr data, int count);

		SkipInfo GetSkipInfo();

		void CompleteSkip(int skipped);

		Stream LoadResource(Uri uri, string mediaType);
	}
}
