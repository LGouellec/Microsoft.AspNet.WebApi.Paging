using WebApi.Paging.Pageable;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace WebApi.Paging.Swagger
{
    public class PagingOperationFilter : IOperationFilter
    {
        private readonly string _pageAttributeName = PageableBinderConfig.DefaultPageParameter;
        private readonly string _sizeAttributeName = PageableBinderConfig.DefaultSizeParameter;

        public PagingOperationFilter()
        {

        }


        public PagingOperationFilter(string pageAttributeName, string sizeAttributeName)
        {
            _pageAttributeName = pageAttributeName;
            _sizeAttributeName = sizeAttributeName;
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            if (apiDescription.ParameterDescriptions.Any(p => p.ParameterDescriptor.ParameterType.Equals(typeof(IPageable))))
            {
                operation.parameters.Add(new Parameter
                {
                    description = "Page number",
                    name = _pageAttributeName,
                    required = false,
                    type = "int",
                    @in = "query"
                });

                operation.parameters.Add(new Parameter
                {
                    description = "Page size",
                    name = _sizeAttributeName,
                    required = false,
                    type = "int",
                    @in = "query"
                });
            }
        }
    }
}
