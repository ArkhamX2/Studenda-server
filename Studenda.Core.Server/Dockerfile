FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Studenda.Core.Server/Studenda.Core.Server.csproj", "Studenda.Core.Server/"]
RUN dotnet restore "Studenda.Core.Server/Studenda.Core.Server.csproj"
COPY . .
WORKDIR "/src/Studenda.Core.Server"
RUN dotnet build "Studenda.Core.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Studenda.Core.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Studenda.Core.Server.dll"]
