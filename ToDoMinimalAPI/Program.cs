using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ToDos.MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IToDoService, ToDoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/todo", (IToDoService service) => service.GetAll());
app.MapGet("/todo/{id}", ([FromServices]IToDoService service,[FromRoute] Guid id) => service.GedById(id));
app.MapPost("/todo", (IToDoService service, [FromBody]ToDo toDo) => service.Create(toDo));
app.MapPut("/todo", (IToDoService service, Guid id, ToDo toDo) => service.Update(toDo));
app.MapDelete("/todo/{id}", (IToDoService service, Guid id) => service.Delete(id));
app.Run();

