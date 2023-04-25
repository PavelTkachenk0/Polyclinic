FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5157

ENV ASPNETCORE_URLS=http://+:5157

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Polyclinic/Polyclinic.csproj", "Polyclinic/"]
RUN dotnet restore "Polyclinic\Polyclinic.csproj"
COPY . .
WORKDIR "/src/Polyclinic"
RUN dotnet build "Polyclinic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Polyclinic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Polyclinic.dll"]
