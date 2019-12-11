
using System.Speech.Recognition;


namespace SpeechRecog
{


    public class Example1 
    {

        // https://github.com/SynHub/syn-speech-samples
        // https://github.com/SynHub/syn-speech
        public static void Test()
        {

            foreach (System.Speech.Recognition.RecognizerInfo ri in System.Speech.Recognition.SpeechRecognitionEngine.InstalledRecognizers())
            {
                System.Console.WriteLine(ri.Culture.TwoLetterISOLanguageName);
            }
            

            // Create an in-process speech recognizer for the en-US locale.  
            using (System.Speech.Recognition.SpeechRecognitionEngine recognizer = new System.Speech.Recognition.SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("en-US")
                ))
            {

                // Create and load a dictation grammar.  
                recognizer.LoadGrammar(new System.Speech.Recognition.DictationGrammar());

                // Add a handler for the speech recognized event.  
                recognizer.SpeechRecognized +=
                  new System.EventHandler<System.Speech.Recognition.SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

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
                }
            }
        }


        // Handle the SpeechRecognized event.  
        static void recognizer_SpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine("Recognized text: " + e.Result.Text);
        }


    }

}
