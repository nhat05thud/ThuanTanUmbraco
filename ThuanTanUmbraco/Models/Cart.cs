namespace ThuanTanUmbraco.Models
{
    public class Cart
    {
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}