using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class AuthorController : Controller
{
    private readonly AppDb _db;
    public AuthorController(AppDb db)
    {
        _db = db;
    }
    
    [HttpGet]
    public IActionResult AuthorList()
    {
        var authors = _db.Authors.Include(x => x.Companies).ToList();
        var books = _db.Books.Include(x => x.Author).ToList();

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

        _db.Authors.Add(author);
        _db.SaveChanges();

        return Redirect($"/Author/AuthorList/");
    }
    
    [HttpPost]
    public IActionResult DeleteAuthor(int Id)
    {
        var author = _db.Authors.FirstOrDefault(x => x.Id == Id);

        _db.Authors.Remove(author);
        _db.SaveChanges();

        return Redirect($"/Author/AuthorList/");
    }
}
