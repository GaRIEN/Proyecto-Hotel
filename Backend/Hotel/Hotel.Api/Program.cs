using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Registrar DbContext
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelConnection")));

// 2️⃣ Registrar controladores
builder.Services.AddControllers();

// 3️⃣ Registrar Swagger tradicional
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
