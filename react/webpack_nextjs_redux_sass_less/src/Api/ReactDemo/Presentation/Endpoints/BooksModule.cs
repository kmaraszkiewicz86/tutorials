using Carter;
using ReactDemo.Domain.Repositories;
using ReactDemo.Models;

namespace ReactDemo.Presentation.Endpoints
{
    public class BooksModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books", (IBookRepository repo) => repo.GetAllBooks());

            app.MapGet("/books/{id:int}", (int id, IBookRepository repository) =>
            {
                var book = repository.GetBookById(id);
                return book is not null ? Results.Ok(book) : Results.NotFound();
            });

            app.MapPost("/books", async (HttpRequest req, IBookRepository repository) =>
            {
                var book = await req.ReadFromJsonAsync<Book>();
                if (book is null) return Results.BadRequest();
                repository.AddBook(book);
                return Results.Created($"/books/{book.Id}", book);
            });

            app.MapPut("/books/{id:int}", async (int id, HttpRequest req, IBookRepository repository) =>
            {
                var book = await req.ReadFromJsonAsync<Book>();
                if (book is null) return Results.BadRequest();
                return repository.UpdateBook(id, book) ? Results.Ok(book) : Results.NotFound();
            });

            app.MapDelete("/books/{id:int}", (int id, IBookRepository repository) =>
                repository.DeleteBook(id) ? Results.NoContent() : Results.NotFound()
            );
        }
    }

}
