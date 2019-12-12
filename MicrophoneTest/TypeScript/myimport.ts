
// https://github.com/github/fetch/issues/155
async function BetterFetch(url: RequestInfo, options?: RequestInit): Promise<Response>
{
    if (options == null) options = {}
    if (options.credentials == null) options.credentials = 'same-origin'
    
    return new Promise<Response>(async function (resolve, reject)
    {
        try
        {
            let response: Response = await fetch(url, options);
            if (response.status >= 200 && response.status < 300)
            {
                resolve(response);
            }
            else
                reject(new Error("HTTP " + response.status));
        }
        catch (e)
        {
            reject(e);
        }
    });
}


async function test()
{
    console.log("Main load !");
    // @ts-ignore
    // let obj = await require('../js/test.js');
    console.log("Main load success !");
    // console.log(obj);
    // obj.foo();
}



async function main()
{
    console.log("Main !");
    await ScriptLoader.loadPolyfill("/ts/date.js");
    console.log("date loaded !");
    console.log("date loaded2 !");

    // await ScriptLoader.wait(2000);

    console.log("foo", $);
    console.log($.datepicker.formatDate("ddMMyyyy", new Date()));
    console.log("picked !");
    await test();

    // ScriptLoader.promiseTimeout<void>(400, test());

    try
    {
        console.log("before exists");
        let foo = await BetterFetch("index.htm");
        console.log("after exists");
        console.log(foo);

        console.log("before not exists");
        let bar = await BetterFetch("index1.htm");
        console.log("after not exists");
        console.log(bar);
    }
    catch (ex)
    {
        console.log("ex", ex.message);
    }
    
}

main();
