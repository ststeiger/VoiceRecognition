var ScriptLoader;
(function (ScriptLoader) {
    var curScript = document.currentScript || (function () {
        var scripts = document.getElementsByTagName('script');
        return scripts[scripts.length - 1];
    })();
    function supportsDynamicImport() {
        try {
            new Function('import("")');
        }
        catch (err) {
            return false;
        }
        return true;
    }
    function supportsStaticImport() {
        var script = document.createElement('script');
        return 'noModule' in script;
    }
    function isFetchAPISupported() {
        return 'fetch' in window;
    }
    function isPromiseSupported() {
        return 'Promise' in window;
    }
    function isAsyncSupported() {
        try {
            eval("typeof Object.getPrototypeOf(async function() {}).constructor === 'function'");
        }
        catch (exception) {
            return false;
        }
        return true;
    }
    function isRestSupported() {
        try {
            eval('function foo(bar, ...rest) { return 1; };');
        }
        catch (error) {
            return false;
        }
        return true;
    }
    function sum() {
        var va_arg = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            va_arg[_i] = arguments[_i];
        }
        var result = 0;
        for (var i = 0; i < va_arg.length; ++i) {
            result += va_arg[i];
        }
        return result;
    }
    function wait(timeout) {
        return new Promise(function (resolve, reject) {
            try {
                var wait_1 = setTimeout(function () {
                    clearTimeout(wait_1);
                    resolve(timeout);
                }, timeout);
            }
            catch (e) {
                reject(e);
            }
        });
    }
    ScriptLoader.wait = wait;
    function promiseTimeout(ms, promise) {
        var timeout = new Promise(function (resolve, reject) {
            try {
                var id_1 = setTimeout(function () {
                    clearTimeout(id_1);
                    reject('Timed out in ' + ms + 'ms.');
                }, ms);
            }
            catch (e) {
                reject(e);
            }
        });
        return Promise.race([promise, timeout]);
    }
    ScriptLoader.promiseTimeout = promiseTimeout;
    function loadPolyfill(src, useCache) {
        if (!useCache) {
            if (src.indexOf("?") === -1)
                src += "?no_cache=" + new Date().getTime();
            else
                src += "&no_cache=" + new Date().getTime();
        }
        return new Promise(function (resolve, reject) {
            var js = document.createElement('script');
            js.src = src;
            if (!('onload' in js)) {
                js.onreadystatechange = function () {
                    if (js.readyState === 'loaded') {
                        js.onreadystatechange = null;
                        resolve();
                    }
                };
            }
            js.onload = function () {
                js.onload = null;
                resolve();
            };
            js.onerror = function () {
                js.onerror = null;
                reject(new Error('Failed to load script ' + src));
            };
            js.oncancel = function () {
                js.oncancel = null;
                reject(new Error('Cancelled loading of script ' + src));
            };
            if (document.head)
                document.head.appendChild(js);
            else
                document.getElementsByTagName('head')[0].appendChild(js);
        });
    }
    ScriptLoader.loadPolyfill = loadPolyfill;
    function loadScript(src, done, useCache) {
        console.log(src);
        if (!useCache) {
            if (src.indexOf("?") === -1)
                src += "?no_cache=" + new Date().getTime();
            else
                src += "&no_cache=" + new Date().getTime();
        }
        var js = document.createElement('script');
        js.src = src;
        if (!('onload' in js)) {
            js.onreadystatechange = function () {
                if (js.readyState === 'loaded') {
                    js.onreadystatechange = null;
                    if (done != null)
                        done();
                }
            };
        }
        js.onload = function () {
            js.onload = null;
            console.log("onload " + src);
            if (done != null)
                done();
        };
        js.onerror = function () {
            js.onerror = null;
            console.log("onerror " + src);
            if (done != null)
                done(new Error('Failed to load script ' + src));
        };
        js.oncancel = function () {
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
    function supportsSetPrototype() {
        return !(typeof Object.setPrototypeOf === 'undefined' && typeof Object.getOwnPropertyNames === 'function');
    }
    function ensureConsole() {
        var method;
        var noop = function () {
        };
        var methods = [
            'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
            'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
            'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
            'timeStamp', 'trace', 'warn'
        ];
        var length = methods.length;
        var console = (window.console = window.console || {});
        while (length--) {
            method = methods[length];
            if (!console[method]) {
                console[method] = noop;
            }
        }
    }
    var hasBeenLoaded = false;
    function domReady() {
        if (hasBeenLoaded)
            return;
        hasBeenLoaded = true;
        console.log("dom ready");
        ensureConsole();
        var prom = isPromiseSupported();
        var fetch = isFetchAPISupported();
        var dyn = supportsDynamicImport();
        var setProto = supportsSetPrototype();
        var needed = [];
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
        if (!String.prototype.trim)
            needed.push("string-trim");
        if (!String.prototype.trimStart)
            needed.push("string-trimStart");
        if (!String.prototype.trimEnd)
            needed.push("string-trimEnd");
        if (!setProto)
            needed.push("object-setprototypeof-ie9");
        if (!prom)
            needed.push("es6-promise-2.0.0.min");
        if (!fetch)
            needed.push("es6-fetch-ie8");
        function onPolyfilled(err) {
            if (err != null)
                throw Error("Couldn't load polyfill.\r\n" + err.message);
            var script = curScript.getAttribute("data-main").split(',')[0];
            loadScript(script, null);
        }
        if (needed.length > 0) {
            var files = encodeURIComponent(JSON.stringify(needed));
            loadScript("js/polyfills.ashx?polyfills=" + files + "&ext=.js", onPolyfilled);
        }
        else
            onPolyfilled();
    }
    ScriptLoader.domReady = domReady;
})(ScriptLoader || (ScriptLoader = {}));
if (document.addEventListener)
    document.addEventListener("DOMContentLoaded", ScriptLoader.domReady, false);
else if (document.attachEvent)
    document.attachEvent("onreadystatechange", function () { if (document.readyState === "complete")
        ScriptLoader.domReady(); });
else
    window.onload = ScriptLoader.domReady;
