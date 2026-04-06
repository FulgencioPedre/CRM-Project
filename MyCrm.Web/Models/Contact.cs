namespace MyCrm.Web.Models
{
    public class Contact
    {
        public Company company { get; set; };

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;


        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
