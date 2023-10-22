namespace Medium.BL.Wrappers
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public PaginatedResult(
            bool succeed,
            List<T> data = default,
            List<string>? messages = null,
            int count = 0,
            int page = 1,
            int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeed = succeed;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Messages = messages;
        }

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }

        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public bool Succeed { get; set; }
        public int PageSize { get; set; }
        public object Meta { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<string>? Messages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}