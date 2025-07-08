
using ApiDemo;
using ApiDemo.Services.Abstraction;
using ApiDemo.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(x => 
{
    x.UseSqlite("Data Source = ApiDemoDb.db3");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "An ASP.NET Core Web API for managing XYZ",
        Contact = new OpenApiContact
        {
            Name = "Prabesh",
            Email = "contact@example.com"
        }
    });
});

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

var app = builder.Build();

var scope = app.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // This sets Swagger UI to root (/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
