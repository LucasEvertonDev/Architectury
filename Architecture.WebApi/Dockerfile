#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Architecture.WebApi/Architecture.WebApi.csproj", "Architecture.WebApi/"]
COPY ["Architecture.Infra/Architeture.Infra.IoC/Architecture.Infra.IoC.csproj", "Architecture.Infra/Architeture.Infra.IoC/"]
COPY ["Architecture.Application/Architecture.Application.Core/Architecture.Application.Core.csproj", "Architecture.Application/Architecture.Application.Core/"]
COPY ["Architecture.Application/Architecture.Application.Domain/Architecture.Application.Domain.csproj", "Architecture.Application/Architecture.Application.Domain/"]
COPY ["Architecture.Application/Architecture.Application.UseCases/Architecture.Application.UseCases.csproj", "Architecture.Application/Architecture.Application.UseCases/"]
COPY ["Architecture.Infra/Architecture.Infra.Data/Architecture.Infra.Data.csproj", "Architecture.Infra/Architecture.Infra.Data/"]
COPY ["Architecture.Infra/Architectury.Infra.Plugins/Architectury.Infra.Plugins.csproj", "Architecture.Infra/Architectury.Infra.Plugins/"]
RUN dotnet restore "Architecture.WebApi/Architecture.WebApi.csproj"
COPY . .
WORKDIR "/src/Architecture.WebApi"
RUN dotnet build "Architecture.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Architecture.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Architecture.WebApi.dll"]