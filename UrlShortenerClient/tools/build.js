{
    appDir: '../',
        mainConfigFile: '../js/common.js',
        dir: '../www-built',
        wrap: true,
        fileExclusionRegExp: /node_modules|tools/,
        removeCombined: true,
        optimize: 'uglify',
        modules:
    [
        {
            name: '../common',
            include: [
                'jquery',
                'bootstrap',
                'amplify',
                'toastr',
                'validate',
                'nProgress',
                'moment',
                'jqueryMobile',
                'Clipboard'
            ]
        },

        {
            name: 'app/view/home',
            exclude: ['../common']
        }

    ]
}