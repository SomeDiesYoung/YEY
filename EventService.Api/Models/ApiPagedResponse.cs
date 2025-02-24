namespace EventService.Api.Models
{
    public sealed class ApiPagedResponse<TData>
    {
        public required List<TData> Data { get; set; }
        public required Meta Meta { get; set; }
        public required Links Links { get; set; }
    }
    public class Links
    {
        public string? Self { get; set; }
        public string? Next { get; set; }
        public string? Prev { get; set; }
    }

    public sealed class Meta
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
