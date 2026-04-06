namespace MyCrm.Shared
{
    public class Project
    {

        public bool IsActive { get; set; } = true;
        public enum ProjectStatus {Negotiation, Accepted, InProcess, Finished};

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public ProjectStatus Status { get; set; }



        public int CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
