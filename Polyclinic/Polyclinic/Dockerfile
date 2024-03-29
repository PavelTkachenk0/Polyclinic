FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5150

ENV ASPNETCORE_URLS=http://+:5150/

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Polyclinic/Polyclinic.csproj", "Polyclinic/"]
COPY ["Policlinic.DAL/Polyclinic.DAL.csproj", "Policlinic.DAL/"]
COPY ["Polyclinic.Domain/Polyclinic.Domain.csproj", "Polyclinic.Domain/"]
COPY ["Polyclinic.Service/Polyclinic.Service.csproj", "Polyclinic.Service/"]
RUN dotnet restore "Polyclinic/Polyclinic.csproj"
COPY . .
WORKDIR "/src/Polyclinic"
RUN dotnet build "Polyclinic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Polyclinic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Polyclinic.dll"]