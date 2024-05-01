FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Operations.Application/Operations.Application.csproj", "Operations.Application/"]
COPY ["Operations.Core/Operations.Core.csproj", "Operations.Core/"]
COPY ["Operations.Infrastructure/Operations.Infrastructure.csproj", "Operations.Infrastructure/"]
COPY ["Operations.API/Operations.API.csproj", "Operations.API/"]
RUN dotnet restore "./Operations.API/./OperationsAPI.csproj"
COPY . .
WORKDIR "/src/Operations.API"
RUN dotnet build "./Operations.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Operations.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Operations.API.dll"]