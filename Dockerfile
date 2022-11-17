FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

# Go and get tailwind
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
RUN npm install -D tailwindcss

# Build and publish a release
RUN dotnet publish -c Release -o out
RUN npx tailwindcss -c ./Client/tailwind.config.js -i ./Client/wwwroot/css/input.css -o ./out/wwwroot/css/output.css --minify

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out/ .
ENTRYPOINT ["dotnet", "DevTools.Server.dll"]