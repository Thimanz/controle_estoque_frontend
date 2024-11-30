#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./services/GDE.Produtos.API/GDE.Produtos.API.csproj", "services/GDE.Produtos.API/"]
COPY ["./building blocks/GDE.Core/GDE.Core.csproj", "building blocks/GDE.Core/"]
COPY ["./building blocks/GDE.MessageBus/GDE.MessageBus.csproj", "building blocks/GDE.MessageBus/"]

RUN dotnet restore "services/GDE.Produtos.API/GDE.Produtos.API.csproj"
COPY . .
WORKDIR "services/GDE.Produtos.API"
RUN dotnet build "GDE.Produtos.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GDE.Produtos.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GDE.Produtos.API.dll"]