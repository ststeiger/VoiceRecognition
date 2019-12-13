
interface Window
{
    attachEvent(event: string, listener: EventListener): boolean;
    detachEvent(event: string, listener: EventListener): void;
}

interface Document
{
    attachEvent(event: string, listener: EventListener): boolean;
    detachEvent(event: string, listener: EventListener): void;
}

type IE8ScriptReadyState = "loading" | "loaded";

interface IE8CompatibleScriptElement extends HTMLScriptElement
{
    onreadystatechange: () => void;
    readyState: IE8ScriptReadyState;
}

interface String 
{
    trimStart: () => string;
    trimEnd: () => string;
}


interface Array<T>
{
    includes: (o: T, fromIndex?: number) => boolean;
}



namespace ScriptLoader
{

    let curScript = document.currentScript || (function ()
    {
        let scripts = document.getElementsByTagName('script');
        // Note: this is for IE as IE doesn't support currentScript
        // this does not work if you have deferred loading with async
        // e.g. <script src="..." async="async" ></script>
        // https://web.archive.org/web/20180618155601/https://www.w3schools.com/TAgs/att_script_async.asp
        return scripts[scripts.length - 1];
    })();

    function supportsDynamicImport()
    {
        try
        {
            new Function('import("")');
        } catch (err)
        {
            return false;
        }

        return true;
    }


    function supportsStaticImport()
    {
        const script = document.createElement('script');
        return 'noModule' in script;
    }


    function isFetchAPISupported()
    {
        return 'fetch' in window;
    }

    function isPromiseSupported()
    {
        return 'Promise' in window;
    }

    function isAsyncSupported()
    {
        try
        {
            eval(`typeof Object.getPrototypeOf(async function() {}).constructor === 'function'`);
        } catch (exception)
        {
            return false;
        }

        return true;
    }
    
    
    // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/rest_parameters
    function isRestSupported() 
    {
        try {
            eval('function foo(bar, ...rest) { return 1; };');
        } catch (error) {
            return false;
        }
        return true;
    }


    function sum(...va_arg) 
    {
        let result =0;
        
        for(let i = 0; i < va_arg.length; ++i)
        {
            result += va_arg[i];
        }
        
        return result;
        // return theArgs.reduce((previous, current) => { return previous + current; });
    }
    
    
    export function wait(timeout:number) :Promise<number>
    {
        return new Promise<number>( function(resolve, reject)
        {
            try
            {
                let wait = setTimeout(function ()
                {
                    clearTimeout(wait);
                    resolve(timeout);
                }, timeout);
            }
            catch (e)
            {
                reject(e);
            }
            
        });
    }

    export function promiseTimeout<T>(ms: number, promise: Promise<T>): Promise<T>
    {
        // Create a promise that rejects in <ms> milliseconds
        let timeout = new Promise<T>( function(resolve, reject)
        {
            try
            {
                let id = setTimeout( function()
                {
                    clearTimeout(id);
                    reject('Timed out in ' + ms + 'ms.')
                }, ms);
            }
            catch (e)
            {
                reject(e);
            }
        }); 

        // Returns a race between our timeout and the passed in promise
        return Promise.race([ promise, timeout ]);
    }
    
    export function loadPolyfill(src, useCache?: boolean): Promise<void>
    {
        if (!useCache)
        {
            if (src.indexOf("?") === -1)
                src += "?no_cache=" + new Date().getTime();
            else
                src += "&no_cache=" + new Date().getTime();
        }


        return new Promise(function (resolve, reject)
        {
            let js = <IE8CompatibleScriptElement>document.createElement('script');
            js.src = src;

            if (!('onload' in js))
            {
                // @ts-ignore
                js.onreadystatechange = function ()
                {
                    if (js.readyState === 'loaded')
                    {
                        js.onreadystatechange = null;
                        resolve();
                    }
                };
            }
            

            js.onload = function ()
            {
                js.onload = null;
                resolve();
            };

            js.onerror = function ()
            {
                js.onerror = null;
                reject(new Error('Failed to load script ' + src));
            };

            js.oncancel = function ()
            {
                js.oncancel = null;
                reject(new Error('Cancelled loading of script ' + src));
            };

            if (document.head)
                document.head.appendChild(js);
            else
                document.getElementsByTagName('head')[0].appendChild(js);            
        });
    }

