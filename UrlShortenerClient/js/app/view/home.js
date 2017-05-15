(function () {
    'use strict';

    define([
            'jquery',
            'model/model',
            'dataAccess/dataAccess',
            'service/config',
            'service/utilities',
            'service/urls',
            'bootstrap' //we have not added this as parameters because we don't use it in the home view (this view)
        ],
        function (
            $,
            model,
            dataAccess,
            config,
            utilities) {

            // this class is a view class so feel free to do what you need !
            // DOM objects

            function pageLoader() {
                
                // load partials
                if(utilities.isValidRout('/show')){

                    // search for partial key & title
                    var key = utilities.getParameterValues('key');
                    key = key ? key : utilities.getParameterValues('t');

                    if(key) {
                        switch (key) {
                            // your case
                        }
                        return; // don't show home anymore
                    }
                }

                // home page
            }

            // handlers
            $(window).on('hashchange', function () {
                utilities.hideProgressBar();
                pageLoader();
            });

            // function calls
            pageLoader();
            
        });

})();