using System.Runtime.InteropServices;

namespace System.Speech.Synthesis.TtsEngine
{
	[TypeLibType(16)]
	internal enum SPVACTIONS
	{
		SPVA_Speak,
		SPVA_Silence,
		SPVA_Pronounce,
		SPVA_Bookmark,
		SPVA_SpellOut,
		SPVA_Section,
		SPVA_ParseUnknownTag
	}
}
