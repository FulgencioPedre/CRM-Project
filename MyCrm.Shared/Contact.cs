using System.ComponentModel.DataAnnotations;

namespace MyCrm.Shared
{
    public class Contact
    {
        public bool IsActive { get; set; } = true;

        public int Id { get; set; }

        [Required(ErrorMessage = "The name is empty")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The surname is empty")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email is empty")]
        [StringLength(100, ErrorMessage = "The email is to long (100ch max)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Phone is empty")]
        [StringLength(100, ErrorMessage = "The phone is to long (15num max)")]
        public string Phone { get; set; } = string.Empty;


        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
