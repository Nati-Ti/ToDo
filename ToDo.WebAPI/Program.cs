using Microsoft.EntityFrameworkCore;
using ToDo.Persistence.Data;
using ToDo.Persistence.Repositories;
using ToDo.Application.Query;
using ToDo.Application;
using FluentValidation;
using ToDo.Application.Commands.ToDoList;
using ToDo.Application.Validators;
using ToDo.Application.Commands.ToDoItem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ToDoListRepository>();
builder.Services.AddScoped<ToDoItemRepository>();
builder.Services.AddScoped<ToDoDataAccess>();

builder.Services.AddScoped<DapperContext>();

builder.Services.AddControllers();


builder.Services.AddTransient<IValidator<CreateToDoListCommand>, CreateToDoListCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateToDoListCommand>, UpdateToDoListCommandValidator>();
builder.Services.AddTransient<IValidator<CreateToDoItemCommand>, CreateToDoItemCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateToDoItemProgressCommand>, UpdateToDoItemProgressCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateToDoItemPropertyCommand>, UpdateToDoItemPropertyCommandValidator>();


builder.Services
    .AddApplication()
    .AddPersistence();



// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();