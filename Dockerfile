FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Property.API/Property.API.csproj", "Property.API/"]
RUN dotnet restore "Property.API/Property.API.csproj"
COPY src/Property.API/. Property.API/
WORKDIR "/src/Property.API"
RUN dotnet build "Property.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Property.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5002
ENTRYPOINT ["dotnet", "Property.API.dll"]
