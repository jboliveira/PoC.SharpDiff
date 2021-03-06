# PoC.SharpDiff #

- **Builds**

[![Actions Status](https://github.com/jboliveira/PoC.SharpDiff/workflows/Build/badge.svg)](https://github.com/jboliveira/PoC.SharpDiff/actions)
[![Travis Status](https://img.shields.io/travis/com/jboliveira/PoC.SharpDiff?label=Build&logo=travis&logoColor=%23FFF)](https://travis-ci.com/jboliveira/PoC.SharpDiff)
[![Build Status](https://buenocode.visualstudio.com/PoC.SharpDiff/_apis/build/status/jboliveira.PoC.SharpDiff?branchName=master)](https://buenocode.visualstudio.com/PoC.SharpDiff/_build/latest?definitionId=8&branchName=master)

- **Codacy**

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/aea87107017c4c3b8d52b760b32970be)](https://www.codacy.com/app/jader.oliveira/PoC.SharpDiff?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=jboliveira/PoC.SharpDiff&amp;utm_campaign=Badge_Grade)
[![Codacy Badge](https://api.codacy.com/project/badge/Coverage/aea87107017c4c3b8d52b760b32970be)](https://www.codacy.com/app/jader.oliveira/PoC.SharpDiff?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=jboliveira/PoC.SharpDiff&amp;utm_campaign=Badge_Coverage)

- **SonarCloud**

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=alert_status)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=security_rating)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=coverage)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=code_smells)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=jboliveira_PoC.SharpDiff&metric=sqale_index)](https://sonarcloud.io/dashboard?id=jboliveira_PoC.SharpDiff)

- **Docker Cloud**

[![Docker Cloud Build Status](https://img.shields.io/docker/cloud/build/jbuenoliveira/dockerhub.svg)](https://cloud.docker.com/repository/docker/jbuenoliveira/dockerhub/builds)
[![Docker Cloud Automated build](https://img.shields.io/docker/cloud/automated/jbuenoliveira/dockerhub.svg)](https://cloud.docker.com/repository/docker/jbuenoliveira/dockerhub/builds)

- **GitHub**

[![StackShare](http://img.shields.io/badge/tech-stack-0690fa.svg?style=flat)](https://stackshare.io/jbuenoliveira/poc-sharpdiff-tech-stack)
![GitHub repo size](https://img.shields.io/github/repo-size/jboliveira/PoC.SharpDiff.svg)
![GitHub last commit](https://img.shields.io/github/last-commit/jboliveira/PoC.SharpDiff.svg)
[![Total alerts](https://img.shields.io/lgtm/alerts/g/jboliveira/PoC.SharpDiff.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/jboliveira/PoC.SharpDiff/alerts/)

## Overview


### Project Structure:

```
src
 |__ PoC.SharpDiff.WebAPI
 |__ PoC.SharpDiff.Domain
 |__ PoC.SharpDiff.Persistence
 |__ PoC.SharpDiff.Resources
tests (order Test Explorer by Traits)
 |__ PoC.SharpDiff.TestUtilities
 |__ PoC.SharpDiff.Resources.Tests (unit)
 |__ PoC.SharpDiff.WebAPI.Tests (unit)
 |__ PoC.SharpDiff.Tests (integration)
```


### Tech Stack:

- Visual Studio 2019 (Mac/Win)
- .NET Core 3.0 [3.0.100]
- Entity Framework Core (for data access)
- Entity Framework In-Memory Provider (for testing purposes)
- HealthCheck
- Swagger
- Serilog
- FluentValidation
- SQL Server
- xUnit
- Docker
- TravisCI
- Azure DevOps
- GitHub Actions
- SonarCloud
- Codacy

### Installing and Running

```sh
    #Clone Git Repository
    git clone git@github.com:jboliveira/PoC.SharpDiff.git

    #Access Project Root Folder
    cd PoC.SharpDiff
    
    #Build and Run
    docker-compose build
    docker-compose up

    #Access through address:
    https://localhost:8000/swagger
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

- [x] Include/Improve test layer with a better code coverage
- [x] Review comments and documentation
- [x] Use Docker/Docker Compose for database/SQLServer
- [x] Improve persistence layer and models
- [x] Migration to .NET Core 3.0
- [ ] Add Postman collection and environment
- [ ] Create a Watchdog UI to watch health and report about the API
- [ ] Capture application logs via third-party service
- [ ] Add a Vault service to manage secrets and protect sensitive data
- [ ] Add System and Smoke tests
- [ ] Improve validation layer
- [ ] WIP :rocket:


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
