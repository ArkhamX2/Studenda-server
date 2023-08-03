FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Studenda.Service/Studenda.Service.csproj", "Studenda.Service/"]
RUN dotnet restore "Studenda.Service/Studenda.Service.csproj"
COPY . .
WORKDIR "/src/Studenda.Service"
RUN dotnet build "Studenda.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Studenda.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Studenda.Service.dll"]
