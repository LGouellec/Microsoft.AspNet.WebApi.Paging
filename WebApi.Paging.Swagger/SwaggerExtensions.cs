using Swashbuckle.Application;

namespace WebApi.Paging.Swagger
{
    public static class SwaggerExtensions
    {
        public static void EnablePaging(this SwaggerDocsConfig config)
        {
            config.OperationFilter(() => new PagingOperationFilter());
        }

        public static void EnablePaging(this SwaggerDocsConfig config, string pageAttributeName, string sizeAttributeName)
        {
            config.OperationFilter(() => new PagingOperationFilter(pageAttributeName, sizeAttributeName));
        }
    }
}
