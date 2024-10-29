namespace CB_Backend_FAB.Models
{
    public class Institution
    {
        public int InstitutionID { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public byte Status { get; set; } 
        public DateTime RegisterDate { get; set; } 
        public DateTime LastUpdate { get; set; } 
        public short UserID { get; set; }

        public Institution()
        {
        }

        public Institution(string? name, string? contactNumber, short userID)
        {
            Name = name;
            ContactNumber = contactNumber;
            UserID = userID;
        }

        public Institution(int institutionID, string? name, string? contactNumber, short userID)
        {
            InstitutionID = institutionID;
            Name = name;
            ContactNumber = contactNumber;
            UserID = userID;
        }
    }
}
