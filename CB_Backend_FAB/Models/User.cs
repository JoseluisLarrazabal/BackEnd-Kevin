namespace CB_Backend_FAB.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public RoleUser Role { get; set; }
        public GroupFAB Group { get; set; }

        public User()
        {
            
        }

        public User(string email, string password, RoleUser roleUser, GroupFAB group)
        {
            Email = email;
            Password = password;
            Role = roleUser;
            Group = group;
        }

        public User(int userID, string email, string password, RoleUser roleUser, GroupFAB group)
        {
            UserID = userID;
            Email = email;
            Password = password;
            Role = roleUser;
            Group = group;
        }
    }

}
