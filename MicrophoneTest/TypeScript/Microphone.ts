
namespace mic2
{




    function foo()
    {
        return new Promise(function(resolve, reject)
        {
            // navigator.mediaDevices.enumerateDevices(resolve, reject);
        });
        
    }
    async function bar(){
        let devs:MediaDeviceInfo[] = await navigator.mediaDevices.enumerateDevices();
            // devs.filter((d) => d.kind === 'audioinput');

    }
    
    
    function getUserMediaPromise(constraints: MediaStreamConstraints): Promise<MediaStream>
    {
        // https://italonascimento.github.io/applying-a-timeout-to-your-promises/
        // https://javascript.info/promise-error-handling
        // https://developers.google.com/web/fundamentals/primers/async-functions
        // https://developers.google.com/web/fundamentals/media/capturing-images/
        // https://www.youtube.com/watch?v=K6L38xk2rkk

        return new Promise(function(resolve, reject)
        {
            navigator.getUserMedia(constraints, resolve, reject);
        });
    }


    async function getUserMediaAsyncTest()
    {
        try
        {
            let stream: MediaStream = await getUserMediaPromise({ audio: true });
        }
        catch (e)
        {
            console.log(e.name);
            console.log(e.message);

            console.log(e.fileName);
            console.log(e.lineNumber);
            console.log(e.columnNumber);
            console.log(e.stack);
        }

    }
    
    async function batteryInfo()
    {
        // await navigator.getBattery()
    }
    
    async function ReplayVideo()
    {
        try
        {
            // <video id="player" controls autoplay></video>
            let video = <HTMLVideoElement>document.getElementById("player");
            // let stream: MediaStream = await getUserMediaPromise({ video: true, audio: true });
            let stream: MediaStream = await navigator.getDisplayMedia({video: true, audio: true});
            video.srcObject = stream;
        } catch (e)
        {
            console.log(e.message);
        }
    }


    function stopVideoOld(stream: MediaStream)
    {
        let as: MediaStreamTrack[] = stream.getAudioTracks();
        let vs: MediaStreamTrack[] = stream.getVideoTracks();
        
        stream.getAudioTracks().forEach(function(track) 
        {
            track.stop();
        });

        stream.getVideoTracks().forEach(function(track) 
        {
            track.stop();
        });
        
        stream = null;
    }
    
    
    function stopVideo(stream: MediaStream)
    {
        let ts: MediaStreamTrack[] = stream.getTracks();
        
        for(let i =0; i < ts.length; ++i)
        {
            ts[i].stop();
        }
        
        stream = null;
    }
    
    
    let webaudio_tooling_obj = function ()
    {

        let audioContext = new AudioContext();

        console.log("audio is starting up ...");

        let BUFF_SIZE = 16384;

        let audioInput = null,
            microphone_stream = null,
            gain_node = null,
            script_processor_node = null,
            script_processor_fft_node = null,
            analyserNode = null;

        if (!navigator.getUserMedia)
            navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia ||
                navigator.mozGetUserMedia || navigator.msGetUserMedia;

        if (navigator.getUserMedia)
        {
            navigator.getUserMedia({ audio: true },
                function (stream: MediaStream)
                {
                    start_microphone(stream);
                },
                function (e: MediaStreamError)
                {
                    alert('Error capturing audio.');
                }
            );

        }
        else
        {
            alert('getUserMedia not supported in this browser.');
        }


        function show_some_data(given_typed_array, num_row_to_display, label)
        {

            let size_buffer = given_typed_array.length;
            let index = 0;
            let max_index = num_row_to_display;

            console.log("__________ " + label);

            for (; index < max_index && index < size_buffer; index += 1)
            {

                console.log(given_typed_array[index]);
            }
        }

        function process_microphone_buffer(event)
        {
            // invoked by event loop
            let i, N, inp, microphone_output_buffer;

            microphone_output_buffer = event.inputBuffer.getChannelData(0); // just mono - 1 channel for now

            // microphone_output_buffer  <-- this buffer contains current gulp of data size BUFF_SIZE
            show_some_data(microphone_output_buffer, 5, "from getChannelData");
        }

        function start_microphone(stream)
        {
            gain_node = audioContext.createGain();
            gain_node.connect(audioContext.destination);

            microphone_stream = audioContext.createMediaStreamSource(stream);
            microphone_stream.connect(gain_node);

            script_processor_node = audioContext.createScriptProcessor(BUFF_SIZE, 1, 1);
            script_processor_node.onaudioprocess = process_microphone_buffer;

            microphone_stream.connect(script_processor_node);

            // --- enable volume control for output speakers

            // <p>Volume</p><input id="volume" type = "range" min = "0" max = "1" step = "0.1" value = "0.5" />
            document.getElementById('volume').addEventListener('change', function ()
            {
                let curr_volume = (<HTMLInputElement>this).value;
                gain_node.gain.value = curr_volume;

                console.log("curr_volume ", curr_volume);
            });

            // --- setup FFT

            script_processor_fft_node = audioContext.createScriptProcessor(2048, 1, 1);
            script_processor_fft_node.connect(gain_node);

            analyserNode = audioContext.createAnalyser();
            analyserNode.smoothingTimeConstant = 0;
            analyserNode.fftSize = 2048;

            microphone_stream.connect(analyserNode);

            analyserNode.connect(script_processor_fft_node);

            script_processor_fft_node.onaudioprocess = function ()
            {

                // get the average for the first channel
                let array = new Uint8Array(analyserNode.frequencyBinCount);
                analyserNode.getByteFrequencyData(array);

                // draw the spectrogram
                if (microphone_stream.playbackState == microphone_stream.PLAYING_STATE)
                {

                    show_some_data(array, 5, "from fft");
                }
            };
        }

    }(); //  webaudio_tooling_obj = function()


}



