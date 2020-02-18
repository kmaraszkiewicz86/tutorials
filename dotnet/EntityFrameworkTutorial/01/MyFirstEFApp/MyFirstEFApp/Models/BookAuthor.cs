using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstEFApp.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        
        public int AuthorId { get; set; }
        
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public int BookId { get; set; }
        
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}