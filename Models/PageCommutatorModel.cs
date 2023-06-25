namespace CommutatorAccounting.Models
{
    public class PageCommutatorModel
    {
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PageCommutatorModel(int contentSize, int currentPage, int pageSize) 
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(contentSize / (double) pageSize);
        }
    }
}
