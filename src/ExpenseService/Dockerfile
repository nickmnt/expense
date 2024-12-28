# Use the .NET 8 SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining application files
COPY . ./
RUN dotnet publish -c Release -o /out

# Use a smaller runtime image for final deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published application files from the build stage
COPY --from=build /out .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the necessary ports
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "ExpenseService.dll"]