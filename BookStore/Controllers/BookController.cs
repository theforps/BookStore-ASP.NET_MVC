using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class BookController : Controller
{
    private readonly AppDbContext _dbContext;
    public BookController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    [HttpGet]
    public async Task< IActionResult> BookList()
    {
        var books = await _dbContext.Books.ToListAsync();
        
        
        return View(books);
    }

    [HttpGet]
    public async Task<IActionResult> BookInfo(int Id)
    {
        var book = await _dbContext.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == Id);
        
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
        
        return Redirect($"/Book/BookList/");
    }
    
    [HttpGet]
    public async Task<IActionResult> BookUpsert(int? Id)
    {
        var book = await _dbContext.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == Id);

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
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == book.Author.Id);

            book.Author = author;

            _dbContext.Books.Add(book);
        }
        else
        {
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == book.Author.Id);

            book.Author = author;
            
            _dbContext.Books.Update(book);
        }
        
        await _dbContext.SaveChangesAsync();
        return RedirectToAction($"BookList");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddOrder(int id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

        Order order = new Order()
        {
            Book = book
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        
       
        return RedirectToAction("BookList");
    }
}