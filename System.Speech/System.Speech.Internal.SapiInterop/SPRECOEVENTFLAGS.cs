namespace System.Speech.Internal.SapiInterop
{
	[Flags]
	internal enum SPRECOEVENTFLAGS
	{
		SPREF_AutoPause = 0x1,
		SPREF_Emulated = 0x2,
		SPREF_SMLTimeout = 0x4,
		SPREF_ExtendableParse = 0x8,
		SPREF_ReSent = 0x10,
		SPREF_Hypothesis = 0x20,
		SPREF_FalseRecognition = 0x40
	}
}
