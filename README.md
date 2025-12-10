# Product Service

## Mô tả

Product Service là một microservice quản lý thông tin sản phẩm, được xây dựng theo Clean Architecture với .NET 8.

## Kiến trúc

```
src/
├── API/              # Presentation layer (Controllers, gRPC Services)
├── Application/      # Business logic (MediatR Handlers, Queries, Commands)
├── Domain/           # Entities, Value Objects
└── Infrastructure/   # Database, External Services
```

## Ports

| Protocol  | Port | Mô tả                                   |
| --------- | ---- | --------------------------------------- |
| HTTP/REST | 6001 | REST API endpoints                      |
| gRPC      | 6003 | gRPC service cho internal communication |

## gRPC Service

### Cấu hình

gRPC được cấu hình trong `appsettings.json`:

```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:6001",
        "Protocols": "Http1"
      },
      "Grpc": {
        "Url": "http://localhost:6003",
        "Protocols": "Http2"
      }
    }
  }
}
```

### Proto file

File `Protos/product.proto` định nghĩa gRPC service:

```protobuf
service ProductGrpc {
  rpc GetProduct (GetProductRequest) returns (ProductResponse);
  rpc GetProducts (GetProductsRequest) returns (GetProductsResponse);
}
```

### Các RPC methods

| Method        | Mô tả                                           |
| ------------- | ----------------------------------------------- |
| `GetProduct`  | Lấy thông tin 1 product theo ID                 |
| `GetProducts` | Lấy thông tin nhiều products theo danh sách IDs |

## Chạy ứng dụng

```bash
cd src/API
dotnet run --launch-profile Develop
```

Service sẽ listen trên:

- REST API: http://localhost:6001
- gRPC: http://localhost:6003
- Swagger UI: http://localhost:6001/swagger

## GraphQL API

GraphQL endpoint được expose tại `http://localhost:6001/graphql`, hỗ trợ cả Queries và Mutations.

### Cấu hình

GraphQL được cấu hình trong `Program.cs` sử dụng **HotChocolate**:

```csharp
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<ProductType>()
    .AddType<CategoryType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

app.MapGraphQL(); // Endpoint: /graphql
```

### Cấu trúc thư mục

```
src/API/GraphQL/
├── Query.cs              # Các query operations
├── Mutation.cs           # Các mutation operations
├── Types/
│   ├── ProductType.cs    # Type definition cho Product
│   └── CategoryType.cs   # Type definition cho Category
└── InputTypes/
    └── InputTypes.cs     # Input types cho mutations
```

### Queries

| Query          | Mô tả                 | Tham số    |
| -------------- | --------------------- | ---------- |
| `products`     | Lấy tất cả products   | -          |
| `product(id)`  | Lấy product theo ID   | `id: Int!` |
| `categories`   | Lấy tất cả categories | -          |
| `category(id)` | Lấy category theo ID  | `id: Int!` |

**Ví dụ Query:**

```graphql
query {
  products {
    id
    name
    brand
    price
    stock
    isActive
    category {
      id
      name
    }
  }
}
```

### Mutations

| Mutation         | Mô tả             | Input Type            |
| ---------------- | ----------------- | --------------------- |
| `createProduct`  | Tạo product mới   | `CreateProductInput`  |
| `updateProduct`  | Cập nhật product  | `UpdateProductInput`  |
| `deleteProduct`  | Xóa product       | `id: Int!`            |
| `createCategory` | Tạo category mới  | `CreateCategoryInput` |
| `updateCategory` | Cập nhật category | `UpdateCategoryInput` |
| `deleteCategory` | Xóa category      | `id: Int!`            |

**Ví dụ Mutation:**

```graphql
mutation {
  createProduct(
    input: {
      name: "iPhone 16"
      brand: "Apple"
      price: 999.99
      description: "Latest iPhone"
      stock: 100
      categoryId: 1
    }
  ) {
    id
    name
    category {
      name
    }
  }
}
```

### Input Types

```graphql
input CreateProductInput {
  name: String!
  brand: String!
  price: Decimal!
  description: String
  stock: Int!
  categoryId: Int!
}

input UpdateProductInput {
  id: Int!
  name: String!
  brand: String!
  price: Decimal!
  description: String
  stock: Int!
  categoryId: Int!
  isActive: Boolean!
}

input CreateCategoryInput {
  name: String!
  description: String
}

input UpdateCategoryInput {
  id: Int!
  name: String!
  description: String
  isActive: Boolean!
}
```

### Tính năng nâng cao

HotChocolate hỗ trợ các tính năng nâng cao thông qua attributes:

- **`[UseProjection]`**: Chỉ lấy các fields được yêu cầu trong query
- **`[UseFiltering]`**: Cho phép filter dữ liệu (where clause)
- **`[UseSorting]`**: Cho phép sắp xếp dữ liệu

**Ví dụ với Filtering và Sorting:**

```graphql
query {
  products(
    where: { isActive: { eq: true }, price: { gt: 100 } }
    order: { price: DESC }
  ) {
    id
    name
    price
  }
}
```

### GraphQL Playground

Truy cập `http://localhost:6001/graphql` để sử dụng GraphQL Playground tích hợp:

- Viết và test queries/mutations
- Xem schema documentation
- Auto-complete và syntax highlighting

## Dependencies

- .NET 8
- Entity Framework Core 8
- MediatR
- Grpc.AspNetCore
- **HotChocolate.AspNetCore** (GraphQL Server)
- Pomelo.EntityFrameworkCore.MySql
