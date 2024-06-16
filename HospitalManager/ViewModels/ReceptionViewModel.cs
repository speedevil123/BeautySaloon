using HospitalManager.Models;

namespace HospitalManager.ViewModels
{
    public class ReceptionViewModel
    {
        public ICollection<Reception> receptions { get; set; } = new List<Reception>();
        public DateTime? SelectedDate { get; set; }
    }
}
