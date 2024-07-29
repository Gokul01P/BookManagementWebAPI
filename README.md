# BookManagementWebAPI

## Overview
***The Book Management Web API is a RESTful service built using .NET 8, Fast Endpoints, and Entity Framework 8. It allows for the management of a book collection with functionalities for CRUD operations. This API supports operations for retrieving, adding, updating, and deleting books.***
## Setup Instructions
Prerequisites
  - .NET 8 SDK installed.
  - SQL Server Express or LocalDB installed and SSMS.
  - Visual Studio Code or any other code editor of your choice.
## Clone the Repository
git clone https://github.com/your-username/BookManagementAPI.git 

cd BookManagementAPI

## File format of the project
Open the project in Visual Studio 2022

![file format](https://github.com/user-attachments/assets/59db2ad5-0f96-4a5c-895d-a12366c70a9c)

## Restore Dependencies
  - dotnet restore
## Download dependencies in NuGet Package manager
  - FastEndPoints

  - FastEndpoints.Swagger

  - Microsoft.EntityFrameworkCore

  - Microsoft.EntityFrameworkCore.Design

  - Microsoft.EntityFrameworkCore.SqlServer

  - Microsoft.EntityFrameworkCore.Tools

  - Swashbuckle.AspNetCore
## Update Connection String
Update the appsettings.json file to include your database connection string.

{

  "ConnectionStrings": {
  
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    
  }
  
}
## Apply Migrations in NuGet pakage manager
  - add-migration "Inital Migration"

  - update-database

## Run the Application
  - dotnet run

The API will be available at 'http://localhost:5000'.

# API Documentation
## Endpoints
### 1. CREATE OPERATION
  (POST /api/books)
  
Adds a new book. All required fields must be provided.

Request Body:

{

  "title": "New Book Title",
  
  "author": "New Book Author",
  
  "publicationYear": 2024,
  
  "isbn": "9876543210987"
  
}

Response:

201 Created

{

  "id": "guid",
  
  "title": "New Book Title",
  
  "author": "New Book Author",
  
  "publicationYear": 2024,
  
  "isbn": "9876543210987"
  
}

### 2. READ OPERATION
i. (GET /api/books)
 
Retrieves all books. Supports optional filtering by author and publication year.

Query Parameters:

author (optional): Filter books by author.

publicationYear (optional): Filter books by publication year.

Response:

200 OK

ii. GET /api/books/{id}
    
Retrieves a specific book by its ID.

Response:

  - 200 OK

{

  "id": "guid",
  
  "title": "Book Title",
  
  "author": "Book Author",
  
  "publicationYear": 2024,
  
  "isbn": "1234567890123"
  
}

  - 404 Not Found if the book does not exist.

## 3. DELETE OPERATION
DELETE /api/books/{id}
 
Deletes a book by its ID.

Response:

  - 200 No Content if the deletion was successful.

  - 404 Not Found if the book does not exist.

## 4. PUT OPERATION
PUT /api/books/{id}

Updates an existing book. Ensures the book exists before updating.

Request Body:

{

  "title": "Updated Book Title",
  
  "author": "Updated Book Author",
  
  "publicationYear": 2025,
  
  "isbn": "1234567890123"
  
}

Response:

  - 200 OK
    
{

  "id": "guid",
  
  "title": "Updated Book Title",
  
  "author": "Updated Book Author",
  
  "publicationYear": 2025,
  
  "isbn": "1234567890123"
  
}

  - 404 Not Found if the book does not exist.
    
## Error Handling

Global error handling is implemented to provide consistent responses for various error conditions. The API returns appropriate HTTP status codes and error messages:

  - 400 Bad Request: Invalid request data.
  - 404 Not Found: Resource not found.
  - 500 Internal Server Error: Unexpected server errors.
    
## Testing
### Testing Framework
install dependencies in NuGet PM
    
  - xUnit

## Unit Tests
Unit tests are written for the following endpoints:

  - GetAllBooksEndpointTests: Verifies the GET /api/books endpoint.

  - CreateBookEndpointTests: Tests the POST /api/books endpoint.

then use below operations:

  - clean Soluiton
  - Build Solution
  - RunAll Tests

    It should pass all tests.

### Contribution
Feel free to open issues or submit pull requests if you have suggestions or improvements.

***Thats's all about Book Management Web API***
