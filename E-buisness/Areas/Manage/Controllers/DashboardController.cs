using E_buisness.Context;
using E_buisness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_buisness.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles ="SuperAdmin,Admin,Editor")]
public class DashboardController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly EBuisnessDbContext _eBuisnessDbContext;

    public DashboardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,EBuisnessDbContext eBuisnessDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _eBuisnessDbContext = eBuisnessDbContext;
    }
    public IActionResult Index()
    {
        return View();
    }

    ////CreateAdmin----------------------------------------------------------------------
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    AppUser appUser = new AppUser
    //    {
    //        Fullname="Zaminali Rustemov",
    //        UserName="Zamin",
    //        Email="zamin@bk.ru",
    //    };

    //    var result= await _userManager.CreateAsync(appUser, "Zamin123");
    //    return Ok(result);
    //}


    //////CreateRole----------------------------------------------------------------------
    //public async Task<IActionResult> CreateRole()
    //{
    //    IdentityRole role1 = new IdentityRole("SuperAdmin");
    //    IdentityRole role2 = new IdentityRole("Admin");
    //    IdentityRole role3 = new IdentityRole("Editor");
    //    IdentityRole role4 = new IdentityRole("Member");

    //    await _roleManager.CreateAsync(role1);
    //    await _roleManager.CreateAsync(role2);
    //    await _roleManager.CreateAsync(role3);
    //    await _roleManager.CreateAsync(role4);

    //    return Content(">>>Created Roles");
    //}
    //////CreateRole----------------------------------------------------------------------
    //public async Task<IActionResult> AddRole()
    //{
    //    AppUser appUser =await _userManager.FindByNameAsync("Zamin");
    //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
    //    return Content(">>>Added Role");
    //}
}

