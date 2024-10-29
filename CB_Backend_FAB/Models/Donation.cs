namespace CB_Backend_FAB.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public Institution? Institution { get; set; }
        public Storage? Storage { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserID { get; set; }

        public List<DonationDetail>? Details { get; set; }

        public Donation() {}

        public Donation(int donationID, Institution institution, Storage storage)
        {
            DonationID = donationID;
            Institution = institution;
            Storage = storage;
        }
    }
}
