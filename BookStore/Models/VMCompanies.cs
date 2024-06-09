namespace BookStore.Models;

public class VMCompanies
{
    public List<Company> Companies { get; set; }
    public Author Author { get; set; } = new Author();
    public Company Company { get; set; } = new Company();
}