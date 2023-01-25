using E_buisness.Context;
using E_buisness.Helpers;
using E_buisness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_buisness.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class SpecialTeamController : Controller
{
    private readonly EBuisnessDbContext _eBuisnessDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SpecialTeamController(EBuisnessDbContext eBuisnessDbContext,IWebHostEnvironment webHostEnvironment)
    {
        _eBuisnessDbContext = eBuisnessDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //READ--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Index(int page=1)
    {
        var query = _eBuisnessDbContext.SpecialTeams.Include(x=>x.Position).Where(x=>x.IsDeleted==false).AsQueryable();

        var paginatedlist = new PaginatedList<SpecialTeam>(query.Skip((page-1)*2).Take(2).ToList(),query.Count(),2,page);

        return View(paginatedlist);
    }
    //CREATE--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Create()
    {
        ViewBag.Positions = _eBuisnessDbContext.Positions.Where(x=>x.IsDeleted==false).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(SpecialTeam teamMember)
    {
        ViewBag.Positions = _eBuisnessDbContext.Positions.Where(x => x.IsDeleted == false).ToList();
        if (teamMember.ImageFile is null)
        {
            ModelState.AddModelError("ImageFile", "The Image field is required..");
            return View(teamMember);
        }
        if (teamMember.ImageFile.ContentType!="image/jpeg" && teamMember.ImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("ImageFile", "You can only upload files in jpeg and png format.");
            return View(teamMember);
        }
        if (teamMember.ImageFile.Length> 2097152)
        {
            ModelState.AddModelError("ImageFile", "You can only upload files in than 2 MB size.");
            return View(teamMember);
        }
        if (!ModelState.IsValid) return View(teamMember);

        teamMember.ImageName = FileManager.SaveFile(teamMember.ImageFile, _webHostEnvironment.WebRootPath, "uploads/team");

        _eBuisnessDbContext.SpecialTeams.Add(teamMember);
        _eBuisnessDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //UPDATE--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        ViewBag.Positions = _eBuisnessDbContext.Positions.Where(x => x.IsDeleted == false).ToList();
        SpecialTeam specialTeam = _eBuisnessDbContext.SpecialTeams.FirstOrDefault(x => x.Id == id);
        if (specialTeam == null) return NotFound();

        return View(specialTeam);
    }
    [HttpPost]
    public IActionResult Update(SpecialTeam newSpecialTeam)
    {
        ViewBag.Positions = _eBuisnessDbContext.Positions.Where(x => x.IsDeleted == false).ToList();
        SpecialTeam existSpecialTeam = _eBuisnessDbContext.SpecialTeams.FirstOrDefault(x => x.Id == newSpecialTeam.Id);
        if (existSpecialTeam == null) return NotFound();
        if (newSpecialTeam.ImageFile!=null)
        {
            if (newSpecialTeam.ImageFile.ContentType != "image/jpeg" && newSpecialTeam.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "You can only upload files in jpeg and png format.");
                return View(newSpecialTeam);
            }
            if (newSpecialTeam.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You can only upload files in than 2 MB size.");
                return View(newSpecialTeam);
            }
            FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team",existSpecialTeam.ImageName);
            existSpecialTeam.ImageName = FileManager.SaveFile(newSpecialTeam.ImageFile, _webHostEnvironment.WebRootPath, "uploads/team");
        }

        if (!ModelState.IsValid) return View(newSpecialTeam);

        existSpecialTeam.PositionId = newSpecialTeam.PositionId;
        existSpecialTeam.Fullname = newSpecialTeam.Fullname;
        existSpecialTeam.TwUrl = newSpecialTeam.TwUrl;
        existSpecialTeam.FbUrl = newSpecialTeam.FbUrl;
        existSpecialTeam.InstUrl = newSpecialTeam.InstUrl;

        _eBuisnessDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //SoftDelete--------------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        SpecialTeam specialTeam = _eBuisnessDbContext.SpecialTeams.FirstOrDefault(x => x.Id == id);
        if (specialTeam == null) return NotFound();

        specialTeam.IsDeleted = true;
        _eBuisnessDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}

