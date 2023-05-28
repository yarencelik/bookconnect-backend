#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/App.API/App.API.csproj", "src/App.API/"]
COPY ["src/App.Application/App.Application.csproj", "src/App.Application/"]
COPY ["src/App.Domain/App.Domain.csproj", "src/App.Domain/"]
COPY ["src/App.Infrastructure/App.Infrastructure.csproj", "src/App.Infrastructure/"]
RUN dotnet restore "src/App.API/App.API.csproj"
COPY . .
WORKDIR "/src/src/App.API"
RUN dotnet build "App.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.API.dll"]