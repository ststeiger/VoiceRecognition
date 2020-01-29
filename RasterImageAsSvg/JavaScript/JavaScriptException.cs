
namespace JavaScript
{
    

    [System.Serializable]
    public class JavaScriptException
        : System.Exception
    {

        public JavaScriptException(string message, System.Exception innerException, JavaScriptError errorNumber)
            : base(message, innerException)
        { }

        
        public JavaScriptException()
            : this("", null, JavaScriptError.NoError)
        { }


        public JavaScriptException(string message)
        : this(message, null, JavaScriptError.NoError)
        { }

        public JavaScriptException(string message, System.Exception innerException)
        : this(message, innerException, JavaScriptError.NoError)
        {

        }

        public JavaScriptException(JavaScriptError error)
        : this(error.ToString(), null, JavaScriptError.NoError)
        { }



    }
}
