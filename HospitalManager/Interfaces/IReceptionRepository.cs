using HospitalManager.Models;

namespace HospitalManager.Interfaces
{
    public interface IReceptionRepository
    {
        Task<IEnumerable<Reception>> GetReceptionsByDayMonthYear(DateTime date);
    }
}
