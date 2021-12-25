namespace Helpers
{
    public class TeleportList
    {
        public string Name { get; set; }
        public int GetX { get; set; }
        public int GetY { get; set; }
        public int GetZ { get; set; }
        public int Price { get; set; }
        public int Type { get; set; }
        
        public TeleportList()
        {
            
        }

        public TeleportList(string name, int x, int y, int z, int price, int type)
        {
            Name = name;
            GetX = x;
            GetY = y;
            GetZ = z;
            Price = price;
            Type = type;
        }
    }
}
