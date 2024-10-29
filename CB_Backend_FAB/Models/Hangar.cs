namespace CB_Backend_FAB.Models
{
    public class Hangar
    {
        public int HangarID { get; set; }
        public string CampusName { get; set; } = string.Empty;
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
