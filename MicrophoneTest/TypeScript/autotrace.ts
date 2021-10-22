
// https://stackoverflow.com/questions/11853256/how-to-get-javascript-function-calls-trace-at-runtime
class FunctionLogger
{
    private log: boolean;


    constructor()
    {
        this.log = true;//Set this to false to disable logging 
        this.getLoggableFunction = this.getLoggableFunction.bind(this);
        this.addLoggingToNamespace = this.addLoggingToNamespace.bind(this);
    }


    /**
    * Gets a function that when called will log information about itself if logging is turned on.
    *
    * @param func The function to add logging to.
    * @param name The name of the function.
    *
    * @return A function that will perform logging and then call the function. 
    */
    private getLoggableFunction(func, name)
    {

        if (this.log)
        {
            let logText = name + '(';

            for (let i = 0; i < arguments.length; i++)
            {
                if (i > 0)
                {
                    logText += ', ';
                }
                logText += arguments[i];
            }
            logText += ');';

            console.log(logText);
        }

        return func.apply(this, arguments);
    }


    /**
    * After this is called, all direct children of the provided namespace object that are 
    * functions will log their name as well as the values of the parameters passed in.
    *
    * @param namespaceObject The object whose child functions you'd like to add logging to.
    */
    public addLoggingToNamespace(namespaceObject)
    {
        for (let name in namespaceObject)
        {
            let potentialFunction = namespaceObject[name];

            if (Object.prototype.toString.call(potentialFunction) === '[object Function]')
            {
                namespaceObject[name] = this.getLoggableFunction(potentialFunction, name);
            }
        }
    }

}



let flog = new FunctionLogger();






class TestClass
{

    private _bar: boolean;


    constructor()
    {
        this._bar = false;

        this.autoBind(this);

        // if (this.log)
        if (true && true)
            this.autoTrace(this);
    }


    private autoBind(self: any): any
    {
        for (const key of Object.getOwnPropertyNames(self.constructor.prototype))
        {

            if (key !== 'constructor')
            {
                // console.log(key);
                // function has a propertyDescriptor as well, with function as value 
                let desc = Object.getOwnPropertyDescriptor(self.constructor.prototype, key);

                if (desc != null)
                {
                    // We can only redefine configurable properties !
                    if (!desc.configurable)
                    {
                        console.log("AUTOBIND-WARNING: Property \"" + key + "\" not configurable ! (" + self.constructor.name + ")");
                        continue;
                    }

                    let g = desc.get != null;
                    let s = desc.set != null;

                    if (g || s)
                    {
                        let newDescriptor: PropertyDescriptor = {};
                        newDescriptor.enumerable = desc.enumerable;
                        newDescriptor.configurable = desc.configurable

                        if (g)
                            newDescriptor.get = desc.get.bind(self);

                        if (s)
                            newDescriptor.set = desc.set.bind(self);

                        Object.defineProperty(self, key, newDescriptor);
                        continue; // if it's a property, it can't be a function 
                    } // End if (g || s) 

                } // End if (desc != null) 

                if (typeof (self[key]) === 'function')
                {
                    let val = self[key];
                    self[key] = val.bind(self);
                } // End if (typeof (self[key]) === 'function') 

            } // End if (key !== 'constructor') 

        } // Next key 

        return self;
    } // End Function autoBind



    private autoTrace(self: any): any
    {


        function getLoggableFunction_old(func, type, name)
        {
            return function (...args)
            {
                let logText = name + '(';

                for (let i = 0; i < args.length; i++)
                {
                    if (i > 0)
                    {
                        logText += ', ';
                    }
                    logText += args[i];
                }
                logText += ');';

                console.log(type + " " + logText);
                return func.apply(self, args);
            };
        }


        function getLoggableFunction(func, type, name)
        {
            return function (...args)
            {
                let logText = name + '(';

                for (let i = 0; i < args.length; i++)
                {
                    if (i > 0)
                    {
                        logText += ', ';
                    }
                    logText += args[i];
                }
                logText += ')';

                console.log("Pre " + type + " " + logText + "; ");
                let res = func.apply(self, args);
                console.log("Post " + type + " " + logText + ":", res);
                return res;
            };
        }


        for (const key of Object.getOwnPropertyNames(self.constructor.prototype))
        {

            if (key !== 'constructor')
            {
                // console.log(key);
                // function has a propertyDescriptor as well, with function as value 
                let desc = Object.getOwnPropertyDescriptor(self.constructor.prototype, key);

                if (desc != null)
                {
                    // We can only redefine configurable properties !
                    if (!desc.configurable)
                    {
                        console.log("AUTOTRACE-WARNING: Property \"" + key + "\" not configurable ! (" + self.constructor.name + ")");
                        continue;
                    }

                    let g = desc.get != null;
                    let s = desc.set != null;

                    if (g || s)
                    {
                        let newDescriptor: PropertyDescriptor = {};
                        newDescriptor.enumerable = desc.enumerable;
                        newDescriptor.configurable = desc.configurable

                        if (g)
                            newDescriptor.get = getLoggableFunction(desc.get.bind(self), "Property", "get_" + key)

                        if (s)
                            newDescriptor.set = getLoggableFunction(desc.set.bind(self), "Property", "set_" + key)

                        Object.defineProperty(self, key, newDescriptor);
                        continue; // if it's a property, it can't be a function 
                    } // End if (g || s) 

                } // End if (desc != null) 

                // if it's not a property, it can only be a function or not a function 
                if (typeof (self[key]) === 'function')
                {
                    let val = self[key];
                    self[key] = getLoggableFunction(val.bind(self), "Function", key);
                } // End if (typeof (self[key]) === 'function') 

            } // End if (key !== 'constructor' && typeof val === 'function') 

        } // Next key 

        return self;
    } // End Function autoTrace



