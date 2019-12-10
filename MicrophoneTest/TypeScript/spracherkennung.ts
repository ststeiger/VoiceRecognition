
/*
 <div>
  <a href="#" id="start_button" onclick="startDictation(event)">Dictate</a>
</div>

<div id="results">
  <span id="final_span" class="final"></span>
  <span id="interim_span" class="interim"></span>
</div>
export interface IWindow extends Window {
  webkitSpeechRecognition: any;
}
*/


// https://ctrlq.org/code/19680-html5-web-speech-api
// https://dictation.io/speech



if (!window.SpeechRecognition)
    window.SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;


class Dictation 
{
    private final_span: HTMLSpanElement;
    private interim_span: HTMLSpanElement;

    private final_transcript: string;
    private recognition: SpeechRecognition;
    private recognizing: boolean;


    constructor()
    {
        let that = this;
        this.startDictation = this.startDictation.bind(this);
        this.final_transcript = "";
        this.recognizing = false;


        if ('webkitSpeechRecognition' in window)
        {
            this.recognition = new SpeechRecognition();

            this.recognition.continuous = true;
            this.recognition.interimResults = true;

            this.recognition.onstart = function ()
            {
                that.recognizing = true;
            };

            this.recognition.onerror = function (event)
            {
                console.log(event.error);
            };

            this.recognition.onend = function ()
            {
                that.recognizing = false;
            };

            this.recognition.onresult = function (event)
            {
                let interim_transcript = '';
                for (let i = event.resultIndex; i < event.results.length; ++i)
                {
                    if (event.results[i].isFinal)
                    {
                        that.final_transcript += event.results[i][0].transcript;
                    } else
                    {
                        interim_transcript += event.results[i][0].transcript;
                    }
                }

                let two_line = /\n\n/g;
                let one_line = /\n/g;
                function linebreak(s)
                {
                    return s.replace(two_line, '<p></p>').replace(one_line, '<br>');
                }

                function capitalize(s)
                {
                    return s.replace(s.substr(0, 1), function (m) { return m.toUpperCase(); });
                }

                that.final_transcript = capitalize(that.final_transcript);
                that.final_span.innerHTML = linebreak(that.final_transcript);
                that.interim_span.innerHTML = linebreak(interim_transcript);
            };

        } // End if ('webkitSpeechRecognition' in window) 

    } // End Constructor 


    public startDictation(event)
    {
        if ('webkitSpeechRecognition' in window)
        {
            if (this.recognizing)
            {
                this.recognition.stop();
                return;
            }

            this.final_span = document.getElementById("final_span");
            this.interim_span = document.getElementById("interim_span");

            this.final_transcript = '';
            this.recognition.lang = 'en-US';
            this.recognition.start();
            this.final_span.innerHTML = '';
            this.interim_span.innerHTML = '';
        }

    }


}


let aaa = new Dictation();
aaa.startDictation(null);
