using System.Globalization;
using System.Runtime.Serialization;

namespace System.Speech.Internal.Synthesis
{
	[Serializable]
	internal class AudioException : Exception
	{
		internal AudioException()
		{
		}

		internal AudioException(MMSYSERR errorCode)
			: base(string.Format(CultureInfo.InvariantCulture, "{0} - Error Code: 0x{1:x}", new object[2]
			{
				SR.Get(SRID.AudioDeviceError),
				(int)errorCode
			}))
		{
		}

		protected AudioException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
