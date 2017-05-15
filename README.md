# YAS
Yet Another Shortener in .NET

## What is it ?
A simple .NET web api compatible with SQL Server and Redis as backend database and using advantages of Javascript as client side application.

Client application is plain js to make a single page application framework from scratch and also I included backend project with a fair design pattern.

## What is next?
1. some unit test around functional sections
2. make comments reacher 
3. performance and query logging (for further data mining)
4. better UI if you dont like what it is

## How to run?
1. clone the repo.

### Backend (web api)
1. open solution in VS
2. install nuget packages 
3. run

### client 
1. install node (ignore if you already installed)
2. install npm (ignore if you already installed)
3. cd to Client folder
4. run `npm install`
5. run `bower install`
6. run `gulp`

* if everything goes well in your default browser automatically app would be run

## Good to know!
If you like to build the client app (minify and uglify js files for any reason) do following steps :
1. `gulp build`
2. `build` 

* if you dont get any errors `www-built` folder would be created. then you may like to copy content of the build folder to root directory of your web hosting (IIS server or etc).
