FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/src/LcN.Articles.Keda.Consumer/LcN.Articles.Keda.Consumer.csproj", "LcN.Articles.Keda.Consumer/"]
RUN dotnet restore "LcN.Articles.Keda.Consumer/LcN.Articles.Keda.Consumer.csproj"
COPY . .
WORKDIR "src/LcN.Articles.Keda.Consumer"
RUN dotnet build "LcN.Articles.Keda.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LcN.Articles.Keda.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LcN.Articles.Keda.Consumer.dll"]