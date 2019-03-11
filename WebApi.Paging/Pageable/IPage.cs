namespace WebApi.Paging.Pageable
{
    public interface IPage<T> : ISlice<T> where T : class
    {
        int TotalPages { get; }
        int TotalElements { get; }
    }
}
