namespace Application.Utils
{
    public record QueryBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
