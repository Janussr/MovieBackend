using AutoMapper;
using CodeFirstProject.DbConnector;
using CodeFirstProject.DTOs;
using CodeFirstProject.Models;
using CodeFirstProject.Services;
using Microsoft.EntityFrameworkCore;

var policyName = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure AutoMapper and register it with the dependency injection container
var mapperConfig = new MapperConfiguration(config =>
{
    config.CreateMap<User, UserDTO>().ReverseMap();
    // Add more mappings as needed
});
builder.Services.AddScoped<IMapper>(sp => mapperConfig.CreateMapper());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:3000")
                            //.AllowAnyOrigin()
                            //.WithMethods("GET")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});


// DB Connection
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// By adding the builder.Services.AddScoped<UserService>() line, you are instructing the dependency injection container to
// create a new instance of UserService per HTTP request and make it available for injection wherever it is needed.
builder.Services.AddScoped<UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
 