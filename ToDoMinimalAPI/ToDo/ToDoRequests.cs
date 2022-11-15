
using ToDos.MinimalAPI;

namespace ToDoMinimalAPI.ToDos
{
    public static class ToDoRequests
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/todo", ToDoRequests.GetAll /*(IToDoService service) => service.GetAll()*/)
                .Produces<List<ToDo>>()
                .WithTags("ToDos");

            app.MapGet("/todo/{id}", ToDoRequests.GetById/*([FromServices]IToDoService service,[FromRoute] Guid id) => service.GedById(id)*/)
                .Produces<ToDo>()
                .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/todo", ToDoRequests.Create/*(IToDoService service, [FromBody]ToDo toDo) => service.Create(toDo)*/)
                .Produces<ToDo>(StatusCodes.Status201Created)
                .Accepts<ToDo>("application/json");

            app.MapPut("/todo", ToDoRequests.Update /*(IToDoService service, Guid id, ToDo toDo) => service.Update(toDo)*/)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Accepts<ToDo>("application/json");

            app.MapDelete("/todo/{id}", ToDoRequests.Delete/*(IToDoService service, Guid id) => service.Delete(id)*/)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                /*.ExcludeFromDescription()*/;

            return app;
        }
        public static IResult GetAll(IToDoService service)
        {
            var todos = service.GetAll();
            return Results.Ok(todos);
        }

        public static IResult GetById(IToDoService service, Guid id)
        {
            var todo = service.GedById(id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(todo);
        }

        public static IResult Create(IToDoService service, ToDo toDo)
        {
            service.Create(toDo);
            return Results.Created($"/todo/{toDo.Id}", toDo);
        }

        public static IResult Update(IToDoService service, Guid id, ToDo toDo)
        {
            var todo = service.GedById(id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            service.Update(todo);
            return Results.Ok();
        }

        public static IResult Delete(IToDoService service, Guid id)
        {
            var todo = service.GedById(id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            service.Delete(id);
            return Results.Ok();
        }

    }
}
