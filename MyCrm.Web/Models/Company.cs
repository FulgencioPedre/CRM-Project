namespace MyCrm.Web.Models
{
    public class Company
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Proyect> Proyects { get; set; } = new List<Contact>();

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CIF { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
