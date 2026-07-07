FROM mcr.microsoft.com/dotnet/sdk:10.0.103 AS build

WORKDIR /src

COPY . .

RUN dotnet restore ScoreZone.slnx

RUN dotnet publish src/ScoreZone.API/ScoreZone.API.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0.3 AS final

ENV ASPNETCORE_ENVIRONMENT=Docker

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "ScoreZone.API.dll" ]