using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;

namespace BookStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDb _db;

    public HomeController(ILogger<HomeController> logger, AppDb db)
    {
        _logger = logger;
        _db = db;
    }



    public IActionResult Index()
    {
        


        return View(_db.Books.FirstOrDefault());
    }
}