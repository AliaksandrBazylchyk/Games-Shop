#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GamesShop.Gateway/GamesShop.Gateway.csproj", "GamesShop.Gateway/"]
RUN dotnet restore "GamesShop.Gateway/GamesShop.Gateway.csproj"
COPY . .
WORKDIR "/src/GamesShop.Gateway"
RUN dotnet build "GamesShop.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesShop.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesShop.Gateway.dll"]