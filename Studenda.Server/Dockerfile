FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Studenda.Server/Studenda.Server.csproj Studenda.Server/
RUN dotnet restore Studenda.Server/Studenda.Server.csproj
COPY . .
WORKDIR /src/Studenda.Server
RUN dotnet build Studenda.Server.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Studenda.Server.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Studenda.Server.dll" ]
