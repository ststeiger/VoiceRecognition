using System.Speech.Internal;

namespace System.Speech.Recognition
{
	public class SpeechUI
	{
		internal SpeechUI()
		{
		}

		public static bool SendTextFeedback(RecognitionResult result, string feedback, bool isSuccessfulAction)
		{
			Helpers.ThrowIfNull(result, "result");
			Helpers.ThrowIfEmptyOrNull(feedback, "feedback");
			return result.SetTextFeedback(feedback, isSuccessfulAction);
		}
	}
}
