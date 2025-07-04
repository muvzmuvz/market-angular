# Базовый образ для runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5043
ENV ASPNETCORE_URLS=http://+:5043

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем ТОЛЬКО файл проекта (исправленное имя!)
COPY ["Products.Api.csproj", "./"]
RUN dotnet restore "Products.Api.csproj"

# Копируем ВЕСЬ остальной код
COPY . .

# Этап публикации
RUN dotnet publish "Products.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Оптимизации
USER 1000
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

ENTRYPOINT ["dotnet", "Products_Api.dll"] 
