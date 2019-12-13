/* just in case - not required  */
if (!String.prototype.trimStart)
{
    String.prototype.trimStart = function ()
    {
        return this.replace(/^[\s\uFEFF\xA0]+/g, '');
    };
}
