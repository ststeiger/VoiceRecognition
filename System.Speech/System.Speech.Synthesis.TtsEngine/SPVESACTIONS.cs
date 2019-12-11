namespace System.Speech.Synthesis.TtsEngine
{
	[Flags]
	internal enum SPVESACTIONS
	{
		SPVES_CONTINUE = 0x0,
		SPVES_ABORT = 0x1,
		SPVES_SKIP = 0x2,
		SPVES_RATE = 0x4,
		SPVES_VOLUME = 0x8
	}
}
