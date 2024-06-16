using HospitalManager.Data;
using HospitalManager.Interfaces;
using HospitalManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManager.Repository
{
    public class ReceptionRepository : IReceptionRepository
    {
        private readonly ApplicationDbContext _context;
        public ReceptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reception>> GetReceptionsByDayMonthYear(DateTime date)
        {
            return await _context.Receptions.Include(i => i.Doctor).Where(c => c.Date.Day == date.Day && c.Date.Month == date.Month && c.Date.Year == date.Year).ToListAsync();
        }
    }
}
