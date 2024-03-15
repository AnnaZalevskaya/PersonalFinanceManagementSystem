FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OperationsApplication/Operations.Application.csproj", "OperationsApplication/"]
COPY ["OperationsCore/Operations.Core.csproj", "OperationsCore/"]
COPY ["OperationsInfrastructure/Operations.Infrastructure.csproj", "OperationsInfrastructure/"]
COPY ["OperationsAPI/Operations.API.csproj", "OperationsAPI/"]
RUN dotnet restore "./OperationsAPI/./OperationsAPI.csproj"
COPY . .
WORKDIR "/src/OperationsAPI"
RUN dotnet build "./Operations.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Operations.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Operations.API.dll"]