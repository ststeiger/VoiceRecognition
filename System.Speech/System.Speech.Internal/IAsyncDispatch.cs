namespace System.Speech.Internal
{
	internal interface IAsyncDispatch
	{
		void Post(object evt);

		void Post(object[] evt);

		void PostOperation(Delegate callback, params object[] parameters);
	}
}
