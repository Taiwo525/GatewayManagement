using GatewayManagement.Models;
using GatewayManagement.Repository;
using GatewayManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    // Use in-memory database for development
    builder.Services.AddDbContext<GatewayDbContext>(options =>
        options.UseInMemoryDatabase(databaseName: "InMemoryDb"));
}
else
{
    // Use SQL Server for production
    builder.Services.AddDbContext<GatewayDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("con")));
}

// Add Repositories
builder.Services.AddTransient<IGatewayRepo, GatewayRepository>();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//using GatewayManagement.Models;
//using GatewayManagement.Repository;
//using GatewayManagement.Repository.Interface;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();

//// Add DbContext
//builder.Services.AddDbContext<GatewayDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

//builder.Services.AddDbContext<GatewayDbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "InMemoryDb"));

//// Add SqlConnection
////builder.Services.AddSingleton<SqlConnection>(_ =>
////{
////    var connectionString = builder.Configuration.GetConnectionString("con");
////    return new SqlConnection(connectionString);
////});

//// Add Repositories
//builder.Services.AddTransient<IGatewayRepo, GatewayRepository>();
////builder.Services.AddTransient<IPeripheralDRepo, PeripheralDRepo>();

//// Swagger
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
