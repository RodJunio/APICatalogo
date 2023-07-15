FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APICatalogo/APICatalogo.csproj", "APICatalogo/"]
RUN dotnet restore "APICatalogo/APICatalogo.csproj"
COPY . .
WORKDIR "/src/APICatalogo"
RUN dotnet build "APICatalogo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APICatalogo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APICatalogo.dll"]
