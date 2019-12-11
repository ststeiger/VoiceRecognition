namespace System.Speech.Internal.SapiInterop
{
	[Flags]
	internal enum SPADAPTATIONSETTINGS
	{
		SPADS_Default = 0x0,
		SPADS_CurrentRecognizer = 0x1,
		SPADS_RecoProfile = 0x2,
		SPADS_Immediate = 0x4,
		SPADS_Reset = 0x8
	}
}
