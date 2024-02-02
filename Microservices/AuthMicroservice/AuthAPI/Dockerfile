FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthInfrastructure/Auth.Infrastructure.csproj", "AuthInfrastructure/"]
COPY ["AuthCore/Auth.Core.csproj", "AuthCore/"]
COPY ["AuthApplication/Auth.Application.csproj", "AuthApplication/"]
COPY ["AuthAPI/Auth.API.csproj", "AuthAPI/"]
RUN dotnet restore "./AuthAPI/./Auth.API.csproj"
COPY . .
WORKDIR "/src/AuthAPI"
RUN dotnet build "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.API.dll"]
