# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY . ./
ENTRYPOINT ["dotnet", "DevTools.Server.dll"]