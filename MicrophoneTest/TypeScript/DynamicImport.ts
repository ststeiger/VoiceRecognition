
namespace ScriptLoader
{


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

    function loadScript(src: string, done: (err?: Error) => void)
    {
        let js = document.createElement('script');
        js.src = src;
        js.onload = function ()
        {
            done();
        };
        js.onerror = function ()
        {
            done(new Error('Failed to load script ' + src));
        };

        document.head.appendChild(js);
    }
    
    function supportsSetPrototype()
    {
        // @ts-ignore
        return !(typeof Object.setPrototypeOf === 'undefined' && typeof Object.getOwnPropertyNames === 'function');
    }




    async function autorun()
    {
        console.log("Main load !");
        // @ts-ignore
        let obj = await import('../js/test.js');
        console.log("Main load success !");
        console.log(obj);
        // obj.foo();
    }
    
    async function main()
    {
        console.log("Main !");
        await autorun();
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


    export async function domReady()
    {
        ensureConsole();

        let curScript = document.currentScript || (function ()
        {
            let scripts = document.getElementsByTagName('script');
            // Note: this is for IE as IE doesn't support currentScript
            // this does not work if you have deferred loading with async
            // e.g. <script src="..." async="async" ></script>
            // https://web.archive.org/web/20180618155601/https://www.w3schools.com/TAgs/att_script_async.asp
            return scripts[scripts.length - 1];
        })();

        let prom = isPromiseSupported();
        let fetch = isFetchAPISupported();
        let dyn = supportsDynamicImport();
        let setProto = supportsSetPrototype();
        // let sta = supportsStaticImport();
        // let asy = isAsyncSupported();


        function onDynamicImportsAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load dynamicImports polyfill.\r\n" + err.message);

            // curScript.getAttribute("data-main").split(',');
            
            main();
        } // End Function onDynamicImportsAvailable 

        function onFechAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load fetch polyfill.\r\n" + err.message);

            if (!dyn)
            {
                console.log("load dynamic imports");
                loadScript("js/polyfills/dynamic-import-polyfill.umd.js", onDynamicImportsAvailable);
            } else
            {
                console.log("dynamic imports available");
                onDynamicImportsAvailable();
            }

        } // End Function onFechAvailable 

        function onPromiseAvailable(err?: Error)
        {
            if (err != null)
                throw Error("Couldn't load promise polyfill.\r\n" + err.message);

            if (!fetch)
            {
                console.log("load fetch");
                loadScript("js/polyfills/fetch.js", onFechAvailable);
            } else
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
            } else
            {
                console.log("promise available");
                onPromiseAvailable();
            }

        } // End Function onSetPrototypeOfAvailable

        if (!setProto)
        {
            console.log("load onSetPrototypeOf");
            loadScript("js/polyfills/object-setprototypeof-ie9.js", onSetPrototypeOfAvailable);
        } else
        {
            console.log("onSetPrototypeOf available");
            onSetPrototypeOfAvailable();
        }

    } // End Function domReady 
    
    
} // End Namespace 


interface Window
{
    attachEvent:any;
}

if (window.addEventListener) window.addEventListener("load", ScriptLoader.domReady, false);
else if (window.attachEvent) window.attachEvent("onload", ScriptLoader.domReady);
else window.onload = ScriptLoader.domReady;
