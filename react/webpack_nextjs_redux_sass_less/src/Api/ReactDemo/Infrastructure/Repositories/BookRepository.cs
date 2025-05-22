using System.Xml.Linq;
using ReactDemo.Domain.Repositories;
using ReactDemo.Models;

namespace ReactDemo.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Category> _categories = [
            new(1, "Science Fiction"),
            new(2, "History"),
            new(3, "Programming")
        ];

        private readonly List<Book> _books = [
            new(1, "Book 1", "Author 1", 1, 1993),
            new(2, "Book 2", "Author 2", 2, 1993),
            new(3, "Book 3", "Author 3", 2, 2006),
            new(4, "Book 4", "Author 4", 1, 1992),
            new(5, "Book 5", "Author 5", 3, 1998),
            new(6, "Book 6", "Author 6", 3, 2011),
            new(7, "Book 7", "Author 7", 2, 2017),
            new(8, "Book 8", "Author 8", 3, 2010),
            new(9, "Book 9", "Author 9", 1, 2022),
            new(10, "Book 10", "Author 10", 1, 1999)
        ];

        public IEnumerable<Book> GetAllBooks() => _books;
        public Book? GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);
        public void AddBook(Book book) => _books.Add(book);
        public bool UpdateBook(int id, Book book)
        {
            var index = _books.FindIndex(b => b.Id == id);
            if (index == -1) return false;
            _books[index] = book with { Id = id };
            return true;
        }
        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null) return false;
            _books.Remove(book);
            return true;
        }

        public IEnumerable<Category> GetAllCategories() => _categories;
        public Category? GetCategoryById(int id) => _categories.FirstOrDefault(c => c.Id == id);
    }
}
