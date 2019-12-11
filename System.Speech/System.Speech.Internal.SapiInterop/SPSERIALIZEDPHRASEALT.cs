using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[StructLayout(LayoutKind.Sequential)]
	internal class SPSERIALIZEDPHRASEALT
	{
		internal uint ulStartElementInParent;

		internal uint cElementsInParent;

		internal uint cElementsInAlternate;

		internal uint cbAltExtra;
	}
}
