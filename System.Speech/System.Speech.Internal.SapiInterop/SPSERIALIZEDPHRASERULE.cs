using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class SPSERIALIZEDPHRASERULE
	{
		internal uint pszNameOffset;

		internal uint ulId;

		internal uint ulFirstElement;

		internal uint ulCountOfElements;

		internal uint NextSiblingOffset;

		internal uint FirstChildOffset;

		internal float SREngineConfidence;

		internal sbyte Confidence;
	}
}
