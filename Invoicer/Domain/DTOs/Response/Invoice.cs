namespace Domain.DTOs.Response
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public float Subtotal { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
    }
}
