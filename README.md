# Librarium

## Setup

### Requirements
- .NET 8 SDK
- PostgreSQL

### Database configuration
The API uses PostgreSQL with the connection string in:

`src/Librarium/Librarium.Api/appsettings.json`

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres"
}
