namespace Infrastructure.Persistence.Mappers.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Invoice_id { get; set; }
    }
}
