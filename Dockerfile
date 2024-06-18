FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.18 AS build-env
WORKDIR /app

COPY Invoicer/API/API.csproj ./Invoicer/API/
RUN dotnet restore ./Invoicer/API/API.csproj

COPY . ./
RUN dotnet publish ./Invoicer/API/API.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1-alpine3.18
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]
