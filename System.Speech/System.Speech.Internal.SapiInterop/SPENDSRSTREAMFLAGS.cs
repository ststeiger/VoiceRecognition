namespace System.Speech.Internal.SapiInterop
{
	[Flags]
	internal enum SPENDSRSTREAMFLAGS
	{
		SPESF_NONE = 0x0,
		SPESF_STREAM_RELEASED = 0x1,
		SPESF_EMULATED = 0x2
	}
}
