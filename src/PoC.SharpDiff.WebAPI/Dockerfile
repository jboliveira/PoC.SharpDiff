FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src

COPY src/*/*.csproj /src/csproj-files/

COPY tools/database-update.sh /app/

COPY . .
WORKDIR /src/src/PoC.SharpDiff.WebAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PoC.SharpDiff.WebAPI.dll"]

RUN chmod +x ./database-update.sh
CMD /bin/bash ./database-update.sh
