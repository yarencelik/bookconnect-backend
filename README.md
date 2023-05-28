
# bookconnect api
Backend services for **`bookconnect`**

## Tech

**`bookconnect`** used some of the following tech and architecture for this project:

- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)  - ORM used for this project.
- [Postgresql/Npgsql](https://www.npgsql.org/) - Postgresql for .Net and Database used for this project.
- [StackExchange Redis](https://www.nuget.org/packages/Microsoft.Extensions.Caching.StackExchangeRedis/8.0.0-preview.3.23177.8) - Caching Service for Auth Tokens.
- [Automapper](https://automapper.org/) - A package that used for mapping objects to another object (Entity, DTO, etc.).
- [FluentValidation](https://fluentvalidation.net/) - A package that used for object validation.
- [MedatR](https://www.nuget.org/packages/MediatR) - For Implementing Mediator Pattern and used for communicating internal Services (like an in-memory pub-sub pattern).
- [Isopoh.Cryptography.Argon2](https://www.nuget.org/packages/Isopoh.Cryptography.Argon2) - For password hashing using Argon2.
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/8.0.0-preview.3.23177.8) - A external package or a middleware that receives a JWT Bearer Token.
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt) - For Serializing and Validating the JWT Tokens.
- [Microsoft.AspNetCore.JsonPatch](https://www.nuget.org/packages/Microsoft.AspNetCore.JsonPatch/8.0.0-preview.3.23177.8) - .NET core support for Json Patch
- [Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson/8.0.0-preview.3.23177.8) - For input and output formatters using Json and Json patch.
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)  - For decoupling the software into layers.
- [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) - A Built in Package for accepting REST API requests inside the VS Code.
- [Docker](https://www.docker.com/) - For App Containerization.

## Installation
***Pre-requisites:***
- Clone this repository 
- Install **Docker** here => https://www.docker.com/
	- #### Optional (For API Testing):
		- Install **REST Client** here => https://marketplace.visualstudio.com/items?itemName=humao.rest-client
		- Install **POSTMAN** here => https://www.postman.com/
		
- Install .Net Core here => https://dotnet.microsoft.com/en-us/download **(Not Required, Only required if you want to run the app locally, and not thru docker)**
- **This is Required: `appsettings.json` or `appsettings.Development.json` must be created inside the `src/App.API` folder/project with this format:**
```sh
{
  "Serilog": {
    "Using":  [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
    "MinimumLevel": {
      "Default": "Information",
      "Overrride": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      { 
        "Name": "File", 
        "Args": { 
          "path": "Logs/log-.txt",
          "rollingInterval": "Day",
          "rollOnFileSizeLimit": true,
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  },
  "ConnectionStrings": {
    "DB": "Host=localhost:5432;Database=bookconnect_db;Username=admin;Password=root"
  },
  "Seed": {
    "Admin_Username": "admin",
    "Admin_Password": "admin",
    "Admin_Email": "admin@admin.com",
    "Reader_Username": "reader",
    "Reader_Password": "reader",
    "Reader_Email": "reader@reader.com"

  },
  "PasswordSettings": {
    "Salt":  "0Tenz4aDe-GVWdvgRVf9"
  },
  "CacheSettings": {
    "ConnectionString": "localhost:6379"
  },
  "Authentication": {
    "SecretForKey": "L[S18I}'J&2>&YC(b%~*kOnFvLHv+]vI-sv%!1gpY}8GZ0]NMY%HJMh@vAEjy;Q",
    "Issuer": "http://localhost:8001",
    "Audience": "http://localhost:8001"
  },
  "ClientURL": "http://localhost:3000"
}
```



## Initializing/Starting
**For Initializing/Starting the application and the docker apps, kindly enter these commands below:**

> ***Note***: should be in the root folder.

```sh
docker compose up -d 
```
**You can now use the app with specific URLs:**
- `localhost:8001` **for normal client calls/requests**
- `localhost:8001/swagger` **for API swagger documentation**

> **Note**: If you already have .NET Core installed on your machine, and you want to run the app locally (and not in docker) kindly enter the command below:

```sh
src/App.API // Navigate to the API Folder
dotnet run // To run the Application
```
**You can now use the app with specific URLs:**
- `localhost:5086` **for normal client calls/requests**
- `localhost:5086/swagger` **for API swagger documentation**
