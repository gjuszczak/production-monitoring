FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0
WORKDIR /src
COPY ["ProductionMonitoring.Admin/ProductionMonitoring.Admin.csproj", "ProductionMonitoring.Admin/"]
COPY ["ProductionMonitoring/ProductionMonitoring.csproj", "ProductionMonitoring/"]
RUN dotnet restore "ProductionMonitoring.Admin/ProductionMonitoring.Admin.csproj"
COPY . .
WORKDIR "/src/ProductionMonitoring.Admin"
RUN dotnet build "ProductionMonitoring.Admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductionMonitoring.Admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductionMonitoring.Admin.dll"]