/* for IE8 - not required */
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