    get bar(): boolean
    {
        return this._bar;
    }
    set bar(value: boolean)
    {
        this._bar = value;
    }


    public hello()
    {
        console.log("hello", "this", this);
    }


    public world(x, y)
    {
        console.log("world", "this", this);

    }

} // End Class TestClass 


let testInstance = new TestClass();
testInstance.hello();
testInstance.world(1, 2);
testInstance.world(1, 3);
let a = testInstance.bar;
console.log("test.bar equals", a);
testInstance.bar = true;
let b = testInstance.bar;
console.log("b equals", b);


interface Window
{
    opera: any;
}


// https://www.scalyr.com/blog/javascript-stack-trace-understanding-it-and-using-it-to-debug/
// http://www.eriwen.com/javascript/js-stack-trace/s
function printStackTrace()
{
    let callstack = [];
    let isCallstackPopulated = false;
    try
    {
        // i.dont.exist += 0; //doesn't exist- that's the point
        throw new Error("i.dont.exist");
    } catch (e)
    {
        if (e.stack)
        { //Firefox
            let lines:string[] = e.stack.split('\n');
            for (let i = 0, len = lines.length; i < len; i++) {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/))
                {
                    callstack.push(lines[i]);
                }
            }
            //Remove call to printStackTrace()
            callstack.shift();
            isCallstackPopulated = true;
        }
        else if (window.opera && e.message)
        { //Opera
            let lines = e.message.split('\n');
            for (let i = 0, len = lines.length; i < len; i++)
            {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/))
                {
                    let entry = lines[i];
                    //Append next line also since it has the file info
                    if (lines[i + 1])
                    {
                        entry += ' at ' + lines[i + 1];
                        i++;
                    }
                    callstack.push(entry);
                }
            }
            //Remove call to printStackTrace()
            callstack.shift();
            isCallstackPopulated = true;
        }
    }
    if (!isCallstackPopulated)
    {
        //IE and Safari
        let currentFunction = arguments.callee.caller;
        while (currentFunction)
        {
            let fn = currentFunction.toString();
            let fname = fn.substring(fn.indexOf("function") + 8, fn.indexOf('')) || "anonymous";
            callstack.push(fname);
            currentFunction = currentFunction.caller;
        }
    }

    output(callstack);
}

function output(arr)
{
    //Output however you want
    console.log(arr.join('\n\n'));
}





interface Error
{
    stacktrace: string;
}


function getStackTrace()
{
    let err = new Error();
    let stack = err.stack || /*old opera*/ err.stacktrace || ( /*IE11*/ console.trace ? console.trace() : "no stack info");
    return stack;
}

interface ErrorConstructor
{
    captureStackTrace: (...args) => string; // not in IE
}

function getStackTraceNodeJS()
{
    let obj = {};
    Error.captureStackTrace(obj, getStackTrace);
    return obj["stack"];
}

console.log(getStackTrace());

// http://www.stacktracejs.com/



// https://github.com/stacktracejs/stacktrace.js
// https://raw.githubusercontent.com/stacktracejs/stacktrace.js/master/dist/stacktrace.js
// https://github.com/stacktracejs/stacktrace.js/blob/master/dist/stacktrace.min.js
function backtrace(opts) 
{
    let stack = [];
    let maxStackSize = 10;

    if (typeof opts === 'object' && typeof opts.maxStackSize === 'number')
    {
        maxStackSize = opts.maxStackSize;
    }

    let curr = arguments.callee;
    while (curr && stack.length < maxStackSize && curr['arguments'])
    {
        // Allow V8 optimizations
        let args = new Array(curr['arguments'].length);
        for (let i = 0; i < args.length; ++i)
        {
            args[i] = curr['arguments'][i];
        }
        if (/function(?:\s+([\w$]+))+\s*\(/.test(curr.toString()))
        {
            //stack.push(new StackFrame({ functionName: RegExp.$1 || undefined, args: args }));
            stack.push({ functionName: RegExp.$1 || undefined, args: args });

        } else
        {
            // stack.push(new StackFrame({ args: args }));
            stack.push({ args: args });
        }

        try
        {
            curr = curr.caller;
        } catch (e)
        {
            break;
        }
    }
    return stack;
}
