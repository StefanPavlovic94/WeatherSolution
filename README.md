# WeatherSolution
What is Weather solution:

-----------------------------------------  What is Weather solution  ----------------------------------------------------------



   Weather solution is ASP.NET MVC open source solution that will give you weather info for searched city.
You can see weather info in form of "5-day cards", charts and weather map with information about clouds, rain, snow etc.




------------------------------------------  How to set up Weather solution: ---------------------------------------------------

Weather solution have users that are manipulated throught database with Entity framework so:

1. Create database AccountsContext with table Accounts in which fields must be named like Account class parameters.

How table should look like:
---------------------------------------
    [Id]       NVARCHAR (50) NOT NULL,
    [Name]     NVARCHAR (30) NOT NULL,
    [Lastname] NVARCHAR (30) NOT NULL,
    [Age]      INT           NOT NULL,
    [Email]    NVARCHAR (40) NOT NULL,
    [Username] NVARCHAR (30) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Status]   INT           NOT NULL,
    
    Id is primary key
----------------------------------------

2.  Configure ApplicationConfiguration class:
  
       -Paste connection string in ApplicationConfiguration class in function Initialize(){connectionString = "your connection string";
  
       -Paste your Email/Password combination in ApplicationConfiguration class that app will use for sending confirmation emails.


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
