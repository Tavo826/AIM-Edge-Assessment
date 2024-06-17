namespace Infrastructure.Persistence.Mappers.Entities
{
    public class InvoiceEntity
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public float Subtotal { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
    }
}
