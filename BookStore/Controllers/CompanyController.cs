using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class CompanyController : Controller
{
    private readonly AppDb _db;
    public CompanyController(AppDb db)
    {
        _db = db;
    }
    
    
    // GET
    public IActionResult CompanyList()
    {
        var companies = _db.Companies
            .Include(x => x.Authors)
            .AsNoTracking()
            .ToList();

        VMCompanies vm = new VMCompanies()
        {
            Companies = companies
        };
        
        
        
        return View(vm);
    }
    
    [HttpPost]
    public IActionResult AddCompany(VMCompanies companies)
    {
        _db.Companies.Add(companies.Company);
        _db.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult AddAuthorToCompany(VMCompanies companies)
    {
        var author = _db.Authors.Include(x=>x.Companies).FirstOrDefault(x => x.Id == companies.Author.Id);
        var company = _db.Companies.FirstOrDefault(x => x.Id == companies.Company.Id);
        
        author.Companies.Add(company);
        
        //company.Authors.U(author);
        
        _db.Authors.Update(author);
        _db.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult DeleteAuthorFromCompany(VMCompanies companies)
    {
        var author = _db.Authors
            .FirstOrDefault(x => x.Id == companies.Author.Id);
        var company = _db.Companies
            .Include(x=> x.Authors)
            .FirstOrDefault(x => x.Id == companies.Company.Id);
        
        company.Authors.Remove(author);
        
        _db.Companies.Update(company);
        _db.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult DeleteCompany(int Id)
    {
        var companies = _db.Companies
            .FirstOrDefault(x => x.Id == Id);

        _db.Companies.Remove(companies);
        _db.SaveChanges();
        
        return RedirectToAction("CompanyList");
    }
}