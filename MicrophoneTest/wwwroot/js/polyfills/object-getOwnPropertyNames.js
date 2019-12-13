/* for IE8 */
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
    }
}