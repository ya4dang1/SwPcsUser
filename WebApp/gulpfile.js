/// <binding Clean='clean, minify, scripts' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var rimraf = require("rimraf");
var merge = require('merge-stream');
var sass = require('gulp-sass');

sass.compiler = require('node-sass');

gulp.task("minify", function () {

    var streams = [
        gulp.src(["wwwroot/js/*.js"])
            .pipe(uglify())
            .pipe(concat("site.min.js"))
            .pipe(gulp.dest("wwwroot/lib/site"))
    ];

    return merge(streams);
});

// Dependency Dirs
var deps = {
    "@coreui": {
        "**": ""
    },
    "@fortawesome": {
        "**": ""
    },
    "angularjs": {
        "**": ""
    },
    "bootstrap": {
        "**": ""
    },
    "bootstrap-datepicker": {
        "**": ""
    },
    "datatables": {
        "**": ""
    },
    "flag-icon-css": {
        "**": ""
    },
    "inputmask": {
        "**": ""
    },
    "jquery": {
        "**": ""
    },
    "select2": {
        "**": ""
    },
    "simple-line-icons": {
        "**": ""
    },
    "toastr": {
        "**": ""
    }
};

gulp.task("clean", function (cb) {
    return rimraf("wwwroot/lib/", cb);
});

gulp.task("scripts", function () {

    var streams = [];

    for (var prop in deps) {
        console.log("Prepping Scripts for: " + prop);
        for (var itemProp in deps[prop]) {
            streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
                .pipe(gulp.dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
        }
    }

    return merge(streams);

});

gulp.task('sass', function () {
  return gulp.src('wwwroot/scss/**/*.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('wwwroot/css'));
});
 
gulp.task('sass:watch', function () {
  gulp.watch('wwwroot/scss/**/*.scss', ['sass']);
});

gulp.task("default", gulp.series('clean', 'scripts', 'sass','minify'));