
namespace SpeechRecog
{


    public class Example2
    {


        public static void Test()
        {
            System.Speech.Recognition.SpeechRecognitionEngine recEngine = new System.Speech.Recognition.SpeechRecognitionEngine();
            recEngine.SetInputToDefaultAudioDevice();

            System.Speech.Recognition.Choices commands = new System.Speech.Recognition.Choices();
            commands.Add(new string[] { "say Hi", "say Hello" });
            System.Speech.Recognition.GrammarBuilder gb = new System.Speech.Recognition.GrammarBuilder();
            gb.Append(commands);
            System.Speech.Recognition.Grammar g = new System.Speech.Recognition.Grammar(gb);

            recEngine.LoadGrammarAsync(g);
            recEngine.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple);

            recEngine.SpeechRecognized += OnSpeechRecognized;
        } // End Sub Test 


        // Create a simple handler for the SpeechRecognized event
        static void OnSpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine("Speech recognized: {0}", e.Result.Text);
            switch (e.Result.Text)
            {
                case "Red":
                    System.Console.WriteLine("you said hi");
                    break;
                    //  default:
                    //  break;
            } // End Switch 
        } // End Sub OnSpeechRecognized


    } // End Class Example2 


} // End Namespace SpeechRecog
