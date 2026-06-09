#UTILIZADO APENAS PARA FAZER DEPLOY NO RENDER, PORQUE NÃO ESTAVA DANDO PARA FAZER NORMAL, ESSA FOI A SOLUÇÃO QUE EU ACHEI!.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AstroTrack.csproj", "."]
RUN dotnet restore "AstroTrack.csproj"
COPY . .
RUN dotnet publish "AstroTrack.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AstroTrack.dll"]
