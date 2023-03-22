#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG http_proxy=http://proxy.fpts.com.vn:8080
ARG https_proxy=http://proxy.fpts.com.vn:8080
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConnectDB/ConnectDB.csproj", "ConnectDB/"]
RUN dotnet restore "ConnectDB/ConnectDB.csproj"
COPY . .
WORKDIR "/src/ConnectDB"
RUN dotnet build "ConnectDB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConnectDB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectDB.dll"]