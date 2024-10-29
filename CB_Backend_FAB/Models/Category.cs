namespace CB_Backend_FAB.Models
{
    public class Category
    {
        public int CategoryID { get; set; } 
        public string? Name { get; set; }
        public byte Status { get; set; } 
        public DateTime RegistrationDate { get; set; } 
        public DateTime LastUpdate { get; set; } 
    }
}
