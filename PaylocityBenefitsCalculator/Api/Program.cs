using Microsoft.OpenApi.Models;
using Api.Extensions;
using Business.Extensions;
using Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

builder
    .Services
        .AddAutoMapper(x => x.AddMaps(new List<string> { "Api", "Business", "Data" }))
        .AddBusinessServices()
        .AddDataServices()
        .AddDataClientServices(builder.Configuration.GetSection("Data.Client.Settings"));

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowLocalhost,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000", "http://localhost")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseApiExceptionHandler();

app.Run();