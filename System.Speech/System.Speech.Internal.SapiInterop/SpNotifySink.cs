using System.Threading;

namespace System.Speech.Internal.SapiInterop
{
	internal class SpNotifySink : ISpNotifySink
	{
		private WeakReference _eventNotifyReference;

		public SpNotifySink(EventNotify eventNotify)
		{
			_eventNotifyReference = new WeakReference(eventNotify);
		}

		void ISpNotifySink.Notify()
		{
			EventNotify eventNotify = (EventNotify)_eventNotifyReference.Target;
			if (eventNotify != null)
			{
				ThreadPool.QueueUserWorkItem(eventNotify.SendNotification);
			}
		}
	}
}
