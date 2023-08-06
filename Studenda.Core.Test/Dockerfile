FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Studenda.Core.Test/Studenda.Core.Test.csproj", "Studenda.Core.Test/"]
RUN dotnet restore "Studenda.Core.Test/Studenda.Core.Test.csproj"
COPY . .
WORKDIR "/src/Studenda.Core.Test"
RUN dotnet build "Studenda.Core.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Studenda.Core.Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Studenda.Core.Test.dll"]
