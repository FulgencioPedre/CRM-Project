using System.ComponentModel.DataAnnotations;

namespace MyCrm.Shared
{
    public class Company
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Project> Projects { get; set; } = new List<Project>();

        public bool IsActive { get; set; } = true;

        public int Id { get; set; }

        [Required(ErrorMessage = "The name is empty")]
        [StringLength(100, ErrorMessage ="Name is to long (100 ch max.)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The CIF is empty")]
        public string CIF { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Country is empty")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Address is empty")]
        public string Address { get; set; } = string.Empty;

    }
}
