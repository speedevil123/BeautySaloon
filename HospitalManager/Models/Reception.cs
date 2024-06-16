using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManager.Models
{
    public class Reception
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Procedure { get; set; }
        public string DoctorId { get; set; }
        public string? ClientId { get; set; }
        public AppUser Doctor { get; set; }
        public AppUser? Client { get; set; }

    }
}
