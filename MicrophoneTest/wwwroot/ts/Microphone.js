var Micro;
(function (Micro) {
    function askForMicrophone() {
        navigator.permissions.query({ name: 'microphone' }).then(function (result) {
            if (result.state == 'granted') {
            }
            else if (result.state == 'prompt') {
            }
            else if (result.state == 'denied') {
            }
            result.onchange = function () {
            };
        });
    }
    function askgeo2() {
        var perm = navigator.permissions;
        perm.query({ name: 'geolocation' })
            .then(function (permissionStatus) {
            console.log('geolocation permission state is ', permissionStatus.state);
            permissionStatus.onchange = function () {
                console.log('geolocation permission state has changed');
            };
        });
    }
    function askGeolocation() {
        navigator.permissions.query({ name: 'geolocation' }).then(function (result) {
            if (result.state === 'granted') {
            }
            else if (result.state === 'prompt') {
            }
        });
    }
    function test() {
        return "123";
    }
    Micro.test = test;
})(Micro || (Micro = {}));
