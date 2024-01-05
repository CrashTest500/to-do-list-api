using Microsoft.AspNetCore.Mvc;
using to_do_list_api.Data;
using to_do_list_api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
    {
        string[] corsPolicy = Environment.GetEnvironmentVariable("CORS_WHITELIST")?.Split(',');
        p.WithOrigins(corsPolicy);
        p.AllowAnyMethod();
        p.AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapGet("/todo", ([FromServices] IToDoRepository repo) =>
{
    return repo.GetToDoItems();
}).WithName("Fetch All")
.WithOpenApi();

app.MapGet("/todo/{idString}", ([FromServices] IToDoRepository repo, string idString) =>
{
    if (!Guid.TryParse(idString, out Guid id))
    {
        return Results.BadRequest($"{idString} is not a proper GUID");
    }

    return Results.Ok(repo.GetToDoItem(id));
}).WithName("Fetch Item")
.WithOpenApi();

app.MapPost("/todo/add", ([FromServices] IToDoRepository repo, [FromBody] string description) =>
{
    ToDoItem newItem = new()
    {
        Key = Guid.NewGuid(),
        Description = description.Trim()
    };

    try
    {
        newItem.Validate();
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.InnerException.Message);
    }

    repo.AddToDoItem(newItem);

    return Results.Ok();
}).WithName("Add")
.WithOpenApi();

app.MapPut("/todo/complete", ([FromServices] IToDoRepository repo, [FromBody] string idString) =>
{
    if (!Guid.TryParse(idString, out Guid id))
    {
        return Results.BadRequest($"{idString} is not a proper GUID");
    }

    repo.CompleteToDoItem(id);

    return Results.Ok();
}).WithName("Complete")
.WithOpenApi();

app.Run();