var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var mic2;
(function (mic2) {
    function foo() {
        return new Promise(function (resolve, reject) {
        });
    }
    function bar() {
        return __awaiter(this, void 0, void 0, function () {
            var devs;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4, navigator.mediaDevices.enumerateDevices()];
                    case 1:
                        devs = _a.sent();
                        return [2];
                }
            });
        });
    }
    function getUserMediaPromise(constraints) {
        return new Promise(function (resolve, reject) {
            navigator.getUserMedia(constraints, resolve, reject);
        });
    }
    function getUserMediaAsyncTest() {
        return __awaiter(this, void 0, void 0, function () {
            var stream, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        return [4, getUserMediaPromise({ audio: true })];
                    case 1:
                        stream = _a.sent();
                        return [3, 3];
                    case 2:
                        e_1 = _a.sent();
                        console.log(e_1.name);
                        console.log(e_1.message);
                        console.log(e_1.fileName);
                        console.log(e_1.lineNumber);
                        console.log(e_1.columnNumber);
                        console.log(e_1.stack);
                        return [3, 3];
                    case 3: return [2];
                }
            });
        });
    }
    function batteryInfo() {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2];
            });
        });
    }
    function ReplayVideo() {
        return __awaiter(this, void 0, void 0, function () {
            var video, stream, e_2;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        video = document.getElementById("player");
                        return [4, navigator.getDisplayMedia({ video: true, audio: true })];
                    case 1:
                        stream = _a.sent();
                        video.srcObject = stream;
                        return [3, 3];
                    case 2:
                        e_2 = _a.sent();
                        console.log(e_2.message);
                        return [3, 3];
                    case 3: return [2];
                }
            });
        });
    }
    function stopVideoOld(stream) {
        var as = stream.getAudioTracks();
        var vs = stream.getVideoTracks();
        stream.getAudioTracks().forEach(function (track) {
            track.stop();
        });
        stream.getVideoTracks().forEach(function (track) {
            track.stop();
        });
        stream = null;
    }
    function stopVideo(stream) {
        var ts = stream.getTracks();
        for (var i = 0; i < ts.length; ++i) {
            ts[i].stop();
        }
        stream = null;
    }
    var webaudio_tooling_obj = function () {
        var audioContext = new AudioContext();
        console.log("audio is starting up ...");
        var BUFF_SIZE = 16384;
        var audioInput = null, microphone_stream = null, gain_node = null, script_processor_node = null, script_processor_fft_node = null, analyserNode = null;
        if (!navigator.getUserMedia)
            navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia ||
                navigator.mozGetUserMedia || navigator.msGetUserMedia;
        if (navigator.getUserMedia) {
            navigator.getUserMedia({ audio: true }, function (stream) {
                start_microphone(stream);
            }, function (e) {
                alert('Error capturing audio.');
            });
        }
        else {
            alert('getUserMedia not supported in this browser.');
        }
        function show_some_data(given_typed_array, num_row_to_display, label) {
            var size_buffer = given_typed_array.length;
            var index = 0;
            var max_index = num_row_to_display;
            console.log("__________ " + label);
            for (; index < max_index && index < size_buffer; index += 1) {
                console.log(given_typed_array[index]);
            }
        }
        function process_microphone_buffer(event) {
            var i, N, inp, microphone_output_buffer;
            microphone_output_buffer = event.inputBuffer.getChannelData(0);
            show_some_data(microphone_output_buffer, 5, "from getChannelData");
        }
        function start_microphone(stream) {
            gain_node = audioContext.createGain();
            gain_node.connect(audioContext.destination);
            microphone_stream = audioContext.createMediaStreamSource(stream);
            microphone_stream.connect(gain_node);
            script_processor_node = audioContext.createScriptProcessor(BUFF_SIZE, 1, 1);
            script_processor_node.onaudioprocess = process_microphone_buffer;
            microphone_stream.connect(script_processor_node);
            document.getElementById('volume').addEventListener('change', function () {
                var curr_volume = this.value;
                gain_node.gain.value = curr_volume;
                console.log("curr_volume ", curr_volume);
            });
            script_processor_fft_node = audioContext.createScriptProcessor(2048, 1, 1);
            script_processor_fft_node.connect(gain_node);
            analyserNode = audioContext.createAnalyser();
            analyserNode.smoothingTimeConstant = 0;
            analyserNode.fftSize = 2048;
            microphone_stream.connect(analyserNode);
            analyserNode.connect(script_processor_fft_node);
            script_processor_fft_node.onaudioprocess = function () {
                var array = new Uint8Array(analyserNode.frequencyBinCount);
                analyserNode.getByteFrequencyData(array);
                if (microphone_stream.playbackState == microphone_stream.PLAYING_STATE) {
                    show_some_data(array, 5, "from fft");
                }
            };
        }
    }();
})(mic2 || (mic2 = {}));
var Micro;
(function (Micro) {
    function recordAudio() {
        return __awaiter(this, void 0, void 0, function () {
            function saveChunkToRecording(event) {
                chunks_1.push(event.data);
            }
            function saveRecording() {
                var blob = new Blob(chunks_1, {
                    type: 'audio/mp4; codecs=opus'
                });
                var url = URL.createObjectURL(blob);
                audioElement_1.setAttribute('src', url);
            }
            var audioElement_1, chunks_1, stream, recorder, e_3;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        audioElement_1 = document.getElementById("player");
                        chunks_1 = [];
                        return [4, navigator.getDisplayMedia({ audio: true })];
                    case 1:
                        stream = _a.sent();
                        recorder = new MediaRecorder(stream);
                        recorder.ondataavailable = saveChunkToRecording;
                        recorder.onstop = saveRecording;
                        recorder.start();
                        return [3, 3];
                    case 2:
                        e_3 = _a.sent();
                        console.log(e_3.message);
                        return [3, 3];
                    case 3: return [2];
                }
            });
        });
    }
    function askForMicrophoneAsync() {
        return __awaiter(this, void 0, void 0, function () {
            var permissionStatus;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4, navigator.permissions.query({ name: 'microphone' })];
                    case 1:
                        permissionStatus = _a.sent();
                        if (permissionStatus.state == 'granted') {
                        }
                        else if (permissionStatus.state == 'prompt') {
                        }
                        else if (permissionStatus.state == 'denied') {
                        }
                        permissionStatus.onchange = function () {
                        };
                        return [2];
                }
            });
        });
    }
    function askGeoAsync() {
        return __awaiter(this, void 0, void 0, function () {
            var permissionStatus;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4, navigator.permissions.query({ name: 'geolocation' })];
                    case 1:
                        permissionStatus = _a.sent();
                        console.log('geolocation permission state is ', permissionStatus.state);
                        permissionStatus.onchange = function () {
                            console.log('geolocation permission state has changed');
                        };
                        return [2];
                }
            });
        });
    }
    function askForMicrophone() {
        navigator.permissions.query({ name: 'microphone' }).then(function (result) {
            if (result.state == 'granted') {
            }
            else if (result.state == 'prompt') {
            }
            else if (result.state == 'denied') {
            }
            result.onchange = function () {
            };
        });
    }
    function askGeo() {
        var perm = navigator.permissions;
        perm.query({ name: 'geolocation' })
            .then(function (permissionStatus) {
            console.log('geolocation permission state is ', permissionStatus.state);
            permissionStatus.onchange = function () {
                console.log('geolocation permission state has changed');
            };
        });
    }
    function askGeolocation() {
        navigator.permissions.query({ name: 'geolocation' }).then(function (result) {
            if (result.state === 'granted') {
            }
            else if (result.state === 'prompt') {
            }
        });
    }
    function test() {
        return "123";
    }
    Micro.test = test;
})(Micro || (Micro = {}));
