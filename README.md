# PartsTracker

## Setup Instructions

1. **Prerequisites**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - [Node.js & npm](https://nodejs.org/) (for JavaScript/TypeScript client)
   - [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or compatible database

2. **Clone the Repository**
   git clone <repository-url> cd PartsTracker

3. **Database Migrations**
   - Migrations are applied automatically on startup via `MigrationExtensions.ApplyMigrations`.
   - Ensure your connection strings are set in `appsettings.json`.

4. **Build & Run Server**
cd src/server dotnet build dotnet run --project Api/PartsTracker.Api

5. **Run Client (if applicable)**
cd src/client npm install npm start

6. **Run Tests**
dotnet test

## Architecture Diagram

![Azure Deployment Architecture](https://raw.githubusercontent.com/PartsTracker/diagrams/main/azure-architecture.png)

## Rationale

- **Modular Design:** Separates Parts and Users for maintainability and scalability.
- **Automatic Migrations:** Ensures database schema is up-to-date on deployment.
- **Domain Events & Outbox:** Supports reliable event-driven workflows.
- **Layered Testing:** Unit, integration, and architecture tests ensure robustness.