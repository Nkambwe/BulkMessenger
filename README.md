# Using BackgroundWorker Service with Web API C# ASP.NET Core 7 API

## Overview
BulkMessenger is a C# ASP.NET Core 7 API designed to showcase the implementation of a background service within an ASP.NET Core API. The project utilizes Entity Framework (EF) with Microsoft SQL Server as the database.

## Features
- **Background Service:** Demonstrates the usage of the BackgroundService in ASP.NET Core for performing background tasks.
- **Entity Framework (EF):** Utilizes EF for data access and management.
- **Microsoft SQL Server:** The project is configured to use Microsoft SQL Server as the database.

## Prerequisites
- .NET 7 SDK or later
- Microsoft SQL Server
- Visual Studio or any preferred code editor

## Setup Instructions
1. Clone the repository: `git clone https://github.com/your-username/BulkMessenger.git`
2. Navigate to the project directory: `cd BulkMessenger`
3. Update the database connection string in `appsettings.json` to point to your Microsoft SQL Server instance.
4. Open the project in Visual Studio or your preferred code editor.
5. Build and run the project.

## Database Migration
To apply the database migrations and update the database schema, run the following commands in the project directory:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
