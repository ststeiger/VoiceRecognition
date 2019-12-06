

// Property 'permissions' does not exist on type 'Navigator'
// for googling: Navigator.permission types in typescript@3.5.3 works fine
interface Navigator
{
    permissions: any;
}


namespace Micro
{
    function askForMicrophone()
    {
        navigator.permissions.query({ name: 'microphone' }).then(function (result)
        {
            if (result.state == 'granted')
            {

            } else if (result.state == 'prompt')
            {

            } else if (result.state == 'denied')
            {

            }
            result.onchange = function ()
            {

            };
        });
    }


    function askgeo2()
    {
        const perm = navigator.permissions;
        perm.query({ name: 'geolocation' })
            .then(permissionStatus =>
            {
                console.log('geolocation permission state is ', permissionStatus.state);
                permissionStatus.onchange = () =>
                {
                    console.log('geolocation permission state has changed');
                };
            });
    }


    function askGeolocation()
    {
        navigator.permissions.query({ name: 'geolocation' }).then(function (result)
        {
            if (result.state === 'granted')
            {
                // showMap();
            } else if (result.state === 'prompt')
            {
                // showButtonToEnableMap();
            }
            // Don't do anything if the permission was denied.
        });
    }


    export function test()
    {
        return "123";
    }
    
    
}
