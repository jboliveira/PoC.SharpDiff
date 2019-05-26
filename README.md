# PoC.SharpDiff

[![Build Status](https://travis-ci.com/jboliveira/PoC.SharpDiff.svg?token=hoKsky9xhb4rvz3jQYmq&branch=master)](https://travis-ci.com/jboliveira/PoC.SharpDiff)


## Overview


### Project Structure:

```
src
 |__ PoC.SharpDiff.WebAPI
tests
 |__ PoC.SharpDiff.Tests
```


### Tech Stack:

- Visual Studio 2019 (Mac/Win)
- .NET Core 2.2
- CSharp 7.3
- Entity Framework Core (for data access)
- Entity Framework In-Memory Provider (for testing purposes)
- HealthCheck
- Swagger
- Serilog
- FluentValidation
- Azure SQL Server
- xUnit


### Installing and Running

```sh
    #Clone Git Repository
    git clone git@github.com:jboliveira/PoC.SharpDiff.git

    #Access Project Root Folder
    cd PoC.SharpDiff
    
    #Access API folder
    cd src/PoC.SharpDiff

    #Build and Restore packages
    dotnet restore
    dotnet build

    #Run API
    dotnet run

    #Access through address:
    https://localhost:5001/swagger
```


### API Documentation

- Swagger: `{host}/swagger`
- HealthCheck: `{host}/hc`


#### Endpoint: Creates the content for left side.
- URL: `{host}/v1/diff/{id}/left`
- Method: `POST`
- URL params:
    - `id=integer` [Required]
- Body params: `{ "data": "[base64 encoded data]" }`
- Content-Type: `application/json`
- Success Response:
    - Code: 200 
    - Content: `{ "id": 0, "direction": "left", "base64String": "string"}`
- Error Response:
    - Code: 400 BAD REQUEST
    
    
#### Endpoint: Creates the content for right side.
- URL: `{host}/v1/diff/{id}/right`
- Method: `POST`
- URL params:
    - `id=integer` [Required]
- Body params: `{ "data": "[base64 encoded data]" }`
- Content-Type: `application/json`
- Success Response:
    - Code: 200 
    - Content: `{ "id": 0, "direction": "right", "base64String": "string"}`
- Error Response:
    - Code: 400 BAD REQUEST
    
    
#### Endpoint: Compare the specified content id and returns the differences.
- URL: `{host}/v1/diff/{id}`
- Method: `GET`
- URL params:
    - `id=integer` [Required]
- Content-Type: `application/json`
- Success Response:
    - Code: 200 
    - Content: 
        - `{ "string" }` - If differences not found, just message
        - `{ { "offset": 0, "lenght": 0 } }` - If differences found
- Error Response:
    - Code: 400 BAD REQUEST
- Not Found Response:
    - Code: 404 NOT FOUND
    - Content: `{ "string" }`


### Next

- Include a few more tests
- Improve persistence layer and models
- Review comments and documentation
- Use Docker/Docker Compose for database/SQLServer
- WIP


### Packages

HealthChecks Packages:
- `Microsoft.Extensions.Diagnostics.HealthChecks`
- `AspNetCore.HealthChecks.UI.Client`
- `AspNetCore.HealthChecks.SqlServer`

API Versioning Packages: 
- `Microsoft.AspNetCore.Mvc.Versioning`
- `Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer`

Swagger Packages:
- `Swashbuckle.AspNetCore`
- `Microsoft.AspNetCore.StaticFiles`

Logging Packages:
- `Serilog.AspNetCore`
- `Serilog.Settings.Configuration`
- `Serilog.Sinks.Console`

FluentValidation Packages:
- `FluentValidation.AspNetCore`
- `MicroElements.Swashbuckle.FluentValidation`

EFCore - SQLServer Packages:
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`


[//]: #
   [VS2019]: <https://visualstudio.microsoft.com/vs/>
   [.NET SDK]: <https://dotnet.microsoft.com/download/dotnet-core/2.2>