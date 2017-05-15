(function () {
    'use strict';

    define([
            'dataAccess/dataAccess.dataUrl'
        ],
        function (dataUrl) {
            return {
                DataUrl: dataUrl
            };
        });
})();