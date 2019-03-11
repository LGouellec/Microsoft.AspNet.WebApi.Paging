using System;
using System.Linq;

namespace WebApi.Paging.Pageable
{
    public static class QueryableExtensions
    {
        public static IPage<TEntity> UsePageable<TEntity>(this IQueryable<TEntity> receiver, IPageable pageable)
            where TEntity : class
        {
            if (pageable is UnPaged)
            {
                return new Page<TEntity>(receiver.ToList(), pageable, receiver.Count());
            }
            else
            {
                var entities = receiver.Skip(pageable.Offset)
                    .Take(Math.Min(pageable.PageSize, PageableBinderConfig.DefaultMaxPageSize));
                return new Page<TEntity>(entities.ToList(), pageable, receiver.Count());
            }
        }
    }
}
