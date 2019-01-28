# RSS Feed reader

The idea of this project is to able to read news articles from RSS feeds in a web application.

This repository has a web api made in .Net Core that reads RSS Feeds and a web solution meda in angular 7 to show those feeds.

## Requirements for Installation
- Visual Studio 2017 or VS Code
- .NET Core 2.2 framework
- Check you have NODE installed in your PC

## Installation
This repo has 2 folders:
- __RssFeedApi__: is the .Net Core Web API made with C# and .Net Core 2.2
- __Web__: is the web project made in angular 7 

After downloading this repo and unzip it, please follow the next instructions to configure everything.

### Web Api
1) Open the solution of the folder __"RssFeedApi"__ with VS2017 or newer 
2) File __appsettings.json__: you don't need to change this, but if you do please ensure you do it consistently.
   - __RssFeed__: has the URL of the RSS Feed
   - __WebSolution__: has the URL of the angular web page. This page will be running by default on http://localhost:4200 , if you change this you will need to change the base URL of the angular project as well.
3) Ensure the file __launchSettings.json__ is pointing to this URL http://localhost:10140 as the Web project is looking at this URL to access the Web API
4) There is a file __FakeXmlFeed.xml__ in the root of the __RssFeedApi__ folder with a few news that is used for testing in the testing project. Be sure to keep this file in order to avoid issues running the tests

  __Note__: more likely you will need to change the path of this file to match with a directory of your computer where you copied the  web API

5) The configuration file __testappconfig.json__ in the test project, is used to set the path of the previous xml file
6) Compile the solution and press F5 to run the API

### Web solution installation
1) Go to the command line and navigate until the __"Web\rss-feed-app"__ folder, in the root of this folder execute these commands:

  #### npm install 
  > To install the Node packages
  
  #### ng serve
  > To run the web server
  
2) The service __feed.service.ts__ located in this path __Web\rss-feed-app\src\app\feeds__ has a variable that points to the API (API_URL), if you change the API base URL be sure to change also this

## Execution
Once you have configured everything like in the previous steps, you should have running:
- the web solution in the URL http://localhost:4200/ 
- the Web API in the URL http://localhost:10140 , you should see some JSON data in the browser when you launch it
- click on the link "Load feeds" to see the news articles coming from the API, or go to the URL: http://localhost:4200/feeds/ 