namespace Micro
{

    

    async function recordAudio()
    {
        // https://blog.sambego.be/turn-your-browser-into-an-audio-recorder/
        // https://caniuse.com/#feat=mediarecorder
        // https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder
        // https://developer.mozilla.org/en-US/docs/Web/API/MediaRecorder/mimeType
        // https://developer.mozilla.org/en-US/docs/Web/Media/Formats
        // https://www.iana.org/assignments/media-types/media-types.xhtml
        // https://jsfiddle.net/remarkablemark/8763r506/
        
        try
        {
            // <video id="player" controls autoplay></video>
            let audioElement = document.getElementById("player");
            
            let chunks = [];
            function saveChunkToRecording(event) 
            {
                chunks.push(event.data);
            }
            
            function saveRecording()
            {
                let blob = new Blob(chunks, {
                    type: 'audio/mp4; codecs=opus'
                });
                
                let url = URL.createObjectURL(blob);
                // With this Blob, we can create a data-url 
                // which we can set as the src of an <audio> element.
                audioElement.setAttribute('src', url);
            }
            
            let stream = await navigator.getDisplayMedia({ audio: true });
            let recorder = new MediaRecorder(stream);
            recorder.ondataavailable = saveChunkToRecording;
            recorder.onstop = saveRecording;
            
            recorder.start();
            // recoder.stop();
        }
        catch (e)
        {
            console.log(e.message);
        }

    }
    

    async function askForMicrophoneAsync()
    {

        let permissionStatus: PermissionStatus = await navigator.permissions.query({ name: 'microphone' });

        if (permissionStatus.state == 'granted')
        {

        } else if (permissionStatus.state == 'prompt')
        {

        } else if (permissionStatus.state == 'denied')
        {

        }
        permissionStatus.onchange = function ()
        {

        };

    }


    async function askGeoAsync()
    {
        let permissionStatus: PermissionStatus = await navigator.permissions.query({ name: 'geolocation' });

        console.log('geolocation permission state is ', permissionStatus.state);
        permissionStatus.onchange = () =>
        {
            console.log('geolocation permission state has changed');
        };

    }


    function askForMicrophone()
    {
        navigator.permissions.query({ name: 'microphone' }).then(function (result)
        {
            if (result.state == 'granted')
            {

            } else if (result.state == 'prompt')
            {

            } else if (result.state == 'denied')
            {

            }
            result.onchange = function ()
            {

            };
        });
    }


    function askGeo()
    {
        const perm = navigator.permissions;
        perm.query({ name: 'geolocation' })
            .then(permissionStatus =>
            {
                console.log('geolocation permission state is ', permissionStatus.state);
                permissionStatus.onchange = () =>
                {
                    console.log('geolocation permission state has changed');
                };
            });
    }


    function askGeolocation()
    {
        navigator.permissions.query({ name: 'geolocation' }).then(function (result)
        {
            if (result.state === 'granted')
            {
                // showMap();
            } else if (result.state === 'prompt')
            {
                // showButtonToEnableMap();
            }
            // Don't do anything if the permission was denied.
        });
    }


    export function test()
    {
        return "123";
    }


}
