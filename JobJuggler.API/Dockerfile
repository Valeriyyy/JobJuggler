#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobJuggler.API/JobJuggler.API.csproj", "JobJuggler.API/"]
COPY ["JobJuggler.Application/JobJuggler.Application.csproj", "JobJuggler.Application/"]
COPY ["JobJuggler.Domain/JobJuggler.Domain.csproj", "JobJuggler.Domain/"]
COPY ["JobJuggler.Persistence/JobJuggler.Persistence.csproj", "JobJuggler.Persistence/"]
RUN dotnet restore "./JobJuggler.API/JobJuggler.API.csproj"
COPY . .
WORKDIR "/src/JobJuggler.API"
RUN dotnet build "./JobJuggler.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

copy appsettings.json /app/appsettings.json

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobJuggler.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobJuggler.API.dll"]