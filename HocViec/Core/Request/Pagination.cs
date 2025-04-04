namespace Core.Request
{
    public class PaginationRequest<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }

}
