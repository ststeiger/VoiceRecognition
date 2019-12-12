
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

    export function loadPolyfill(src): Promise<void>
    {
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
                    };
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
    function loadScript(src: string, done: (err?: Error) => void)
    {
        console.log(src);

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
                };
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


    let hasBeenLoaded = false;


    
    


    export function domReady()
    {
        if (hasBeenLoaded) return; hasBeenLoaded = true;

        console.log("dom ready");

        // for IE8 
        if (!Array.isArray)
        {
            Array.isArray = function (vArg: any | any[]): vArg is any[]
            {
                return Object.prototype.toString.call(vArg) === "[object Array]";
            };
        }

        // for IE8 - not required 
        if (!Array.prototype.includes)
        {
            // https://mariusschulz.com/blog/ecmascript-2016-array-prototype-includes
            Array.prototype.includes = function (obj: any, fromIndex?: number)
            {
                if (null == this)
                    throw new TypeError('"this" is null or not defined');

                // >>> is a right shift without sign extension
                let t = Object(this), n = t.length >>> 0;

                if (0 === n)
                    return false;

                let i, o, a = 0 | fromIndex,
                    u = Math.max(0 <= a ? a : n - Math.abs(a), 0);

                for (; u < n;)
                {
                    if ((i = t[u]) === (o = obj) || "number" == typeof i && "number" == typeof o && isNaN(i) && isNaN(o))
                        return true;

                    u++;
                }

                return false;
            };
        }


        // for IE8 
        if (!Array.prototype.indexOf)
            Array.prototype.indexOf = (function (Object, max, min)
            {
                "use strict"
                return function indexOf(member, fromIndex)
                {
                    if (this === null || this === undefined)
                        throw TypeError("Array.prototype.indexOf called on null or undefined")

                    var that = Object(this), Len = that.length >>> 0, i = min(fromIndex | 0, Len)
                    if (i < 0) i = max(0, Len + i)
                    else if (i >= Len) return -1

                    if (member === void 0)
                    {        // undefined
                        for (; i !== Len; ++i) if (that[i] === void 0 && i in that) return i
                    } else if (member !== member)
                    { // NaN
                        return -1 // Since NaN !== NaN, it will never be found. Fast-path it.
                    } else                          // all else
                        for (; i !== Len; ++i) if (that[i] === member) return i

                    return -1 // if the value was not found, then return -1
                }
            })(Object, Math.max, Math.min);

        // for IE8
        if (!Array.prototype.forEach)
        {
            Array.prototype.forEach = function (callback, thisArg)
            {
                thisArg = thisArg || window;
                for (var i = 0; i < this.length; i++)
                {
                    callback.call(thisArg, this[i], i, this);
                }
            };
        }

        // for IE8
        if (!Object.getOwnPropertyNames)
        {
            Object.getOwnPropertyNames = function (obj: any): string[]
            {
                let arr = [];
                for (let k in obj)
                {
                    if (obj.hasOwnProperty(k))
                        arr.push(k);
                }

                return arr;
            }
        }
        

        // for IE8
        if (!String.prototype.trim)
        {
            String.prototype.trim = function ()
            {
                // return this.replace(/^\s+|\s+$/g, '');
                return this.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, '');
            };
        }

        // just in case - not required 
        if (!String.prototype.trimStart)
        {
            String.prototype.trimStart = function ()
            {
                // return this.replace(/^\s+/g, '');
                return this.replace(/^[\s\uFEFF\xA0]+/g, '');
            };
        }

        // just in case - not required 
        if (!String.prototype.trimEnd)
        {
            String.prototype.trimEnd = function ()
            {
                // return this.replace(/\s+$/g, '');
                return this.replace(/[\s\uFEFF\xA0]+$/g, '');
            };
        }
        

        ensureConsole();

        let prom = isPromiseSupported();
        let fetch = isFetchAPISupported();
        let dyn = supportsDynamicImport();
        let setProto = supportsSetPrototype();
        // let sta = supportsStaticImport();
        // let asy = isAsyncSupported();


        function onFechAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load fetch polyfill.\r\n" + err.message);

            let script = curScript.getAttribute("data-main").split(',')[0];
            loadScript(script, null);
        } // End Function onFechAvailable 

        function onPromiseAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load promise polyfill.\r\n" + err.message);

            if (!fetch)
            {
                console.log("load fetch");
                // loadScript("js/polyfills/fetch.js", onFechAvailable);
                loadScript("js/polyfills/fetch_ie8.js", onFechAvailable);
            }
            else
            {
                console.log("fetch available");
                onFechAvailable();
            }

        } // End Function onPromiseAvailable
        
        function onSetPrototypeOfAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load onSetPrototypeOf polyfill.\r\n" + err.message);

            if (!prom)
            {
                console.log("load promise");
                loadScript("js/polyfills/es6-promise-2.0.0.min.js", onPromiseAvailable);
            }
            else
            {
                console.log("promise available");
                onPromiseAvailable();
            }

        } // End Function onSetPrototypeOfAvailable

        if (!setProto)
        {
            console.log("load onSetPrototypeOf");
            loadScript("js/polyfills/object-setprototypeof-ie9.js", onSetPrototypeOfAvailable);
        }
        else
        {
            console.log("onSetPrototypeOf available");
            onSetPrototypeOfAvailable();
        }

    } // End Function domReady 
    
    
} // End Namespace 

if (document.addEventListener) document.addEventListener("DOMContentLoaded", ScriptLoader.domReady, false);
// else if (document.attachEvent) document.attachEvent("onreadystatechange", ScriptLoader.domReady);
else if (document.attachEvent) document.attachEvent("onreadystatechange", function () { if (document.readyState === "complete") ScriptLoader.domReady(); });
else window.onload = ScriptLoader.domReady;

// if (window.addEventListener) window.addEventListener("load", ScriptLoader.domReady, false);
// else if (window.attachEvent) window.attachEvent("onload", ScriptLoader.domReady);
// else window.onload = ScriptLoader.domReady;
