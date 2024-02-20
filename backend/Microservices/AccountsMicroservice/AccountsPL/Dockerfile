FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AccountsBLL/Accounts.BusinessLogic.csproj", "AccountsBLL/"]
COPY ["AccountsDAL/Accounts.DataAccess.csproj", "AccountsDAL/"]
COPY ["AccountsPL/Accounts.Presentation.csproj", "AccountsPL/"]
RUN dotnet restore "./AccountsPL/./Accounts.Presentation.csproj"
COPY . .
WORKDIR "/src/AccountsPL"
RUN dotnet build "./AccountsPL.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Accounts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Presentation.dll"]