FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY Domain/*.csproj ./Domain/
COPY Application/*.csproj ./Application/
COPY Infrastructure.Data/*.csproj ./Infrastructure.Data/
COPY web/*.csproj ./web/

RUN dotnet restore

COPY Domain/. ./Domain/
COPY Application/. ./Application/
COPY Infrastructure.Data/. ./Infrastructure.Data/
COPY web/. ./web/

RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build ./app/out .

ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENV ASPNETCORE_ENVIRONMENT=Production
    
ENTRYPOINT ["dotnet", "web.dll"]

EXPOSE 80   
EXPOSE 44357
EXPOSE 8080