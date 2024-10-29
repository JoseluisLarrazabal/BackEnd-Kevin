namespace CB_Backend_FAB.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string? Name { get; set; } 
        public Category? Category{ get; set; }

        public Item()
        {
        }

        public Item(int itemID)
        {
            ItemID = itemID;
        }
    }
}
