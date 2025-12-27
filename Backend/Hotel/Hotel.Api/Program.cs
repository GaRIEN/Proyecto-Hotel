using Hotel.Application.Services;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Registrar DbContext
var connectionString = builder.Configuration.GetConnectionString("HotelConnection")
    ?? throw new InvalidOperationException("Connection string 'HotelConnection' not found.");

builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(connectionString));


// 2️⃣ Registrar controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// 3️⃣ Registrar Swagger tradicional
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Hotel API",
        Version = "v1"
    });
});

builder.Services.AddScoped<ReservationService>();


var app = builder.Build();

// 4️⃣ Configurar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5️⃣ HTTPS y Authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// 6️⃣ Mapear controladores
app.MapControllers();

app.Run();
