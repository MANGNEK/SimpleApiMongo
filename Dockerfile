# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
# WORKDIR /src
# COPY . .
# RUN dotnet restore "SimpleWithMongo/SimpleWithMongo.csproj"
# WORKDIR /src
# RUN dotnet build "SimpleWithMongo/SimpleWithMongo.csproj" -c Release -o /app/build
# RUN dotnet publish "SimpleWithMongo/SimpleWithMongo.csproj" -c Release -o /app/publish

# From mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# WORKDIR /app
# COPY --from=build /app/publish .
# ENTRYPOINT ["dotnet", "AsimKiosk.WebAPI.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SimpleWithMongo/SimpleWithMongo.csproj"
WORKDIR /src
RUN dotnet build "SimpleWithMongo/SimpleWithMongo.csproj" -c Release -o /app/build

RUN dotnet publish "SimpleWithMongo/SimpleWithMongo.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 8080

WORKDIR /app

COPY --from=build /app/publish .


ENTRYPOINT ["dotnet", "SimpleWithMongo.dll"]