FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ScriptService.App/ScriptService.App.csproj
RUN dotnet build ScriptService.App/ScriptService.App.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ScriptService.App/ScriptService.App.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ScriptService.App/nginx.conf /etc/nginx/nginx.conf