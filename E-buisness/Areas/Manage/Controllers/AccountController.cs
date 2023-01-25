using E_buisness.Areas.Manage.ViewModels;
using E_buisness.Migrations;
using E_buisness.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_buisness.Areas.Manage.Controllers;
[Area("Manage")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
    {

        if (!ModelState.IsValid)
        {

            ModelState.AddModelError("", "Username or passwprd is invalid");
            return View();
        }
        AppUser appUser =await _userManager.FindByNameAsync(adminLoginVM.UserName);

        if(appUser == null)
        {
            ModelState.AddModelError("", "Username or passwprd is invalid");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(appUser, adminLoginVM.Password, false, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or passwprd is invalid");
            return View();
        }


        return RedirectToAction("index","dashboard");
    }
    //Logout----------------------------------------------------------------------------------------------------
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction("login", "account");
    }
}

