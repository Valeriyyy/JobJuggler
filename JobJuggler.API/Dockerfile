FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobJuggler.API/JobJuggler.API.csproj", "JobJuggler.API/"]
COPY ["JobJuggler.Application/JobJuggler.Application.csproj", "JobJuggler.Application/"]
COPY ["JobJuggler.Domain/JobJuggler.Domain.csproj", "JobJuggler.Domain/"]
COPY ["JobJuggler.DTO/JobJuggler.DTO.csproj", "JobJuggler.DTO/"]
COPY ["JobJuggler.Persistence/JobJuggler.Persistence.csproj", "JobJuggler.Persistence/"]
COPY ["JobJuggler.Common/JobJuggler.Common.csproj", "JobJuggler.Common/"]
COPY ["JobJuggler.Mapping/JobJuggler.Mapping.csproj", "JobJuggler.Mapping/"]
COPY ["JobJuggler.Infrastructure/JobJuggler.Infrastructure.csproj", "JobJuggler.Infrastructure/"]
RUN dotnet restore "JobJuggler.API/JobJuggler.API.csproj"
COPY . .
WORKDIR "/src/JobJuggler.API"
RUN dotnet build "./JobJuggler.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# Generate a self-signed certificate for local dev inside the image (PFX)
# NOTE: This certificate will be self-signed and NOT trusted by the host/browser by default.
# Password is intentionally simple for local dev; change if you prefer.
RUN mkdir -p /https \
    && dotnet dev-certs https -ep /https/aspnetapp.pfx -p "LocalPassword123!"

RUN dotnet publish "./JobJuggler.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# Copy published app
COPY --from=publish /app/publish .
# Copy generated certificate into final image
COPY --from=publish /https/aspnetapp.pfx /https/aspnetapp.pfx

USER root
RUN chmod 0644 /https/aspnetapp.pfx || true
USER $APP_UID

# Configure Kestrel to listen on required ports and use the generated cert
ENV ASPNETCORE_URLS="http://+:8080;https://+:5001"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=LocalPassword123!
ENV ASPNETCORE_ENVIRONMENT=Local

ENTRYPOINT ["dotnet", "JobJuggler.API.dll"]

# Build command:
# docker build -t jobjuggler.api:local -f JobJuggler.API
# Run command:
# docker run --rm -p 8080:8080 -p 5001:5001 --name jobjuggler-api -d job-juggler  