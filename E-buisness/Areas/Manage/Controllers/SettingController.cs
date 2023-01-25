using E_buisness.Context;
using E_buisness.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace E_buisness.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class SettingController : Controller
    {
        private readonly EBuisnessDbContext _eBuisnessDbContext;

        public SettingController(EBuisnessDbContext eBuisnessDbContext)
        {
            _eBuisnessDbContext = eBuisnessDbContext;
        }
        public IActionResult Index()
        {
            List<Settings> settings = _eBuisnessDbContext.Settings.ToList();
            return View(settings);
        }
        public IActionResult Update(int id)
        {
            Settings settings = _eBuisnessDbContext.Settings.FirstOrDefault(x=>x.Id==id);
            if (settings == null) return NotFound();
            return View(settings);
        }
        [HttpPost]
        public IActionResult Update(Settings newSettings)
        {
            Settings existSettings = _eBuisnessDbContext.Settings.FirstOrDefault(x => x.Id == newSettings.Id);
            if (existSettings == null) return NotFound();

            existSettings.Value = newSettings.Value;
            _eBuisnessDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
