# YAS
Yet Another Shortener in .NET

## What is it ?
A simple .NET web api compatible with SQL Server and Redis as backend database and using advantages of Javascript as client side application.

Client application is plain js to make a single page application framework from scratch and also I included backend project with a fair design pattern.

## How to run?
1. clone the repo.

### Backend (web api)
1. open solution in VS (located in `UrlShortener/UrlShortener.sln`)
2. install nuget packages (something like this in `package manager` console: `Update-Package -safe -reinstall -IgnoreDependencies`)
3. have a look at [web.config](https://github.com/malekmakan/yas/blob/master/UrlShortener/UrlShortener.Api/Web.config) to make sure all the configurations suits your environment; specially sql server/redis connection string.
4. run [ you know good! ctrl+f5:) ]

### client 
1. install node (ignore if you already installed)
2. install npm (ignore if you already installed)
3. cd to Client folder
4. run `npm install`
5. run `bower install`
6. it would be awesome if you can open the [config.js](https://github.com/malekmakan/yas/blob/master/UrlShortenerClient/js/app/service/config.js) file to see if everything if set properly based on your preferences.
7. run `gulp`

* if everything goes well in your default browser automatically app would be run

## Good to know!
If you like to build the client app (minify and uglify js files for any reason) do following steps :
1. `gulp build`
2. `build` 

* if you dont get any errors `www-built` folder would be created. then you may like to copy content of the build folder to root directory of your web hosting (IIS server or etc).

## What is next?
1. some unit test around functional sections
2. make comments reacher 
3. performance and query logging (for further data mining)
4. better UI if you dont like what it is
