
// HTML2JSX: https://magic.reactjs.net/htmltojsx.htm
// JSX2JS: https://babeljs.io/repl/
// https://www.typescriptlang.org/play
// https://www.typescriptlang.org/docs/handbook/jsx.html
// file needs extension tsx 
async function jsxTest()
{ 
    let NewCrapComponent = React.createClass({
        render: function ()
        {
            return (
                <div>
                    {/* Hello world */}
                    <div className="awesome" style={{ border: '1px solid red' }}>
                        <label htmlFor="name">Enter your name: </label>
                        <input type="text" id="name" />
                    </div>
                    <p>Enter your HTML here</p>
                </div>
            );
        }
    });

    /*
    let NewCrapComponent = React.createClass({
                                    render: function ()
        {
            return (React.createElement("div", null,
                React.createElement("div", {className: "awesome", style: {border: '1px solid red' } },
                    React.createElement("label", {htmlFor: "name" }, "Enter your name: "),
                    React.createElement("input", {type: "text", id: "name" })),
                            React.createElement("p", null, "Enter your HTML here")));
                    }
        });
    */
}




// https://stackoverflow.com/questions/8202195/using-document-createdocumentfragment-and-innerhtml-to-manipulate-a-dom
// document.createDocumentFragment().firstElementChild
function StringToFragment(txt: string): DocumentFragment 
{
    // hooray, IE doesn't support template
    // let temp = document.createElement('template');
    // temp.innerHTML = txt;
    // return temp.content;
    
    let frag = document.createDocumentFragment(),
        tmp = document.createElement('body'), child;
    tmp.innerHTML = txt;
    while (child = tmp.firstElementChild)
    {
        frag.appendChild(child);
    }

    return frag;
}

// https://gist.github.com/yidas/797c9e6d5c856158cffd685b8a8b4ee4
function htmlEncode(txt: string): string
{
    let el:HTMLSpanElement = document.createElement("span");
    el.appendChild(document.createTextNode(txt))

    let s: string = el.innerHTML;
    el = null;

    return s;
}

function htmldecode(str: string): string
{
    if (!str)
        return str;

    let txt:HTMLTextAreaElement = document.createElement('textarea');
    txt.innerHTML = str;
    let s: string = txt.value.replace(/<br\s*[\/]?>/gi, "\n");
    txt = null;

    return s;
}


StringToFragment("<div><span>hello world</span></div>").firstElementChild;



function getScrollPercent() 
{
    let d = document.documentElement,
        b = document.body,
        st = 'scrollTop',
        sh = 'scrollHeight';
    
    
    // scrollTop + window_height
    
    
    // An element's scrollTop value is a measurement of the distance from the element's top to its topmost visible content. 
    // When an element's content does not generate a vertical scrollbar, then its scrollTop value is 0.)
    
    // The scrollHeight value is equal to the minimum height the element would require 
    // in order to fit all the content in the viewport without using a vertical scrollbar. 

    // clientHeight can be calculated as: CSS height + CSS padding - height of horizontal scrollbar (if present).


    // document.documentElement.scrollHeight = 10285
    // document.documentElement.clientHeight = 678
    
    // document.documentElement.scrollTo(0,120)
    // document.documentElement.scrollTop / (document.documentElement.scrollHeight - document.documentElement.clientHeight)
    // scrolltop/(scrollHeight - clientHeight)
    // = scrollPosition / (document.height - window.height)
    return (d[st]||b[st]) / ((d[sh]||b[sh]) - d.clientHeight) * 100;
}
