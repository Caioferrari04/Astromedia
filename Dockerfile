ARG PGUSER
ARG PGHOST
ARG PGDATABASE
ARG PGPASSWORD
ARG PGPORT
ARG CLIENT_ID

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Astromedia
COPY ["Astromedia.csproj", "."]
RUN dotnet restore "./Astromedia.csproj"
COPY . .
WORKDIR "/Astromedia/."
RUN dotnet build "Astromedia.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Astromedia.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Astromedia.dll