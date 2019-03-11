using Microsoft.Owin;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Paging
{
    public static class ActionResultExtensions
    {
        public static IHttpActionResult WithHeaders(this IHttpActionResult receiver, IHeaderDictionary headers)
        {
            return new ActionResultWithHeaders(receiver, headers);
        }
    }

    public class ActionResultWithHeaders : IHttpActionResult
    {
        private readonly IHeaderDictionary _headers;
        private readonly IHttpActionResult _result;

        public ActionResultWithHeaders(IHttpActionResult receiver, IHeaderDictionary headers)
        {
            _result = receiver;
            _headers = headers;
        }

        private void AddHeaders(HttpResponse response)
        {
            foreach (var header in _headers)
            {
                NameValueCollection collection = new NameValueCollection();
                foreach (string v in header.Value)
                    collection.Add(header.Key, v);
                response.Headers.Add(collection);
            }
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            AddHeaders(HttpContext.Current.Response);
            return await _result.ExecuteAsync(cancellationToken);
        }
    }
}
