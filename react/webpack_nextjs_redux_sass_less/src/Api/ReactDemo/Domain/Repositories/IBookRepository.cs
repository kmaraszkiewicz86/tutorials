using ReactDemo.Models;

namespace ReactDemo.Domain.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        bool DeleteBook(int id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Category> GetAllCategories();
        Book? GetBookById(int id);
        Category? GetCategoryById(int id);
        bool UpdateBook(int id, Book book);
    }
}
