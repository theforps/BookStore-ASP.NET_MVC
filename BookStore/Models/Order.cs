using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public Book Book { get; set; }
}