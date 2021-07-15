FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GameService/GameService.csproj", "GameService/"]
RUN dotnet restore "GameService/GameService.csproj"
COPY . .
WORKDIR "/src/GameService"
RUN dotnet build "GameService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameService.dll"]
