using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[StructLayout(LayoutKind.Sequential)]
	internal class SPPHRASEREPLACEMENT
	{
		internal byte bDisplayAttributes;

		internal uint pszReplacementText;

		internal uint ulFirstElement;

		internal uint ulCountOfElements;
	}
}
