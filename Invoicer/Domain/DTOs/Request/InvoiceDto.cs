namespace Domain.DTOs.Request
{
    public class InvoiceDto
    {
        public string Client { get; set; }
        public DateOnly Date { get; set; }
        public int Subtotal { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
    }
}
