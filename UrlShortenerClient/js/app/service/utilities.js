(function () {
    'use strict';

    define([
        'jquery',
        'nProgress'
    ], function ($, nProgress) {

        // all utilities and cool tools would come here
        // query strings
        function getParameterValues(param) {

            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++)
            {
                var urlparam = url[i].split('=');
                if (urlparam[0] === param) {
                    return urlparam[1];
                }
            }
        }

        // rout getter
        function isValidRout(rout) {

            var url = window.location.href.slice(window.location.href.indexOf('#!') + 1);

            if (url.indexOf('?') > -1) {
                url = url.split('?')[0];
            }

            return !!(url && (url === '!' + rout || url === '!' + rout + '/'));
        }

        // show progress bar
        function showProgressBar() {
            nProgress.configure({ showSpinner: false });
            nProgress.start();
        }

        // hide progress bar
        function hideProgressBar() {
            nProgress.done();
        }

        // replace all
        String.prototype.replaceAll = function (str1, str2, ignore) {
            return this.replace(new RegExp(str1.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, "\\$&"), (ignore ? "gi" : "g")), (typeof (str2) == "string") ? str2.replace(/\$/g, "$$$$") : str2);
        };

        // scroll window to top
        function scrollToTop() {

            $('body,html').animate({
                scrollTop: 0
            });
            return false;
        }
        
        return {

            getParameterValues: getParameterValues,
            isValidRout: isValidRout,
            showProgressBar: showProgressBar,
            hideProgressBar: hideProgressBar,
            scrollToTop: scrollToTop
        };

    });

})();