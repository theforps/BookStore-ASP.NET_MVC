using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class OrderController : Controller
{
    private readonly AppDbContext _dbContext;
    public OrderController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // GET
    public IActionResult OrderList()
    {
        var orders = _dbContext.Orders.Include(x => x.Book).ToList();
        
        
        return View(orders);
    }
    
    [HttpGet]
    public IActionResult DeleteOrder(int Id)
    {
        var orders = _dbContext.Orders.FirstOrDefault(x => x.Id == Id);

        _dbContext.Orders.Remove(orders);
        _dbContext.SaveChanges();
        
        
        return RedirectToAction("OrderList");
    }
}