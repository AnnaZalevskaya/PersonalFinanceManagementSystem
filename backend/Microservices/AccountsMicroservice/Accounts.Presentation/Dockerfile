FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Accounts.BusinessLogic/Accounts.BusinessLogic.csproj", "Accounts.BusinessLogic/"]
COPY ["Accounts.DataAccess/Accounts.DataAccess.EFCore/Accounts.DataAccess.EFCore.csproj", "Accounts.DataAccess/Accounts.DataAccess.EFCore/"]
COPY ["Accounts.DataAccess/Accounts.DataAccess.Dapper/Accounts.DataAccess.Dapper.csproj", "Accounts.DataAccess/Accounts.DataAccess.Dapper/"]
COPY ["Accounts.Presentation/Accounts.Presentation.csproj", "Accounts.Presentation/"]
RUN dotnet restore "./AccountsPL/./Accounts.Presentation.csproj"
COPY . .
WORKDIR "/src/Accounts.Presentation"
RUN dotnet build "./Accounts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Accounts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Presentation.dll"]