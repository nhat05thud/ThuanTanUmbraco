namespace ThuanTan.Entity.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Quantity { get; set; }
        public string Note { get; set; }
        public string CreateDate { get; set; }
    }
}
