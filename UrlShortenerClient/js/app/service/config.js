(function () {
    'use strict';

    define(['jquery'], function () {

        var isDebugMode = true;

        return {
            baseApiUrl: isDebugMode ? 'http://localhost/v1.0/' : 'http://yasdotnet.azurewebsites.net/v1.0/'
        };

    });

})();