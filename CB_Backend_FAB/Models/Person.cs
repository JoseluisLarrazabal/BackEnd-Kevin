namespace CB_Backend_FAB.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Ci { get; set; }
        public DateTime Birthday { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public short UserID { get; set; }

        public Person() {}

        public Person(int personID, string? name, string? lastname, string? ci, DateTime birthday, short userID)
        {
            PersonID = personID;
            Name = name;
            Lastname = lastname;
            Ci = ci;
            Birthday = birthday;
            UserID = userID;
        }

        public Person(string? name, string? lastname, string? ci, DateTime birthday, short userID)
        {
            Name = name;
            Lastname = lastname;
            Ci = ci;
            Birthday = birthday;
            UserID = userID;
        }
    }    
}
