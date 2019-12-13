/* IE8 */
if (!Array.isArray)
{
    Array.isArray = function (vArg: any | any[]): vArg is any[]
    {
        return Object.prototype.toString.call(vArg) === "[object Array]";
    };
}
