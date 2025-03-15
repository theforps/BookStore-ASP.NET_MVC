using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;

public class CompanyController : Controller
{
    private readonly AppDbContext _dbContext;
    public CompanyController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    // GET
    public IActionResult CompanyList()
    {
        var companies = _dbContext.Companies
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
        _dbContext.Companies.Add(companies.Company);
        _dbContext.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult AddAuthorToCompany(VMCompanies companies)
    {
        var author = _dbContext.Authors.Include(x=>x.Companies).FirstOrDefault(x => x.Id == companies.Author.Id);
        var company = _dbContext.Companies.FirstOrDefault(x => x.Id == companies.Company.Id);
        
        author.Companies.Add(company);
        
        //company.Authors.U(author);
        
        _dbContext.Authors.Update(author);
        _dbContext.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult DeleteAuthorFromCompany(VMCompanies companies)
    {
        var author = _dbContext.Authors
            .FirstOrDefault(x => x.Id == companies.Author.Id);
        var company = _dbContext.Companies
            .Include(x=> x.Authors)
            .FirstOrDefault(x => x.Id == companies.Company.Id);
        
        company.Authors.Remove(author);
        
        _dbContext.Companies.Update(company);
        _dbContext.SaveChanges();
        
        
        return RedirectToAction("CompanyList");
    }
    
    [HttpPost]
    public IActionResult DeleteCompany(int Id)
    {
        var companies = _dbContext.Companies
            .FirstOrDefault(x => x.Id == Id);

        _dbContext.Companies.Remove(companies);
        _dbContext.SaveChanges();
        
        return RedirectToAction("CompanyList");
    }
}