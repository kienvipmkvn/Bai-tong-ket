#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MISA.CukCuk/MISA.CukCuk.csproj", "MISA.CukCuk/"]
COPY ["MISA.Infrastructure/MISA.Infrastructure.csproj", "MISA.Infrastructure/"]
COPY ["MISA.ApplicationCore/MISA.ApplicationCore.csproj", "MISA.ApplicationCore/"]
RUN dotnet restore "MISA.CukCuk/MISA.CukCuk.csproj"
COPY . .
WORKDIR "/src/MISA.CukCuk"
RUN dotnet build "MISA.CukCuk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.CukCuk.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISA.CukCuk.dll"]