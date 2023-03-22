#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG http_proxy=http://proxy.fpts.com.vn:8080
ARG https_proxy=http://proxy.fpts.com.vn:8080
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EFCore/EFCore.csproj", "EFCore/"]
RUN dotnet restore "EFCore/EFCore.csproj"
COPY . .
WORKDIR "/src/EFCore"
RUN dotnet build "EFCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCore.dll"]