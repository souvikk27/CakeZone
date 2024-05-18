# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY /CakeZone.Services.Product.csproj ./
RUN dotnet restore

# Copy the project files
COPY . ./

# Build the project
RUN dotnet build

# Publish the project
RUN dotnet publish -c Release -o out

# Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port the app will run on
EXPOSE 80

# Command to run the application
CMD ["dotnet", "CakeZone.Services.Product.dll"]