namespace QuranHub.Web.Models
{
    public class Response<T>
    {
        public ResponseStatusCodeEnum Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
