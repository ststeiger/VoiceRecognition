namespace System.Speech.Internal.SapiInterop
{
	[Serializable]
	internal struct SPRECORESULTTIMES
	{
		internal FILETIME ftStreamTime;

		internal ulong ullLength;

		internal uint dwTickCount;

		internal ulong ullStart;
	}
}
