using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Author> Authors { get; set; }
}