    // loadScript("foo", function () { alert("hi"); });
    // loadScript("/ts/myimport.js", function () { alert("hi"); });
    function loadScript(src: string, done: (err?: Error) => void, useCache?: boolean)
    {
        console.log(src);

        if (!useCache)
        {
            if (src.indexOf("?") === -1)
                src += "?no_cache=" + new Date().getTime();
            else
                src += "&no_cache=" + new Date().getTime();
        }


        let js = <IE8CompatibleScriptElement>document.createElement('script');
        js.src = src;

        
        if (!('onload' in js))
        {
            // @ts-ignore
            js.onreadystatechange = function ()
            {
                if (js.readyState === 'loaded')
                {
                    js.onreadystatechange = null;
                    if (done != null)
                        done();
                }
            };
        }
        

        js.onload = function ()
        {
            js.onload = null;
            console.log("onload " + src);
            if(done != null)
                done();
        };

        js.onerror = function ()
        {
            js.onerror = null;
            console.log("onerror " + src);

            if (done != null)
                done(new Error('Failed to load script ' + src));
        };


        //js.onloadend = function ()
        //{
        //    alert("onerror");
        //    done(new Error('Failed to load script ' + src));
        //};


        js.oncancel = function ()
        {
            js.oncancel = null;
            console.log("oncancel " + src);
            if (done != null)
                done(new Error('Cancelled loading of script ' + src));
        };

        if (document.head)
            document.head.appendChild(js);
        else
            document.getElementsByTagName('head')[0].appendChild(js);
    }
    
    function supportsSetPrototype()
    {
        // @ts-ignore
        return !(typeof Object.setPrototypeOf === 'undefined' && typeof Object.getOwnPropertyNames === 'function');
    }



    // Avoid `console` errors in browsers that lack a console.
    function ensureConsole()
    {
        let method;
        let noop = function ()
        {
        };
        let methods = [
            'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
            'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
            'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
            'timeStamp', 'trace', 'warn'
        ];

        let length = methods.length;
        // @ts-ignore
        let console = (window.console = window.console || {});

        while (length--)
        {
            method = methods[length];

            // Only stub undefined methods.
            if (!console[method])
            {
                console[method] = noop;
            }
        }
    }

    // Needed because IE5 doesn't have JSON 
    function encodeJSON(arr: string[])
    {
        if (arr == null || arr.length === 0)
            return "[]";

        let stringBuilder = ["["];

        for (let i = 0; i < arr.length; ++i)
        {
            if (i !== 0)
                stringBuilder.push(',');

            if (arr[i] == null)
                stringBuilder.push("null");
            else
            {
                stringBuilder.push('"');
                stringBuilder.push(arr[i]); // we don't use special characters
                stringBuilder.push('"');
            }
        }

        stringBuilder.push("]");
        return stringBuilder.join("");
    }


    let hasBeenLoaded = false;


    export function domReady()
    {
        if (hasBeenLoaded) return; hasBeenLoaded = true;
        
        console.log("dom ready");
        
        ensureConsole();
        
        let prom = isPromiseSupported();
        let fetch = isFetchAPISupported();
        let dyn = supportsDynamicImport();
        let setProto = supportsSetPrototype();
        let hasJSON = (typeof JSON === 'object' && typeof JSON.parse === 'function');
        // let sta = supportsStaticImport();
        // let asy = isAsyncSupported();
        
        let needed = [];

        if (!Array.isArray)
            needed.push("array-isArray");
        
        if (!Array.prototype.includes)
            needed.push("array-includes");
        
        if (!Array.prototype.indexOf)
            needed.push("array-indexOf");
        
        if (!Array.prototype.forEach)
            needed.push("array-forEach");
        
        if (!Object.getOwnPropertyNames)
            needed.push("object-getOwnPropertyNames");

        if (!hasJSON)
            needed.push("json3.min");

        if (!String.prototype.trim)
            needed.push("string-trim");
        if (!String.prototype.trimStart)
            needed.push("string-trimStart");
        if (!String.prototype.trimEnd)
            needed.push("string-trimEnd");
        
        if (!setProto)
            needed.push("object-setPrototypeOf-ie9");
        
        if (!prom)
            needed.push("es6-promise-2.0.0.min");
        
        if (!fetch)
            needed.push("es6-fetch-ie8");
        
        function onPolyfilled(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load polyfill.\r\n" + err.message);

            let script = curScript.getAttribute("data-main").split(',')[0];
            loadScript(script, null);
        } // End Function onPolyfilled
        
        if(needed.length > 0)
        {
            let files = encodeURIComponent(encodeJSON(needed));
            loadScript("js/polyfills.ashx?polyfills=" + files + "&ext=.js", onPolyfilled);
        }
        else
            onPolyfilled();
    } // End Function domReady
    
    
} // End Namespace 

if (document.addEventListener) document.addEventListener("DOMContentLoaded", ScriptLoader.domReady, false);
// else if (document.attachEvent) document.attachEvent("onreadystatechange", ScriptLoader.domReady);
else if (document.attachEvent) document.attachEvent("onreadystatechange", function () { if (document.readyState === "complete") ScriptLoader.domReady(); });
else window.onload = ScriptLoader.domReady;

// if (window.addEventListener) window.addEventListener("load", ScriptLoader.domReady, false);
// else if (window.attachEvent) window.attachEvent("onload", ScriptLoader.domReady);
// else window.onload = ScriptLoader.domReady;
