using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class AuthorController : Controller
{
    private readonly AppDbContext _dbContext;
    public AuthorController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult AuthorList()
    {
        var authors = _dbContext.Authors.Include(x => x.Companies).ToList();
        var books = _dbContext.Books.Include(x => x.Author).ToList();

        VMAuthors vm = new VMAuthors()
        {
            Authors = authors,
            Books = books
        };
        
        return View(vm);
    }

    [HttpPost]
    public IActionResult AddAuthor(VMAuthors authors)
    {
        Author author = new Author()
        {
            Name = authors.Author.Name
        };

        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();

        return Redirect($"/Author/AuthorList/");
    }
    
    [HttpPost]
    public IActionResult DeleteAuthor(int Id)
    {
        var author = _dbContext.Authors.FirstOrDefault(x => x.Id == Id);

        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();

        return Redirect($"/Author/AuthorList/");
    }
}
