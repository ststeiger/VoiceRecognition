
declare class webkitSpeechRecognition implements SpeechRecognition
{
    continuous: boolean; grammars: SpeechGrammarList;
    interimResults: boolean;
    lang: string;
    maxAlternatives: number;
    onaudioend: (this: SpeechRecognition, ev: Event) => any;
    onaudiostart: (this: SpeechRecognition, ev: Event) => any;
    onend: (this: SpeechRecognition, ev: Event) => any;
    onerror: (this: SpeechRecognition, ev: SpeechRecognitionError) => any;
    onnomatch: (this: SpeechRecognition, ev: SpeechRecognitionEvent) => any;
    onresult: (this: SpeechRecognition, ev: SpeechRecognitionEvent) => any;
    onsoundend: (this: SpeechRecognition, ev: Event) => any;
    onsoundstart: (this: SpeechRecognition, ev: Event) => any;
    onspeechend: (this: SpeechRecognition, ev: Event) => any;
    onspeechstart: (this: SpeechRecognition, ev: Event) => any;
    onstart: (this: SpeechRecognition, ev: Event) => any;
    serviceURI: string;

    abort(): void;
    start(): void;
    stop(): void;


    addEventListener<K extends "error" | "audioend" | "audiostart" | "end" | "nomatch" | "result" | "soundend" | "soundstart" | "speechend" | "speechstart" | "start">(type: K, listener: (this: SpeechRecognition, ev: SpeechRecognitionEventMap[K]) => any, options?: boolean | AddEventListenerOptions): void;
    addEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | AddEventListenerOptions): void;
    addEventListener(type: any, listener: any, options?: any);

    removeEventListener<K extends "error" | "audioend" | "audiostart" | "end" | "nomatch" | "result" | "soundend" | "soundstart" | "speechend" | "speechstart" | "start">(type: K, listener: (this: SpeechRecognition, ev: SpeechRecognitionEventMap[K]) => any, options?: boolean | EventListenerOptions): void;
    removeEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | EventListenerOptions): void;

    removeEventListener(type: any, listener: any, options?: any): void;
    dispatchEvent(event: Event): boolean;
}


interface Window
{
    SpeechRecognition: SpeechRecognition;
    webkitSpeechRecognition: SpeechRecognition;
}
