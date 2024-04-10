using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Contracts;
using StudentManagementApi.Extension;
using StudentManagementApi.Models;
using StudentManagementApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "SwaggerUI.xml"));
});
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();