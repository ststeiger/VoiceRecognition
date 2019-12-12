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
var ScriptLoader;
(function (ScriptLoader) {
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
    function loadScript(src, done) {
        var js = document.createElement('script');
        js.src = src;
        js.onload = function () {
            done();
        };
        js.onerror = function () {
            done(new Error('Failed to load script ' + src));
        };
        document.head.appendChild(js);
    }
    function supportsSetPrototype() {
        return !(typeof Object.setPrototypeOf === 'undefined' && typeof Object.getOwnPropertyNames === 'function');
    }
    function autorun() {
        return __awaiter(this, void 0, void 0, function () {
            var obj;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        console.log("Main load !");
                        return [4, Promise.resolve().then(function () { return require('../js/test.js'); })];
                    case 1:
                        obj = _a.sent();
                        console.log("Main load success !");
                        console.log(obj);
                        return [2];
                }
            });
        });
    }
    function main() {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        console.log("Main !");
                        return [4, autorun()];
                    case 1:
                        _a.sent();
                        return [2];
                }
            });
        });
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
    function domReady() {
        return __awaiter(this, void 0, void 0, function () {
            function onDynamicImportsAvailable(err) {
                if (err != null)
                    throw Error("Couldn't load dynamicImports polyfill.\r\n" + err.message);
                main();
            }
            function onFechAvailable(err) {
                if (err != null)
                    throw Error("Couldn't load fetch polyfill.\r\n" + err.message);
                if (!dyn) {
                    console.log("load dynamic imports");
                    loadScript("js/polyfills/dynamic-import-polyfill.umd.js", onDynamicImportsAvailable);
                }
                else {
                    console.log("dynamic imports available");
                    onDynamicImportsAvailable();
                }
            }
            function onPromiseAvailable(err) {
                if (err != null)
                    throw Error("Couldn't load promise polyfill.\r\n" + err.message);
                if (!fetch) {
                    console.log("load fetch");
                    loadScript("js/polyfills/fetch.js", onFechAvailable);
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
            var curScript, prom, fetch, dyn, setProto;
            return __generator(this, function (_a) {
                ensureConsole();
                curScript = document.currentScript || (function () {
                    var scripts = document.getElementsByTagName('script');
                    return scripts[scripts.length - 1];
                })();
                prom = isPromiseSupported();
                fetch = isFetchAPISupported();
                dyn = supportsDynamicImport();
                setProto = supportsSetPrototype();
                if (!setProto) {
                    console.log("load onSetPrototypeOf");
                    loadScript("js/polyfills/object-setprototypeof-ie9.js", onSetPrototypeOfAvailable);
                }
                else {
                    console.log("onSetPrototypeOf available");
                    onSetPrototypeOfAvailable();
                }
                return [2];
            });
        });
    }
    ScriptLoader.domReady = domReady;
})(ScriptLoader || (ScriptLoader = {}));
if (window.addEventListener)
    window.addEventListener("load", ScriptLoader.domReady, false);
else if (window.attachEvent)
    window.attachEvent("onload", ScriptLoader.domReady);
else
    window.onload = ScriptLoader.domReady;
