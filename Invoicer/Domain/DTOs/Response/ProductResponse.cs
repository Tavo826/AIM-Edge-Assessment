namespace Domain.DTOs.Response
{
    public class ProductResponse : GenericResponse
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
