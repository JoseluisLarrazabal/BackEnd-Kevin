namespace CB_Backend_FAB.Models
{
    public class DonationDetail
    {
        public Donation? Donation { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
        public byte Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserID { get; set; }

        public DonationDetail() { }

        public DonationDetail(Donation? donation, Item? item, int quantity, int userID)
        {
            Donation = donation;
            Item = item;
            Quantity = quantity;
            UserID = userID;
        }
    }
}
