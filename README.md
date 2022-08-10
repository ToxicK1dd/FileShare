ithub# File Share Api
ASP&#46;NET Core Web API built with .NET 6, and Swagger

### Getting Started

To run this you will need a MySQL database. The easiest way is using [Xampp](https://www.apachefriends.org/).\
You will also need to run the [EF Core migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli), to create the database, and schema.

It's recomended that you open, and run the project in [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/).

#### Installing

To run the migrations you will need the package manager console.\
In the top of Visual Studio go to:\
`View -> Other Windows -> Package Manager Console`

When the window is opened, change default project in the window  to `FileShare.DataAccess`

Write the following into the Package Manager to create the database:
```
update-database
```
This requires that Xampp is running the MySQL module,\
and the connection string in appsettings is correct.

You will also have to manually create a database for Hangfire,\
otherwise the application will crash on startup if it cannot connect to it.

After the databases are created, you will have to change\
`max_allowed_packet` in the MySQL configuration.

You will find this variable where your Xampp installation is located.\
This is usually at `C:\xampp\mysql\bin\my.ini`, unless you installed it elsewhere.

Change to the following:
```
[mysqld]
max_allowed_packet=16M

[mysqldump]
max_allowed_packet=16M
```

You now only need to configure the appsettings.json file,\
and you should be able to run the application without problems.

For this i recommend that you utilize [user secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows),\
if you are running it on your local machine.

### Authentication

The api is configured to use JWT bearer authentication.\
Tokens are signed using a HMAC512 signature.

When calling the api, the authorization header must be set,\
and have to follow this specific format:
```
Authorization: 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU3VwZXJtYW4iLCJqdGkiOiJiNmQ2MTYxZi0zNzJlLTQ2MWUtOWZiNi1iMWM0YWFkZGUwYjQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjYwMTU4MjM2fQ.djvSea7mS10zVF2JYq5I-p2VkOnPJ8jqUQsigAYxKRAaKTRDh8ibhu-EBcP3r1rAWrltKl-7bZGd8VQb5cE3LQ'
```

The api also utilize refresh tokens for obtaining a new JWT,\
without the need for re-authentication.

Refresh tokens are valid for 30 days, and after each use\
the token is rotated, and expiration extended.

If they are not used within 30 days, they will expire.

### License
This project is available under the [MIT license](https://github.com/ToxicK1dd/FileShare/blob/master/LICENSE).

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ToxicK1dd/FileShare/blob/master/LICENSE)