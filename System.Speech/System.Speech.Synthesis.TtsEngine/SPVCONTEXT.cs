using System.Runtime.InteropServices;

namespace System.Speech.Synthesis.TtsEngine
{
	[TypeLibType(16)]
	internal struct SPVCONTEXT
	{
		public IntPtr pCategory;

		public IntPtr pBefore;

		public IntPtr pAfter;
	}
}
