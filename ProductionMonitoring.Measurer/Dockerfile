FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["ProductionMonitoring.Measurer/ProductionMonitoring.Measurer.csproj", "ProductionMonitoring.Measurer/"]
RUN dotnet restore "ProductionMonitoring.Measurer/ProductionMonitoring.Measurer.csproj"
COPY . .
WORKDIR "/src/ProductionMonitoring.Measurer"
RUN dotnet build "ProductionMonitoring.Measurer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductionMonitoring.Measurer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductionMonitoring.Measurer.dll"]