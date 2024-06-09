namespace BookStore.Models;

public class VMAuthors
{
    public Author Author { get; set; } = new Author();
    public List<Author> Authors { get; set; }
    public List<Book> Books { get; set; }
}