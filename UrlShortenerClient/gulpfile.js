// Include gulp
var gulp = require('gulp');

// Include plugins
var shell = require('gulp-shell');
var concat = require('gulp-concat');
var connect = require('gulp-connect');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var sass = require('gulp-ruby-sass');
var imagemin = require('gulp-imagemin');
var cache = require('gulp-cache');
var jshint = require('gulp-jshint');
var less = require('gulp-less');
var path = require('path');
var minifyCSS = require('gulp-minify-css');
var sourcemaps = require('gulp-sourcemaps');
var server = require('gulp-server-livereload');
var htmlmin = require('gulp-htmlmin');
//var livereload = require('gulp-livereload');

// live reload
gulp.task('webserver', function () {
    gulp.src('./')
        .pipe(server({
            //livereload: true,
            directoryListing: false,
            open: true,
            defaultFile: 'index.html'
        }));
});

// html minification
gulp.task('html', function () {
    gulp.src('./www/test.html')
        .pipe(htmlmin({collapseWhitespace: true}))
        .pipe(gulp.dest('./www-built/test.html'));
});

// Compile CSS from Sass files
gulp.task('sass', function () {
    return sass('./styles/sass/*.sass', {style: 'compressed'})
        .pipe(rename({suffix: '.min'}))
        .pipe(gulp.dest('../www-built/styles/'));
});

// Less task
gulp.task('less-core', function () {
    return gulp.src('./styles/core.less')
        .pipe(sourcemaps.init())
        .pipe(less())
        .pipe(sourcemaps.write())
        .pipe(minifyCSS())
        .pipe(concat('styles-core.min.css'))
        .pipe(gulp.dest('./styles/'));
        //.pipe(livereload());
});

// Optimize images
gulp.task('images', function () {
    return gulp.src('./www/images/**/*')
        .pipe(cache(imagemin({optimizationLevel: 5, progressive: true, interlaced: true})))
        .pipe(gulp.dest('../www-built/build/images'));
});

// Code analysis
gulp.task('analyze', function () {
    return gulp.src(['./www/js/app/**/*.js', '!./app/lib.js', '!./app/main.js'])
        .pipe(jshint('.jshintrc'))
        .pipe(jshint.reporter('jshint-stylish'));
});

// Run the r.js command, such a simple task :)
gulp.task('scripts', function () {
    return shell.task(['node ../tools/r.js -o ../tools/build.js']);
});

// uglify the js file
gulp.task('uglify', function () {
    return gulp.src('../www-built/build/js/app/view/home.js')
        .pipe(uglify());
});

// Watch for changes in files
gulp.task('watch', function () {
    // Watch .js files
    gulp.watch('./app/**/*.js', ['scripts']);
    // Watch .less files
    gulp.watch('./styles/**/*.less', ['less-core']);
    // Watch .css files
    gulp.watch(['./styles/**/*.css', '!./styles/*.css'], ['less-core']);
    // Watch .sass files
    gulp.watch('./styles/sass/*.sass', ['sass']);
    // Watch image files
    gulp.watch('./images/**/*', ['images']);

    //livereload.listen();
});

// Default Task
gulp.task('default', ['analyze', 'scripts', 'less-core', 'sass', 'images', 'webserver', 'watch']);

gulp.task('build', ['analyze', 'scripts', 'uglify', 'less-core', 'sass', 'images']);

gulp.task('less', ['less-core']);