FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5042

# Этап сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["marketplace_api.csproj", "./"]
RUN dotnet restore "marketplace_api.csproj"
COPY . .
RUN dotnet build "marketplace_api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации проекта
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "marketplace_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный этап для production
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "marketplace_api.dll"]