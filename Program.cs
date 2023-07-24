global using firstapi.Models;
global using firstapi.Services.StudentServices;
global using firstapi.Dtos.Student;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using firstapi.Data;
global using firstapi.Services.AddressServices;
global using firstapi.Dtos.Addresses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAddressService<City, AddCityDto>, CityService>();
builder.Services.AddScoped<IAddressService<Area, AddAreaDto>, AreaService>();
builder.Services.AddScoped<IAddressService<Street, AddStreetDto>, StreetService>();
builder.Services.AddScoped<IAddressService<Address, AddAddressDto>, AddressService>();

var app = builder.Build();
app.Use((ctx, next) => { ctx.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:57986"; return next(); });
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
