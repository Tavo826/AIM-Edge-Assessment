namespace Domain.DTOs.Response
{
    public class InvoiceResponse<T> : GenericResponse
    {
        public IEnumerable<T> Details { get; set; }
    }
}
