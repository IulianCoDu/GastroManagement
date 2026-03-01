FROM node:22-alpine AS angular-build
WORKDIR /app
COPY lightnap-ng/package*.json ./
RUN npm ci
COPY lightnap-ng/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "LightNap.WebApi/LightNap.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=angular-build /LightNap.WebApi/wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "LightNap.WebApi.dll"]
