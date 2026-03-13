# PC Gear E-Commerce API

## Overview
PC Gear is a RESTful Web API engineered to serve as the backend for a computer hardware e-commerce platform. It provides a secure, fast, and scalable set of endpoints for managing products, categories, manufacturers, and customer reviews.

## Architecture
The codebase strictly adheres to Clean Architecture, utilizing the Repository Pattern to maintain a high level of abstraction and testability:
* **PcGear.Api**: The entry point of the application, containing RESTful controllers and dependency injection configurations.
* **PcGear.Core**: Contains business logic, interfaces, Data Transfer Objects (DTOs), and mapping extensions.
* **PcGear.Infrastructure**: Handles cross-cutting concerns, including custom exception handling middleware, logging, and JWT configuration.
* **PcGear.Database**: Manages data access via Entity Framework Core, database contexts, migrations, and concrete repository implementations.

## Key Features
* **JWT Authentication**: Secure stateless user authentication and role-based authorization pipeline.
* **Product Catalog**: Comprehensive CRUD operations for hardware components, manufacturers, and nested categories.
* **Review System**: Endpoints allowing authenticated users to submit and retrieve product reviews.
* **Advanced Querying**: Implementation of complex filtering, sorting, and pagination mechanisms for product retrieval.
* **Global Error Handling**: Custom middleware ensuring standardized and secure API error responses.

## Technologies Used
* C# / ASP.NET Core Web API
* Entity Framework Core
* Microsoft SQL Server
* JSON Web Tokens (JWT)

## Getting Started
1. Clone the repository.
2. Navigate to the `PcGear.Api` directory and open `appsettings.Development.json`.
3. Configure the `ConnectionStrings` and provide a secure `JwtSettings:Secret`.
4. Apply migrations by running `dotnet ef database update` in the `PcGear.Database` project.
5. Run the project using `dotnet run` or via Visual Studio to launch the Swagger UI.
