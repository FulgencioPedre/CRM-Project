using System.ComponentModel.DataAnnotations;

namespace MyCrm.Shared
{
    public class Project
    {

        public bool IsActive { get; set; } = true;
        public enum ProjectStatus {Negotiation, Accepted, InProcess, Finished};

        public int Id { get; set; }

        [Required(ErrorMessage = "The name is empty")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The name is empty")]
        [StringLength(300, ErrorMessage = "The description is too long (300ch max)")]
        public string Description { get; set; } = string.Empty;

        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public ProjectStatus Status { get; set; }



        public int CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
