# WeatherSolution
What is Weather solution:

-----------------------------------------  What is Weather solution  ----------------------------------------------------------

Weather solution is ASP.NET MVC open source solution that will give you weather info for searched city.
You can see weather info in form of "5-day cards", charts and weather map with information about clouds, rain, snow etc.


------------------------------------------  How to set up Weather solution: ---------------------------------------------------

1.  Weather solution have users that are manipulated throught database with Entity framework so create database with table Accounts
  in which fields must be named like Account class parameters.

2.  Configure ApplicationConfiguration class:
  -Paste connection string in Aplication configuration class in function Initialize(){connectionString = "your connection string";
  -Paste your Email/Password combination that will App use for sending confirmation emails.


----------------------------------------------------- Informations ------------------------------------------------------------


Weather solution is using OpenWeatherMap api (5 days weather forecast and map api) for collecting information about weather.
It have custom writen api wraper for easier use of api (Rest sharp). 

Used:
-jQuery functions called asynchroniously for displaying 5-day cards.
-chart.js library for displaying chart. Measurement on each 3 hours for 5 days.
-leaflant.js for map integration.
-Entity framework for manipulaing users
-Authentication and authorization throught form authentication
-Email confirmation after sign up
