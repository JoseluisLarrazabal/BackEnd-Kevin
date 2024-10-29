using System.ComponentModel.DataAnnotations;

namespace CB_Backend_FAB.Models
{
    public class RoleUser
    {
        public int RoleID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public RoleUser()
        {
                
        }

        public RoleUser(string name, string description)
        {
            Name = name;
            Description = description; 
        }

        public RoleUser(int roleID, string name, string description)
        {
            RoleID = roleID;
            Name = name;
            Description = description;
        }
    }
}
