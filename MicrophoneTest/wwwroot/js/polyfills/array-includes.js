/* for IE8 - not required */
if (!Array.prototype.includes)
{
    Array.prototype.includes = function (obj, fromIndex)
    {
        if (null == this)
            throw new TypeError('"this" is null or not defined');

        var t = Object(this), n = t.length >>> 0;

        if (0 === n)
            return false;

        var i, o, a = 0 | fromIndex, u = Math.max(0 <= a ? a : n - Math.abs(a), 0);

        for (; u < n;)
        {
            if ((i = t[u]) === (o = obj) || "number" == typeof i && "number" == typeof o && isNaN(i) && isNaN(o))
                return true;

            u++;
        }

        return false;
    };
}