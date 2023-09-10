# Novorender ASP.NET Core Project

## Introduction

This project is an example ASP.NET Core application that demonstrates the use of Swagger for API documentation and generating zip files with random content.

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional, but recommended)

### Installation

1. Clone the repository to your local machine:
   ```bash
   git clone <repository-url>
    ```
2. Navigate to the project directory:
	```bash
	cd NovorenderAspNetCoreProject
	```
3. Build and run the project using the .NET CLI:
	```bash
	dotnet run
	```
	The application will start and be accessible at [https://localhost:{{PORT_NUMBER}}](https://localhost:{{PORT_NUMBER}}).
	Replace `{{PORT_NUMBER}}` with the actual port number when running the application. By default, the application runs on port 7188 in development.

### Usage

-	API Documentation with Swagger
	Swagger is integrated into the project to provide API documentation. You can access the Swagger UI at:
	```bash
	https://localhost:7188/swagger
	```
-   Generate Zip Files
	The application provides an endpoint to generate zip files with random content. You can use the following API endpoint to generate a zip file:
	```bash
	https://localhost:7201/api/zip/{files:int}
	```
	Replace {files} with the number of files you want to include in the zip.

### Configuration
You can configure the application by modifying the appsettings.json file or using environment variables. 