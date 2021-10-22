var FunctionLogger = (function () {
    function FunctionLogger() {
        this.log = true;
        this.getLoggableFunction = this.getLoggableFunction.bind(this);
        this.addLoggingToNamespace = this.addLoggingToNamespace.bind(this);
    }
    FunctionLogger.prototype.getLoggableFunction = function (func, name) {
        if (this.log) {
            var logText = name + '(';
            for (var i = 0; i < arguments.length; i++) {
                if (i > 0) {
                    logText += ', ';
                }
                logText += arguments[i];
            }
            logText += ');';
            console.log(logText);
        }
        return func.apply(this, arguments);
    };
    FunctionLogger.prototype.addLoggingToNamespace = function (namespaceObject) {
        for (var name_1 in namespaceObject) {
            var potentialFunction = namespaceObject[name_1];
            if (Object.prototype.toString.call(potentialFunction) === '[object Function]') {
                namespaceObject[name_1] = this.getLoggableFunction(potentialFunction, name_1);
            }
        }
    };
    return FunctionLogger;
}());
var flog = new FunctionLogger();
var TestClass = (function () {
    function TestClass() {
        this._bar = false;
        this.autoBind(this);
        if (true && true)
            this.autoTrace(this);
    }
    TestClass.prototype.autoBind = function (self) {
        for (var _i = 0, _a = Object.getOwnPropertyNames(self.constructor.prototype); _i < _a.length; _i++) {
            var key = _a[_i];
            if (key !== 'constructor') {
                var desc = Object.getOwnPropertyDescriptor(self.constructor.prototype, key);
                if (desc != null) {
                    if (!desc.configurable) {
                        console.log("AUTOBIND-WARNING: Property \"" + key + "\" not configurable ! (" + self.constructor.name + ")");
                        continue;
                    }
                    var g = desc.get != null;
                    var s = desc.set != null;
                    if (g || s) {
                        var newDescriptor = {};
                        newDescriptor.enumerable = desc.enumerable;
                        newDescriptor.configurable = desc.configurable;
                        if (g)
                            newDescriptor.get = desc.get.bind(self);
                        if (s)
                            newDescriptor.set = desc.set.bind(self);
                        Object.defineProperty(self, key, newDescriptor);
                        continue;
                    }
                }
                if (typeof (self[key]) === 'function') {
                    var val = self[key];
                    self[key] = val.bind(self);
                }
            }
        }
        return self;
    };
    TestClass.prototype.autoTrace = function (self) {
        function getLoggableFunction_old(func, type, name) {
            return function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i] = arguments[_i];
                }
                var logText = name + '(';
                for (var i = 0; i < args.length; i++) {
                    if (i > 0) {
                        logText += ', ';
                    }
                    logText += args[i];
                }
                logText += ');';
                console.log(type + " " + logText);
                return func.apply(self, args);
            };
        }
        function getLoggableFunction(func, type, name) {
            return function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i] = arguments[_i];
                }
                var logText = name + '(';
                for (var i = 0; i < args.length; i++) {
                    if (i > 0) {
                        logText += ', ';
                    }
                    logText += args[i];
                }
                logText += ')';
                console.log("Pre " + type + " " + logText + "; ");
                var res = func.apply(self, args);
                console.log("Post " + type + " " + logText + ":", res);
                return res;
            };
        }
        for (var _i = 0, _a = Object.getOwnPropertyNames(self.constructor.prototype); _i < _a.length; _i++) {
            var key = _a[_i];
            if (key !== 'constructor') {
                var desc = Object.getOwnPropertyDescriptor(self.constructor.prototype, key);
                if (desc != null) {
                    if (!desc.configurable) {
                        console.log("AUTOTRACE-WARNING: Property \"" + key + "\" not configurable ! (" + self.constructor.name + ")");
                        continue;
                    }
                    var g = desc.get != null;
                    var s = desc.set != null;
                    if (g || s) {
                        var newDescriptor = {};
                        newDescriptor.enumerable = desc.enumerable;
                        newDescriptor.configurable = desc.configurable;
                        if (g)
                            newDescriptor.get = getLoggableFunction(desc.get.bind(self), "Property", "get_" + key);
                        if (s)
                            newDescriptor.set = getLoggableFunction(desc.set.bind(self), "Property", "set_" + key);
                        Object.defineProperty(self, key, newDescriptor);
                        continue;
                    }
                }
                if (typeof (self[key]) === 'function') {
                    var val = self[key];
                    self[key] = getLoggableFunction(val.bind(self), "Function", key);
                }
            }
        }
        return self;
    };
    Object.defineProperty(TestClass.prototype, "bar", {
        get: function () {
            return this._bar;
        },
        set: function (value) {
            this._bar = value;
        },
        enumerable: true,
        configurable: true
    });
    TestClass.prototype.hello = function () {
        console.log("hello", "this", this);
    };
    TestClass.prototype.world = function (x, y) {
        console.log("world", "this", this);
    };
    return TestClass;
}());
var testInstance = new TestClass();
testInstance.hello();
testInstance.world(1, 2);
testInstance.world(1, 3);
var a = testInstance.bar;
console.log("test.bar equals", a);
testInstance.bar = true;
var b = testInstance.bar;
console.log("b equals", b);
function printStackTrace() {
    var callstack = [];
    var isCallstackPopulated = false;
    try {
        throw new Error("i.dont.exist");
    }
    catch (e) {
        if (e.stack) {
            var lines = e.stack.split('\n');
            for (var i = 0, len = lines.length; i < len; i++) {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                    callstack.push(lines[i]);
                }
            }
            callstack.shift();
            isCallstackPopulated = true;
        }
        else if (window.opera && e.message) {
            var lines = e.message.split('\n');
            for (var i = 0, len = lines.length; i < len; i++) {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                    var entry = lines[i];
                    if (lines[i + 1]) {
                        entry += ' at ' + lines[i + 1];
                        i++;
                    }
                    callstack.push(entry);
                }
            }
            callstack.shift();
            isCallstackPopulated = true;
        }
    }
    if (!isCallstackPopulated) {
        var currentFunction = arguments.callee.caller;
        while (currentFunction) {
            var fn = currentFunction.toString();
            var fname = fn.substring(fn.indexOf("function") + 8, fn.indexOf('')) || "anonymous";
            callstack.push(fname);
            currentFunction = currentFunction.caller;
        }
    }
    output(callstack);
}
function output(arr) {
    console.log(arr.join('\n\n'));
}
function getStackTrace() {
    var err = new Error();
    var stack = err.stack || err.stacktrace || (console.trace ? console.trace() : "no stack info");
    return stack;
}
function getStackTraceNodeJS() {
    var obj = {};
    Error.captureStackTrace(obj, getStackTrace);
    return obj["stack"];
}
console.log(getStackTrace());
function backtrace(opts) {
    var stack = [];
    var maxStackSize = 10;
    if (typeof opts === 'object' && typeof opts.maxStackSize === 'number') {
        maxStackSize = opts.maxStackSize;
    }
    var curr = arguments.callee;
    while (curr && stack.length < maxStackSize && curr['arguments']) {
        var args = new Array(curr['arguments'].length);
        for (var i = 0; i < args.length; ++i) {
            args[i] = curr['arguments'][i];
        }
        if (/function(?:\s+([\w$]+))+\s*\(/.test(curr.toString())) {
            stack.push({ functionName: RegExp.$1 || undefined, args: args });
        }
        else {
            stack.push({ args: args });
        }
        try {
            curr = curr.caller;
        }
        catch (e) {
            break;
        }
    }
    return stack;
}
