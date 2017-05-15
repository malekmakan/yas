(function () {
    'use strict';

    define([
        'jquery',
        'Clipboard',
        'service/config',
        'dataAccess/dataAccess',
        'service/logger',
        'validate'
    ], function ($, Clipboard, config, dataAccess, logger) {

        // this class is a view class so feel free to do what you need !
        // DOM objects

        // get shortened url
        function getShortenedUrl(inflateUrl) {

            // new model
            var newModel = dataAccess.DataUrl.newItem({
                inflateUrl: encodeURIComponent(inflateUrl)
            });

            // await
            return $.Deferred(function (def) {

                // call search
                dataAccess.DataUrl.getShortenedUrl({

                    // if action call was successful
                    success: function (data) {

                        var shortenedUrl = data["ShortenedUrl"];

                        var html =
                            '<button id="btn-copy" class="btn btn-info" data-clipboard-target="#shortented-address">' +
                            '<i class="fa fa-copy"/>' +
                            '</button>' +
                            '&nbsp;&nbsp;' +
                            '<a id="shortented-address" href=' + shortenedUrl + ' target="_blank">' + shortenedUrl + '</a>';

                        var shortenedUrlWrapper = $('#shortened-url-wrapper');
                        shortenedUrlWrapper.html(html);
                        shortenedUrlWrapper.removeClass('hidden');

                        var clipboard = new Clipboard('#btn-copy');

                        def.resolve();
                    },
                    // if any Unhanded error accrues
                    error: function () {

                        logger.qLogWarning('An error occurred!');
                        def.reject();
                    }
                }, newModel);

            }).promise();
        }

        // get inflate url
        function getInflateUrl(shortenedUrl) {

            // new model
            var newModel = dataAccess.DataUrl.newItem({
                shortenedUrl: encodeURIComponent(shortenedUrl)
            });

            // await
            return $.Deferred(function (def) {

                // call search
                dataAccess.DataUrl.getInflateUrl({

                    // if action call was successful
                    success: function (data) {

                        var inflateUrl = data["InflateUrl"];

                        var html =
                            '<button id="btn-copy" class="btn btn-info" data-clipboard-target="#inflated-address">' +
                            '<i class="fa fa-copy"/>' +
                            '</button>' +
                            '&nbsp;&nbsp;' +
                            '<a id="inflated-address" href=' + inflateUrl + ' target="_blank">' + inflateUrl + '</a>';

                        var inflatedUrlWrapper = $('#inflate-url-wrapper');
                        inflatedUrlWrapper.html(html);
                        inflatedUrlWrapper.removeClass('hidden');

                        var clipboard = new Clipboard('#btn-copy');

                        def.resolve();
                    },
                    // if any Unhanded error accrues
                    error: function () {

                        logger.qLogWarning('An error occurred!');
                        def.reject();
                    }
                }, newModel);

            }).promise();
        }

        // shortened form 
        $('#shortened-form').validate({
            ignore: [],
            rules: {
                inflateUrl: {
                    required: true
                }
            },
            errorPlacement: function (error, element) {
                var id = $(element).attr('id');
                error.appendTo($("#" + id + '-validate'));
            },
            submitHandler: function () {

                var btn = $('#shortened-btn');
                var inflateUrl = $('input[name="inflateUrl"]');
                btn.button('loading');

                /* call get shortened url*/
                getShortenedUrl(inflateUrl.val()).done(function () {
                    inflateUrl.val('');
                    btn.button('reset');
                });
            }
        });

        // inflate form
        $('#inflate-form').validate({
            ignore: [],
            rules: {
                shortenedUrl: {
                    required: true,
                    url: false
                }
            },
            errorPlacement: function (error, element) {
                var id = $(element).attr('id');
                error.appendTo($("#" + id + '-validate'));
            },
            submitHandler: function () {

                var btn = $('#inflate-btn');
                var shortenedUrl = $('input[name="shortenedUrl"]');
                btn.button('loading');

                /* call get shortened url*/
                getInflateUrl(shortenedUrl.val()).done(function () {
                    shortenedUrl.val('');
                    btn.button('reset');
                });
            }
        });

        return {};

    });

})();