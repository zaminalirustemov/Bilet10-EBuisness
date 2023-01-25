
using E_buisness.Context;
using E_buisness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_buisness.Controllers;
public class HomeController : Controller
{
    private readonly EBuisnessDbContext _eBuisnessDbContext;

    public HomeController(EBuisnessDbContext eBuisnessDbContext)
    {
        _eBuisnessDbContext = eBuisnessDbContext;
    }
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel
        {
            SpecialTeams=_eBuisnessDbContext.SpecialTeams.Include(x=>x.Position).Where(x=>x.IsDeleted==false).ToList()
        };
        return View(homeViewModel);
    }

}
