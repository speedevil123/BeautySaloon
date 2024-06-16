using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string? Specialization { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<Reception> ClientsReceptions { get; set; } = new List<Reception>();
        public ICollection<Reception> DoctorsReceptions { get; set; } = new List<Reception>();
    }
}
