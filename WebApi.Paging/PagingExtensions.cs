using WebApi.Paging.Pageable;
using System.Web.Http;

namespace WebApi.Paging
{
    public static class PagingExtensions
    {
        public static HttpConfiguration UsePaging(this HttpConfiguration configuration)
        {
            configuration.BindParameter(typeof(IPageable), new PageableBinder());
            return configuration;
        }
    }
}
