FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ElasticSearchKibana/ElasticSearchKibana.csproj", "ElasticSearchKibana/"]
RUN dotnet restore "ElasticSearchKibana/ElasticSearchKibana.csproj"
COPY . .
WORKDIR "/src/ElasticSearchKibana"
RUN dotnet build "ElasticSearchKibana.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearchKibana.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticSearchKibana.dll"]
