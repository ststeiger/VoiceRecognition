
namespace SpeechRecog
{


    public class Example1 
    {


        // https://github.com/SynHub/syn-speech-samples
        // https://github.com/SynHub/syn-speech
        public static void Test()
        {
            System.Console.WriteLine("System.Speech needs Microsoft Speech SDK installed (COM-Object)");
            System.Console.WriteLine("https://www.microsoft.com/en-us/download/details.aspx?id=10121");
            System.Console.WriteLine("Depending on Framework-Version, recognizers installed with language pack are missing.");
            System.Console.WriteLine("Despite NetStandard, System.Speech is Windows-ONLY !");
            System.Console.WriteLine(System.Environment.NewLine);


            System.Console.WriteLine("Installed recognizers:");

            // Searches in the COM-Object defined in HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Recognizers
            foreach (System.Speech.Recognition.RecognizerInfo ri in System.Speech.Recognition.SpeechRecognitionEngine.InstalledRecognizers())
            {
                System.Console.Write("  - ");
                // System.Console.WriteLine(ri.Culture.TwoLetterISOLanguageName);
                System.Console.WriteLine(ri.Culture.IetfLanguageTag);
            } // Next ri 


            // Create an in-process speech recognizer for the en-US locale.  
            using (System.Speech.Recognition.SpeechRecognitionEngine recognizer = new System.Speech.Recognition.SpeechRecognitionEngine(
                // new System.Globalization.CultureInfo("en-US")
                // new System.Globalization.CultureInfo("de-CH")
                System.Globalization.CultureInfo.InstalledUICulture
                ))
            {

                // Create and load a dictation grammar.  
                recognizer.LoadGrammar(new System.Speech.Recognition.DictationGrammar());

                // Add a handler for the speech recognized event.  
                recognizer.SpeechRecognized +=
                  new System.EventHandler<System.Speech.Recognition.SpeechRecognizedEventArgs>(OnSpeechRecognized);

                // Configure input to the speech recognizer.  
                recognizer.SetInputToDefaultAudioDevice();

                // recognizer.SetInputToNull();
                // recognizer.SetInputToWaveFile("");
                // recognizer.SetInputToWaveStream(new System.IO.MemoryStream());

                // System.Speech.AudioFormat.SpeechAudioFormatInfo af = 
                //    new System.Speech.AudioFormat.SpeechAudioFormatInfo(System.Speech.AudioFormat.EncodingFormat.Pcm, 4000, 12, 1, 12, 0, null);
                // recognizer.SetInputToAudioStream(new System.IO.MemoryStream(), af);


                // Start asynchronous, continuous speech recognition.  
                recognizer.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple);

                // Keep the console window open.  
                while (true)
                {
                    System.Console.ReadLine();
                } // Whend 
            } // End Using recognizer 

        } // End Sub Test 


        // Handle the SpeechRecognized event.  
        private static void OnSpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine("Recognized text: " + e.Result.Text);
        } // End Sub OnSpeechRecognized 


    } // End Class Example1 


} // End Namespace SpeechRecog 
