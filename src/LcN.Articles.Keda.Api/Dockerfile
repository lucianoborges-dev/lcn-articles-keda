#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/LcN.Articles.Keda.Api/LcN.Articles.Keda.Api.csproj", "src/LcN.Articles.Keda.Api/"]
RUN dotnet restore "src/LcN.Articles.Keda.Api/LcN.Articles.Keda.Api.csproj"
COPY . .
WORKDIR "/src/src/LcN.Articles.Keda.Api"
RUN dotnet build "LcN.Articles.Keda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LcN.Articles.Keda.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LcN.Articles.Keda.Api.dll"]