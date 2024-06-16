using HospitalManager.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalManager.ViewModels
{
    public class CreateReceptionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Дата обязательна!")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Стоимость обязательна!")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Вид процедуры обязателен!")]
        public string? Procedure { get; set; }
        [Required(ErrorMessage = "Выберите доктора!")]
        public string? DoctorId { get; set; }
        public string? ClientId { get; set; }
        public AppUser? Doctor { get; set; }
        public AppUser? Client { get; set; }
    }

}
