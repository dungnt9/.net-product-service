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

## Dependencies

- .NET 8
- Entity Framework Core 8
- MediatR
- Grpc.AspNetCore
- Pomelo.EntityFrameworkCore.MySql
