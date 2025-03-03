namespace Application.DTO
{
    public class PagedRequest
    {
        public string? search { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public string? sortOrder { get; set; }
        public string? sortField { get; set; }
    }
}
