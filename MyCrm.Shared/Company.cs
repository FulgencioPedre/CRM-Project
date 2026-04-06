namespace MyCrm.Shared
{
    public class Company
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Project> Projects { get; set; } = new List<Project>();

        public bool IsActive { get; set; } = true;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CIF { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
