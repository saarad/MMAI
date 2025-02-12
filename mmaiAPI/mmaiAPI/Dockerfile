FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mmaiAPI/mmaiAPI.csproj", "mmaiAPI/"]
RUN dotnet restore "mmaiAPI/mmaiAPI.csproj"
COPY . .
WORKDIR "/src/mmaiAPI"
RUN dotnet build "mmaiAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mmaiAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mmaiAPI.dll"]
