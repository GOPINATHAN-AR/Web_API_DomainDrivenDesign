# Product Management API - Domain Driven Design (DDD)

This is a Web API project implementing Domain Driven Design principles for product management with full CRUD operations.

## Architecture Overview

The solution follows Domain Driven Design (DDD) principles with the following layers:

### Domain Layer (`ProductManagement.Domain`)
- **Entities**: Core business entities with business logic and domain events
- **Value Objects**: Immutable objects like Money
- **Domain Events**: Events raised by entities when important business actions occur
- **Repositories**: Interfaces for data access

### Application Layer (`ProductManagement.Application`)
- **Commands & Queries**: CQRS pattern implementation using MediatR
- **Handlers**: Command and query handlers
- **DTOs**: Data transfer objects
- **Validators**: FluentValidation rules

### Infrastructure Layer (`ProductManagement.Infrastructure`)
- **Persistence**: Entity Framework Core DbContext and configurations
- **Repositories**: Repository implementations

### API Layer (`ProductManagement.API`)
- **Controllers**: REST API endpoints
- **Configuration**: Dependency injection and middleware setup

## Key DDD Concepts Implemented

1. **Rich Domain Models**: Product entity with encapsulated business logic
2. **Value Objects**: Money value object with currency support
3. **Domain Events**: Events for product creation, updates, price changes, etc.
4. **Repository Pattern**: Abstracted data access
5. **Aggregates**: Product as an aggregate root
6. **CQRS**: Separate commands and queries with MediatR

## Features

- Complete CRUD operations for products
- Price management with currency support
- Stock quantity management
- Product activation/deactivation
- Category-based filtering
- Domain events for business actions
- Input validation with FluentValidation
- In-memory database for development
- Swagger documentation

## API Endpoints

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/category/{category}` - Get products by category
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product details
- `PATCH /api/products/{id}/price` - Update product price
- `PATCH /api/products/{id}/stock` - Update stock quantity
- `PATCH /api/products/{id}/activate` - Activate product
- `PATCH /api/products/{id}/deactivate` - Deactivate product
- `DELETE /api/products/{id}` - Delete product

## Running the Application

1. Navigate to the API project directory:
   ```bash
   cd src/ProductManagement.API
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Access Swagger UI at: `https://localhost:5001/swagger`

## Sample Request Bodies

### Create Product
```json
{
  "name": "Laptop",
  "description": "High-performance gaming laptop",
  "price": 1299.99,
  "currency": "USD",
  "stockQuantity": 50,
  "category": "Electronics"
}
```

### Update Product
```json
{
  "name": "Updated Laptop",
  "description": "Updated high-performance gaming laptop",
  "category": "Electronics"
}
```

### Update Price
```json
{
  "price": 1199.99,
  "currency": "USD"
}
```

### Update Stock
```json
{
  "stockQuantity": 25
}
```

## Domain Events

The following domain events are raised:
- `ProductCreatedEvent`
- `ProductUpdatedEvent`
- `ProductPriceChangedEvent`
- `ProductStockUpdatedEvent`
- `ProductActivatedEvent`
- `ProductDeactivatedEvent`

## Building the Solution

```bash
# From the solution root
dotnet build
```

## Testing

The API uses an in-memory database, so data will be reset on each application restart.
