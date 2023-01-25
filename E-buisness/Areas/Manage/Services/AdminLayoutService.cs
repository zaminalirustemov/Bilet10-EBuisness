﻿using E_buisness.Models;
using Microsoft.AspNetCore.Identity;

namespace E_buisness.Areas.Manage.Services
{
    public class AdminLayoutService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminLayoutService(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AppUser> GetUser()
        {
            string name = _httpContextAccessor.HttpContext.User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(name);
            return appUser;
        }
    }
}
