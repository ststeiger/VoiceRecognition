
function saveLink()
{
    let b = <HTMLAnchorElement>document.body.appendChild(document.createElement("a"));
    b.setAttribute("style", "display: none; ");
    b.href = "blob:https://web.whatsapp.com/dmca9d007ff";
    b.download = "sbb.mp4";
    b.click();
}


function saveBlob(data, fileName)
{
    let a = document.createElement("a");
    document.body.appendChild(a);
    a.setAttribute("style", "display: none; ");

    let json = JSON.stringify(data),
        blob = new Blob([json], { type: "octet/stream" }),
        url = window.URL.createObjectURL(blob);

    a.href = url;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(url);
}


function testSaveBlob()
{
    let data = { x: 42, s: "hello, world", d: new Date() };
    let fileName = "my-download.json";

    saveBlob(data, fileName);
}
