FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY src/API/ src/API/
COPY src/Core/ src/Core/
COPY src/Infrastructure/ src/Infrastructure/

RUN dotnet restore src/API/Investry.API/Investry.API.csproj
RUN dotnet publish src/API/Investry.API/Investry.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Investry.API.dll"]