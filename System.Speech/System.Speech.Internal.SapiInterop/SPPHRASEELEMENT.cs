using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class SPPHRASEELEMENT
	{
		internal uint ulAudioTimeOffset;

		internal uint ulAudioSizeTime;

		internal uint ulAudioStreamOffset;

		internal uint ulAudioSizeBytes;

		internal uint ulRetainedStreamOffset;

		internal uint ulRetainedSizeBytes;

		internal IntPtr pszDisplayText;

		internal IntPtr pszLexicalForm;

		internal IntPtr pszPronunciation;

		internal byte bDisplayAttributes;

		internal byte RequiredConfidence;

		internal byte ActualConfidence;

		internal byte Reserved;

		internal float SREngineConfidence;
	}
}
