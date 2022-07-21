# Image API
ASP&#46;NET Core Web API built with .NET 6, and Swagger

### Getting Started

To run this you will need a MySQL database. The easiest way is using [Xampp](https://www.apachefriends.org/).\
You will also need to run the [EF Core migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli), to create the database, and schema.

It's recomended that you open, and run the project in [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/).

#### Installing

To run the migrations you will need the package manager console.\
In the top of Visual Studio go to:\
`View -> Other Windows -> Package Manager Console`

When the window is opened, change default project in the window  to `ImageApi.DataAccess`

Write the following into the Package Manager to create the database:
```
update-database
```
This requires that Xampp is running the MySQL module,\
and the connection string in appsettings is correct.

### License
This project is available under the [MIT license](https://github.com/ToxicK1dd/ImageApi/blob/master/LICENSE).

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)