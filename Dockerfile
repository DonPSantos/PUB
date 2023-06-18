# syntax = docker/dockerfile:1.2

#Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

RUN --mount=type=secret,id=SECRETS,dst=/etc/secrets/SECRETS
RUN dotnet restore "./PUB.API/PUB.API.csproj"
RUN dotnet publish "./PUB.API/PUB.API.csproj" -c Release -o /app --no-restore

#Server stage

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "PUB.API.dll"]