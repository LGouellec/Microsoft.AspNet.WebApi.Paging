using WebApi.Paging.Pageable;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace WebApi.Paging
{
    public static class PaginationUtil
    {
        public static IHeaderDictionary GeneratePaginationHttpHeaders<T>(IPage<T> page, HttpRequest request)
            where T : class
        {
            var scheme = request.Url.Scheme;
            var host = request.Url.Host;
            var path = request.Path;
            if (string.IsNullOrEmpty(path)) path = "/";
            var baseUrl = $"{scheme}://{host}:{request.Url.Port}{path}";
            return GeneratePaginationHttpHeaders(page, request, baseUrl);
        }

        public static IHeaderDictionary GeneratePaginationHttpHeaders<T>(IPage<T> page, HttpRequest request, string baseUrl) where T : class
        {
            IHeaderDictionary headers = new HeaderDictionary(new Dictionary<string, string[]>());
            headers.Add("X-Paging-PageCount", new string[] { page.NumberOfElements.ToString() });
            headers.Add("X-Paging-PageSize", new string[] { page.Size.ToString() });
            headers.Add("X-Paging-PageNo", new string[] { page.Number.ToString() });
            headers.Add("X-Paging-TotalCount", new string[] { page.TotalElements.ToString() });

            if (page.Number > 0)
                headers.Add("X-Paging-Previous", new string[] { GenerateUri(baseUrl, page.Number - 1, page.Size) });

            if (page.Number + 1 < page.TotalPages)
                headers.Add("X-Paging-Next", new string[] { GenerateUri(baseUrl, page.Number + 1, page.Size) });

            return headers;
        }

        private static string GenerateUri(string baseUrl, int page, int size)
        {
            return new UriBuilder(baseUrl)
            {
                Query = $"page={page}&size={size}"
            }.Uri.ToString();
        }
    }
}
