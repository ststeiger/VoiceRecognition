if (!window.SpeechRecognition)
    window.SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
var Dictation = (function () {
    function Dictation() {
        var that = this;
        this.startDictation = this.startDictation.bind(this);
        this.final_transcript = "";
        this.recognizing = false;
        if ('webkitSpeechRecognition' in window) {
            this.recognition = new SpeechRecognition();
            this.recognition.continuous = true;
            this.recognition.interimResults = true;
            this.recognition.onstart = function () {
                that.recognizing = true;
            };
            this.recognition.onerror = function (event) {
                console.log(event.error);
            };
            this.recognition.onend = function () {
                that.recognizing = false;
            };
            this.recognition.onresult = function (event) {
                var interim_transcript = '';
                for (var i = event.resultIndex; i < event.results.length; ++i) {
                    if (event.results[i].isFinal) {
                        that.final_transcript += event.results[i][0].transcript;
                    }
                    else {
                        interim_transcript += event.results[i][0].transcript;
                    }
                }
                var two_line = /\n\n/g;
                var one_line = /\n/g;
                function linebreak(s) {
                    return s.replace(two_line, '<p></p>').replace(one_line, '<br>');
                }
                function capitalize(s) {
                    return s.replace(s.substr(0, 1), function (m) { return m.toUpperCase(); });
                }
                that.final_transcript = capitalize(that.final_transcript);
                that.final_span.innerHTML = linebreak(that.final_transcript);
                that.interim_span.innerHTML = linebreak(interim_transcript);
            };
        }
    }
    Dictation.prototype.startDictation = function (event) {
        if ('webkitSpeechRecognition' in window) {
            if (this.recognizing) {
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
    };
    return Dictation;
}());
var aaa = new Dictation();
aaa.startDictation(null);
