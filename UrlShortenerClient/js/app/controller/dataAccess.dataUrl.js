(function () {
    'use strict';

    define(['service/config', 'model/model', 'amplify'], function (config, model, amplify) {


        var service = {
            newItem: newItem,
            getInflateUrl: getInflateUrl,
            getShortenedUrl: getShortenedUrl
        };

        function init() {

            // get shortened url
            amplify.request.define('getShortenedUrl', 'ajax', {
                url: config.baseApiUrl + 'url/getShortenedUrl?inflateUrl={inflateUrl}',
                dataType: 'json',
                type: 'GET',
                decoder: function (data, status, xhr, success, error) {
                    if (status === 'success') {
                        success(data, status);
                    } else if (status === 'fail' || status === 'error') {
                        try {
                            error(JSON.parse(xhr.responseText), status);
                        } catch (er) {
                            error(xhr.responseText, status);
                        }
                    }
                }
            });

            // get inflate url
            amplify.request.define('getInflateUrl', 'ajax', {
                url: config.baseApiUrl + 'url/getInflateUrl?shortenedUrl={shortenedUrl}',
                dataType: 'json',
                type: 'GET',
                decoder: function (data, status, xhr, success, error) {
                    if (status === 'success') {
                        success(data, status);
                    } else if (status === 'fail' || status === 'error') {
                        try {
                            error(JSON.parse(xhr.responseText), status);
                        } catch (er) {
                            error(xhr.responseText, status);
                        }
                    }
                }
            });
        }

        // get shortened url
        function getShortenedUrl(callbacks, data) {
            return amplify.request({
                resourceId: 'getShortenedUrl',
                data: data,
                success: callbacks.success,
                error: callbacks.error
            });
        }

        // get inflate url
        function getInflateUrl(callbacks, data) {
            return amplify.request({
                resourceId: 'getInflateUrl',
                data: data,
                success: callbacks.success,
                error: callbacks.error
            });
        }

        // new model
        function newItem(options) {
            return new model.ModelUrl(options);
        }

        init();

        return service;

    });

})();