using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	internal struct SPRECOCONTEXTSTATUS
	{
		internal SPINTERFERENCE eInterference;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		internal short[] szRequestTypeOfUI;

		internal uint dwReserved1;

		internal uint dwReserved2;
	}
}
