//The build will inline common dependencies into this file.

//For any third party dependencies, like jQuery, place them in the lib folder.

//Configure loading modules from the lib directory,
//except for 'app' ones, which are in a sibling
//directory.
requirejs.config({
    baseUrl: 'js/app',
    paths: {
        app: '../app',

        // plugin for text!
        text: '../lib/require-text',
        jquery: '../../bower_components/jquery/dist/jquery',
        bootstrap: '../../bower_components/bootstrap/dist/js/bootstrap.min',
        amplify: '../../bower_components/amplify/lib/amplify',
        toastr : '../../bower_components/toastr/toastr',
        validate : '../../bower_components/jquery-validation/dist/jquery.validate.min',
        nProgress : '../../bower_components/nprogress/nprogress',
        moment : '../../bower_components/moment/min/moment.min',
        jqueryMobile : '../lib/jquery.mobile.custom.min',
        Clipboard : '../../bower_components/clipboard/dist/clipboard.min',

        template: '../../views',

        model: 'model',
        dataAccess: 'controller',
        service: 'service',
        view: 'view'
    },
    shim: {
        jquery: {
            exports: 'jquery'
        },
        bootstrap: {
            deps: ['jquery'],
            exports: 'bootstrap'
        },
        amplify: {
            deps: ['jquery'],
            exports: 'amplify'
        },
        toastr: {
            deps: ['jquery'],
            exports: 'toastr'
        },
        validate: {
            deps: ['jquery'],
            exports: 'validate'
        },
        nProgress: {
            deps: ['jquery'],
            exports: 'nProgress'
        },
        moment:{
            deps: ['jquery'],
            exports: 'moment'
        },
        jqueryMobile:{
            deps: ['jquery'],
            exports: 'jqueryMobile'
        },
        Clipboard:{
            deps: ['jquery'],
            exports: 'Clipboard'
        },
        model: {
            deps: ['jquery'],
            exports: 'model'
        },
        template: {
            exports: 'template'
        },
        dataAccess: {
            deps: ['jquery','amplify'],
            exports: 'dataAccess'
        },
        view: {
            deps: ['jquery'],
            exports: 'view'
        }
    }
});