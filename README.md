# BookManagementWebAPI
## Overview
The Book Management Web API is a RESTful service built using .NET 8, Fast Endpoints, and Entity Framework 8. It allows for the management of a book collection with functionalities for CRUD operations. This API supports operations for retrieving, adding, updating, and deleting books.
## Setup Instructions
Prerequisites
  - .NET 8 SDK installed.
  - SQL Server Express or LocalDB installed.
  - Visual Studio Code or any other code editor of your choice.
## Clone the Repository
git clone https://github.com/your-username/BookManagementAPI.git 

cd BookManagementAPI
## Restore Dependencies
dotnet restore
## Download dependencies in NuGet Package manager
FastEndPoints

FastEndpoints.Swagger

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

Swashbuckle.AspNetCore
## Update Connection String
Update the appsettings.json file to include your database connection string.

{

  "ConnectionStrings": {
  
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    
  }
  
}
## Apply Migrations in NuGet pakage manager
add-migration "Inital Migration"

update-database

## Run the Application
dotnet run

The API will be available at 'http://localhost:5000'.

# API Documentation
## Endpoints
### 1. CREATE OPERATION
  (POST /api/books)
Adds a new book. All required fields must be provided.
