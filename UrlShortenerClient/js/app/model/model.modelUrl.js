(function () {
    'use strict';

    define(
        function () {

            var model = function (options) {

                this.shortenedUrl = options.shortenedUrl;
                this.inflateUrl = options.inflateUrl;
                
            };

            return model;
        });
})();