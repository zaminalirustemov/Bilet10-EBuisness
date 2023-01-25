using E_buisness.Context;
using E_buisness.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_buisness.Services
{
    public class LayoutService
    {
        private readonly EBuisnessDbContext _eBuisnessDbContext;

        public LayoutService(EBuisnessDbContext eBuisnessDbContext)
        {
            _eBuisnessDbContext = eBuisnessDbContext;
        }
        public async Task<List<Settings>> GetSettings()
        {
            return await _eBuisnessDbContext.Settings.ToListAsync();
        }
    }
}
