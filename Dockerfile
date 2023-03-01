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
RUN npm install -D @tailwindcss/typography

# Build and publish a release
RUN dotnet publish -c Release -o out

# Generate tailwind css
WORKDIR /App/Client
RUN npx tailwindcss -i ./wwwroot/css/input.css -o ../out/wwwroot/css/output.css --minify

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out/ .
ENTRYPOINT ["dotnet", "DevTools.Server.dll"]