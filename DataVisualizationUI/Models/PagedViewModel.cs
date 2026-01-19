namespace DataVisualizationUI.Models
{
    public record PagedViewModel<T>
    {
        required public List<T> Items { get; init; }
        required public int PageNumber { get; init; }
        required public int PageSize { get; init; }
        required public int TotalItems { get; init; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
