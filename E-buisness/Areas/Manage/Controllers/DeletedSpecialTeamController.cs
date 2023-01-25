using E_buisness.Context;
using E_buisness.Helpers;
using E_buisness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_buisness.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class DeletedSpecialTeamController : Controller
{
    private readonly EBuisnessDbContext _eBuisnessDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DeletedSpecialTeamController(EBuisnessDbContext eBuisnessDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _eBuisnessDbContext = eBuisnessDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //READ--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Index(int page=1)
    {
        var query = _eBuisnessDbContext.SpecialTeams.Include(x=>x.Position). Where(x => x.IsDeleted == true).AsQueryable();

        var paginatedList = new PaginatedList<SpecialTeam>(query.Skip((page - 1) * 2).Take(2).ToList(), query.Count(), 2, page);

        return View(paginatedList);
    }
    //Restore--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        SpecialTeam specialTeam = _eBuisnessDbContext.SpecialTeams.FirstOrDefault(x => x.Id == id);
        if (specialTeam == null) return NotFound();

        specialTeam.IsDeleted = false;
        _eBuisnessDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //HardDELETE--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        SpecialTeam specialTeam = _eBuisnessDbContext.SpecialTeams.FirstOrDefault(x => x.Id == id);
        if (specialTeam == null) return NotFound();

        FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team", specialTeam.ImageName);
        _eBuisnessDbContext.SpecialTeams.Remove(specialTeam);
        _eBuisnessDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}

