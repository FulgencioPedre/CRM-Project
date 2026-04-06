namespace MyCrm.Shared
{
    public class Project
    {
        public enum States {Negotiation, Accepted, InProcess, Finished};

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; }
        public States Status { get; set; }


        public int CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
