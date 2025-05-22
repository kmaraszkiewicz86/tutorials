using Carter;
using ReactDemo.Domain.Repositories;

namespace ReactDemo.Presentation.Endpoints
{
    public class CategoriesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories", (IBookRepository repository) => repository.GetAllCategories());

            app.MapGet("/categories/{id:int}", (int id, IBookRepository repository) =>
            {
                var category = repository.GetCategoryById(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });
        }
    }

}
