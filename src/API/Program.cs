using Microsoft.EntityFrameworkCore;
using Application;
using Infrastructure;
using Infrastructure.Persistence;
using System.Diagnostics;  // hỗ trợ các tác vụ gỡ lỗi (debugging)

// object builder để cấu hình và xây dựng ứng dụng Web bằng cách sử dụng các tham số dòng lệnh (args)
var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    try
    {
        Console.WriteLine("🚀 Starting MySQL...");
        Process.Start(new ProcessStartInfo("docker-compose", "up -d") { UseShellExecute = false });
        await Task.Delay(8000); // Wait 8 seconds
        Console.WriteLine("✅ MySQL started!");
    }
    catch { Console.WriteLine("⚠️ Run 'docker-compose up -d' manually"); }
}

// Add services to the container.
builder.Services.AddControllers();  // Đăng ký service MVC Controller vào dependency injection container để sử dụng
builder.Services.AddEndpointsApiExplorer(); // Kích hoạt API endpoints để ứng dụng có thể khám phá/hiển thị tài liệu API
builder.Services.AddSwaggerGen();  // Kích hoạt Swagger để tạo giao diện tài liệu

// Application & Infrastructure services
// Đăng ký service trong tầng Application và Infrastructure với dependency injection container
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Dev")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();  // middleware xử lý xác thực và phân quyền
app.MapControllers(); // Map (định tuyến) các controller để ứng dụng có thể xử lý các yêu cầu HTTP

// Auto migrate database
// Tạo scope cho service để xử lý dữ liệu với cơ chế Dependency Injection
using (var scope = app.Services.CreateScope())
{
    // Lấy đối tượng ApplicationDbContext thông qua DI container
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    
    // Kiểm tra nếu chưa có bản ghi nào trong bảng Products
    if (!context.Products.Any())
    {
        //Đọc file data.sql từ đường dẫn định sẵn
        var sqlFile = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "data.sql");
        if (File.Exists(sqlFile))
        {
            var sql = await File.ReadAllTextAsync(sqlFile);
            await context.Database.ExecuteSqlRawAsync(sql);
            Console.WriteLine("✅ Data loaded from data.sql!");
        }
    }
}

app.Run();