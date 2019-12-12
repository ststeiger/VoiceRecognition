

Date.prototype.getTicksUTC = function () {
    return Date.parse(this.toUTCString()) + this.getUTCMilliseconds();
}; // End Function getTicksUTC


Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--)
    {
        if (this[i] === obj)
        {
            return true;
        }
    }
    return false;
}; // End Function contains


// Read a page's GET URL variables and return them as an associative array.
function getUrlVars(urlHref)
{
    var vars = [], hash;
    var hashes = urlHref.slice(urlHref.indexOf('?') + 1).split('&');
    var i;

    for (i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    } // Next i 

    return vars;
} // End Function getUrlVars
