#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./api gateways/GDE.Bff.MovimentacaoEstoque/GDE.Bff.MovimentacaoEstoque.csproj", "api gateways/GDE.Bff.MovimentacaoEstoque/"]
COPY ["./building blocks/GDE.Core/GDE.Core.csproj", "building blocks/GDE.Core/"]
COPY ["./building blocks/GDE.MessageBus/GDE.MessageBus.csproj", "building blocks/GDE.MessageBus/"]

RUN dotnet restore "api gateways/GDE.Bff.MovimentacaoEstoque/GDE.Bff.MovimentacaoEstoque.csproj"
COPY . .
WORKDIR "api gateways/GDE.Bff.MovimentacaoEstoque"
RUN dotnet build "GDE.Bff.MovimentacaoEstoque.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GDE.Bff.MovimentacaoEstoque.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GDE.Bff.MovimentacaoEstoque.dll"]