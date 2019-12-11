using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[ComImport]
	[Guid("259684DC-37C3-11D2-9603-00C04F8EE628")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ISpNotifySink
	{
		void Notify();
	}
}
