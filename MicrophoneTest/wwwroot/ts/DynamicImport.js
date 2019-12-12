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
                    ;
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
                ;
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
        if (!Array.isArray) {
            Array.isArray = function (vArg) {
                return Object.prototype.toString.call(vArg) === "[object Array]";
            };
        }
        if (!Array.prototype.includes) {
            Array.prototype.includes = function (obj, fromIndex) {
                if (null == this)
                    throw new TypeError('"this" is null or not defined');
                var t = Object(this), n = t.length >>> 0;
                if (0 === n)
                    return false;
                var i, o, a = 0 | fromIndex, u = Math.max(0 <= a ? a : n - Math.abs(a), 0);
                for (; u < n;) {
                    if ((i = t[u]) === (o = obj) || "number" == typeof i && "number" == typeof o && isNaN(i) && isNaN(o))
                        return true;
                    u++;
                }
                return false;
            };
        }
        if (!Array.prototype.indexOf)
            Array.prototype.indexOf = (function (Object, max, min) {
                "use strict";
                return function indexOf(member, fromIndex) {
                    if (this === null || this === undefined)
                        throw TypeError("Array.prototype.indexOf called on null or undefined");
                    var that = Object(this), Len = that.length >>> 0, i = min(fromIndex | 0, Len);
                    if (i < 0)
                        i = max(0, Len + i);
                    else if (i >= Len)
                        return -1;
                    if (member === void 0) {
                        for (; i !== Len; ++i)
                            if (that[i] === void 0 && i in that)
                                return i;
                    }
                    else if (member !== member) {
                        return -1;
                    }
                    else
                        for (; i !== Len; ++i)
                            if (that[i] === member)
                                return i;
                    return -1;
                };
            })(Object, Math.max, Math.min);
        if (!Array.prototype.forEach) {
            Array.prototype.forEach = function (callback, thisArg) {
                thisArg = thisArg || window;
                for (var i = 0; i < this.length; i++) {
                    callback.call(thisArg, this[i], i, this);
                }
            };
        }
        if (!Object.getOwnPropertyNames) {
            Object.getOwnPropertyNames = function (obj) {
                var arr = [];
                for (var k in obj) {
                    if (obj.hasOwnProperty(k))
                        arr.push(k);
                }
                return arr;
            };
        }
        if (!String.prototype.trim) {
            String.prototype.trim = function () {
                return this.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, '');
            };
        }
        if (!String.prototype.trimStart) {
            String.prototype.trimStart = function () {
                return this.replace(/^[\s\uFEFF\xA0]+/g, '');
            };
        }
        if (!String.prototype.trimEnd) {
            String.prototype.trimEnd = function () {
                return this.replace(/[\s\uFEFF\xA0]+$/g, '');
            };
        }
        ensureConsole();
        var prom = isPromiseSupported();
        var fetch = isFetchAPISupported();
        var dyn = supportsDynamicImport();
        var setProto = supportsSetPrototype();
        function onFechAvailable(err) {
            if (err != null)
                throw Error("Couldn't load fetch polyfill.\r\n" + err.message);
            var script = curScript.getAttribute("data-main").split(',')[0];
            loadScript(script, null);
        }
        function onPromiseAvailable(err) {
            if (err != null)
                throw Error("Couldn't load promise polyfill.\r\n" + err.message);
            if (!fetch) {
                console.log("load fetch");
                loadScript("js/polyfills/fetch_ie8.js", onFechAvailable);
            }
            else {
                console.log("fetch available");
                onFechAvailable();
            }
        }
        function onSetPrototypeOfAvailable(err) {
            if (err != null)
                throw Error("Couldn't load onSetPrototypeOf polyfill.\r\n" + err.message);
            if (!prom) {
                console.log("load promise");
                loadScript("js/polyfills/es6-promise-2.0.0.min.js", onPromiseAvailable);
            }
            else {
                console.log("promise available");
                onPromiseAvailable();
            }
        }
        if (!setProto) {
            console.log("load onSetPrototypeOf");
            loadScript("js/polyfills/object-setprototypeof-ie9.js", onSetPrototypeOfAvailable);
        }
        else {
            console.log("onSetPrototypeOf available");
            onSetPrototypeOfAvailable();
        }
        var foo = [];
        if (!Array.isArray)
            foo.push("Array-isArray");
        if (!Array.prototype.includes)
            foo.push("Array-includes");
        if (!Array.prototype.indexOf)
            foo.push("Array-indexOf");
        if (!Array.prototype.forEach)
            foo.push("Array-forEach");
        if (!Object.getOwnPropertyNames)
            foo.push("Object-getOwnPropertyNames");
        if (!String.prototype.trim)
            foo.push("String-trim");
        if (!String.prototype.trimStart)
            foo.push("String-trimStart");
        if (!String.prototype.trimEnd)
            foo.push("String-trimEnd");
        if (!setProto)
            foo.push("object-setprototypeof-ie9");
        if (!prom)
            foo.push("es6-promise-2.0.0.min");
        if (!fetch)
            foo.push("fetch_ie8");
    }
    ScriptLoader.domReady = domReady;
    function takeMeOut() {
        if (!Array.isArray) {
            Array.isArray = function (vArg) {
                return Object.prototype.toString.call(vArg) === "[object Array]";
            };
        }
        if (!Array.prototype.includes) {
            Array.prototype.includes = function (obj, fromIndex) {
                if (null == this)
                    throw new TypeError('"this" is null or not defined');
                var t = Object(this), n = t.length >>> 0;
                if (0 === n)
                    return false;
                var i, o, a = 0 | fromIndex, u = Math.max(0 <= a ? a : n - Math.abs(a), 0);
                for (; u < n;) {
                    if ((i = t[u]) === (o = obj) || "number" == typeof i && "number" == typeof o && isNaN(i) && isNaN(o))
                        return true;
                    u++;
                }
                return false;
            };
        }
        if (!Array.prototype.indexOf)
            Array.prototype.indexOf = (function (Object, max, min) {
                "use strict";
                return function indexOf(member, fromIndex) {
                    if (this === null || this === undefined)
                        throw TypeError("Array.prototype.indexOf called on null or undefined");
                    var that = Object(this), Len = that.length >>> 0, i = min(fromIndex | 0, Len);
                    if (i < 0)
                        i = max(0, Len + i);
                    else if (i >= Len)
                        return -1;
                    if (member === void 0) {
                        for (; i !== Len; ++i)
                            if (that[i] === void 0 && i in that)
                                return i;
                    }
                    else if (member !== member) {
                        return -1;
                    }
                    else
                        for (; i !== Len; ++i)
                            if (that[i] === member)
                                return i;
                    return -1;
                };
            })(Object, Math.max, Math.min);
        if (!Array.prototype.forEach) {
            Array.prototype.forEach = function (callback, thisArg) {
                thisArg = thisArg || window;
                for (var i = 0; i < this.length; i++) {
                    callback.call(thisArg, this[i], i, this);
                }
            };
        }
        if (!Object.getOwnPropertyNames) {
            Object.getOwnPropertyNames = function (obj) {
                var arr = [];
                for (var k in obj) {
                    if (obj.hasOwnProperty(k))
                        arr.push(k);
                }
                return arr;
            };
        }
        if (!String.prototype.trim) {
            String.prototype.trim = function () {
                return this.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, '');
            };
        }
        if (!String.prototype.trimStart) {
            String.prototype.trimStart = function () {
                return this.replace(/^[\s\uFEFF\xA0]+/g, '');
            };
        }
        if (!String.prototype.trimEnd) {
            String.prototype.trimEnd = function () {
                return this.replace(/[\s\uFEFF\xA0]+$/g, '');
            };
        }
    }
})(ScriptLoader || (ScriptLoader = {}));
if (document.addEventListener)
    document.addEventListener("DOMContentLoaded", ScriptLoader.domReady, false);
else if (document.attachEvent)
    document.attachEvent("onreadystatechange", function () { if (document.readyState === "complete")
        ScriptLoader.domReady(); });
else
    window.onload = ScriptLoader.domReady;
