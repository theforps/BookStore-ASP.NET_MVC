using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class OrderController : Controller
{
    private readonly AppDb _db;
    public OrderController(AppDb db)
    {
        _db = db;
    }
    // GET
    public IActionResult OrderList()
    {
        var orders = _db.Orders.Include(x => x.Book).ToList();
        
        
        return View(orders);
    }
    
    [HttpGet]
    public IActionResult DeleteOrder(int Id)
    {
        var orders = _db.Orders.FirstOrDefault(x => x.Id == Id);

        _db.Orders.Remove(orders);
        _db.SaveChanges();
        
        
        return RedirectToAction("OrderList");
    }
}