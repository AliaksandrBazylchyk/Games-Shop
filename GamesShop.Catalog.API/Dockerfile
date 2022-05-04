#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GamesShop.Catalog.API/GamesShop.Catalog.API.csproj", "GamesShop.Catalog.API/"]
RUN dotnet restore "GamesShop.Catalog.API/GamesShop.Catalog.API.csproj"
COPY . .
WORKDIR "/src/GamesShop.Catalog.API"
RUN dotnet build "GamesShop.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesShop.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesShop.Catalog.API.dll"]