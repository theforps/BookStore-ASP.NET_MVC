using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PageCount { get; set; }
        public int Price { get; set; }
        public Author Author  { get; set; }
    }
}
