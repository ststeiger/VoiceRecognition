/* for IE8 */
if (!Object.getOwnPropertyNames)
{
    Object.getOwnPropertyNames = function (obj)
    {
        var arr = [];
        for (var k in obj)
        {
            if (obj.hasOwnProperty(k))
                arr.push(k);
        }

        return arr;
    };
}