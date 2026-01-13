# Property Service

Microservice for managing property information in the Mortgage Application system.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/properties | Get all properties |
| GET | /api/properties/{id} | Get property by ID |
| GET | /api/properties/search | Search properties |
| POST | /api/properties | Create property |
| PUT | /api/properties/{id} | Update property |
| DELETE | /api/properties/{id} | Delete property |
| GET | /api/properties/{id}/appraisal | Get appraisal |
| POST | /api/properties/{id}/appraisal | Create appraisal |
| GET | /api/properties/{id}/title | Get title search |
| POST | /api/properties/{id}/title | Create title search |
| GET | /api/properties/{id}/insurance | Get insurance |
| POST | /api/properties/{id}/insurance | Create insurance |

## Running

```bash
cd src/Property.API
dotnet run
```

Swagger UI: http://localhost:5002/swagger
