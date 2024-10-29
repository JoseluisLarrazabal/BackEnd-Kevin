namespace CB_Backend_FAB.Models
{
    public class MilitaryPerson
    {
        public Person? Person { get; set; }
        public string? Area { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public short UserID { get; set; }

        public MilitaryPerson() {}

        public MilitaryPerson(Person? person, string? area, short userID)
        {
            Person = person;
            Area = area;
            UserID = userID;
        }
    }
}
