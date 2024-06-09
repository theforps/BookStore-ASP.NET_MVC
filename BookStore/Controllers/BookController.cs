using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class BookController : Controller
{
    private readonly AppDb _db;
    public BookController(AppDb db)
    {
        _db = db;
    }
    
    
    [HttpGet]
    public async Task< IActionResult> BookList()
    {
        var books = await _db.Books.ToListAsync();
        
        
        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> BookInfo(int Id)
    {
        var book = await _db.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == Id);
        
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        
        return Redirect($"/Book/BookList/");
    }
    
    [HttpGet]
    public async Task<IActionResult> BookUpsert(int? Id)
    {
        var book = await _db.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == Id);

        if (book == null)
        {
            book = new Book()
            {
                Author = new Author()
            };
        }
        return View(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> BookUpsert(Book book)
    {
        if (book.Id == null || book.Id == 0)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == book.Author.Id);

            book.Author = author;

            _db.Books.Add(book);
        }
        else
        {
            var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == book.Author.Id);

            book.Author = author;
            
            _db.Books.Update(book);
        }
        
        await _db.SaveChangesAsync();
        return RedirectToAction($"BookList");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddOrder(int id)
    {
        var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);

        Order order = new Order()
        {
            Book = book
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        
       
        return RedirectToAction("BookList");
    }
}