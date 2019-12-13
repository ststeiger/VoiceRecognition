/* IE8 */
if (!Array.isArray)
{
    Array.isArray = function (vArg)
    {
        return Object.prototype.toString.call(vArg) === "[object Array]";
    };
}
