# PoC.SharpDiff

[![Build Status](https://travis-ci.com/jboliveira/PoC.SharpDiff.svg?token=hoKsky9xhb4rvz3jQYmq&branch=master)](https://travis-ci.com/jboliveira/PoC.SharpDiff)

## Overview

```
src
 |__ PoC.SharpDiff.WebAPI
tests
 |__ PoC.SharpDiff.Tests
```

### Tech Stack

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

#### Packages

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


### Next/Improvements
[] ...


[//]: #
   [VS2019]: <https://visualstudio.microsoft.com/vs/>
   [.NET SDK]: <https://dotnet.microsoft.com/download/dotnet-core/2.2>