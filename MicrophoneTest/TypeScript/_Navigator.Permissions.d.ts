
// @ts-ignore
declare type PermissionName = "geolocation" | "notifications" | "push" | "midi" | "camera" | "microphone" | "speaker" | "device-info" | "background-sync" | "bluetooth" | "persistent-storage" | "ambient-light-sensor" | "accelerometer" | "gyroscope" | "magnetometer" | "clipboard";
// @ts-ignore
declare type PermissionState = "granted" | "denied" | "prompt";

interface PermissionDescriptor
{
    name: PermissionName;
}

interface PermissionStatusEventMap
{
    "change": Event;
}

interface PermissionStatus extends EventTarget
{
    onchange: ((this: PermissionStatus, ev: Event) => any) | null;
    readonly state: PermissionState;
    addEvreadonlyentListener<K extends keyof PermissionStatusEventMap>(type: K, listener: (this: PermissionStatus, ev: PermissionStatusEventMap[K]) => any, options?: boolean | AddEventListenerOptions): void;
    addEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | AddEventListenerOptions): void;
    removeEventListener<K extends keyof PermissionStatusEventMap>(type: K, listener: (this: PermissionStatus, ev: PermissionStatusEventMap[K]) => any, options?: boolean | EventListenerOptions): void;
    removeEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | EventListenerOptions): void;
}


interface DevicePermissionDescriptor extends PermissionDescriptor
{
    deviceId?: string;
    name: "camera" | "microphone" | "speaker";
}

interface MidiPermissionDescriptor extends PermissionDescriptor
{
    name: "midi";
    sysex?: boolean;
}


interface PushPermissionDescriptor extends PermissionDescriptor
{
    name: "push";
    userVisibleOnly?: boolean;
}



interface Permissions
{
    query(permissionDesc: PermissionDescriptor | DevicePermissionDescriptor | MidiPermissionDescriptor | PushPermissionDescriptor): Promise<PermissionStatus>;
}


// Property 'permissions' does not exist on type 'Navigator'
// for googling: Navigator.permission types in typescript@3.5.3 works fine
// ripped from https://github.com/microsoft/TypeScript/blob/master/lib/lib.webworker.d.ts
interface Navigator
{
    readonly permissions: Permissions;
    webkitGetUserMedia: any;
    msGetUserMedia: any;
    mozGetUserMedia: any;
}
