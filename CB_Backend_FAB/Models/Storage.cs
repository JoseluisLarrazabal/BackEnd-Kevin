namespace CB_Backend_FAB.Models
{
    public class Storage
    {
        public int StorageID { get; set; }
        public string? StorageName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Hangar? Hangar { get; set; }
        public Department? Department { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public Storage()
        {
        }

        public Storage(int storageID)
        {
            StorageID = storageID;
        }

        public Storage(int storageID, string? storageName, double latitude, double longitude, Hangar? hangar, Department? department) : this(storageID)
        {
            StorageName = storageName;
            Latitude = latitude;
            Longitude = longitude;
            Hangar = hangar;
            Department = department;
        }
    }
}
