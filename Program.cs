var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Swagger generator + Swagger UI ³£­n
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // ¡÷ JSON endpoint¡G/swagger/v1/swagger.json
    app.UseSwaggerUI();      // ¡÷ Swagger UI ¡÷ /swagger
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
