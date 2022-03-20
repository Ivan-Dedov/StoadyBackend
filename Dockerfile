FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 84
ENV ASPNETCORE_URLS=http://*:84

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Stoady.sln", "./"]
COPY ["/Stoady/Stoady.csproj", "./Stoady/"]
COPY ["/Stoady.DataAccess/Stoady.DataAccess.csproj", "./Stoady.DataAccess/"]

RUN dotnet restore "Stoady.sln"
COPY . .
WORKDIR /src/Stoady
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "Stoady.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Stoady.dll
