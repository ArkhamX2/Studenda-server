FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Studenda.Server.Test/Studenda.Server.Test.csproj Studenda.Server.Test/
RUN dotnet restore Studenda.Server.Test/Studenda.Server.Test.csproj
COPY . .
WORKDIR /src/Studenda.Server.Test
RUN dotnet build Studenda.Server.Test.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Studenda.Server.Test.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Studenda.Server.Test.dll" ]
