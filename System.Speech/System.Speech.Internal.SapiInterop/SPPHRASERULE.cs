using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class SPPHRASERULE
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pszName;

		internal uint ulId;

		internal uint ulFirstElement;

		internal uint ulCountOfElements;

		internal IntPtr pNextSibling;

		internal IntPtr pFirstChild;

		internal float SREngineConfidence;

		internal byte Confidence;
	}
}
