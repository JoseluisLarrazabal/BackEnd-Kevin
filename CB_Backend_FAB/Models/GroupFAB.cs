namespace CB_Backend_FAB.Models
{
    public class GroupFAB
    {
        public int GroupID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public GroupFAB()
        {
                
        }

        public GroupFAB(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public GroupFAB(int groupID, string name, string description)
        {
            GroupID = groupID;
            Name = name;
            Description = description;
        }
    }



}
