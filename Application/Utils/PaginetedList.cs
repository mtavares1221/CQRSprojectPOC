namespace Application.Utils
{
    public record PaginetedList<T>
    {
        public int TotalItens { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StatIndex { get; private set; }
        public int EndIndex { get; private set; }
        public List<T> Itens { get; private set; }

        public PaginetedList(List<T> itens, int page, int pageSize)
        {
            TotalItens = itens.Count();
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalItens / (double)pageSize);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage;
            CurrentPage = CurrentPage > TotalPages ? TotalPages : CurrentPage;
            StatIndex = (CurrentPage - 1) * PageSize;
            EndIndex = Math.Min(StatIndex + PageSize - 1, TotalItens - 1);
            Itens = itens.Skip(StatIndex).Take(PageSize).ToList();
        }
    }
}
