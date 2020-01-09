function saveLink() {
    var b = document.body.appendChild(document.createElement("a"));
    b.setAttribute("style", "display: none; ");
    b.href = "blob:https://web.whatsapp.com/dmca9d007ff";
    b.download = "sbb.mp4";
    b.click();
}
function saveBlob(data, fileName) {
    var a = document.createElement("a");
    document.body.appendChild(a);
    a.setAttribute("style", "display: none; ");
    var json = JSON.stringify(data), blob = new Blob([json], { type: "octet/stream" }), url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(url);
}
function testSaveBlob() {
    var data = { x: 42, s: "hello, world", d: new Date() };
    var fileName = "my-download.json";
    saveBlob(data, fileName);
}
