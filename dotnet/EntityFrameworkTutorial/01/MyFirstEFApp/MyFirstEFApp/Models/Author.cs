using System.Collections.Generic;

namespace MyFirstEFApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public string WebUrl { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}