
namespace SpeechRecogCore
{


    class Program
    {


        public static void TestResourceNamespace()
        {
            System.Resources.ResourceManager resourceManager =
                    new System.Resources.ResourceManager("System.Speech.ExceptionStringTable", typeof(System.Speech.Synthesis.InstalledVoice).Assembly);

            string foo = resourceManager.GetString("AlreadyInLex");
            System.Console.WriteLine(foo);
        } // End Sub TestResourceNamespace 


        static void Main(string[] args)
        {
            SpeechRecog.Example1.Test();
            SpeechRecog.Example2.Test();

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace SpeechRecogCore 
