using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[StructLayout(LayoutKind.Sequential)]
	internal class SPSEMANTICERRORINFO
	{
		internal uint ulLineNumber;

		internal uint pszScriptLineOffset;

		internal uint pszSourceOffset;

		internal uint pszDescriptionOffset;

		internal int hrResultCode;
	}
}
