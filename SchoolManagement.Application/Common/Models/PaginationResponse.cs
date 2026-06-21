namespace SchoolManagement.Application.Common.Models
{
    public class PaginationResponse<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = [];
    }
}
