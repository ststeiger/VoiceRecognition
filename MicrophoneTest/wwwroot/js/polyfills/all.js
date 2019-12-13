
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
    };
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






/*
        
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
        */

