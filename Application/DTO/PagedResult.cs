namespace Application.DTO
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public List<T> Data { get; set; }

        public PagedResult()
        {
            Data = new List<T>();
        }
    }
}
