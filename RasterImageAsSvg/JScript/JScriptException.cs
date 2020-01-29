
namespace Microsoft.JScript
{
    

    [System.Serializable]
    public class JScriptException
        : System.Exception
    {

        public JScriptException(string message, System.Exception innerException, JSError errorNumber)
            : base(message, innerException)
        { }

        
        public JScriptException()
            : this("", null, JSError.NoError)
        { }


        public JScriptException(string message)
        : this(message, null, JSError.NoError)
        { }

        public JScriptException(string message, System.Exception innerException)
        : this(message, innerException, JSError.NoError)
        {

        }

        public JScriptException(JSError error)
        : this(error.ToString(), null, JSError.NoError)
        { }



    }
}
