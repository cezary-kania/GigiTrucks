FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/Services/Products/GigiTrucks.Services.Products.Api -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/out .
ENTRYPOINT dotnet GigiTrucks.Services.Products.Api.dll